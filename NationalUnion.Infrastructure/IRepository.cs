using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NationalUnion.Infrastructure
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        TEntity FindBy(object id);
        IQueryable<TEntity> FindAll();
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindBy(Expression predicate);
        int Add(TEntity entity);
        int Remove(TEntity entity);
        int Updates(string modelName, TEntity entity);//add by lyang
        int Adds(string modelName, TEntity entity);
        int Update(TEntity entity);
        int Commit();
    }
}
