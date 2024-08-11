using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class setInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Name],[Email] ,[Password],[UserRole],[IsActive])VALUES ('manager', 'manager@test.pl', '0y2hZJngk432qjdyUi11mg==' ,2 ,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Name],[Email],[Password],[UserRole],[IsActive]) VALUES('test', 'user@test.pl' ,'6wDOrMcwJgr4EcGN49lJvA==',1,1)");

            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Name],[Email],[Password],[UserRole],[IsActive]) VALUES('test2', 'user2@test.pl' ,'6wDOrMcwJgr4EcGN49lJvA==',1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case1'  ,'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.' ,1 ,0 ,1 ,'2018-10-25' ,1 ,'2018-10-25' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case2'  ,'Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.' ,3 ,0 ,1 ,'2020-10-25' ,1 ,'2020-10-25' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case3'  ,'Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur' ,1 ,0 ,1 ,'2021-10-28' ,1 ,'2021-10-28' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User3Case1'  ,'Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.' ,4 ,0 ,3 ,'2022-08-25' ,1 ,'2022-08-25' ,null ,null)");

            migrationBuilder.Sql("UPDATE [dbo].[Users] SET [ManagerId] = 2 WHERE UserRole = 1");

            migrationBuilder.Sql("DELETE FROM [dbo].[Cases] WHERE CreatedBy = 3");
            migrationBuilder.Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 1, [Id] FROM [dbo].[Cases] where CreatedBy = 1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 2, [Id] FROM [dbo].[Cases] where CreatedBy = 2)");
            migrationBuilder.Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 4, [Id] FROM [dbo].[Cases] where CreatedBy = 4)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
