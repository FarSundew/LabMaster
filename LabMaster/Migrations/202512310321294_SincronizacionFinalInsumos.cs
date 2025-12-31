namespace LabMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SincronizacionFinalInsumos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insumoes", "CarreraID", c => c.Int(nullable: false));
            AlterColumn("dbo.Insumoes", "NombreInsumo", c => c.String(nullable: false));
            CreateIndex("dbo.Insumoes", "CarreraID");
            AddForeignKey("dbo.Insumoes", "CarreraID", "dbo.Carreras", "CarreraID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Insumoes", "CarreraID", "dbo.Carreras");
            DropIndex("dbo.Insumoes", new[] { "CarreraID" });
            AlterColumn("dbo.Insumoes", "NombreInsumo", c => c.String());
            DropColumn("dbo.Insumoes", "CarreraID");
        }
    }
}
