using BeersForAyuda.Controllers;
using Xunit;
using System.Web.Mvc;
using BeersForAyuda.Common;

namespace BeersForAyuda.Tests.Controllers
{
    public class StyleControllerTest
    {
        [Theory()]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(34)]
        public void ShouldReturnStyleWithGivenId(int Id)
        {
            APICaller.LoadCacheData();
            var controller = new StyleController();
            ActionResult result = controller.Browse(Id);
            Assert.NotNull(result);
        }
    }
}
