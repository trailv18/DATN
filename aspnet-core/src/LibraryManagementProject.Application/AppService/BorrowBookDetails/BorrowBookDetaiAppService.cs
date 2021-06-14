using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.BorrowBookDetails.Dto;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.Authorization;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBookDetails;
using LibraryManagementProject.Validator.BorrowBookDetails;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.BorrowBookDetails
{
    [AbpAuthorize]
    public class BorrowBookDetaiAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;
        private readonly IRepository<Book, Guid> _bookRepository;

        public BorrowBookDetaiAppService(IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                        IRepository<Book, Guid> bookRepository)
        {
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _bookRepository = bookRepository;
        }

        //Get all
        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDetailDto>> GetAllBorrowBookDetail(int? page, DateTime? fromDate, DateTime? toDate, int? month)
        {
            var count = 0;
            var values = _borrowBookDetailRepository
                .GetAll()
                .WhereIf(fromDate.HasValue && toDate.HasValue || month != null, x => x.DateBorrow.Date >= fromDate && x.DateBorrow.Date <= toDate || x.DateBorrow.Month == month)
                .Select(result => new GetAllBorrowBookDetailDto
                {
                    Id = result.Id,
                    BookName = result.Book.Name,
                    DateBorrow = result.DateBorrow,
                    DateRepay = result.DateRepay,
                    Status = result.Status,
                    UserName = result.User.FullName,
                    Note = result.Note
                }).OrderByDescending(x => x.DateBorrow);

            count = values.Count();
            int pageSize = 9;
            var result = new PageResult<GetAllBorrowBookDetailDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = pageSize,
                Items = await values.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };
            return result;
        }

        [HttpPost]
        public async Task AddBorrowBookDetail(BorrowBookDetailDto input)
        {
            List<string> errorList = new List<string>();

            var userId = AbpSession.UserId.Value;
            var bookItems = _bookRepository.GetAll();
            var borrows = _borrowBookDetailRepository.GetAll();

            foreach (var borrow in borrows)
            {
                if (borrow.BookId == input.BookId && borrow.UserId == userId && borrow.Status != "Đã trả")
                {
                    throw new UserFriendlyException("Bạn đang mượn sách này!");
                }
            }

            DateTime today = DateTime.Now;
            TimeSpan aInterval = new System.TimeSpan(5, 0, 0, 0);
            DateTime newTime = today.Add(aInterval);

            var addBorrowBookDetail = new BorrowBookDetail
            {
                BookId = input.BookId,
                Qty = 1,
                DateBorrow = today,
                DateRepay = newTime,
                Status = "Đang xử lý",
                UserId = userId,
                Note = "Không"
            };


            BorrowBookDetailValiadtor validator = new BorrowBookDetailValiadtor();
            ValidationResult validationResult = validator.Validate(addBorrowBookDetail);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }

            //Update stock
            var data = bookItems.Where(x => x.Id == input.BookId).FirstOrDefault();

            if (data.Stock < 1)
            {
                throw new UserFriendlyException(string.Format("{0} have {1} in stock", data.Name, data.Stock));
            }
            else
            {
                data.Stock = data.Stock - 1;
                await _bookRepository.UpdateAsync(data);
            }

            //add
            await _borrowBookDetailRepository.InsertAsync(addBorrowBookDetail);

        }

        [HttpGet]
        public async Task<BorrowBookDetail> GetById(Guid id)
        {
            return await _borrowBookDetailRepository.GetAsync(id);
        }

        //Update status
        [AbpAuthorize(PermissionNames.Pages_Librarians)]
        [HttpPut]
        public async Task UpdateStatus(UpdateStatusDto input)
        {
            var data = await GetById(input.Id);
            data.Status = input.Status;
            data.Note = input.Note;

            var book = _bookRepository.GetAll().Where(x => x.Id == input.BookId).FirstOrDefault();

            BorrowBookDetailValiadtor validator = new BorrowBookDetailValiadtor();
            ValidationResult validationResult = validator.Validate(data);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                if (input.Status == "Đã trả")
                {
                    book.Stock = book.Stock + 1;
                    await _bookRepository.UpdateAsync(book);
                    await _borrowBookDetailRepository.UpdateAsync(data);
                }
            }
        }

        //get borrow book of user
        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDetailDto>> GetBorrowBookPageByUserId(int? page)
        {
            long userID = AbpSession.UserId.Value;
            var count = 0;
            var results = _borrowBookDetailRepository
                   .GetAll()
                   .Where(user => user.UserId == userID)
                   .Select(value => new GetAllBorrowBookDetailDto
                   {
                       Id = value.Id,
                       BookName = value.Book.Name,
                       DateBorrow = value.DateBorrow,
                       DateRepay = value.DateRepay,
                       Status = value.Status,
                       Note = value.Note
                   }).OrderByDescending(x => x.DateBorrow);


            count = results.Count();

            int pageSize = 9;
            var result = new PageResult<GetAllBorrowBookDetailDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await results.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        //Delete
        [HttpDelete]
        public async Task Delete(DeleteDto input)
        {
            var data = await GetById(input.Id);

            await _borrowBookDetailRepository.DeleteAsync(data);
        }

    }
}
