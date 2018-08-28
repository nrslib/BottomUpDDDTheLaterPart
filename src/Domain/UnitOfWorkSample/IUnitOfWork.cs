using System;
using Domain.Domain.Users;

namespace Domain.UnitOfWorkSample
{
    public interface IUnitOfWork : IDisposable {
        IUserRepository UserRepository { get; }
        void Commit();
        void Rollback();
    }
}
