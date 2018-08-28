using System;
using Domain.Domain.Users;
using Domain.UnitOfWorkSample;
using MySql.Data.MySqlClient;

namespace ProductInfrastructure.UnitOfWorkSample {
    public class UnitOfWork : IUnitOfWork {
        private readonly MySqlConnection con;
        private readonly MySqlTransaction transaction;
        private bool disposed;

        private UserRepository userRepository;

        public UnitOfWork() {
            con = new MySqlConnection(Config.ConnectionString);
            con.Open();
            transaction = con.BeginTransaction();
        }

        public IUserRepository UserRepository {
            get {
                if (userRepository == null) {
                    userRepository = new UserRepository(con);
                }
                return userRepository;
            }
        }

        public void Commit() {
            transaction.Commit();
        }

        public void Rollback() {
            transaction.Rollback();
        }

        public void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) {
            if (!disposed) {
                if (disposing) {
                    transaction.Dispose();
                    con.Dispose();
                }
            }
            disposed = true;
        }
    }
}
