namespace WinFormsCrud.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setUserCases : DbMigration
    {
        public override void Up()
        {
            Sql("DELETE FROM [dbo].[Cases] WHERE CreatedBy = 3");

            Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 1, [Id] FROM [dbo].[Cases] where CreatedBy = 1)");
            Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 2, [Id] FROM [dbo].[Cases] where CreatedBy = 2)");
            Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 4, [Id] FROM [dbo].[Cases] where CreatedBy = 4)");
        }
        
        public override void Down()
        {
        }
    }
}
