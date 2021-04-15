using System.Threading.Tasks;
using LibraryManagementProject.Models.TokenAuth;
using LibraryManagementProject.Web.Controllers;
using Shouldly;
using Xunit;

namespace LibraryManagementProject.Web.Tests.Controllers
{
    public class HomeController_Tests: LibraryManagementProjectWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}