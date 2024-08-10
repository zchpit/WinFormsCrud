namespace WinFormsCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetCaseDeletedNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Cases", "DeletedBy", c => c.Int());
            AlterColumn("dbo.Cases", "DeletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Cases", "DeletedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Cases", "DeletedBy", c => c.Int(nullable: false));
        }
    }
}
