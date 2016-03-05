namespace NationalUnion.Domain.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatasummaryAddAvalibleColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DataSummary", "SharedClientID", c => c.Int(nullable: false));
            AddColumn("dbo.DataSummary", "ProductsQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.DataSummary", "AvaliableOrderAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.DataSummary", "AvaliableOrderQuantity", c => c.Int(nullable: false));
            AddColumn("dbo.DataSummary", "SharedQunatity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DataSummary", "SharedQunatity");
            DropColumn("dbo.DataSummary", "AvaliableOrderQuantity");
            DropColumn("dbo.DataSummary", "AvaliableOrderAmount");
            DropColumn("dbo.DataSummary", "ProductsQuantity");
            DropColumn("dbo.DataSummary", "SharedClientID");
        }
    }
}
