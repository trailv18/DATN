using Abp.Application.Services.Dto;

namespace LibraryManagementProject.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

