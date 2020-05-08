using Microsoft.EntityFrameworkCore.Migrations;

namespace gym.Migrations
{
    public partial class Second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MobileNumber",
                table: "User",
                nullable: true,
                oldClrType: typeof(short));

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "Gym",
                nullable: true,
                oldClrType: typeof(short));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "MobileNumber",
                table: "User",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "ContactNumber",
                table: "Gym",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
