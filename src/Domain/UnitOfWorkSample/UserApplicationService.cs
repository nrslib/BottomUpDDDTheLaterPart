using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using Domain.Application.Models;
using Domain.Domain.Users;

namespace Domain.UnitOfWorkSample
{
    public class UserApplicationServiceByUow
    {
        private readonly IUserFactory userFactory;
        private readonly IUnitOfWork uow;
        private readonly UserService userService;

        public UserApplicationServiceByUow(
            IUserFactory userFactory,
            IUnitOfWork uow) {
            this.userFactory = userFactory;
            this.uow = uow;
            userService = new UserService(uow.UserRepository);
        }

        public void RegisterUser(string username, string firstname, string familyname) {
            try {
                var user = userFactory.CreateUser(
                    new UserName(username),
                    new FullName(firstname, familyname)
                );
                if (userService.IsDuplicated(user)) {
                    throw new Exception("重複しています");
                } else {
                    uow.UserRepository.Save(user);
                }
                uow.Commit();
            } catch {
                uow.Rollback();
                throw;
            }
        }

        public void ChangeUserInfo(string id, string username, string firstname, string familyname) {
            try {
                var targetId = new UserId(id);
                var target = uow.UserRepository.Find(targetId);
                if (target == null) {
                    throw new Exception("not found. target id:" + id);
                }
                var newUserName = new UserName(username);
                target.ChangeUserName(newUserName);
                var newName = new FullName(firstname, familyname);
                target.ChangeName(newName);
                uow.UserRepository.Save(target);
                uow.Commit();
            } catch {
                uow.Rollback();
                throw;
            }
        }

        public void RemoveUser(string id) {
            try {
                var targetId = new UserId(id);
                var target = uow.UserRepository.Find(targetId);
                if (target == null) {
                    throw new Exception("not found. target id:" + id);
                }
                uow.UserRepository.Remove(target);
                uow.Commit();
            } catch {
                uow.Rollback();
                throw;
            }
        }

        public UserModel GetUserInfo(string id) {
            var userId = new UserId(id);
            var target = uow.UserRepository.Find(userId);
            if (target == null) {
                return null;
            }

            return new UserModel(target);
        }

        public List<UserSummaryModel> GetUserList() {
            var users = uow.UserRepository.FindAll();
            return users.Select(x => new UserSummaryModel(x)).ToList();
        }
    }
}
