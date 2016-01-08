using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MyDapper.Business.Entity;

namespace MyDapper.Business
{
    public class ProductService
    {
        private readonly string _connectionString =
            ConfigurationManager.ConnectionStrings["SportStoreDB"].ConnectionString;

        public List<Product> GetProducts()
        {

            using (var conn = new SqlConnection(_connectionString))
            {
                var a = conn.Query<Product>("select * from Products where ImageData is null and ProductID >@id",
                    new { id = 2 });
                return a.ToList();
            }
        }

        public int BulkAddProducts()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
               return conn.Execute(
                        @"INSERT [dbo].[Products] ([Name], [Description], [Category], [Price]) VALUES (@a, @b,@c,@d)",
                        new[]
                        {
                            new
                            {
                                a = "Lifejacket",
                                b = "Protective and fashionable",
                                c = "WaterSports",
                                d = 1
                            },
                            new
                            {
                                a = "Lifejacket2",
                                b = "Protective and fashionable2",
                                c = "WaterSports2",
                                d = 2
                            },
                            new
                            {
                                a = "Lifejacket3",
                                b = "Protective and fashionable3",
                                c = "WaterSports3",
                                d =  3
                            }
                        });
            }
        }

        public bool Update()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var r = conn.Execute(@"update Product set ProductName='Nginx' where ProductId=@ProductId",
                    new {ProductId = 2});
                return r > 0;
            }
        }

        public bool Delete()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                var r = conn.Execute(@"delete from Product where ProductId=@ProductId",
                    new { ProductId = 2 });
                return r > 0;
            }
        }
    }
}
