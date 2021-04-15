using System.Threading.Tasks;
using LibraryManagementProject.Configuration.Dto;

namespace LibraryManagementProject.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
