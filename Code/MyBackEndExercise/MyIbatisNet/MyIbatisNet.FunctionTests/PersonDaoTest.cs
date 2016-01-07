using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIbatisNet.Dao;
using MyIbatisNet.Domain;

namespace MyIbatisNet.FunctionTests
{
    [TestClass]
    public class PersonDaoTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var dao = new PersonDao();
            var listPerson = dao.GetList();
            Assert.IsTrue(listPerson.Count > 0);
        }
    }
}
