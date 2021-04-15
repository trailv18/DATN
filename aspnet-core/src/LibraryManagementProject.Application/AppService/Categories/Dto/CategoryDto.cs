using Abp.AutoMapper;
using LibraryManagementProject.Entity.Categories;
using System;

namespace LibraryManagementProject.AppService.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

    }
}
