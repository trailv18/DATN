using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.BorrowBooks.Dto;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.Authorization;
using LibraryManagementProject.Entity.BorrowBookDetails;
using LibraryManagementProject.Entity.BorrowBooks;
using LibraryManagementProject.Validator.BorrowBooks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.BorrowBooks
{
    [AbpAuthorize]
    public class BorrowBookAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<BorrowBook, Guid> _borrowBookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;


        public BorrowBookAppService(IRepository<BorrowBook, Guid> borrowBookRepository,
                                IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository)
        {
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _borrowBookRepository = borrowBookRepository;
        }

        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDto>> GetPageBorrowBook(int? page)
        {
            int pageSize = 9;
            var result = new PageResult<GetAllBorrowBookDto>
            {
                Count = _borrowBookRepository.GetAll().Count(),
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await _borrowBookRepository.GetAll().Select(value => new GetAllBorrowBookDto
                {
                    Id = value.Id,
                    DateBorrow = value.DateBorrow,
                    DateRepay = value.DateRepay,
                    Status = value.Status,
                    Total = value.Total,
                    User = value.User.FullName
                })
                .Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        //
        [HttpGet]
        public async Task<List<GetBorrowBookDto>> GetBorrowBookDetailById(Guid id)
        {
            var values = await _borrowBookRepository
                .GetAll()
                .Where(d => d.Id == id)
                .Select(value => new GetBorrowBookDto
                {
                    Id = value.Id,
                    BorrowBookDetails = _borrowBookDetailRepository
                                        .GetAll()
                                        .Where(xx => xx.BorrowBookId == value.Id)
                                        .Select(data => new GetBorrowBookDetailDto
                                        {
                                            Id = data.Id,
                                            Book = data.Book.Name,
                                            Category = data.Book.Category.Name,
                                            Author = data.Book.Author.Name,
                                            Publisher = data.Book.Publisher.Name,
                                            PriceBorrow = data.PriceBorrow,
                                            Qty = data.Qty,
                                            Total = data.Total
                                        }).ToList(),
                    Status = value.Status,
                    Total = value.Total
                })
                .ToListAsync();
            return values;
        }

        [HttpGet]
        public async Task<BorrowBook> GetById(Guid id)
        {
            return await _borrowBookRepository.GetAsync(id);
        }

        //Update status
        [AbpAuthorize(PermissionNames.Pages_Librarians)]
        [HttpPut]
        public async Task UpdateStatus(UpdateStatusDto input)
        {
            var data = await GetById(input.Id);
            data.Status = input.Status;


            BorrowBookValiadtor validator = new BorrowBookValiadtor();
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
                await _borrowBookRepository.UpdateAsync(data);
            }
        }

        //get borrow book of user
        [HttpGet]
        public async Task<PageResult<GetAllBorrowBookDto>> GetBorrowBookPageByUserId(int? page)
        {
            long userID = AbpSession.UserId.Value;

            int pageSize = 9;
            var result = new PageResult<GetAllBorrowBookDto>
            {
                Count = _borrowBookRepository.GetAll().Where(x => x.UserId == userID).Count(),
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await _borrowBookRepository
                    .GetAll()
                    .Where(user => user.UserId == userID)
                    .Select(value => new GetAllBorrowBookDto
                    {
                        Id = value.Id,
                        DateBorrow = value.DateBorrow,
                        DateRepay = value.DateRepay,
                        Status = value.Status,
                        Total = value.Total,
                        User = value.User.FullName
                    })
                .Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

    }
}
