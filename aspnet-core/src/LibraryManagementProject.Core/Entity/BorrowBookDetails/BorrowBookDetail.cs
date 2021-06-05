using Abp.Domain.Entities;
using LibraryManagementProject.Authorization.Users;
using LibraryManagementProject.Entity.Books;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryManagementProject.Entity.BorrowBookDetails
{
    [Table("BorrowBookDetails")]
    public class BorrowBookDetail: Entity<Guid>
    {
        public Guid BookId { get; set; }
        public Book Book { get; set; }
        public int Qty { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateRepay { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }

        public string Note { get; set; }
    }
}
