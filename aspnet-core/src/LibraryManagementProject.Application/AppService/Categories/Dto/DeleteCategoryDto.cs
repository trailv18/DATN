using Abp.AutoMapper;
using LibraryManagementProject.Entity.Categories;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Categories.Dto
{
    [AutoMapFrom(typeof(Category))]
    public class DeleteCategoryDto
    {
        public Guid Id { get; set; }
    }
}
