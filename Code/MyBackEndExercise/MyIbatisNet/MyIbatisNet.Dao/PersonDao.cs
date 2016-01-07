using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBatisNet.DataMapper;
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
    }
}
