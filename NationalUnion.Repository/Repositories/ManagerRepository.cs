using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Practices.Unity.Utility;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ManagerRepository : EntityFrameworkRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(NationalUnionContext context) : base(context)
        {
        }

        public bool ManagerNameExists(string managerName)
        {
            Guard.ArgumentNotNullOrEmpty(managerName, "managerName");
            var count = this.FindBy(m => m.ManagerName == managerName).Count();

            return count > 0;
        }

        public bool ManagerIdExists(Int64 managerId)
        {
            var count = this.FindBy(m => m.ManagerId == managerId).Count();

            return count > 0;
        }

        public Manager GetManagerByName(string managerName)
        {
            Guard.ArgumentNotNullOrEmpty(managerName, "managerName");

            return this.FindBy(m => m.ManagerName == managerName).FirstOrDefault();
        }

        public Manager GetManagerById(Int64 managerId)
        {
            return this.FindBy(m => m.ManagerId == managerId).FirstOrDefault();
        }

        public IQueryable<Manager> GetManagerList()
        {
            return this.FindAll();
        }

        public int AddManager(Manager entity)
        {
            return this.Add(entity);
        }

        public int AddManagerByfk(string modelName, Manager entity)
        {
            return this.Adds(modelName, entity);
        }

        public int EditManager(Manager entity)
        {
            return this.Update(entity);
        }

        public int DeleteManager(Manager entity)
        {
            return this.Remove(entity);
        }

        //public Manager ManagerLogin(string username, string password)
        //{
        //    return this.FindBy(m => m.ManagerName == username && m.Password == password).FirstOrDefault();
        //}

        public Manager ManagerLogin(long managerId)
        {
            return this.FindBy(m => m.ManagerId == managerId).FirstOrDefault();
        }
    }
}
