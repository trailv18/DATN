using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.BorrowBooks.Dto
{
    public class GetBorrowBookDto
    {
        public Guid Id { get; set; }
        public List<GetBorrowBookDetailDto> BorrowBookDetails { get; set; }
        public string Status { get; set; }
        public int Total { get; set; }
    }
}
