using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Statistic_Report.Dto
{
    public class StatisticReportDto
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string CategoryName { get; set; }
        public DateTime DateBorrow { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public int Quantity { get; set; }
    }
}
