namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerQuote_to_Quote : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pieces = c.Int(nullable: false),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        PickupTimeWindow = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryZip = c.String(),
                        DeliveryTimeWindow = c.String(),
                        QuotedPrice = c.Int(nullable: false),
                        PackagesSerialized = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        DimensionalWeight = c.Int(nullable: false),
                        DimFactor = c.Int(nullable: false),
                        Quote_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.Quote_Id)
                .Index(t => t.Quote_Id);
            
            DropTable("dbo.CustomerQuotes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CustomerQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pieces = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        PickupWindowStart = c.String(),
                        PickupWindowEnd = c.String(),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryZip = c.String(),
                        DeliveryWindowStart = c.String(),
                        DeliveryWindowEnd = c.String(),
                        Cost = c.Int(nullable: false),
                        QuotePrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Packages", "Quote_Id", "dbo.Quotes");
            DropIndex("dbo.Packages", new[] { "Quote_Id" });
            DropTable("dbo.Packages");
            DropTable("dbo.Quotes");
        }
    }
}
