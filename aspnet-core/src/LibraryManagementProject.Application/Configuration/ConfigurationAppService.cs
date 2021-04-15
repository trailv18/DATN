using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using LibraryManagementProject.Configuration.Dto;

namespace LibraryManagementProject.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LibraryManagementProjectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
