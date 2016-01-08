using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MyDapper.Business.Entity;

namespace MyDapper.Business
{
    public class UserPrizeService
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["LotteryDB"].ConnectionString;

        public UserPrizeCollection GetUserPrizes(int userId)
        {
            const string userPrizesSql = @"
                select lr.CustomerId as UserId,la.Name as ActivityName,lp.Name as PrizeName,lr.CreateDate as LotteryTime
                from LotteryRecords lr with(nolock)
                inner join LotteryActivity la on lr.LotteryActivityId=la.Id
                inner join LotteryPrize lp on lr.LotteryPrizeId=lp.Id
                where lr.CustomerId=@UserId              
                ";
            const string userPrizesTotalCountSql = @"
                select Count(0) as UserPrizesTotalCount
                from LotteryRecords lr 
                where lr.CustomerId=@UserId    
                ";
            var multipleSql = string.Format(@"
                {0}
                {1}
                ", userPrizesSql, userPrizesTotalCountSql);
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = connection.QueryMultiple(multipleSql, new { UserId = userId }, CommandType.Text))
                {
                    var userPrizes = multi.Read<UserPrize>().ToList();
                    var userPrizeTotalCount = multi.Read<int>().Single();
                    return new UserPrizeCollection
                    {
                        UserPrizeList = userPrizes,
                        UserPrizesTotalCount = userPrizeTotalCount
                    };
                }
            }
        }
    }
}
