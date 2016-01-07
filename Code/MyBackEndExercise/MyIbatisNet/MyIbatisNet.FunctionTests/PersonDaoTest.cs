using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIbatisNet.Dao;

namespace MyIbatisNet.FunctionTests
{
    [TestClass]
    public class PersonDaoTest
    {
        [TestMethod]
        public void GetListTest()
        {
            var dao = new PersonDao();
            var listPerson = dao.GetList();
            Assert.IsTrue(listPerson.Count > 0);
        }
    }
}
