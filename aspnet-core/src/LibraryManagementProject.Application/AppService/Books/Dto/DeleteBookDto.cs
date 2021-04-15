using Abp.AutoMapper;
using LibraryManagementProject.Entity.Books;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class DeleteBookDto
    {
        public Guid Id { get; set; }
    }
}
