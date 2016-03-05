using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Utility;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository.Interfaces;
using NationalUnion.Infrastructure;

namespace NationalUnion.Repository.Repositories
{
    public class ShareChannelRepository : EntityFrameworkRepository<ShareChannel>, IShareChannelRepository
    {
        public ShareChannelRepository(NationalUnionContext context)
            : base(context)
        {
        }

        public bool ShareChannelIdExists(Int64 shareChannelId)
        {
            Guard.ArgumentNotNull(shareChannelId, "shareChannelId");
            var count = this.FindBy(c => c.ShareChannelId == shareChannelId).Count();

            return count > 0;
        }

        public ShareChannel GetShareChannelById(Int64 shareChannelId)
        {
            Guard.ArgumentNotNull(shareChannelId, "shareChannelId");

            return this.FindBy(c => c.ShareChannelId == shareChannelId).FirstOrDefault();
        }

        public IQueryable<ShareChannel> GetShareChannelList()
        {
            return this.FindAll();
        }

        public int AddShareChannel(ShareChannel entity)
        {
            return this.Add(entity);
        }

        public int AddShareChannelByfk(string modelName, ShareChannel entity)
        {
            return this.Adds(modelName, entity);
        }

        public int EditShareChannel(ShareChannel entity)
        {
            return this.Update(entity);
        }

        public int DeleteShareChannel(ShareChannel entity)
        {
            return this.Remove(entity);
        }
    }
}
