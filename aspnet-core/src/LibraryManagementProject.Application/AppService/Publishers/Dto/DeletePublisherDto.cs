using Abp.AutoMapper;
using LibraryManagementProject.Entity.Publishers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManagementProject.AppService.Publishers.Dto
{
    [AutoMapFrom(typeof(Publisher))]
    public class DeletePublisherDto
    {
        public Guid Id { get; set; }
    }
}
