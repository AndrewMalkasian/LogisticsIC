namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test6 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Shipments", new[] { "Shipment_Id" });
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
                        Shipment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Shipment_Id);
            
            DropColumn("dbo.Shipments", "Shipment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Shipments", "Shipment_Id", c => c.Int());
            DropIndex("dbo.Packages", new[] { "Shipment_Id" });
            DropTable("dbo.Packages");
            CreateIndex("dbo.Shipments", "Shipment_Id");
        }
    }
}
