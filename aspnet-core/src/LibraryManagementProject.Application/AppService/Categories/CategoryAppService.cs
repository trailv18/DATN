using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.Categories.Dto;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.Entity.Categories;
using LibraryManagementProject.Validator.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Categories
{
    [AbpAuthorize]
    public class CategoryAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        public CategoryAppService(IRepository<Category, Guid> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        //Get All
        [HttpGet]
        public async Task<List<GetAllCategoryDto>> GetAllCategory()
        {

            var values = await _categoryRepository
                .GetAll()
                .Select(c => new GetAllCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                })
                .ToListAsync();
            return values;
        }

        //Get page
        [HttpGet]
        public PageResult<GetAllCategoryDto> GetPageCategory(int? page, string categoryName)
        {
            int pageSize = 9;

            var count = 0;
            var values = _categoryRepository
                .GetAll()
                .WhereIf(!String.IsNullOrEmpty(categoryName), x=>x.Name.Contains(categoryName))
                .Select(c => new GetAllCategoryDto
                {
                    Id = c.Id,
                    Name = c.Name
                });

            count = values.Count();

            var result = new PageResult<GetAllCategoryDto>
            {
                Count = count,
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = values.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToList()
            };

            return result;
        }

        //Add
        [HttpPost]
        public async Task AddCategory(CategoryDto input)
        {
            var category = new Category
            {
                Name = input.Name,
            };

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(category);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                await _categoryRepository.InsertAsync(category);
            }
        }

        //Get by Id
        [HttpGet]
        public async Task<Category> GetCategoryById(Guid id)
        {
            return await _categoryRepository.GetAsync(id);
        }

        //Update
        [HttpPut]
        public async Task UpdateCategory(CategoryDto input)
        {
            var ct = await GetCategoryById(input.Id);
            ct.Name = input.Name;

            CategoryValidator categoryValidator = new CategoryValidator();
            ValidationResult validationResult = categoryValidator.Validate(ct);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    throw new UserFriendlyException(string.Format("{0}", failure.ErrorMessage));
                }
            }
            else
            {
                await _categoryRepository.UpdateAsync(ct);
            }

        }

        //Delete
        [HttpDelete]
        public async Task DeleteCategory(DeleteCategoryDto ctD)
        {
            var ct = await GetCategoryById(ctD.Id);

            await _categoryRepository.DeleteAsync(ct);
        }
    }
}
