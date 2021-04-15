using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryManagementProject.Authorization.Accounts.Dto;

namespace LibraryManagementProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
