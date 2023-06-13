using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystem.Infrastructure.Migrations
{
    public partial class SP_GetAllProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetAllProducts
                                AS
                                BEGIN 
                                SELECT P.Id, null EncryptedtId, C.CategoryName, P.NAME ProductName, P.Unit, P.Price, P.IdealQuantity, P.IsActive FROM PRODUCTS P
                                INNER JOIN CATEGORIES C ON P.CATEGORYID = C.ID;     
                                END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[SP_GetAllProducts]");
        }
    }
}
