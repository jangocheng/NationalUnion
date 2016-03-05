using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.Utility;
using NationalUnion.Domain.Mapping;
using NationalUnion.Repository;
using NationalUnion.Repository.Interfaces;

namespace NationalUnion.Repository.Repositories
{
    public class ChannelManageRepository :EntityFrameworkRepository<Channel>, IChannelManageRepository
    {
        public ChannelManageRepository(NationalUnionContext context) : base(context)
        {
        }

        public bool ChannelIdExists(Int64 channelId)
        {
            Guard.ArgumentNotNull(channelId, "channelId");
            var count = this.FindBy(c => c.ChannelId == channelId).Count();

            return count > 0;
        }

        public Channel GetChannelById(Int64 channelId)
        {
            Guard.ArgumentNotNull(channelId, "channelId");

            return this.FindBy(c => c.ChannelId == channelId).FirstOrDefault();
        }

        public List<Channel> GetChannelListByName(string channelName)
        {
            return this.FindBy(c => c.ChannelName.Contains(channelName)).ToList();
        }

        public Channel GetChannelByName(string channelName)
        {
            return this.FindBy(c => c.ChannelName == channelName).FirstOrDefault();
        }

        public IQueryable<Channel> GetChannelList()
        {
            return this.FindAll();
        }

        public int AddChannel(Channel entity)
        {
            return this.Add(entity);
        }

        public int EditChannel(Channel entity)
        {
            return this.Update(entity);
        }

        public int DeleteChannel(Channel entity)
        {
            return this.Remove(entity);
        }
    }
}
