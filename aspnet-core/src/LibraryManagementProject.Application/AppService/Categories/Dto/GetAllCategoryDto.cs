using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using LibraryManagementProject.Entity.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Categories.Dto
{
    public class GetAllCategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
