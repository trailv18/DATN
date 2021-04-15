using Abp.AutoMapper;
using LibraryManagementProject.Authentication.External;

namespace LibraryManagementProject.Models.TokenAuth
{
    [AutoMapFrom(typeof(ExternalLoginProviderInfo))]
    public class ExternalLoginProviderInfoModel
    {
        public string Name { get; set; }

        public string ClientId { get; set; }
    }
}
