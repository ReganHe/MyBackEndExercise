using System.Collections.Generic;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using MyIbatisNet.Domain;

namespace MyIbatisNet.Dao
{
    public class PersonDao
    {
        public IList<PersonModel> GetList()
        {
            ISqlMapper mapper = Mapper.Instance();
            IList<PersonModel> listPerson = mapper.QueryForList<PersonModel>("SelectAllPerson", null);  //这个"SelectAllPerson"就是xml映射文件的Id
            return listPerson;
        }

        public IList<PersonModel> GetAllPerson(string sqlMapConfigFilePath)
        {
            var builder = new DomSqlMapBuilder();
            var mapper = builder.Configure(sqlMapConfigFilePath);
            var listPerson = mapper.QueryForList<PersonModel>("SelectAllPerson", null);
            return listPerson;
        }

        public IList<PersonModel> GetAllPersonOrderByPresonIdDesc(string sqlMapConfigFilePath)
        {
            var builder = new DomSqlMapBuilder();
            var mapper = builder.Configure(sqlMapConfigFilePath);
            var listPerson = mapper.QueryForList<PersonModel>("SelectAllPersonByPersonIdDesc", null);
            return listPerson;
        }
    }
}
