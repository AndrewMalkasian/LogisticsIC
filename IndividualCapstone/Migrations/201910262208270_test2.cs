namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Packages", "PieceCount", c => c.Int(nullable: false));
            AddColumn("dbo.PickupAddresses", "StreetAddress", c => c.String());
            DropColumn("dbo.DeliveryAddresses", "StreetAddressTwo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryAddresses", "StreetAddressTwo", c => c.String());
            DropColumn("dbo.PickupAddresses", "StreetAddress");
            DropColumn("dbo.Packages", "PieceCount");
        }
    }
}
