using Abp.Application.Services;
using LibraryManagementProject.MultiTenancy.Dto;

namespace LibraryManagementProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

