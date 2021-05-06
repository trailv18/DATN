using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using FluentValidation.Results;
using LibraryManagementProject.AppService.Fillter;
using LibraryManagementProject.AppService.Publishers.Dto;
using LibraryManagementProject.Entity.Publishers;
using LibraryManagementProject.Validator.Publishers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagementProject.AppService.Publishers
{
    [AbpAuthorize]
    public class PublisherAppService : LibraryManagementProjectAppServiceBase
    {
        private readonly IRepository<Publisher, Guid> _publisherRepository;

        public PublisherAppService(IRepository<Publisher, Guid> publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        //
        [HttpGet]
        public async Task<List<GetAllPublisherDto>> GetAllPublisher()
        {

            var values = await _publisherRepository
                .GetAll()
                .Select(c => new GetAllPublisherDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    Address = c.Address,
                    Phone = c.Phone
                })
                .ToListAsync();
            return values;
        }

        //Get page
        [HttpGet]
        public async Task<PageResult<GetAllPublisherDto>> GetPagePublisher(int? page, string publishName)
        {
            int pageSize = 9;
            var count = 0;
            var results = _publisherRepository
                   .GetAll()
                   .WhereIf(!String.IsNullOrEmpty(publishName), x=>x.Name.Contains(publishName))
                   .Select(p => new GetAllPublisherDto
                   {
                       Id = p.Id,
                       Name = p.Name,
                       Email = p.Email,
                       Address = p.Address,
                       Phone = p.Phone
                   });

            count = results.Count();

            var result = new PageResult<GetAllPublisherDto>
            {
                Count = _publisherRepository.GetAll().Count(),
                PageIndex = page ?? 1,
                PageSize = 9,
                Items = await results.Skip(((page - 1 ?? 0) * pageSize)).Take(pageSize).ToListAsync()
            };

            return result;
        }

        //Add
        [HttpPost]
        public async Task AddPublisher(PublisherDto input)
        {
            List<string> errorList = new List<string>();

            var publisher = new Publisher
            {
                Name = input.Name,
                Email = input.Email,
                Address = input.Address,
                Phone = input.Phone
            };

            PublisherValidator validator = new PublisherValidator();
            ValidationResult validationResult = validator.Validate(publisher);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _publisherRepository.InsertAsync(publisher);
        }

        [HttpGet]
        public async Task<Publisher> GetPublisherById(Guid id)
        {
            return await _publisherRepository.GetAsync(id);
        }

        //Update
        [HttpPut]
        public async Task UpdatePublisher(PublisherDto input)
        {
            List<string> errorList = new List<string>();

            var data = await GetPublisherById(input.Id);
            data.Name = input.Name;
            data.Email = input.Email;
            data.Address = input.Address;
            data.Phone = input.Phone;

            PublisherValidator validator = new PublisherValidator();
            ValidationResult validationResult = validator.Validate(data);

            if (!validationResult.IsValid)
            {
                foreach (var failure in validationResult.Errors)
                {
                    errorList.Add(string.Format("{0}", failure.ErrorMessage));
                }
                string errorString = string.Join(" ", errorList.ToArray());
                throw new UserFriendlyException(errorString);
            }
            await _publisherRepository.UpdateAsync(data);
        }

        [HttpDelete]
        public async Task DeletePublisher(DeletePublisherDto input)
        {
            var data = await GetPublisherById(input.Id);

            await _publisherRepository.DeleteAsync(data);
        }

    }
}
