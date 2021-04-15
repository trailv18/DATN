using Abp.AutoMapper;
using LibraryManagementProject.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Authors.Dto
{
    [AutoMapFrom(typeof(Author))]
    public class DeleteAuthorDto
    {
        public Guid Id { get; set; }
    }
}
