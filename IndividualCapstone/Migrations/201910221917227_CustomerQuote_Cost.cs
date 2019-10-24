namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerQuote_Cost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerQuotes", "DeliveryDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CustomerQuotes", "QuotePrice", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerQuotes", "QuotePrice");
            DropColumn("dbo.CustomerQuotes", "DeliveryDate");
        }
    }
}
