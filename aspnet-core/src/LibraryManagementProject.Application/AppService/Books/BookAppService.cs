﻿using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.Books.Dto;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Validator.Books;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace LibraryManagementProject.AppService.Books
{
    public class BookAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public BookAppService(IRepository<Book, Guid> bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        //
        [HttpGet]
        public async Task<List<GetAllBookDto>> GetAllBook()
        {

            var values = await _bookRepository
                .GetAll()
                .Select(value => new GetAllBookDto
                {
                    Id = value.Id,
                    Name = value.Name
                })
                .ToListAsync();
            return values;
        }

        //Get page
        [HttpGet]
        public async Task<PageResult<GetAllBookDto>> GetPageBook(int? page, string bookName, string categoryName, string authorName, string publisherName)
        {
            int pageSize = 9;

            var count = 0;
            var values = _bookRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(bookName) || !String.IsNullOrEmpty(categoryName) || !String.IsNullOrEmpty(authorName) || !String.IsNullOrEmpty(publisherName),
                            c => c.Name.Contains(bookName) || c.Category.Name == categoryName || c.Author.Name == authorName || c.Publisher.Name == publisherName)
                .Select(c => new GetAllBookDto
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

            var result = new PageResult<GetAllBookDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await values.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        //Add
        [HttpPost]
        public async Task AddBook(BookDto input)
        {
            var book = new Book
            {
                Name = input.Name,
                PriceBorrow = input.PriceBorrow,
                Stock = input.Stock,
                CategoryId = input.CategoryId,
                PublisherId = input.PublisherId,
                AuthorId = input.AuthorId,
                Description = input.Description,
                UrlImage = input.UrlImage,
                Year = input.Year
            };

            BookValidator validator = new BookValidator();
            ValidationResult validationResult = validator.Validate(book);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                await _bookRepository.InsertAsync(book);
            }
        }

        [HttpGet]
        public async Task<Book> GetBookById(Guid id)
        {
            return await _bookRepository.GetAsync(id);
        }

        //Update
        [HttpPut]
        public async Task UpdateBook(BookDto input)
        {
            var data = await GetBookById(input.Id);
            data.Name = input.Name;
            data.PriceBorrow = input.PriceBorrow;
            data.Stock = input.Stock;
            data.CategoryId = input.CategoryId;
            data.PublisherId = input.PublisherId;
            data.AuthorId = input.AuthorId;
            data.Description = input.Description;
            data.UrlImage = input.UrlImage;
            data.Year = input.Year;

            BookValidator validator = new BookValidator();
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
                await _bookRepository.UpdateAsync(data);
            }
        }

        [HttpDelete]
        public async Task DeleteBook(DeleteBookDto input)
        {
            var data = await GetBookById(input.Id);

            await _bookRepository.DeleteAsync(data);
        }

    }
}