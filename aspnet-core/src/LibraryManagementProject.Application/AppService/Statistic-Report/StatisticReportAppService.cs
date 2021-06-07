using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.AppService.Statistic_Report.Dto;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBookDetails;
using LibraryManagementProject.Entity.Categories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Statistic_Report
{
    public class StatisticReportAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;
        private readonly IRepository<Category, Guid> _categoryRepository;


        public StatisticReportAppService(IRepository<Book, Guid> bookRepository,
                                     IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository,
                                     IRepository<Category, Guid> categoryRepository)
        {
            _bookRepository = bookRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
            _categoryRepository = categoryRepository;
        }

        //statistic by category

        [HttpGet]
        public async Task<PageResult<StatisticReportDto>> GetStatisticByCategory(int? page, DateTime? fromDate, DateTime? toDate,
            int? month, int? quarter)
        {
            int pageSize = 9;
            var counts = 0;


            var data = (from category in _categoryRepository.GetAll()
                        join book in _bookRepository.GetAll() on category.Id equals book.CategoryId into TempBook
                        from book in TempBook.DefaultIfEmpty()
                        join borrowDetail in _borrowBookDetailRepository.GetAll() on book.Id equals borrowDetail.BookId into TempBorrow
                        from borrowDetail in TempBorrow.DefaultIfEmpty()
                        select new
                        {
                            CategoryId = category.Id,
                            CategoryName = category.Name,
                            BookName = book != null ? book.Name : null,
                            AuthorName = book != null ? book.Author.Name : null,
                            PublisherName = book != null ? book.Publisher.Name : null,
                            DateBorrow = borrowDetail.DateBorrow,
                            Status = borrowDetail.Status,
                            Qty = borrowDetail != null ? borrowDetail.Qty : 0
                        })
                       .ToList()
                       .WhereIf(month != null
                            || fromDate.HasValue && toDate.HasValue ||
                            month != null || quarter != null,

                            x => x.DateBorrow.Month == month
                            || x.DateBorrow.Date >= fromDate && x.DateBorrow.Date <= toDate ||
                            x.DateBorrow.Month == month ||
                            ((1 <= x.DateBorrow.Month && x.DateBorrow.Month <= 3) ? 1 : ((4 <= x.DateBorrow.Month && x.DateBorrow.Month <= 6) ? 2 : ((7 <= x.DateBorrow.Month && x.DateBorrow.Month <= 9) ? 3 : ((10 <= x.DateBorrow.Month && x.DateBorrow.Month <= 12) ? 4 : 0)))) == quarter)
                       .GroupBy(x => x.CategoryId)
                       .Select(x => new StatisticReportDto
                       {
                           CategoryId = x.Key,
                           BookName = x.Select(x => x.BookName).FirstOrDefault(),
                           CategoryName = x.Select(x => x.CategoryName).FirstOrDefault(),
                           AuthorName = x.Select(x => x.AuthorName).FirstOrDefault(),
                           PublisherName = x.Select(x => x.PublisherName).FirstOrDefault(),
                           Quantity = x.Sum(x => x.Qty)
                       }).OrderByDescending(x => x.Quantity);

            counts = data.Count();


            var result = new PageResult<StatisticReportDto>
            {
                Count = counts,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await Task.FromResult(data.Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList())
            };

            return result;
        }

        //statistic are borrowed

        [HttpGet]
        public async Task<PageResult<StatisticReportDto>> GetStatisticByBorrow(int? page, DateTime? fromDate, DateTime? toDate,
            int? month, int? quarter, string? status)
        {
            int pageSize = 9;
            var counts = 0;


            var data = (from book in _bookRepository.GetAll()
                        join borrowDetail in _borrowBookDetailRepository.GetAll() on book.Id equals borrowDetail.BookId
                        select new
                        {
                            BookId = book.Id,
                            BookName = book.Name,
                            AuthorName = book.Author.Name,
                            PublisherName = book.Publisher.Name,
                            DateBorrow = borrowDetail.DateBorrow,
                            Qty = borrowDetail.Qty,
                            Status = borrowDetail.Status
                        })
                       .ToList()
                       .WhereIf(!String.IsNullOrEmpty(status) || month != null
                            || fromDate.HasValue && toDate.HasValue ||
                            month != null || quarter != null,
                            x => x.Status == status ||
                            x.DateBorrow.Month == month
                            || x.DateBorrow.Date >= fromDate && x.DateBorrow.Date <= toDate ||
                            x.DateBorrow.Month == month ||
                            ((1 <= x.DateBorrow.Month && x.DateBorrow.Month <= 3) ? 1 : ((4 <= x.DateBorrow.Month && x.DateBorrow.Month <= 6) ? 2 : ((7 <= x.DateBorrow.Month && x.DateBorrow.Month <= 9) ? 3 : ((10 <= x.DateBorrow.Month && x.DateBorrow.Month <= 12) ? 4 : 0)))) == quarter)
                       .GroupBy(x => x.BookId)
                       .Select(x => new StatisticReportDto
                       {
                           BookId = x.Key,
                           BookName = x.Select(x => x.BookName).FirstOrDefault(),
                           AuthorName = x.Select(x => x.AuthorName).FirstOrDefault(),
                           PublisherName = x.Select(x => x.PublisherName).FirstOrDefault(),
                           Quantity = x.Sum(x => x.Qty),
                       }).OrderByDescending(x => x.Quantity);

            counts = data.Count();


            var result = new PageResult<StatisticReportDto>
            {
                Count = counts,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await Task.FromResult(data.Skip((page - 1 ?? 0) * pageSize).Take(pageSize).ToList())
            };

            return result;
        }

    }
}
