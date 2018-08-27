using System;
using System.Collections.Generic;
using Domain.Domain.Users;

namespace Domain.Domain.Circles
{
    public class Circle : IEquatable<Circle> {
        public Circle(
            CircleId id,
            string name,
            List<User> users
        ) {
            Id = id;
            Name = name;
            Users = users;
        }

        public CircleId Id { get; }
        public string Name { get; }
        public List<User> Users { get; }

        public bool Equals(Circle other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Id, other.Id);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Circle) obj);
        }

        public override int GetHashCode() {
            return (Id != null ? Id.GetHashCode() : 0);
        }

        public void Join(User user) {
            Users.Add(user);
        }
    }
}
