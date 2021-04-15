using Abp.AutoMapper;
using LibraryManagementProject.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Publishers.Dto
{
    public class GetAllPublisherDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
