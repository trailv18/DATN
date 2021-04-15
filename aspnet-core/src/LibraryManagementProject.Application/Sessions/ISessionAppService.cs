using System.Threading.Tasks;
using Abp.Application.Services;
using LibraryManagementProject.Sessions.Dto;

namespace LibraryManagementProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
