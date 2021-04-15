using Abp.Domain.Entities;
using LibraryManagementProject.Entity.Books;
using LibraryManagementProject.Entity.BorrowBooks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryManagementProject.Entity.BorrowBookDetails
{
    [Table("BorrowBookDetails")]
    public class BorrowBookDetail: Entity<Guid>
    {
        public Guid BorrowBookId { get; set; }
        public BorrowBook BorrowBook { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
