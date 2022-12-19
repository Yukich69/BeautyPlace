namespace Boost.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class YK : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(nullable: false, maxLength: 30, unicode: false),
                        Email = c.String(maxLength: 30, unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visit",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Client_Id = c.Int(),
                        Employee_Id = c.Int(),
                        Service_Id = c.Int(),
                        DateTime = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.Service_Id)
                .ForeignKey("dbo.Employees", t => t.Employee_Id)
                .ForeignKey("dbo.Clients", t => t.Client_Id)
                .Index(t => t.Client_Id)
                .Index(t => t.Employee_Id)
                .Index(t => t.Service_Id);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Positions_Id = c.Int(),
                        FirstName = c.String(nullable: false, maxLength: 20),
                        LastName = c.String(nullable: false, maxLength: 20),
                        Address = c.String(maxLength: 30, unicode: false),
                        Phone = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Positions", t => t.Positions_Id)
                .Index(t => t.Positions_Id);
            
            CreateTable(
                "dbo.Positions",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NamePositions = c.String(maxLength: 30, unicode: false),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        NameGroup = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Group_Id = c.Int(),
                        NameService = c.String(maxLength: 30, unicode: false),
                        Price = c.Decimal(storeType: "money"),
                        Description = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.Group_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Visit", "Client_Id", "dbo.Clients");
            DropForeignKey("dbo.Visit", "Employee_Id", "dbo.Employees");
            DropForeignKey("dbo.Services", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Visit", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.Positions", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.Employees", "Positions_Id", "dbo.Positions");
            DropIndex("dbo.Services", new[] { "Group_Id" });
            DropIndex("dbo.Positions", new[] { "Group_Id" });
            DropIndex("dbo.Employees", new[] { "Positions_Id" });
            DropIndex("dbo.Visit", new[] { "Service_Id" });
            DropIndex("dbo.Visit", new[] { "Employee_Id" });
            DropIndex("dbo.Visit", new[] { "Client_Id" });
            DropTable("dbo.Services");
            DropTable("dbo.Groups");
            DropTable("dbo.Positions");
            DropTable("dbo.Employees");
            DropTable("dbo.Visit");
            DropTable("dbo.Clients");
        }
    }
}
