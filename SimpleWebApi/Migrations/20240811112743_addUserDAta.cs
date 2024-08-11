using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addUserDAta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[Users] ([Name],[Email],[Password],[UserRole],[IsActive]) VALUES('test2', 'user2@test.pl' ,'6wDOrMcwJgr4EcGN49lJvA==',1,1)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case1'  ,'Lorem ipsum .' ,1 ,0 ,2 ,'2018-10-25' ,2 ,'2018-10-25' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case2'  ,'Ut enim ad minim veniam,' ,3 ,0 ,2 ,'2020-10-25' ,2 ,'2020-10-25' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User1Case3'  ,'Duis aute irure dolor in' ,1 ,0 ,2 ,'2021-10-28' ,2 ,'2021-10-28' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[Cases]  ([Header] ,[Description] ,[Priority] ,[IsDeleted],[CreatedBy],[CreateDate] ,[LastModifiedBy] ,[LastModifiedDate] ,[DeletedBy]  ,[DeletedDate])  VALUES ('User3Case1'  ,'Excepteur sint occaecat' ,4 ,0 ,2 ,'2022-08-25' ,2 ,'2022-08-25' ,null ,null)");
            migrationBuilder.Sql("INSERT INTO [dbo].[UserCases]  ([UserId] ,[CaseId])   (SELECT 2, [Id] FROM [dbo].[Cases] where CreatedBy = 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
