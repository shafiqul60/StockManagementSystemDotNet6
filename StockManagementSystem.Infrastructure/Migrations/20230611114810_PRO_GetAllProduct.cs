using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockManagementSystem.Infrastructure.Migrations
{
    public partial class PRO_GetAllProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE PROCEDURE SP_GetAllProduct
                                AS
                                BEGIN 
                                SELECT P.Id, C.CategoryName, P.NAME ProductName, P.Unit, P.Price, P.IdealQuantity, P.IsActive FROM PRODUCTS P
                                INNER JOIN CATEGORIES C ON P.CATEGORYID = C.ID;    
                                END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE [dbo].[SP_GetAllProduct]");
        }
    }
}
