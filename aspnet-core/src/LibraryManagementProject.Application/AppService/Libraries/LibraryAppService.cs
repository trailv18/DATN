using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.AppService.Libraries.Dto;
using LibraryManagementProject.Entity.Books;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Libraries
{
    public class LibraryAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        public LibraryAppService(IRepository<Book, Guid> bookRepository)
        {
            _bookRepository = bookRepository;
        }

        //Get page
        //Get page
        [HttpGet]
        public async Task<PageResult<GetBookLibraryDto>> GetPageBook(int? page, string bookName, string categoryName, string authorName, string publisherName)
        {
            int pageSize = 12;

            var count = 0;
            var values = _bookRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(bookName) || !String.IsNullOrEmpty(categoryName) || !String.IsNullOrEmpty(authorName) || !String.IsNullOrEmpty(publisherName),
                            c => c.Name.Contains(bookName) || c.Category.Name == categoryName || c.Author.Name == authorName || c.Publisher.Name == publisherName)
                .Select(c => new GetBookLibraryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    PriceBorrow = c.PriceBorrow,
                    Stock = c.Stock,
                    Category = c.Category.Name,
                    Author = c.Author.Name,
                    Publisher = c.Publisher.Name,
                    UrlImage = c.UrlImage,
                    Description = c.Description,
                    Year = c.Year
                })
                .OrderBy(x => x.Name);

            count = values.Count();

            var result = new PageResult<GetBookLibraryDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = 12,
                Items = await values.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        [HttpGet]
        public async Task<GetBookLibraryDto> GetBookDetail(Guid id)
        {
            var result = await _bookRepository.
                    GetAll()
                    .Where(d => d.Id == id)
                    .Select(value => new GetBookLibraryDto
                    {
                        Id = value.Id,
                        Name = value.Name,
                        PriceBorrow = value.PriceBorrow,
                        Stock = value.Stock,
                        Category = value.Category.Name,
                        Author = value.Author.Name,
                        Publisher = value.Publisher.Name,
                        Description = value.Description,
                        UrlImage = value.UrlImage,
                        Year = value.Year
                    }).ToListAsync();

            return result.FirstOrDefault();
        }
    }
}
