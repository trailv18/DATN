using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Libraries.Dto
{
    public class GetBookLibraryDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PriceBorrow { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Year { get; set; }
    }
}
