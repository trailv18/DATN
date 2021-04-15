using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.BorrowBookDetails.Dto;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBookDetails;
using LibraryManagementProject.Entity.BorrowBooks;
using LibraryManagementProject.Validator.BorrowBookDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.BorrowBookDetails
{
    [AbpAuthorize]
    public class BorrowBookDetaiAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BorrowBookDetaiAppService(IRepository<BorrowBook, Guid> borrowBookRepository,
                                        IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                        IRepository<Book, Guid> bookRepository)
        {
            _borrowBookRepository = borrowBookRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _bookRepository = bookRepository;
        }

        //Get all
        [HttpGet]
        public async Task<List<GetAllBorrowBookDetailDto>> GetAllBorrowBookDetail()
        {
            var values = await _borrowBookDetailRepository
                .GetAll()
                .Select(result => new GetAllBorrowBookDetailDto
                {
                    Id = result.Id,
                    Book = result.Book.Name,
                    Qty = result.Qty,
                    PriceBorrow = result.PriceBorrow,
                    Total = result.Total
                })
                .ToListAsync();
            return values;
        }

        [HttpPost]
        public async Task AddBorrowBookDetail(List<BorrowBookDetailDto> input)
        {
            if(input.Count == 0)
            {
                throw new UserFriendlyException("Please choose the book!");
            }

            List<string> errorList = new List<string>();

            DateTime today = DateTime.Now;
            //Add id BorrowBook
            var borrowBook = new BorrowBook
            {
                DateBorrow = today
            };
            await _borrowBookRepository.InsertAsync(borrowBook);

            int total = 0;
            int allTotal = 0;
            //Add borrow book detail
            foreach (var borrowBookDetail in input)
            {
                var addBorrowBookDetail = new BorrowBookDetail
                {
                    Qty = borrowBookDetail.Qty,
                    BookId = borrowBookDetail.BookId,
                    PriceBorrow = borrowBookDetail.PriceBorrow,
                    Total = borrowBookDetail.Qty * borrowBookDetail.PriceBorrow,
                    BorrowBookId = borrowBook.Id
                };

                allTotal = allTotal + (borrowBookDetail.Qty * borrowBookDetail.PriceBorrow);

                BorrowBookDetailValiadtor validator = new BorrowBookDetailValiadtor();
                ValidationResult validationResult = validator.Validate(addBorrowBookDetail);

                if (!validationResult.IsValid)
                {
                    foreach (var error in validationResult.Errors)
                    {
                        errorList.Add(string.Format("{0}", error.ErrorMessage));
                    }
                }
                else
                {
                    await _borrowBookDetailRepository.InsertAsync(addBorrowBookDetail);
                }


                //Update stock
                var items = from s in _bookRepository.GetAll()
                            where s.Id == borrowBookDetail.BookId
                            select s;
                foreach (var item in items)
                {
                    item.Stock = item.Stock - addBorrowBookDetail.Qty;
                    await _bookRepository.UpdateAsync(item);
                }

            }

            //show list error
            if(errorList.Count != 0)
            {
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }


            //Update BorrowBook
            TimeSpan aInterval = new System.TimeSpan(5, 0, 0, 0);

            DateTime newTime = today.Add(aInterval);

            borrowBook.Total = allTotal;
            borrowBook.Status = "Đang xử lý";
            borrowBook.DateRepay = newTime;
            borrowBook.UserId = AbpSession.UserId.Value;
            await _borrowBookRepository.InsertAsync(borrowBook);
        }
    }
}
