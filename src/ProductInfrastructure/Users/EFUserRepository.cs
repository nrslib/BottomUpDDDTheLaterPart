using System.Collections.Generic;
using System.Linq;
using Domain.Domain.Users;
using ProductInfrastructure.DbContexts;

namespace ProductInfrastructure.Users
{
    public class EFUserRepository : IUserRepository {
        private readonly BottomUpDddDbContext dbContext;
        
        public EFUserRepository(BottomUpDddDbContext dbContext) {
            this.dbContext = dbContext;
        }

        public User Find(UserId id) {
            var target = dbContext.Users
                .FirstOrDefault(x => x.Id == id.Value);
            if (target != null) {
                return convertFrom(target);
            } else {
                return null;
            }
        }

        public User Find(UserName name) {
            var target = dbContext.Users
                .FirstOrDefault(x => x.Username == name.Value);
            if (target != null) {
                return convertFrom(target);
            } else {
                return null;
            }
        }

        public IEnumerable<User> FindAll() {
            return dbContext.Users.Select(convertFrom);
        }

        public void Save(User user) {
            var target = dbContext.Users.FirstOrDefault(x => x.Id == user.Id.Value);
            if (target != null) {
                target.Id = user.Id.Value;
                target.Username = user.UserName.Value;
                target.Firstname = user.Name.FirstName;
                target.Familyname = user.Name.FamilyName;
            } else {
                var newUser = new Models.User {
                    Id = user.Id.Value,
                    Username = user.UserName.Value,
                    Firstname = user.Name.FirstName,
                    Familyname = user.Name.FamilyName
                };
                dbContext.Users.Add(newUser);
            }
            dbContext.SaveChanges();
        }

        public void Remove(User user) {
            var target = dbContext.Users.FirstOrDefault(x => x.Id == user.Id.Value);
            if (target != null) {
                dbContext.Users.Remove(target);
            }
            dbContext.SaveChanges(true);
        }

        private User convertFrom(Models.User source) {
            return new User(
                new UserId(source.Id),
                new UserName(source.Username),
                new FullName(source.Firstname, source.Familyname)
            );
        }
    }
}
