using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volo.Abp.Desktop.Migrations
{
    public partial class alter_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_m_Settings",
                table: "m_Settings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_m_BackgroundJobs",
                table: "m_BackgroundJobs");

            migrationBuilder.RenameTable(
                name: "m_Settings",
                newName: "AbpSettings");

            migrationBuilder.RenameTable(
                name: "m_BackgroundJobs",
                newName: "AbpBackgroundJobs");

            migrationBuilder.RenameIndex(
                name: "IX_m_Settings_Name_ProviderName_ProviderKey",
                table: "AbpSettings",
                newName: "IX_AbpSettings_Name_ProviderName_ProviderKey");

            migrationBuilder.RenameIndex(
                name: "IX_m_BackgroundJobs_IsAbandoned_NextTryTime",
                table: "AbpBackgroundJobs",
                newName: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpSettings",
                table: "AbpSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbpBackgroundJobs",
                table: "AbpBackgroundJobs",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpSettings",
                table: "AbpSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AbpBackgroundJobs",
                table: "AbpBackgroundJobs");

            migrationBuilder.RenameTable(
                name: "AbpSettings",
                newName: "m_Settings");

            migrationBuilder.RenameTable(
                name: "AbpBackgroundJobs",
                newName: "m_BackgroundJobs");

            migrationBuilder.RenameIndex(
                name: "IX_AbpSettings_Name_ProviderName_ProviderKey",
                table: "m_Settings",
                newName: "IX_m_Settings_Name_ProviderName_ProviderKey");

            migrationBuilder.RenameIndex(
                name: "IX_AbpBackgroundJobs_IsAbandoned_NextTryTime",
                table: "m_BackgroundJobs",
                newName: "IX_m_BackgroundJobs_IsAbandoned_NextTryTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_Settings",
                table: "m_Settings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_m_BackgroundJobs",
                table: "m_BackgroundJobs",
                column: "Id");
        }
    }
}
