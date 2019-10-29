namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reloadingNuke : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WorkEmail = c.String(),
                        Customers_Id = c.Int(),
                        Employees_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customers_Id)
                .ForeignKey("dbo.Employees", t => t.Employees_Id)
                .Index(t => t.Customers_Id)
                .Index(t => t.Employees_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BusinessName = c.String(),
                        StreetAddress = c.String(),
                        StreetAddress2 = c.String(),
                        ZipCode = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        WorkEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageCost = c.Double(nullable: false),
                        PickupCost = c.Double(nullable: false),
                        DeliveryCost = c.Double(nullable: false),
                        ServiceLevelCost = c.Double(nullable: false),
                        ServiceTypeCost = c.Double(nullable: false),
                        ServiceAreaCost = c.Double(nullable: false),
                        FuelSurcharge = c.Double(nullable: false),
                        ShipmentCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeliveryDate = c.DateTime(nullable: false),
                        StreetAddress = c.String(),
                        DeliveryZip = c.String(),
                        DeliveryTimeWindow = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeliveryAddressId = c.Int(),
                        PickupAddressId = c.Int(),
                        ServiceTypeId = c.Int(),
                        ServiceLevelId = c.Int(),
                        ServiceAreaId = c.Int(),
                        QuoteId = c.Int(),
                        Package_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryAddresses", t => t.DeliveryAddressId)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .ForeignKey("dbo.PickupAddresses", t => t.PickupAddressId)
                .ForeignKey("dbo.Quotes", t => t.QuoteId)
                .ForeignKey("dbo.ServiceAreas", t => t.ServiceAreaId)
                .ForeignKey("dbo.ServiceLevels", t => t.ServiceLevelId)
                .ForeignKey("dbo.ServiceTypes", t => t.ServiceTypeId)
                .Index(t => t.DeliveryAddressId)
                .Index(t => t.PickupAddressId)
                .Index(t => t.ServiceTypeId)
                .Index(t => t.ServiceLevelId)
                .Index(t => t.ServiceAreaId)
                .Index(t => t.QuoteId)
                .Index(t => t.Package_Id);
            
            CreateTable(
                "dbo.Packages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PieceCount = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        Length = c.Int(nullable: false),
                        Width = c.Int(nullable: false),
                        Height = c.Int(nullable: false),
                        DimensionalWeight = c.Int(nullable: false),
                        DimFactor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PickupAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        StreetAddress = c.String(),
                        PickupTimeWindow = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Distance = c.Double(nullable: false),
                        CostOfServiceAreaPoint = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfServiceLevel = c.String(),
                        CostOfServiceLevel = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NameOfService = c.String(),
                        CostOfService = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Shipments", "ServiceTypeId", "dbo.ServiceTypes");
            DropForeignKey("dbo.Shipments", "ServiceLevelId", "dbo.ServiceLevels");
            DropForeignKey("dbo.Shipments", "ServiceAreaId", "dbo.ServiceAreas");
            DropForeignKey("dbo.Shipments", "QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.Shipments", "PickupAddressId", "dbo.PickupAddresses");
            DropForeignKey("dbo.Shipments", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Shipments", "DeliveryAddressId", "dbo.DeliveryAddresses");
            DropForeignKey("dbo.Admins", "Employees_Id", "dbo.Employees");
            DropForeignKey("dbo.Admins", "Customers_Id", "dbo.Customers");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Shipments", new[] { "Package_Id" });
            DropIndex("dbo.Shipments", new[] { "QuoteId" });
            DropIndex("dbo.Shipments", new[] { "ServiceAreaId" });
            DropIndex("dbo.Shipments", new[] { "ServiceLevelId" });
            DropIndex("dbo.Shipments", new[] { "ServiceTypeId" });
            DropIndex("dbo.Shipments", new[] { "PickupAddressId" });
            DropIndex("dbo.Shipments", new[] { "DeliveryAddressId" });
            DropIndex("dbo.Admins", new[] { "Employees_Id" });
            DropIndex("dbo.Admins", new[] { "Customers_Id" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.ServiceLevels");
            DropTable("dbo.ServiceAreas");
            DropTable("dbo.PickupAddresses");
            DropTable("dbo.Packages");
            DropTable("dbo.Shipments");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Quotes");
            DropTable("dbo.Employees");
            DropTable("dbo.Customers");
            DropTable("dbo.Admins");
        }
    }
}
