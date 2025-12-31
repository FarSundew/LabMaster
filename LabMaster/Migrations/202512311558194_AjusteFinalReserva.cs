namespace LabMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AjusteFinalReserva : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservas", "UsuarioID");
            AddForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "UsuarioID", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "UsuarioID" });
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String());
        }
    }
}
