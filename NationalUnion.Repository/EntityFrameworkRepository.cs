using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using NationalUnion.Infrastructure;
using NationalUnion.Domain.Repository;
using System.Data.Objects;

namespace NationalUnion.Repository
{
    public class EntityFrameworkRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly DbContext _context;

        public DbContext Context
        {
            get { return _context; }
        }

        protected DbSet<TEntity> DbSet
        {
            get { return this.Context.Set<TEntity>(); }
        }

        public EntityFrameworkRepository(DbContext context)
        {
            this._context = context;
        }

        #region IRepository<TEntity> 成员

        public TEntity FindBy(object id)
        {
            return this.DbSet.Find(id);
        }

        public IQueryable<TEntity> FindAll()
        {
            return this.DbSet;
        }

        public IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAll().Where(predicate);
        }

        public IQueryable<TEntity> FindBy(Expression predicate)
        {
            Expression exp = Expression.Call(typeof (Queryable), "Where",
                new Type[] {typeof (TEntity)}, this.DbSet.AsQueryable().Expression, predicate);
            return this.DbSet.AsQueryable().Provider.CreateQuery<TEntity>(exp);
        }

        public int Add(TEntity entity)
        {
            try
            { // 写数据库
                this.Context.Entry<TEntity>(entity).State = EntityState.Added;
                return this.Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                NationalUnion.Common.Log.Logger.LogError(dbEx, string.Empty, string.Empty, "添加实体出错");
            }
            return 0;
        }

        public int Remove(TEntity entity)
        {
            //this.DbSet.Remove(entity);
            this.Context.Entry<TEntity>(entity).State = EntityState.Deleted;

            return this.Context.SaveChanges();
        }

        public int Update(TEntity entity)
        {
            try
            { // 写数据库
                this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
                return this.Context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                NationalUnion.Common.Log.Logger.LogError(dbEx, string.Empty, string.Empty, "更新实体出错");
            }
            return 0;
        }
        /// <summary>
        /// 外键关系的更新保存失败
        /// </summary>
        /// <param name="ModelName">实体名称</param>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        public int Updates(string ModelName, TEntity entity)
        {
            var _dc = (IObjectContextAdapter)this.Context; // MyContext context=new MyContext(); MyContext 继承 DbContext
                var _oc = _dc.ObjectContext;
                var key = _oc.CreateEntityKey(ModelName, entity); //entity 为修改的对象
                   //MyDbSets 为 MyContext 中 public DbSet<MyDbSet> MyDbSets { get; set; }
                ObjectStateEntry ose;
                if (_oc.ObjectStateManager.TryGetObjectStateEntry(key, out ose))
                {
                    var _entity = (TEntity)ose.Entity;
                    this.Context.Entry(_entity).State = EntityState.Detached;
                    // Detached状态，就是entity还没有attach到context（实际上是Attach到某个DbSet上）的状态
                }
                this.DbSet.Attach(entity);
                this.Context.Entry(entity).State = EntityState.Modified;
                return this.Context.SaveChanges();
        }

        /// <summary>
        /// 外键关系的更新保存失败
        /// </summary>
        /// <param name="ModelName">实体名称</param>
        /// <param name="entity">对象</param>
        /// <returns></returns>
        public int Adds(string ModelName, TEntity entity)
        {
            var _dc = (IObjectContextAdapter)this.Context; // MyContext context=new MyContext(); MyContext 继承 DbContext
            var _oc = _dc.ObjectContext;
            var key = _oc.CreateEntityKey(ModelName, entity); //entity 为修改的对象
            //MyDbSets 为 MyContext 中 public DbSet<MyDbSet> MyDbSets { get; set; }
            ObjectStateEntry ose;
            if (_oc.ObjectStateManager.TryGetObjectStateEntry(key, out ose))
            {
                var _entity = (TEntity)ose.Entity;
                this.Context.Entry(_entity).State = EntityState.Detached;
                // Detached状态，就是entity还没有attach到context（实际上是Attach到某个DbSet上）的状态
            }
            this.DbSet.Attach(entity);
            this.Context.Entry(entity).State = EntityState.Added;
            return this.Context.SaveChanges();
        }

        public int Commit()
        {
            try
            {
                return this.Context.SaveChanges();
            }

            catch (DbEntityValidationException ex)
            {
                var sbErr = new StringBuilder();
                foreach (var err in ex.EntityValidationErrors)
                {
                    //遍历错误信息
                    foreach (var validationErr in err.ValidationErrors)
                    {
                        sbErr.Append("ErrorMessage:" + validationErr.ErrorMessage + " PropertyName:" + validationErr.PropertyName);
                    }
                }
                Exception e = new InvalidOperationException(sbErr.ToString());
                NationalUnion.Common.Log.Logger.LogError(e, string.Empty, string.Empty, string.Empty);
                throw e;
            }

            catch (DbUpdateException ex)
            {
                if (ex.InnerException is UpdateException)
                {
                    if ((ex.InnerException as UpdateException).InnerException is SqlException)
                    {
                        var sqlEx = (ex.InnerException as UpdateException).InnerException as SqlException;

                        if (sqlEx != null && sqlEx.Number == 547)
                        {
                            NationalUnion.Common.Log.Logger.LogError(ex, string.Empty, string.Empty, string.Empty);
                            throw sqlEx;
                        }
                    }
                }
                NationalUnion.Common.Log.Logger.LogError(ex, string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            catch (SqlException ex)
            {
                NationalUnion.Common.Log.Logger.LogError(ex, string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public void RollBack()
        {
            this.Context.ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());
        }

        #endregion
        
        #region 用于处理外键关系的方法
        
        private object[] GetKeyValues<T>(T entity)
        {
            var keyPropertyInfos = entity.GetType().GetProperties()
                .Where(p => Attribute.GetCustomAttributes(p).Any(attr => attr is System.ComponentModel.DataAnnotations.Schema.ForeignKeyAttribute));
            if (keyPropertyInfos.Count() > 1)
                keyPropertyInfos.OrderBy(p => ((System.ComponentModel.DataAnnotations.Schema.ColumnAttribute)Attribute
                .GetCustomAttribute(p, typeof(System.ComponentModel.DataAnnotations.Schema.ColumnAttribute))).Order);
            return keyPropertyInfos.Select(kp => kp.GetValue(entity, null)).ToArray();
        }
        
        private bool KeyEquals<T>(T leftEntity, T rightEntity)
        {
            var leftKeys = GetKeyValues(leftEntity).ToList();
            var rightKey = GetKeyValues(rightEntity).ToList();
            for (int i = 0; i < leftKeys.Count(); i++)
            {
                if (leftKeys[i] == null)
                {
                    if (rightKey[i] == null) continue;
                    return false;
                }
                if (!leftKeys[i].Equals(rightKey[i]))
                    return false;
            }
            return true;
        }
        
        /// <summary>
        /// 将当前值替换为代理类对象
        /// 用于插入一个新的对象时建立一对多关联
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="currentValues"></param>
        protected void UpdateEntities<T>(List<T> currentValues)
            where T : class, IAggregateRoot
        {
            UpdateEntities(currentValues, currentValues);
        }

        /// <summary>
        /// 用当前值更新初始值，同时替换为代理类对象；
        /// 用于一对多关系的关联更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalValues"></param>
        /// <param name="currentValues"></param>
        protected void UpdateEntities<T>(List<T> originalValues, List<T> currentValues)
            where T : class, IAggregateRoot
        {
            //克隆currentValues以防止originalValues与currentValues来自同一个对象
            //这样清空originalValues是安全的
            var clonedCurrentValues = currentValues.ToList();
            originalValues.Clear();

            originalValues.AddRange(clonedCurrentValues.Select(currentValue => Context.Set<T>().Find(GetKeyValues(currentValue))));
        }

        /// <summary>
        /// 添加、更新、删除一对多关系下的外键对象
        /// 通常用于聚合下的非聚合根对象的更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalValues"></param>
        /// <param name="currentValues"></param>
        protected void UpdateChildEntities<T>(List<T> originalValues, List<T> currentValues)
            where T : class
        {
            var clonedOriginalValues = originalValues.ToList();
            foreach (var originalValue in clonedOriginalValues)
            {
                if (!currentValues.Any(c => KeyEquals(c, originalValue)))
                    Context.Entry(originalValue).State = EntityState.Deleted;
            }

            foreach (var currentValue in currentValues)
            {
                var originalValue = clonedOriginalValues.SingleOrDefault(ov => KeyEquals(ov, currentValue));

                if (originalValue != null)
                {
                    Context.Entry(originalValue).CurrentValues.SetValues(currentValue);
                }
                else
                {
                    originalValues.Add(currentValue);
                }
            }
        }
        #endregion

        #region Dispose

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                this.Context.Dispose();
            }
        }

        #endregion
    }
}
