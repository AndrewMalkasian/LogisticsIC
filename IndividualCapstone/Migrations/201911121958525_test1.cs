namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryAddresses", "AddToRoute", c => c.Boolean(nullable: false));
            AddColumn("dbo.PickupAddresses", "AddToRoute", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PickupAddresses", "AddToRoute");
            DropColumn("dbo.DeliveryAddresses", "AddToRoute");
        }
    }
}
