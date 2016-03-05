using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Utility;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ManagerDetailRepository : EntityFrameworkRepository<ManagerDetail>, IManagerDetailRepository
    {
        public ManagerDetailRepository(NationalUnionContext context)
            : base(context)
        {
        }

        public bool ManagerDetailIdExists(int managerDetailId)
        {
            Guard.ArgumentNotNull(managerDetailId, "managerDetailId");
            var count = FindBy(md => md.ManagerDetailId == managerDetailId).Count();

            return count > 0;
        }

        public ManagerDetail GetManagerDetailById(int managerDetailId)
        {
            Guard.ArgumentNotNull(managerDetailId, "managerDetailId");

            return FindBy(md => md.ManagerDetailId == managerDetailId).FirstOrDefault();
        }

        public IQueryable<ManagerDetail> GetManagerDetailList()
        {
            return FindAll();
        }

        public int AddManagerDetail(ManagerDetail entity)
        {
            return Add(entity);
        }

        public int EditManagerDetail(ManagerDetail entity)
        {
            return Update(entity);
        }

        public int DeleteManagerDetail(ManagerDetail entity)
        {
            return Remove(entity);
        }
    }
}
