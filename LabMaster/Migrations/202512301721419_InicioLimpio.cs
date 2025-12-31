namespace LabMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InicioLimpio : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carreras",
                c => new
                    {
                        CarreraID = c.Int(nullable: false, identity: true),
                        NombreCarrera = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CarreraID);
            
            CreateTable(
                "dbo.Laboratorios",
                c => new
                    {
                        LabID = c.Int(nullable: false, identity: true),
                        NombreLab = c.String(nullable: false),
                        Capacidad = c.Int(nullable: false),
                        CarreraID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LabID)
                .ForeignKey("dbo.Carreras", t => t.CarreraID, cascadeDelete: true)
                .Index(t => t.CarreraID);
            
            CreateTable(
                "dbo.Insumoes",
                c => new
                    {
                        InsumoID = c.Int(nullable: false, identity: true),
                        NombreInsumo = c.String(nullable: false),
                        StockTotal = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InsumoID);
            
            CreateTable(
                "dbo.Reservas",
                c => new
                    {
                        ReservaID = c.Int(nullable: false, identity: true),
                        UsuarioID = c.String(nullable: false),
                        LabID = c.Int(nullable: false),
                        FechaReserva = c.DateTime(nullable: false),
                        HoraInicio = c.String(nullable: false),
                        Motivo = c.String(),
                    })
                .PrimaryKey(t => t.ReservaID)
                .ForeignKey("dbo.Laboratorios", t => t.LabID, cascadeDelete: true)
                .Index(t => t.LabID);
            
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
            DropForeignKey("dbo.Reservas", "LabID", "dbo.Laboratorios");
            DropForeignKey("dbo.Laboratorios", "CarreraID", "dbo.Carreras");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Reservas", new[] { "LabID" });
            DropIndex("dbo.Laboratorios", new[] { "CarreraID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reservas");
            DropTable("dbo.Insumoes");
            DropTable("dbo.Laboratorios");
            DropTable("dbo.Carreras");
        }
    }
}
