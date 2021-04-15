using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryManagementProject.Entity.Authors
{
    [Table("Authors")]
    public class Author : Entity<Guid>
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}
