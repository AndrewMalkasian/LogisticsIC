namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class correctedtypesCustomerQuote : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerQuotes", "Pieces", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "Weight", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "Length", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "Width", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "Height", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "Cost", c => c.Int(nullable: false));
            AlterColumn("dbo.CustomerQuotes", "QuotePrice", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerQuotes", "QuotePrice", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Cost", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Height", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Width", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Length", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Weight", c => c.String());
            AlterColumn("dbo.CustomerQuotes", "Pieces", c => c.String());
        }
    }
}
