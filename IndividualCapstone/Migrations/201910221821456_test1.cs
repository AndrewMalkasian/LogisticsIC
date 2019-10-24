namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pieces = c.String(),
                        Weight = c.String(),
                        Dimensions = c.String(),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        PickupWindowStart = c.String(),
                        PickupWindowEnd = c.String(),
                        DeliveryZip = c.String(),
                        DeliveryWindowStart = c.String(),
                        DeliveryWindowEnd = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerQuotes");
        }
    }
}
