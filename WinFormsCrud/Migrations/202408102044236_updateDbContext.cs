namespace WinFormsCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDbContext : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cases", "ManagerId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cases", "ManagerId");
        }
    }
}
