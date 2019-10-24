namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedQuoteId_QuoteFKtoShipment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shipments", "QuoteId", c => c.Int());
            CreateIndex("dbo.Shipments", "QuoteId");
            AddForeignKey("dbo.Shipments", "QuoteId", "dbo.Quotes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Shipments", "QuoteId", "dbo.Quotes");
            DropIndex("dbo.Shipments", new[] { "QuoteId" });
            DropColumn("dbo.Shipments", "QuoteId");
        }
    }
}
