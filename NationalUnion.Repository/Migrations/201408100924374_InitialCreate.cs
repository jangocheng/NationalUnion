namespace NationalUnion.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Region",
                c => new
                    {
                        RId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EnglishName = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.RId);
            
            CreateTable(
                "dbo.Province",
                c => new
                    {
                        PId = c.Int(nullable: false, identity: true),
                        RId = c.Int(nullable: false),
                        Name = c.String(),
                        EnglishName = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.PId)
                .ForeignKey("dbo.Region", t => t.RId, cascadeDelete: true)
                .Index(t => t.RId);
            
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CId = c.Int(nullable: false, identity: true),
                        PId = c.Int(nullable: false),
                        Name = c.String(),
                        EnglishName = c.String(),
                        Remark = c.String(),
                    })
                .PrimaryKey(t => t.CId)
                .ForeignKey("dbo.Province", t => t.PId, cascadeDelete: true)
                .Index(t => t.PId);
            
            CreateTable(
                "dbo.Channel",
                c => new
                    {
                        ChannelId = c.Long(nullable: false, identity: true),
                        ChannelName = c.String(maxLength: 100),
                        Rank = c.Int(nullable: false),
                        ChannelType = c.Int(nullable: false),
                        ParentId = c.Long(nullable: false),
                        KeyWord = c.String(),
                        Sort = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ChannelId);
            
            CreateTable(
                "dbo.ShareChannel",
                c => new
                    {
                        ShareChannelId = c.Long(nullable: false),
                        ChannelName = c.String(maxLength: 100),
                        Rank = c.Int(nullable: false),
                        ChannelType = c.Int(nullable: false),
                        ParentId = c.Long(nullable: false),
                        Sort = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                        IsActive = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShareChannelId);
            
            CreateTable(
                "dbo.Manager",
                c => new
                    {
                        ManagerId = c.Long(nullable: false),
                        ManagerName = c.String(maxLength: 100),
                        ManagerEmail = c.String(),
                        MobilePhone = c.String(),
                        IdCardNo = c.String(),
                        ChannelType = c.Int(nullable: false),
                        ChannelId = c.Long(nullable: false),
                        ShareChannelId = c.Long(nullable: false),
                        CId = c.Int(nullable: false),
                        CashOutPwd = c.String(maxLength: 50),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                        IsActive = c.Int(nullable: false),
                        ManagerInfo = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ManagerId)
                .ForeignKey("dbo.Channel", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.ShareChannel", t => t.ShareChannelId, cascadeDelete: true)
                .ForeignKey("dbo.City", t => t.CId, cascadeDelete: true)
                .Index(t => t.ChannelId)
                .Index(t => t.ShareChannelId)
                .Index(t => t.CId);
            
            CreateTable(
                "dbo.ManagerDetail",
                c => new
                    {
                        ManagerDetailId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        CurrentChannelId = c.Long(nullable: false),
                        OldChannelId = c.Long(nullable: false),
                        CurrentRank = c.Int(nullable: false),
                        OldRank = c.Int(nullable: false),
                        RankStatus = c.Int(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModifiedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.ManagerDetailId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.ChannelMoudle",
                c => new
                    {
                        MoudleId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 200),
                        EnglishName = c.String(maxLength: 200),
                        ParentId = c.String(maxLength: 50),
                        Url = c.String(maxLength: 200),
                        Iconic = c.String(maxLength: 200),
                        Sort = c.Int(nullable: false),
                        Remark = c.String(maxLength: 4000),
                        MoudleState = c.Int(nullable: false),
                        CreatePerson = c.String(maxLength: 200),
                        CreateTime = c.DateTime(nullable: false),
                        IsLast = c.Int(nullable: false),
                        Version = c.String(),
                    })
                .PrimaryKey(t => t.MoudleId);
            
            CreateTable(
                "dbo.ChannelMoudleOperate",
                c => new
                    {
                        MoudleOperateId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 200),
                        MoudleId = c.String(maxLength: 50),
                        KeyCode = c.String(maxLength: 200),
                        IsValid = c.Int(nullable: false),
                        Sort = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MoudleOperateId)
                .ForeignKey("dbo.ChannelMoudle", t => t.MoudleId)
                .Index(t => t.MoudleId);
            
            CreateTable(
                "dbo.ChannelRole",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 50),
                        Name = c.String(maxLength: 200),
                        Description = c.String(maxLength: 4000),
                        CreateTime = c.DateTime(nullable: false),
                        CreatePerson = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.ChannelRight",
                c => new
                    {
                        RightId = c.String(nullable: false, maxLength: 200),
                        MoudleId = c.String(maxLength: 50),
                        RoleId = c.String(maxLength: 50),
                        RightFlag = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RightId)
                .ForeignKey("dbo.ChannelMoudle", t => t.MoudleId)
                .ForeignKey("dbo.ChannelRole", t => t.RoleId)
                .Index(t => t.MoudleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ChannelRightOperate",
                c => new
                    {
                        RightOperateId = c.String(nullable: false, maxLength: 200),
                        RightId = c.String(maxLength: 200),
                        KeyCode = c.String(maxLength: 200),
                        IsValid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RightOperateId)
                .ForeignKey("dbo.ChannelRight", t => t.RightId)
                .Index(t => t.RightId);
            
            CreateTable(
                "dbo.ChannelRoleManager",
                c => new
                    {
                        ManagerId = c.Long(nullable: false),
                        RoleId = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => new { t.ManagerId, t.RoleId })
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .ForeignKey("dbo.ChannelRole", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.ManagerId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.PromotionRevenue",
                c => new
                    {
                        PromoRevenueId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductQuantity = c.String(),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PromoRevenueId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.Revenue",
                c => new
                    {
                        RevenueId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        PersonalCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TeamCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ManageCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        KpiScore = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalRevenue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        RevenueMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RevenueId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.PersonalPerformance",
                c => new
                    {
                        PersonalPerformanceId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        OrderId = c.Int(nullable: false),
                        OrderCategoryId = c.Int(nullable: false),
                        OrderCategory = c.String(),
                        ProductQuantity = c.String(),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpectedKickback = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderSource = c.String(),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PersonalPerformanceId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.AssistPerformance",
                c => new
                    {
                        AssistPerformanceId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        PV = c.Int(nullable: false),
                        UV = c.Int(nullable: false),
                        LoginCount = c.Int(nullable: false),
                        RegisterCount = c.Int(nullable: false),
                        NewMemberCount = c.Int(nullable: false),
                        DevelopMemberCount = c.Int(nullable: false),
                        PerformanceMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.AssistPerformanceId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.TeamPerformance",
                c => new
                    {
                        TeamPerformanceId = c.Int(nullable: false, identity: true),
                        ManagerId = c.Long(nullable: false),
                        PV = c.Int(nullable: false),
                        UV = c.Int(nullable: false),
                        OrderQuantity = c.Int(nullable: false),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PerformanceMonth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TeamPerformanceId)
                .ForeignKey("dbo.Manager", t => t.ManagerId, cascadeDelete: true)
                .Index(t => t.ManagerId);
            
            CreateTable(
                "dbo.OrderProductOccur",
                c => new
                    {
                        OrderProductOccurId = c.Int(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        ProductSkuId = c.String(maxLength: 50),
                        ProductPrice = c.Decimal(nullable: false, storeType: "money"),
                        ProductNumber = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 1000),
                        ProductPriceAmount = c.Decimal(nullable: false, storeType: "money"),
                        CategoryId = c.String(maxLength: 50),
                        Commission = c.Decimal(nullable: false, storeType: "money"),
                        CommissionRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WebSite = c.String(maxLength: 50),
                        ProductSite = c.String(maxLength: 50),
                        OrderSettlement = c.String(maxLength: 50),
                        OrderIp = c.String(maxLength: 50),
                        OrderStatus = c.Int(nullable: false),
                        PushStatus = c.Int(nullable: false),
                        SharedUserId = c.Long(nullable: false),
                        SharedLevel = c.Int(nullable: false),
                        SharedManagerId = c.Long(nullable: false),
                        ChannelId = c.Long(nullable: false),
                        PlatformId = c.Int(nullable: false),
                        ItemId = c.String(maxLength: 50),
                        ItemType = c.Int(nullable: false),
                        SharedClientId = c.Int(nullable: false),
                        SharedDate = c.DateTime(nullable: false),
                        MemberNo = c.Long(nullable: false),
                        OmnitureFlag = c.Int(nullable: false),
                        OrderOccurTime = c.DateTime(nullable: false),
                        RecordCreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderProductOccurId);
            
            CreateTable(
                "dbo.OrderProductEffect",
                c => new
                    {
                        OrderProductEffectId = c.Int(nullable: false, identity: true),
                        OrderId = c.Long(nullable: false),
                        ProductSkuId = c.String(maxLength: 50),
                        ProductPrice = c.Decimal(nullable: false, storeType: "money"),
                        ProductNumber = c.Int(nullable: false),
                        ProductName = c.String(maxLength: 1000),
                        ProductPriceAmount = c.Decimal(nullable: false, storeType: "money"),
                        CategoryId = c.String(maxLength: 50),
                        Commission = c.Decimal(nullable: false, storeType: "money"),
                        CommissionRatio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        WebSite = c.String(maxLength: 50),
                        ProductSite = c.String(maxLength: 50),
                        OrderIp = c.String(maxLength: 50),
                        OrderStatus = c.Int(nullable: false),
                        ProductStatus = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsCpsCoupon = c.Boolean(nullable: false),
                        Distribution = c.String(maxLength: 50),
                        SharedUserId = c.Long(nullable: false),
                        SharedLevel = c.Int(nullable: false),
                        SharedManagerId = c.Long(nullable: false),
                        ChannelId = c.Long(nullable: false),
                        PlatformId = c.Int(nullable: false),
                        ItemId = c.String(maxLength: 50),
                        ItemType = c.Int(nullable: false),
                        SharedClientId = c.Int(nullable: false),
                        SharedDate = c.DateTime(nullable: false),
                        MemberNo = c.Long(nullable: false),
                        OrderOccurTime = c.DateTime(nullable: false),
                        EffectTime = c.DateTime(nullable: false),
                        RecordCreatedTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OrderProductEffectId);
            
            CreateTable(
                "dbo.ProductType",
                c => new
                    {
                        PtoTypeId = c.String(nullable: false, maxLength: 20),
                        ProTypeName = c.String(maxLength: 200),
                        Pid = c.String(),
                        FinalPid = c.String(),
                        SortId = c.Int(),
                        CateUrl = c.String(maxLength: 2000),
                        IsUsed = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PtoTypeId);
            
            CreateTable(
                "dbo.DataSummary",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        PlatformID = c.Int(nullable: false),
                        SharedClientID = c.Int(nullable: false),
                        SummaryDate = c.DateTime(nullable: false, storeType: "date"),
                        ChannelID = c.Long(nullable: false),
                        ManagerID = c.Long(nullable: false),
                        PV = c.Int(nullable: false),
                        UV = c.Int(nullable: false),
                        OrderQuantity = c.Int(nullable: false),
                        ProductsQuantity = c.Int(nullable: false),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EstimateCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvaliableCommission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvaliableOrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvaliableOrderQuantity = c.Int(nullable: false),
                        SharedQunatity = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Long(nullable: false),
                        NickName = c.String(maxLength: 50),
                        RealName = c.String(maxLength: 50),
                        ChannelId = c.Long(nullable: false),
                        ManagerID = c.Long(nullable: false),
                        UserLevelID = c.Int(nullable: false),
                        UserVip = c.Int(nullable: false),
                        VipExpirationTime = c.DateTime(nullable: false),
                        RegisterTime = c.DateTime(nullable: false),
                        UserInfo = c.String(maxLength: 1000),
                        BindingInfo = c.String(maxLength: 2000),
                        Cash = c.Decimal(nullable: false, storeType: "money"),
                        CashFreeze = c.Decimal(nullable: false, storeType: "money"),
                        Coin = c.Int(nullable: false),
                        CoinFreeze = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        ImageUrl = c.String(maxLength: 100),
                        UserType = c.Int(nullable: false),
                        DepartMent = c.String(),
                        Center = c.String(),
                        GomeNo = c.String(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Channel", t => t.ChannelId, cascadeDelete: true)
                .ForeignKey("dbo.UserLevel", t => t.UserLevelID, cascadeDelete: true)
                .Index(t => t.ChannelId)
                .Index(t => t.UserLevelID);
            
            CreateTable(
                "dbo.UserLevel",
                c => new
                    {
                        LevelID = c.Int(nullable: false, identity: true),
                        Level = c.Int(nullable: false),
                        LevelCode = c.String(),
                        LevelName = c.String(maxLength: 50),
                        LevelDescription = c.String(maxLength: 500),
                        OrderAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderQuantity = c.Int(nullable: false),
                        UVQuantity = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.LevelID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "UserLevelID" });
            DropIndex("dbo.User", new[] { "ChannelId" });
            DropIndex("dbo.TeamPerformance", new[] { "ManagerId" });
            DropIndex("dbo.AssistPerformance", new[] { "ManagerId" });
            DropIndex("dbo.PersonalPerformance", new[] { "ManagerId" });
            DropIndex("dbo.Revenue", new[] { "ManagerId" });
            DropIndex("dbo.PromotionRevenue", new[] { "ManagerId" });
            DropIndex("dbo.ChannelRoleManager", new[] { "RoleId" });
            DropIndex("dbo.ChannelRoleManager", new[] { "ManagerId" });
            DropIndex("dbo.ChannelRightOperate", new[] { "RightId" });
            DropIndex("dbo.ChannelRight", new[] { "RoleId" });
            DropIndex("dbo.ChannelRight", new[] { "MoudleId" });
            DropIndex("dbo.ChannelMoudleOperate", new[] { "MoudleId" });
            DropIndex("dbo.ManagerDetail", new[] { "ManagerId" });
            DropIndex("dbo.Manager", new[] { "CId" });
            DropIndex("dbo.Manager", new[] { "ShareChannelId" });
            DropIndex("dbo.Manager", new[] { "ChannelId" });
            DropIndex("dbo.City", new[] { "PId" });
            DropIndex("dbo.Province", new[] { "RId" });
            DropForeignKey("dbo.User", "UserLevelID", "dbo.UserLevel");
            DropForeignKey("dbo.User", "ChannelId", "dbo.Channel");
            DropForeignKey("dbo.TeamPerformance", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.AssistPerformance", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.PersonalPerformance", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.Revenue", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.PromotionRevenue", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.ChannelRoleManager", "RoleId", "dbo.ChannelRole");
            DropForeignKey("dbo.ChannelRoleManager", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.ChannelRightOperate", "RightId", "dbo.ChannelRight");
            DropForeignKey("dbo.ChannelRight", "RoleId", "dbo.ChannelRole");
            DropForeignKey("dbo.ChannelRight", "MoudleId", "dbo.ChannelMoudle");
            DropForeignKey("dbo.ChannelMoudleOperate", "MoudleId", "dbo.ChannelMoudle");
            DropForeignKey("dbo.ManagerDetail", "ManagerId", "dbo.Manager");
            DropForeignKey("dbo.Manager", "CId", "dbo.City");
            DropForeignKey("dbo.Manager", "ShareChannelId", "dbo.ShareChannel");
            DropForeignKey("dbo.Manager", "ChannelId", "dbo.Channel");
            DropForeignKey("dbo.City", "PId", "dbo.Province");
            DropForeignKey("dbo.Province", "RId", "dbo.Region");
            DropTable("dbo.UserLevel");
            DropTable("dbo.User");
            DropTable("dbo.DataSummary");
            DropTable("dbo.ProductType");
            DropTable("dbo.OrderProductEffect");
            DropTable("dbo.OrderProductOccur");
            DropTable("dbo.TeamPerformance");
            DropTable("dbo.AssistPerformance");
            DropTable("dbo.PersonalPerformance");
            DropTable("dbo.Revenue");
            DropTable("dbo.PromotionRevenue");
            DropTable("dbo.ChannelRoleManager");
            DropTable("dbo.ChannelRightOperate");
            DropTable("dbo.ChannelRight");
            DropTable("dbo.ChannelRole");
            DropTable("dbo.ChannelMoudleOperate");
            DropTable("dbo.ChannelMoudle");
            DropTable("dbo.ManagerDetail");
            DropTable("dbo.Manager");
            DropTable("dbo.ShareChannel");
            DropTable("dbo.Channel");
            DropTable("dbo.City");
            DropTable("dbo.Province");
            DropTable("dbo.Region");
        }
    }
}
