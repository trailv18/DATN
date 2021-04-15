using Abp.AutoMapper;
using LibraryManagementProject.Entity.Authors;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Authors.Dto
{
    public class GetAllAuthorDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string YearOfBirth { get; set; }
        public string Address { get; set; }
    }
}
