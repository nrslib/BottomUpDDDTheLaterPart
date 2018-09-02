using System;
using System.Collections.Generic;
using Domain.Domain.Users;

namespace Domain.Domain.Circles
{
    public class Circle : IEquatable<Circle> {
        private readonly CircleId id;
        private string name;
        private List<UserId> users;

        public Circle(
            CircleId id,
            string name,
            List<UserId> users
        ) {
            this.id = id;
            this.name = name;
            this.users = users;
        }

        public bool Equals(Circle other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Circle) obj);
        }

        public override int GetHashCode() {
            return (id != null ? id.GetHashCode() : 0);
        }

        public void Join(User user) {
            if (users.Count >= 30) {
                throw new Exception("too many members.");
            }
            users.Add(user.Id);
        }

        public void Notify(ICircleNotification note) {
            note.Id(id);
            note.Name(name);
            note.Users(new List<UserId>(users));
        }
    }
}
