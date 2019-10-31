using System;
using System.Linq;
using NHibernate;

namespace Yumemonogatari.Data {
    public class Repository : IRepository, IDisposable {
        private ISession _session = null;
        private ITransaction _transaction = null;

        public Repository() {
            _session = Database.OpenSession();
        }

        public Repository(ISession session) {
            _session = session;
        }

        public virtual void Save(object o) {
            throw new NotImplementedException();
        }

        public virtual void Remove(object o) {
            throw new NotImplementedException();
        }

        public virtual object GetById(Type t, string id) => _session.Load(t, id);

        public virtual IQueryable<TEntity> ToList<TEntity>() => from e in _session.Query<TEntity>() select e;

        public void BeginTransaction() {
            _transaction = _session.BeginTransaction();
        }

        public void CommitTransaction() {
            if(_transaction == null)
                return;
            
            _transaction.Commit();
            CloseTransaction();
        }

        public void RollbackTransaction() {
            _transaction.Rollback();
            CloseTransaction();
            CloseSession();
        }

        private void CloseTransaction() {
            _transaction.Dispose();
            _transaction = null;
        }
        private void CloseSession() {
            CloseTransaction();
            _session.Close();
            _session.Dispose();
            _session = null;
        }

        public void Dispose() {
            if(_transaction != null)
                CommitTransaction();
            if(_session != null) {
                _session.Flush();
                CloseSession();
            }
        }
    }
}