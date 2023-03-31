using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookBird.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishYear = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScheduledFor = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: true),
                    MaxNumberOfVisitors = table.Column<int>(type: "int", nullable: true),
                    CurrentNumberOfVisitors = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meetings_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BooksGenres",
                columns: table => new
                {
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksGenres", x => new { x.BookId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_BooksGenres_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksGenres_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Invitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    InvitationStatus = table.Column<byte>(type: "tinyint", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invitations_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Invitations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MeetingVisitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeetingVisitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeetingVisitors_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MeetingVisitors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "FirstName", "LastName", "ModifiedAt", "Status" },
                values: new object[,]
                {
                    { new Guid("164a4e84-1a4c-422e-ae25-67a98ba4ad91"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Walter", "Isaacson", null, (byte)0 },
                    { new Guid("853eb8af-c5ed-4cb1-b5ef-c0542ea46388"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Dan", "Brown", null, (byte)0 },
                    { new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Daniel", "Whiteson", null, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Description", "ModifiedAt", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("27d571bc-8559-435e-b886-1fc85717ad66"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Fiction is the telling of stories which are not real. More specifically, fiction is an imaginative form of narrative, one of the four basic rhetorical modes.", null, "Fiction", (byte)0 },
                    { new Guid("93105d83-746a-4bb9-aac0-444b8fdc1ec9"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Thrillers are characterized by fast pacing, frequent action, and resourceful heroes who must thwart the plans of more-powerful and better-equipped villains. Literary devices such as suspense, red herrings and cliffhangers are used extensively.", null, "Thriller", (byte)0 },
                    { new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "A biography is a non-fictional account of a person's life. Biographies are written by an author who is not the subject/focus of the book.", null, "Biography", (byte)0 },
                    { new Guid("bf088d71-d618-40d5-a9db-15d65f1aa011"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Adventure fiction is a genre of fiction in which an adventure, an exciting undertaking involving risk and physical danger, forms the main storyline.", null, "Adventure", (byte)0 },
                    { new Guid("faef2407-569b-40bd-95be-fcf54d3293b3"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), "Nonfiction is an account or representation of a subject which is presented as fact. This presentation may be accurate or not; that is, it can give either a true or a false account of the subject in question.", null, "Nonfiction", (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CreatedAt", "ModifiedAt", "Name", "PublishYear", "Rating", "Status" },
                values: new object[,]
                {
                    { new Guid("190d2d7c-b0c3-482e-b77d-162da24ec384"), new Guid("164a4e84-1a4c-422e-ae25-67a98ba4ad91"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Benjamin Franklin: An American Life", 2003, 0.0, (byte)0 },
                    { new Guid("221d28f8-ebaa-43cf-97d1-86a3e8053906"), new Guid("164a4e84-1a4c-422e-ae25-67a98ba4ad91"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Leonardo da Vinci", 2017, 0.0, (byte)0 },
                    { new Guid("3d364a37-b764-427b-a9fd-b7c862d9873d"), new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "We Have No Idea: A Guide to the Unknown Universe", 2017, 0.0, (byte)0 },
                    { new Guid("786718d9-0475-4e3b-a21b-494e9decd86e"), new Guid("853eb8af-c5ed-4cb1-b5ef-c0542ea46388"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Angels & Demons", 2000, 0.0, (byte)0 },
                    { new Guid("bdc6a5b2-bd40-4cf9-8b7d-8e02b65660d4"), new Guid("853eb8af-c5ed-4cb1-b5ef-c0542ea46388"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "The Da Vinci Code", 2003, 0.0, (byte)0 },
                    { new Guid("e215c9f9-6d1e-463a-8b1b-26400f70c719"), new Guid("164a4e84-1a4c-422e-ae25-67a98ba4ad91"), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), null, "Einstein: His Life and Universe", 2007, 0.0, (byte)0 }
                });

            migrationBuilder.InsertData(
                table: "BooksGenres",
                columns: new[] { "BookId", "GenreId" },
                values: new object[,]
                {
                    { new Guid("190d2d7c-b0c3-482e-b77d-162da24ec384"), new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d") },
                    { new Guid("190d2d7c-b0c3-482e-b77d-162da24ec384"), new Guid("faef2407-569b-40bd-95be-fcf54d3293b3") },
                    { new Guid("221d28f8-ebaa-43cf-97d1-86a3e8053906"), new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d") },
                    { new Guid("221d28f8-ebaa-43cf-97d1-86a3e8053906"), new Guid("faef2407-569b-40bd-95be-fcf54d3293b3") },
                    { new Guid("3d364a37-b764-427b-a9fd-b7c862d9873d"), new Guid("faef2407-569b-40bd-95be-fcf54d3293b3") },
                    { new Guid("786718d9-0475-4e3b-a21b-494e9decd86e"), new Guid("27d571bc-8559-435e-b886-1fc85717ad66") },
                    { new Guid("786718d9-0475-4e3b-a21b-494e9decd86e"), new Guid("93105d83-746a-4bb9-aac0-444b8fdc1ec9") },
                    { new Guid("786718d9-0475-4e3b-a21b-494e9decd86e"), new Guid("bf088d71-d618-40d5-a9db-15d65f1aa011") },
                    { new Guid("bdc6a5b2-bd40-4cf9-8b7d-8e02b65660d4"), new Guid("27d571bc-8559-435e-b886-1fc85717ad66") },
                    { new Guid("bdc6a5b2-bd40-4cf9-8b7d-8e02b65660d4"), new Guid("93105d83-746a-4bb9-aac0-444b8fdc1ec9") },
                    { new Guid("bdc6a5b2-bd40-4cf9-8b7d-8e02b65660d4"), new Guid("bf088d71-d618-40d5-a9db-15d65f1aa011") },
                    { new Guid("e215c9f9-6d1e-463a-8b1b-26400f70c719"), new Guid("bc93ac87-79e7-4387-a2f1-721ee0996f4d") },
                    { new Guid("e215c9f9-6d1e-463a-8b1b-26400f70c719"), new Guid("faef2407-569b-40bd-95be-fcf54d3293b3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksGenres_GenreId",
                table: "BooksGenres",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_BookId",
                table: "Feedbacks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_MeetingId",
                table: "Invitations",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_Invitations_UserId",
                table: "Invitations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_OwnerId",
                table: "Meetings",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingVisitors_MeetingId",
                table: "MeetingVisitors",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_MeetingVisitors_UserId",
                table: "MeetingVisitors",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksGenres");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Invitations");

            migrationBuilder.DropTable(
                name: "MeetingVisitors");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
