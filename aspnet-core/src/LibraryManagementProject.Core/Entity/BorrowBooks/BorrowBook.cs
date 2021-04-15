using Abp.Domain.Entities;
using LibraryManagementProject.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryManagementProject.Entity.BorrowBooks
{
    [Table("BorrowBooks")]
    public class BorrowBook: Entity<Guid>
    {
        public DateTime DateBorrow { get; set; }
        public DateTime DateRepay { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }

        [ForeignKey("User")]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
