using Abp.AutoMapper;
using LibraryManagementProject.Entity.BorrowBookDetails;
using System;

namespace LibraryManagementProject.AppService.BorrowBookDetails.Dto
{
    [AutoMapFrom(typeof(BorrowBookDetail))]
    public class DeleteDto
    {
        public Guid Id { get; set; }
    }
}
