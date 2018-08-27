using System;
using System.Threading.Tasks;
using Domain.Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using ProductInfrastructure.DbContexts;
using ProductInfrastructure.Users;

namespace Domain.Tests.Users
{
    [TestClass]
    public class RegisterUserTest
    {
        [TestMethod]
        public void TestUserIsAtomic() {
            var factory = new UserFactory();
            var throwedException = false;
            try {
                Parallel.For(0, 2, _ => {
                    var context = new BottomUpDddDbContext();
                    context.Database.AutoTransactionsEnabled = false;
                    var repository = new EFUserRepository(context);
                    var app = new UserApplicationService(factory, repository);
                    app.RegisterUser("ataro", "taro", "tanaka");
                });
            } catch (AggregateException e) {
                throwedException = true;
            }
            Assert.IsTrue(throwedException);
        }

        private MySqlConnection CreateConnection() {
            var connection = new MySqlConnection("Database=bottomup_ddd;Data Source=localhost;User id=developer;Password=11111111;SslMode=none;");
            connection.Open();
            return connection;
        }
    }
}
