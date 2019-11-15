namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Shipments", "DistanceForPickup", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Shipments", "DistanceForDelivery", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Shipments", "DistanceForFinalMile", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Shipments", "distanceBetweenHubAndAirport", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Shipments", "ShipmentCost", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "PackageCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "PickupCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "DeliveryCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "ServiceLevelCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "ServiceTypeCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "BetweenCitiesCost", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "ServiceAreaCost", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "FuelSurcharge", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Quotes", "ShipmentCost", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Quotes", "ShipmentCost", c => c.Double());
            AlterColumn("dbo.Quotes", "FuelSurcharge", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceAreaCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "BetweenCitiesCost", c => c.Double());
            AlterColumn("dbo.Quotes", "ServiceTypeCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceLevelCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "DeliveryCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "PickupCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "PackageCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Shipments", "ShipmentCost", c => c.Double());
            AlterColumn("dbo.Shipments", "distanceBetweenHubAndAirport", c => c.Double(nullable: false));
            AlterColumn("dbo.Shipments", "DistanceForFinalMile", c => c.Double(nullable: false));
            AlterColumn("dbo.Shipments", "DistanceForDelivery", c => c.Double(nullable: false));
            AlterColumn("dbo.Shipments", "DistanceForPickup", c => c.Double(nullable: false));
        }
    }
}
