using System.Collections.Generic;
using Domain.Domain.Users;

namespace Domain.Domain.Circles
{
    public interface ICircleNotification {
        void Id(CircleId id);
        void Name(string name);
        void Users(List<UserId> users);
    }
}
