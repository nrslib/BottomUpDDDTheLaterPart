namespace Domain.Domain.Users
{
    public interface IUserFactory {
        User CreateUser(UserName username, FullName fullName);
    }
}
