using Abp.AutoMapper;
using LibraryManagementProject.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.BorrowBookDetails.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class UpdateStatusDto
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public int Qty { get; set; }
        public DateTime DateBorrow { get; set; }
        public DateTime DateRepay { get; set; }
        public string Status { get; set; }
        public long UserId { get; set; }
        public string Note { get; set; }

    }
}
