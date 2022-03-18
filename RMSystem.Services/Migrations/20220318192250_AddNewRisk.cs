using Microsoft.EntityFrameworkCore.Migrations;

namespace RMSystem.Services.Migrations
{
    public partial class AddNewRisk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spAddNewRisk
                                                    @Iso int,  
                                                    @PhotoPath nvarchar(100),
                                                    @Process int,   
                                                    @Deverity int,  
                                                    @Likelihood int, 
                                                    @Treatment int,  
                                                    @Category int,   
                                                    @Description nvarchar(100),
                                                    @Cause int,  
                                                    @Source int,  
                                                    @PhaseManagement int,   
                                                    @Equipment int          
                                        as
                                        Begin
                                        INSERT INTO Risks (Iso, PhotoPath, Process,   
                                                           Deverity, Likelihood, Treatment, Category,   
                                                           Description, Cause, Source, PhaseManagement,
                                                           Equipment) 
                                                    VALUES (@Iso, @PhotoPath, @Process,   
                                                           @Deverity, @Likelihood, @Treatment, @Category,   
                                                           @Description, @Cause, @Source, @PhaseManagement,
                                                           @Equipment)
                                        End";
             migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure spAddNewRisk";
            migrationBuilder.Sql(procedure);
        }
    }
}
