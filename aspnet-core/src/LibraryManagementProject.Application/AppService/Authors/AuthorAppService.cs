using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.Authors.Dto;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.Entity.Authors;
using LibraryManagementProject.Validator.Authors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Authors
{
    [AbpAuthorize]
    public class AuthorAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Author, Guid> _authorRepository;

        public AuthorAppService(IRepository<Author, Guid> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<List<GetAllAuthorDto>> GetAllAuthor()
        {

            var values = await _authorRepository
                .GetAll()
                .Select(c => new GetAllAuthorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    YearOfBirth = c.YearOfBirth,
                    Phone = c.Phone,
                    Address = c.Address
                })
                .ToListAsync();
            return values;
        }

        //Get page
        [HttpGet]
        public async Task<PageResult<GetAllAuthorDto>> GetPageAuthor(int? page, string authorName)
        {
            var count = 0;
            var results = _authorRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(authorName), x=>x.Name.Contains(authorName))
                .Select(c => new GetAllAuthorDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Address = c.Address,
                    YearOfBirth = c.YearOfBirth
                });

            int pageSize = 9;
            var result = new PageResult<GetAllAuthorDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await results.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        //Add
        [HttpPost]
        public async Task AddAuthor(AuthorDto input)
        {
            var author = new Author
            {
                Name = input.Name,
                YearOfBirth = input.YearOfBirth,
                Address = input.Address,
                Phone = input.Phone
            };

            AuthorValidator validator = new AuthorValidator();
            ValidationResult validationResult = validator.Validate(author);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                await _authorRepository.InsertAsync(author);
            }
        }

        [HttpGet]
        public async Task<Author> GetAuthorById(Guid id)
        {
            return await _authorRepository.GetAsync(id);
        }

        //Update
        [HttpPut]
        public async Task UpdateAuthor(AuthorDto input)
        {
            var data = await GetAuthorById(input.Id);
            data.Name = input.Name;
            data.Address = input.Address;
            data.Phone = input.Phone;

            AuthorValidator validator = new AuthorValidator();
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
                await _authorRepository.UpdateAsync(data);
            }
        }

        //Delete
        [HttpDelete]
        public async Task DeleteAuthor(DeleteAuthorDto input)
        {
            var data = await GetAuthorById(input.Id);

            await _authorRepository.DeleteAsync(data);
        }
    }
}
