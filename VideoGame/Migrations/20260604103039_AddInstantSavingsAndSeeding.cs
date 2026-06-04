using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGame.Migrations
{
    /// <inheritdoc />
    public partial class AddInstantSavingsAndSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstantSavings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bvn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherMaidenName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateOfOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lga = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LgaDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReligionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocumentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateOfResidence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReasonForRejection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchCode = table.Column<int>(type: "int", nullable: false),
                    LedCode = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DocUpload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefDocUpload = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Permissions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RcNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountCurreny = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PreferedBranch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateofRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScumlNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpectedMonthlyTurnover = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PurposeofAccount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DirectorBVN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DirectorName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryofOrigin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalGovernment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextofKinName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NextofKinPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationShip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCard = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportPhotoGraph = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdentificationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateIssue = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlaceIssue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorporateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdCardExt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportPhotoGraphExt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SignatureExt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryofOriginDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocalGovernmentDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MartialStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateOfOriginDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DircUpload = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstantSavings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstantSavings");
        }
    }
}
