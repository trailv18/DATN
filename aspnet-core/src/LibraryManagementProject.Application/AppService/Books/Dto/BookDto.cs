using Abp.AutoMapper;
using LibraryManagementProject.Entity.Books;
using Microsoft.AspNetCore.Http;
using System;

namespace LibraryManagementProject.AppService.Books.Dto
{
    [AutoMapFrom(typeof(Book))]
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public Guid CategoryId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid AuthorId { get; set; }
        public string Description { get; set; }

        public string UrlImage { get; set; }
        public string Year { get; set; }
    }
}
