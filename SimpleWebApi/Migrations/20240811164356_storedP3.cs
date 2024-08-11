using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class storedP3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            DROP PROCEDURE [dbo].[SimpleReport]
            GO

            CREATE PROCEDURE SimpleReport 
            @ManagerId int
            AS
            BEGIN

	            SET NOCOUNT ON;

	            SELECT 
	            u.[Name] as 'Name', count(c.Id) as 'NumOfCases', CAST(MONTH([CreateDate]) AS VARCHAR(2)) + '-' + CAST(YEAR([CreateDate]) AS VARCHAR(4)) AS 'Month' 
	              FROM [dbo].[Users] AS u
	              inner join [dbo].[UserCases] AS uc ON u.Id = uc.UserId
	              inner join [dbo].[Cases] AS c ON c.Id = uc.CaseId
	              WHERE ManagerId = @ManagerId and c.IsDeleted != 1
	             GROUP BY  u.[Name],
	            CAST(MONTH([CreateDate]) AS VARCHAR(2)) + '-' + CAST(YEAR([CreateDate]) AS VARCHAR(4))
 
            END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
