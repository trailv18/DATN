using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.AppService.Statistic_Report.Dto;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBookDetails;
using LibraryManagementProject.Entity.BorrowBooks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Statistic_Report
{
    public class StatisticReportAppService: LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IRepository<BorrowBookDetail, Guid> _borrowBookDetailRepository;


        public StatisticReportAppService(IRepository<Book, Guid> bookRepository,
                                     IRepository<BorrowBookDetail, Guid> borrowBookDetailRepository)
        {
            _bookRepository = bookRepository;
            _borrowBookDetailRepository = borrowBookDetailRepository;
        }

        [HttpGet]
        public async Task<PageResult<StatisticReportDto>> GetStatisticByCriteria(int? page, DateTime? fromDate, DateTime? toDate,
            int? month, int? quarter)
        {
            int pageSize = 9;
            var counts = 0;


            var data = (from borrowBook in _borrowBookDetailRepository.GetAll()
                        join book in _bookRepository.GetAll() on borrowBook.BookId equals book.Id
                        select new
                        {
                            BookId = borrowBook.BookId,
                            BookName = book.Name,
                            CategoryName = book.Category.Name,
                            AuthorName = book.Author.Name,
                            PublisherName = book.Publisher.Name,
                            DateBorrow = borrowBook.BorrowBook.DateBorrow,
                            Qty = borrowBook.Qty
                        })
                       .ToList()
                       .WhereIf(month != null
                            || fromDate.HasValue && toDate.HasValue ||
                            month != null || quarter != null,

                            x => x.DateBorrow.Month == month
                            || x.DateBorrow.Date >= fromDate&& x.DateBorrow.Date <= toDate ||
                            x.DateBorrow.Month == month ||
                            ((1 <= x.DateBorrow.Month && x.DateBorrow.Month <= 3) ? 1 : ((4 <= x.DateBorrow.Month && x.DateBorrow.Month <= 6) ? 2 : ((7 <= x.DateBorrow.Month && x.DateBorrow.Month <= 9) ? 3 : ((10 <= x.DateBorrow.Month && x.DateBorrow.Month <= 12) ? 4 : 0)))) == quarter)
                       .GroupBy(x => new { x.BookId, x.BookName, x.CategoryName, x.AuthorName, x.PublisherName })
                       .Select(x => new StatisticReportDto
                       {
                           BookId = x.Key.BookId,
                           BookName = x.Key.BookName,
                           CategoryName = x.Key.CategoryName,
                           AuthorName = x.Key.AuthorName,
                           PublisherName = x.Key.PublisherName,
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

    }
}
