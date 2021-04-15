using Abp.Domain.Entities;
using LibraryManagementProject.Entity.Authors;
using LibraryManagementProject.Entity.Categories;
using LibraryManagementProject.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementProject.Entity.Books
{
    [Table("Books")]
    public class Book : Entity<Guid>
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public int Stock { get; set; }
        public int PriceBorrow { get; set; }
        public Guid PublisherId { get; set; }
        public Publisher Publisher { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Year { get; set; }

    }
}
