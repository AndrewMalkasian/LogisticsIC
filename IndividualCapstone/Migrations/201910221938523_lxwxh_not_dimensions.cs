namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lxwxh_not_dimensions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CustomerQuotes", "Length", c => c.String());
            AddColumn("dbo.CustomerQuotes", "Width", c => c.String());
            AddColumn("dbo.CustomerQuotes", "Height", c => c.String());
            DropColumn("dbo.CustomerQuotes", "Dimensions");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomerQuotes", "Dimensions", c => c.String());
            DropColumn("dbo.CustomerQuotes", "Height");
            DropColumn("dbo.CustomerQuotes", "Width");
            DropColumn("dbo.CustomerQuotes", "Length");
        }
    }
}
