using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleWebApi.Migrations
{
    /// <inheritdoc />
    public partial class fixManagers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [dbo].[Users] SET   [ManagerId] = 1 WHERE [ManagerId] is not null");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
