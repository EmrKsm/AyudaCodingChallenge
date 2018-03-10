using Xunit;
using BeersForAyuda.Controllers;
using System.Web.Mvc;

namespace BeersForAyuda.Tests.Controllers
{
    public class HomeControllerTest
    {
        [Fact()]
        public void ShouldReturnRandomBeers()
        {
            var controller = new HomeController();
            ActionResult result = controller.Index();
            Assert.NotNull(result);

        }
    }
}
