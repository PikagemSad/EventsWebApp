using Microsoft.EntityFrameworkCore.Migrations;

public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Events",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                   .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: false),
                Date = table.Column<DateTime>(nullable: false), 
                Location = table.Column<string>(nullable: true),
                Description = table.Column<string>(nullable: true),
                ImagePath = table.Column<string>(nullable: true),
                MaxParticipants = table.Column<int>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Events", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Participants",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(nullable: false),
                Email = table.Column<string>(nullable: false),
                EventId = table.Column<int>(nullable: false),
                RegistrationDate = table.Column<DateTime>(nullable: false),
                BirthdayDate = table.Column<DateTime>(nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Participants", x => x.Id);
                table.ForeignKey(
                    name: "FK_Participants_Events_EventId",
                    column: x => x.EventId,
                    principalTable: "Events",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade); 
            });

        
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(name: "Participants");

        migrationBuilder.DropTable(name: "Events");
    }
}
