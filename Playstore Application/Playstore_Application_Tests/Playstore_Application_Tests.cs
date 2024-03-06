using Playstore.Controllers.UserData;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Newtonsoft.Json;
namespace Playstore;
public class Playstore_Application_Tests
{
    private WebApplicationFactory<AppInfoController> _factory;
    [SetUp]
    public void Setup()
    {
         _factory = new WebApplicationFactory<AppInfoController>();
    }
    //Test to Get The All AppDetails
    [Test]
    public async Task GetAppDetails()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("https://localhost:5001/AppInfo/getAllapps");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    [Test]
    //Test to Get The All App ReviewDetails
    public async Task GetReviewDetails()
    {
        // Arrange

        // var 

    }    
}