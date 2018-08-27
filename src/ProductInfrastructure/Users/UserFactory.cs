using System;
using Domain.Domain.Users;

namespace ProductInfrastructure.Users
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(UserName username, FullName fullName) {
            return new User(
                new UserId(Guid.NewGuid().ToString()),
                username,
                fullName
            );
        }
    }
}
