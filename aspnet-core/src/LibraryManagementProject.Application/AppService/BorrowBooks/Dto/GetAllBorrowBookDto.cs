using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.BorrowBooks.Dto
{
    public class GetAllBorrowBookDto
    {
        public Guid Id { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateRepay { get; set; }
        public int Total { get; set; }
        public string Status { get; set; }
        public  string User { get; set; }
    }
}
