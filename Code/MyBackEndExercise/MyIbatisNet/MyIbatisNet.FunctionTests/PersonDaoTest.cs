using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyIbatisNet.Dao;

namespace MyIbatisNet.FunctionTests
{
    [TestClass]
    public class PersonDaoTest
    {
        [Ignore]
        [TestMethod]
        public void GetListTest()
        {
            var dao = new PersonDao();
            var listPerson = dao.GetList();
            Assert.IsTrue(listPerson.Count > 0);
        }

        [TestMethod]
        public void GetListWithConfigTest()
        {
            var dao = new PersonDao();
            var sqlMapConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs/SqlMap.config");
            var listPerson = dao.GetAllPerson(sqlMapConfigFilePath);
            Assert.IsTrue(listPerson.Count > 0);
        }

        
        [TestMethod]
        public void GetAllPersonOrderByPresonIdDescTest()
        {
            var dao = new PersonDao();
            var sqlMapConfigFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configs/SqlMap.config");
            var listPerson = dao.GetAllPersonOrderByPresonIdDesc(sqlMapConfigFilePath);
            Assert.IsTrue(listPerson.Count > 0);
        }
    }
}
