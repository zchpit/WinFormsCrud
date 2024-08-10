namespace WinFormsCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Header = c.String(),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastModifiedBy = c.Int(nullable: false),
                        LastModifiedDate = c.DateTime(nullable: false),
                        DeletedBy = c.Int(nullable: false),
                        DeletedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserCases",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        CaseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.CaseId })
                .ForeignKey("dbo.Cases", t => t.CaseId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.CaseId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        UserRole = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCases", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserCases", "CaseId", "dbo.Cases");
            DropIndex("dbo.UserCases", new[] { "CaseId" });
            DropIndex("dbo.UserCases", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserCases");
            DropTable("dbo.Cases");
        }
    }
}
