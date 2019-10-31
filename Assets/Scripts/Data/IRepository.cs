using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Yumemonogatari.Data {
    public interface IRepository {
        void Save(object o);
        void Remove(object o);
        object GetById(Type type, string id);
        IQueryable<TEntity> ToList<TEntity>();
    }
}