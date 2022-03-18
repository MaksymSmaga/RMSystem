using Microsoft.EntityFrameworkCore.Migrations;

namespace RMSystem.Services.Migrations
{
    public partial class CodeFirstspGetRiskById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Присваиваем строке текстовку комманды SQL Stored Procedure
            string procedure = @"Create procedure spGetRiskById
                                    @Id int
                                    as 
                                    Begin
	                                SELECT * FROM Risks
	                                WHERE Id = @Id
                                    End";
            // Выполняем комманду.
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Присваиваем строке текстовку комманды удаления SQL Stored Procedure
            string procedure = @"Drop procedure spGetRiskById";

            // Выполняем комманду.
            migrationBuilder.Sql(procedure);
        }
    }
}
