using Xunit;
using BeersForAyuda.Controllers;
using System.Web.Mvc;
using BeersForAyuda.Common;

namespace BeersForAyuda.Tests.Controllers
{
    public class CategoryControllerTest
    {
        [Fact()]
        public void ShouldFillCategoryObjectsAndReturnView()
        {
            var controller = new CategoryController();
            ActionResult result = controller.Index();
            Assert.NotNull(result);
        }

        [Theory()]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(7)]
        public void ShouldReturnCategoryWithGivenId(int Id)
        {
            APICaller.LoadCacheData();
            var controller = new CategoryController();
            ActionResult result = controller.Browse(Id);
            Assert.NotNull(result);
        }
    }
}
