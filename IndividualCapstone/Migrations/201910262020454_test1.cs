namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceAreas", "CostOfServiceAreaPoint", c => c.Double(nullable: false));
            AddColumn("dbo.ServiceLevels", "CostOfServiceLevel", c => c.Double(nullable: false));
            AddColumn("dbo.ServiceTypes", "NameOfServiceType", c => c.String());
            AddColumn("dbo.ServiceTypes", "CostOfServiceType", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "PackageCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "PickupCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "DeliveryCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceLevelCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceTypeCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceAreaCost", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "FuelSurcharge", c => c.Double(nullable: false));
            AlterColumn("dbo.Quotes", "ShipmentCost", c => c.Double(nullable: false));
            AlterColumn("dbo.ServiceAreas", "Distance", c => c.Double(nullable: false));
            DropColumn("dbo.ServiceAreas", "Rate");
            DropColumn("dbo.ServiceLevels", "SameDay");
            DropColumn("dbo.ServiceLevels", "NextDay");
            DropColumn("dbo.ServiceLevels", "TwoDay");
            DropColumn("dbo.ServiceLevels", "ThreeDay");
            DropColumn("dbo.ServiceLevels", "Economy");
            DropColumn("dbo.ServiceTypes", "DockToDock");
            DropColumn("dbo.ServiceTypes", "Basic");
            DropColumn("dbo.ServiceTypes", "OneMan");
            DropColumn("dbo.ServiceTypes", "TwoMan");
            DropColumn("dbo.ServiceTypes", "Premier");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceTypes", "Premier", c => c.String());
            AddColumn("dbo.ServiceTypes", "TwoMan", c => c.String());
            AddColumn("dbo.ServiceTypes", "OneMan", c => c.String());
            AddColumn("dbo.ServiceTypes", "Basic", c => c.String());
            AddColumn("dbo.ServiceTypes", "DockToDock", c => c.String());
            AddColumn("dbo.ServiceLevels", "Economy", c => c.String());
            AddColumn("dbo.ServiceLevels", "ThreeDay", c => c.String());
            AddColumn("dbo.ServiceLevels", "TwoDay", c => c.String());
            AddColumn("dbo.ServiceLevels", "NextDay", c => c.String());
            AddColumn("dbo.ServiceLevels", "SameDay", c => c.String());
            AddColumn("dbo.ServiceAreas", "Rate", c => c.Int(nullable: false));
            AlterColumn("dbo.ServiceAreas", "Distance", c => c.Int(nullable: false));
            AlterColumn("dbo.Quotes", "ShipmentCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "FuelSurcharge", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceAreaCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceTypeCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "ServiceLevelCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "DeliveryCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "PickupCost", c => c.Single(nullable: false));
            AlterColumn("dbo.Quotes", "PackageCost", c => c.Single(nullable: false));
            DropColumn("dbo.ServiceTypes", "CostOfServiceType");
            DropColumn("dbo.ServiceTypes", "NameOfServiceType");
            DropColumn("dbo.ServiceLevels", "CostOfServiceLevel");
            DropColumn("dbo.ServiceAreas", "CostOfServiceAreaPoint");
        }
    }
}
