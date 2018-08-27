using Domain.Domain.Users;

namespace Domain.Domain.Circles
{
    public interface ICircleFactory {
        Circle Create(UserId userId, string name);
    }
}
