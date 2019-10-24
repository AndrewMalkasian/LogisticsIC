namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WorkEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CustomerQuotes", "Cost", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerQuotes", "Cost");
            DropTable("dbo.Employees");
        }
    }
}
