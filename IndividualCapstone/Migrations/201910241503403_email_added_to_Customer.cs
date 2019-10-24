namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class email_added_to_Customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "EmailAddress", c => c.String());
            DropColumn("dbo.Customers", "ContactInfo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "ContactInfo", c => c.String());
            DropColumn("dbo.Customers", "EmailAddress");
        }
    }
}
