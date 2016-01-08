using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDapper.Business;

namespace MyDapper.FunctionTests
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void GetProductsTest()
        {
            var service = new ProductService();
            var products = service.GetProducts();
            Assert.IsTrue(products != null && products.Count > 0);
        }

        [TestMethod]
        public void BulkAddProductsTest()
        {
            var service = new ProductService();
            var result = service.BulkAddProducts();
            Assert.AreEqual(result, 3);
        }
    }
}
