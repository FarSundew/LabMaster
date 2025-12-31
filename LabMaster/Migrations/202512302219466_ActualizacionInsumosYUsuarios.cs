namespace LabMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ActualizacionInsumosYUsuarios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Insumoes", "Stock", c => c.Int(nullable: false));
            AlterColumn("dbo.Insumoes", "NombreInsumo", c => c.String());
            DropColumn("dbo.Insumoes", "StockTotal");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Insumoes", "StockTotal", c => c.Int(nullable: false));
            AlterColumn("dbo.Insumoes", "NombreInsumo", c => c.String(nullable: false));
            DropColumn("dbo.Insumoes", "Stock");
        }
    }
}
