using Abp.AutoMapper;
using LibraryManagementProject.Entity.BorrowBookDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.BorrowBookDetails.Dto
{
    public class GetAllBorrowBookDetailDto
    {
        public Guid Id { get; set; }
        public string Book { get; set; }
        public int Qty { get; set; }
        public int PriceBorrow { get; set; }
        public int Total { get; set; }
    }
}
