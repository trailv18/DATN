using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LibraryManagementProject.Entity.Categories
{
    [Table("Categories")]
    public class Category :Entity<Guid>
    {
        public string Name { get; set; }
    }
}
