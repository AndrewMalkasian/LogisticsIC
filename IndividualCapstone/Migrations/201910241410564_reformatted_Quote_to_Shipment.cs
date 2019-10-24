namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reformatted_Quote_to_Shipment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Packages", "Quote_Id", "dbo.Quotes");
            DropIndex("dbo.Packages", new[] { "Quote_Id" });
            AddColumn("dbo.Quotes", "PackageCost", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "PickupCost", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "DeliveryCost", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "ServiceLevel", c => c.String());
            AddColumn("dbo.Quotes", "ServiceLevelCost", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "ShipmentCost", c => c.Int(nullable: false));
            DropColumn("dbo.Quotes", "Pieces");
            DropColumn("dbo.Quotes", "PickupZip");
            DropColumn("dbo.Quotes", "PickupDate");
            DropColumn("dbo.Quotes", "PickupTimeWindow");
            DropColumn("dbo.Quotes", "DeliveryDate");
            DropColumn("dbo.Quotes", "DeliveryZip");
            DropColumn("dbo.Quotes", "DeliveryTimeWindow");
            DropColumn("dbo.Quotes", "QuotedPrice");
            DropColumn("dbo.Quotes", "PackagesSerialized");
            DropTable("dbo.Packages");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Quotes", "PackagesSerialized", c => c.String());
            AddColumn("dbo.Quotes", "QuotedPrice", c => c.Int(nullable: false));
            AddColumn("dbo.Quotes", "DeliveryTimeWindow", c => c.String());
            AddColumn("dbo.Quotes", "DeliveryZip", c => c.String());
            AddColumn("dbo.Quotes", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quotes", "PickupTimeWindow", c => c.String());
            AddColumn("dbo.Quotes", "PickupDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quotes", "PickupZip", c => c.String());
            AddColumn("dbo.Quotes", "Pieces", c => c.Int(nullable: false));
            DropColumn("dbo.Quotes", "ShipmentCost");
            DropColumn("dbo.Quotes", "ServiceLevelCost");
            DropColumn("dbo.Quotes", "ServiceLevel");
            DropColumn("dbo.Quotes", "DeliveryCost");
            DropColumn("dbo.Quotes", "PickupCost");
            DropColumn("dbo.Quotes", "PackageCost");
            CreateIndex("dbo.Packages", "Quote_Id");
            AddForeignKey("dbo.Packages", "Quote_Id", "dbo.Quotes", "Id");
        }
    }
}
