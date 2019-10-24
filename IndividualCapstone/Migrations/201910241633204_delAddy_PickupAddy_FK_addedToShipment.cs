namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delAddy_PickupAddy_FK_addedToShipment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DeliveryAddresses", "QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.DeliveryAddresses", "ShipmentId", "dbo.Shipments");
            DropForeignKey("dbo.PickupAddresses", "QuoteId", "dbo.Quotes");
            DropIndex("dbo.DeliveryAddresses", new[] { "QuoteId" });
            DropIndex("dbo.DeliveryAddresses", new[] { "ShipmentId" });
            DropIndex("dbo.PickupAddresses", new[] { "QuoteId" });
            RenameColumn(table: "dbo.Shipments", name: "DeliveryAddress_Id", newName: "DeliveryAddressId");
            RenameIndex(table: "dbo.Shipments", name: "IX_DeliveryAddress_Id", newName: "IX_DeliveryAddressId");
            AddColumn("dbo.Shipments", "PickupAddressId", c => c.Int());
            CreateIndex("dbo.Shipments", "PickupAddressId");
            AddForeignKey("dbo.Shipments", "PickupAddressId", "dbo.PickupAddresses", "Id");
            DropColumn("dbo.DeliveryAddresses", "QuoteId");
            DropColumn("dbo.DeliveryAddresses", "ShipmentId");
            DropColumn("dbo.PickupAddresses", "QuoteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PickupAddresses", "QuoteId", c => c.Int());
            AddColumn("dbo.DeliveryAddresses", "ShipmentId", c => c.Int());
            AddColumn("dbo.DeliveryAddresses", "QuoteId", c => c.Int());
            DropForeignKey("dbo.Shipments", "PickupAddressId", "dbo.PickupAddresses");
            DropIndex("dbo.Shipments", new[] { "PickupAddressId" });
            DropColumn("dbo.Shipments", "PickupAddressId");
            RenameIndex(table: "dbo.Shipments", name: "IX_DeliveryAddressId", newName: "IX_DeliveryAddress_Id");
            RenameColumn(table: "dbo.Shipments", name: "DeliveryAddressId", newName: "DeliveryAddress_Id");
            CreateIndex("dbo.PickupAddresses", "QuoteId");
            CreateIndex("dbo.DeliveryAddresses", "ShipmentId");
            CreateIndex("dbo.DeliveryAddresses", "QuoteId");
            AddForeignKey("dbo.PickupAddresses", "QuoteId", "dbo.Quotes", "Id");
            AddForeignKey("dbo.DeliveryAddresses", "ShipmentId", "dbo.Shipments", "Id");
            AddForeignKey("dbo.DeliveryAddresses", "QuoteId", "dbo.Quotes", "Id");
        }
    }
}
