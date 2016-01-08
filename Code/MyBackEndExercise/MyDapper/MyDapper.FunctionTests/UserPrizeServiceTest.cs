using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyDapper.Business;

namespace MyDapper.FunctionTests
{
    [TestClass]
    public class UserPrizeServiceTest
    {
        [TestMethod]
        public void GetUserPrizesTest()
        {
            var service = new UserPrizeService();
            var result = service.GetUserPrizes(4597);
            Assert.IsTrue(result != null);
        }
    }
}
