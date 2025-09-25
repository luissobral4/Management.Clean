using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Management.Clean.Identity.Migrations
{
    /// <inheritdoc />
    public partial class EntityMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1050fbb7-046c-483e-81e4-462e01016c52",
                column: "ConcurrencyStamp",
                value: "8727768b-09e9-4fe1-a0b3-71d924b9e5f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7a57a46-c7e5-433b-9f89-eced9ff84e36",
                column: "ConcurrencyStamp",
                value: "99769e5a-1bfd-4c37-b229-265bb68ce2b2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2fb4e59-c3f1-4114-b892-2b5579e25f5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "441d9870-2d46-47c3-9064-0f813944c2ee", "AQAAAAIAAYagAAAAEHWjbX9DHqvoE+dT4x3mjr7ktxSdFVvNyr2OsVFs3PDhzXRx7kzsikZGcZqZD4g87g==", "f3c1cd5f-9584-4be1-bab9-189c4280be22" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "feeb9cb4-38ca-4fc2-a1a4-a7701c8cfad5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f4e0f29-6d15-46dc-80a0-ab0c813de082", "AQAAAAIAAYagAAAAEMeZcnnoi+yJQrAkA2xS3s0zmiBCCufu4nxA2Aq4pNhJy1KcZicy7KWDTZguDSj1Rw==", "cc1fb969-98e9-4aaf-b766-c80ddb98941c" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1050fbb7-046c-483e-81e4-462e01016c52",
                column: "ConcurrencyStamp",
                value: "b53daafb-e175-47d8-8547-e573aa6c6255");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7a57a46-c7e5-433b-9f89-eced9ff84e36",
                column: "ConcurrencyStamp",
                value: "366f722a-49c1-4400-a7ba-9b2dca2f2b0e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a2fb4e59-c3f1-4114-b892-2b5579e25f5f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2177a697-cafe-4a8f-9129-017d7bc298bd", "AQAAAAIAAYagAAAAEE4aD9ioPtPWkIckjZe22K+Sa2N81hLkwu5/3nHYoECNnB9kufKucfDkqAZGOmtFbQ==", "24b25816-f76d-44e3-9994-4ab33b8fd575" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "feeb9cb4-38ca-4fc2-a1a4-a7701c8cfad5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb885f26-f863-4052-abac-a442569e34ce", "AQAAAAIAAYagAAAAEO0Zd4qKRDLhpXfbTT68Owa9Wo3fYSa3W+24k4Ew/bdij1ZZXLDJETfanpt2qtOJQQ==", "3bf7798b-f6a4-4b09-a50f-02b3e9c65b1c" });
        }
    }
}
