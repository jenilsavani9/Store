using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Entity.Migrations
{
    /// <inheritdoc />
    public partial class useridinstorefeaturetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "StoreFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "StoreFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StoreFeatures_FeatureId",
                table: "StoreFeatures",
                column: "FeatureId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreFeatures_Features_FeatureId",
                table: "StoreFeatures",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "FeatureId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreFeatures_Features_FeatureId",
                table: "StoreFeatures");

            migrationBuilder.DropIndex(
                name: "IX_StoreFeatures_FeatureId",
                table: "StoreFeatures");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "StoreFeatures");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StoreFeatures");
        }
    }
}
