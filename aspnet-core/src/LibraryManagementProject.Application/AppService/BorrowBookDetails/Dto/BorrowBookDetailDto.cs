using Abp.AutoMapper;
using LibraryManagementProject.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.BorrowBookDetails.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class BorrowBookDetailDto
    {
        public Guid Id { get; set; }
        public string BookName { get; set; }
        public Guid BorrowBookId { get; set; }
        public Guid BookId { get; set; }
        public int Qty { get; set; }
        public string UrlImage { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
