namespace LabMaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SincronizacionFinal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reservas", "UsuarioID", c => c.String(nullable: false));
        }
    }
}
