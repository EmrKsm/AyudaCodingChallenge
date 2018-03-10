
using Xunit;
using BeersForAyuda.Controllers;
using System.Web.Mvc;

namespace BeersForAyuda.Tests.Controllers
{
    public class BeerControllerTest
    {
        [Theory()]
        [InlineData("fdWQuE")]
        [InlineData("zGBmz4")]
        [InlineData("ktCjOJ")]
        [InlineData("XC7jiy")]
        public void ShouldReturnBeerWithDetails(string Id)
        {
            var controller = new BeerController();
            ActionResult result = controller.Details(Id);
            Assert.NotNull(result);

        }
    }
}
