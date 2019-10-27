namespace IndividualCapstone.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BusinessName = c.String(),
                        BusinessPhoneNumber = c.String(),
                        StreetAddress = c.String(),
                        StreetAddress2 = c.String(),
                        ZipCode = c.String(),
                        WorkEmail = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageCost = c.Single(nullable: false),
                        PickupCost = c.Single(nullable: false),
                        DeliveryCost = c.Single(nullable: false),
                        ServiceLevelCost = c.Single(nullable: false),
                        ServiceTypeCost = c.Single(nullable: false),
                        ServiceAreaCost = c.Single(nullable: false),
                        FuelSurcharge = c.Single(nullable: false),
                        ShipmentCost = c.Single(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BusinessName = c.String(),
                        StreetAddress = c.String(),
                        StreetAddress2 = c.String(),
                        ZipCode = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DeliveryAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DeliveryDate = c.DateTime(nullable: false),
                        StreetAddress = c.String(),
                        StreetAddressTwo = c.String(),
                        DeliveryZip = c.String(),
                        DeliveryTimeWindow = c.String(),
                        ShipmentSerialized = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Shipments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Pieces = c.Int(nullable: false),
                        AddressInput = c.String(),
                        DeliveryAddressId = c.Int(),
                        PickupAddressId = c.Int(),
                        ServiceTypeId = c.Int(),
                        ServiceLevelId = c.Int(),
                        ServiceAreaId = c.Int(),
                        QuoteId = c.Int(),
                        Package_Id = c.Int(),
                        PickupAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DeliveryAddresses", t => t.DeliveryAddressId)
                .ForeignKey("dbo.Packages", t => t.Package_Id)
                .ForeignKey("dbo.PickupAddresses", t => t.PickupAddress_Id)
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
                .Index(t => t.Package_Id)
                .Index(t => t.PickupAddress_Id);
            
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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PickupAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PickupZip = c.String(),
                        PickupDate = c.DateTime(nullable: false),
                        PickupTimeWindow = c.String(),
                        ShipmentId = c.Int(),
                        ShipmentSerialized = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shipments", t => t.ShipmentId)
                .Index(t => t.ShipmentId);
            
            CreateTable(
                "dbo.ServiceAreas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Distance = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceLevels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfServiceLevel = c.String(),
                        SameDay = c.String(),
                        NextDay = c.String(),
                        TwoDay = c.String(),
                        ThreeDay = c.String(),
                        Economy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DockToDock = c.String(),
                        Basic = c.String(),
                        OneMan = c.String(),
                        TwoMan = c.String(),
                        Premier = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.Shipments", "PickupAddress_Id", "dbo.PickupAddresses");
            DropForeignKey("dbo.PickupAddresses", "ShipmentId", "dbo.Shipments");
            DropForeignKey("dbo.Shipments", "Package_Id", "dbo.Packages");
            DropForeignKey("dbo.Shipments", "DeliveryAddressId", "dbo.DeliveryAddresses");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.PickupAddresses", new[] { "ShipmentId" });
            DropIndex("dbo.Shipments", new[] { "PickupAddress_Id" });
            DropIndex("dbo.Shipments", new[] { "Package_Id" });
            DropIndex("dbo.Shipments", new[] { "QuoteId" });
            DropIndex("dbo.Shipments", new[] { "ServiceAreaId" });
            DropIndex("dbo.Shipments", new[] { "ServiceLevelId" });
            DropIndex("dbo.Shipments", new[] { "ServiceTypeId" });
            DropIndex("dbo.Shipments", new[] { "PickupAddressId" });
            DropIndex("dbo.Shipments", new[] { "DeliveryAddressId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employees");
            DropTable("dbo.ServiceTypes");
            DropTable("dbo.ServiceLevels");
            DropTable("dbo.ServiceAreas");
            DropTable("dbo.PickupAddresses");
            DropTable("dbo.Packages");
            DropTable("dbo.Shipments");
            DropTable("dbo.DeliveryAddresses");
            DropTable("dbo.Customers");
            DropTable("dbo.Quotes");
            DropTable("dbo.Admins");
        }
    }
}
