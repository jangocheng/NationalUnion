using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NationalUnion.Domain.Mapping;

namespace NationalUnion.Repository
{
    public class NationalUnionContext : DbContext
    {
        public NationalUnionContext() : base("NationalUnionContext")
        {
        }

        #region NationalUnion

        public DbSet<Region> Regions { get; set; }

        public DbSet<Province> Provinces { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<ShareChannel> ShareChannels { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<ManagerDetail> ManagerDetails { get; set; }

        public DbSet<ChannelMoudle> ChannelMoudles { get; set; }

        public DbSet<ChannelMoudleOperate> ChannelMoudleOperates { get; set; }

        public DbSet<ChannelRole> ChannelRoles { get; set; }

        public DbSet<ChannelRight> ChannelRights { get; set; }

        public DbSet<ChannelRightOperate> ChannelRightOperates { get; set; }

        public DbSet<ChannelRoleManager> ChannelRoleManagers { get; set; }

        public DbSet<PromotionRevenue> PromotionRevenues { get; set; }

        public DbSet<Revenue> Revenues { get; set; }

        public DbSet<PersonalPerformance> PersonalPerformances { get; set; }

        public DbSet<AssistPerformance> AssistPerformances { get; set; }

        public DbSet<TeamPerformance> TeamPerformances { get; set; }

        public DbSet<OrderProductOccur> OrderProductOccurs { get; set; }

        public DbSet<OrderProductEffect> OrderProductEffects { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        public DbSet<DataSummary> DataSummary { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserLevel> UserLevel { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataSummary>().Property(x => x.SummaryDate).HasColumnType("date");

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
