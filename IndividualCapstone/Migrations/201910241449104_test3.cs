namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryZip = c.String(),
                        DeliveryTimeWindow = c.String(),
                        QuoteId = c.Int(),
                        ShipmentId = c.Int(),
                        PackagesSerialized = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.QuoteId)
                .ForeignKey("dbo.Shipments", t => t.ShipmentId)
                .Index(t => t.QuoteId)
                .Index(t => t.ShipmentId);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pieces = c.Int(nullable: false),
                        PackagesSerialized = c.String(),
                        Shipment_Id = c.Int(),
                        DeliveryAddress_Id = c.Int(),
                        PickupAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shipments", t => t.Shipment_Id)
                .ForeignKey("dbo.DeliveryAddresses", t => t.DeliveryAddress_Id)
                .ForeignKey("dbo.PickupAddresses", t => t.PickupAddress_Id)
                .Index(t => t.Shipment_Id)
                .Index(t => t.DeliveryAddress_Id)
                .Index(t => t.PickupAddress_Id);
            
            CreateTable(
                "dbo.PickupAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        PickupTimeWindow = c.String(),
                        QuoteId = c.Int(),
                        ShipmentId = c.Int(),
                        PackagesSerialized = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.QuoteId)
                .ForeignKey("dbo.Shipments", t => t.ShipmentId)
                .Index(t => t.QuoteId)
                .Index(t => t.ShipmentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "PickupAddress_Id", "dbo.PickupAddresses");
            DropForeignKey("dbo.PickupAddresses", "ShipmentId", "dbo.Shipments");
            DropForeignKey("dbo.PickupAddresses", "QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.Shipments", "DeliveryAddress_Id", "dbo.DeliveryAddresses");
            DropForeignKey("dbo.DeliveryAddresses", "ShipmentId", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "Shipment_Id", "dbo.Shipments");
            DropForeignKey("dbo.DeliveryAddresses", "QuoteId", "dbo.Quotes");
            DropIndex("dbo.PickupAddresses", new[] { "ShipmentId" });
            DropIndex("dbo.PickupAddresses", new[] { "QuoteId" });
            DropIndex("dbo.Shipments", new[] { "PickupAddress_Id" });
            DropIndex("dbo.Shipments", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.Shipments", new[] { "Shipment_Id" });
            DropIndex("dbo.DeliveryAddresses", new[] { "ShipmentId" });
            DropIndex("dbo.DeliveryAddresses", new[] { "QuoteId" });
            DropTable("dbo.PickupAddresses");
            DropTable("dbo.Shipments");
            DropTable("dbo.DeliveryAddresses");
        }
    }
}
