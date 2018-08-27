using System;
using Domain.Domain.Circles;

namespace Domain.Domain.Users {
    public class User {
        private readonly UserId id;
        private UserName userName;
        private FullName name;

        public User(UserId id, UserName userName, FullName name) {
            this.id = id;
            this.userName = userName;
            this.name = name;
        }

        public UserId Id {
            get { return id; }
        }

        public UserName UserName {
            get { return userName; }
        }

        public FullName Name {
            get { return name; }
        }

        public bool EqualsEntity(User arg) {
            if (ReferenceEquals(null, arg)) return false;
            if (ReferenceEquals(this, arg)) return true;
            return id.Equals(arg.id);
        }

        public void ChangeUserName(UserName newName) {
            if (newName == null) throw new ArgumentNullException(nameof(newName));
            userName = newName;
        }

        public void ChangeName(FullName newName) {
            if (newName == null) throw new ArgumentNullException(nameof(newName));
            this.name = newName;
        }

        public Circle CreateCircle(ICircleFactory circleFactory, string circleName) {
            return circleFactory.Create(id, circleName);
        }
    }
}