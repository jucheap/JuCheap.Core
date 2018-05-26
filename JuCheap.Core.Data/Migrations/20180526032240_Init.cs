using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace JuCheap.Core.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    DisplaySequence = table.Column<int>(nullable: false),
                    Enabled = table.Column<bool>(nullable: false),
                    FullSpelling = table.Column<string>(maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    PathCode = table.Column<string>(maxLength: 20, nullable: true),
                    SimpleSpelling = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    FullName = table.Column<string>(maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    PathCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoginLogs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IP = table.Column<string>(maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoginName = table.Column<string>(maxLength: 20, nullable: false),
                    Message = table.Column<string>(maxLength: 200, nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Code = table.Column<string>(maxLength: 20, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    Icon = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    Order = table.Column<int>(nullable: false),
                    ParentId = table.Column<string>(maxLength: 36, nullable: true),
                    PathCode = table.Column<string>(maxLength: 100, nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Contents = table.Column<string>(maxLength: 500, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReadedNumber = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Total = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PageViews",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IP = table.Column<string>(maxLength: 20, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LoginName = table.Column<string>(maxLength: 20, nullable: false),
                    Url = table.Column<string>(maxLength: 300, nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PathCodes",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    Code = table.Column<string>(maxLength: 4, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Len = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PathCodes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteViews",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    Day = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteViews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SystemConfigs",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    DataInitedDate = table.Column<DateTime>(nullable: false),
                    IsDataInited = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SystemName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemConfigs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplate",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Step = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    DepartmentId = table.Column<string>(maxLength: 36, nullable: true),
                    Email = table.Column<string>(maxLength: 36, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSuperMan = table.Column<bool>(nullable: false),
                    LoginName = table.Column<string>(maxLength: 20, nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: false),
                    RealName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MessageReceivers",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsReaded = table.Column<bool>(nullable: false),
                    MessageId = table.Column<string>(maxLength: 36, nullable: false),
                    ReadDate = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageReceivers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageReceivers_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenus",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MenuId = table.Column<string>(maxLength: 36, nullable: false),
                    RoleId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleMenus_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplateForm",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    ControlName = table.Column<string>(maxLength: 50, nullable: false),
                    ControlType = table.Column<int>(nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    TemplateId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplateForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTemplateForm_TaskTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TaskTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplateStep",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    StepName = table.Column<string>(maxLength: 20, nullable: false),
                    TemplateId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplateStep", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTemplateStep_TaskTemplate_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "TaskTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(maxLength: 36, nullable: false),
                    UserId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTemplateStepOperate",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 36, nullable: false),
                    CreateDateTime = table.Column<DateTime>(nullable: false),
                    CreateUserId = table.Column<string>(maxLength: 36, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 20, nullable: false),
                    OperateDirection = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    StepId = table.Column<string>(maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTemplateStepOperate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskTemplateStepOperate_TaskTemplateStep_StepId",
                        column: x => x.StepId,
                        principalTable: "TaskTemplateStep",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MessageReceivers_MessageId",
                table: "MessageReceivers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_MenuId",
                table: "RoleMenus",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleMenus_RoleId",
                table: "RoleMenus",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplateForm_TemplateId",
                table: "TaskTemplateForm",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplateStep_TemplateId",
                table: "TaskTemplateStep",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskTemplateStepOperate_StepId",
                table: "TaskTemplateStepOperate",
                column: "StepId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_DepartmentId",
                table: "Users",
                column: "DepartmentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "LoginLogs");

            migrationBuilder.DropTable(
                name: "MessageReceivers");

            migrationBuilder.DropTable(
                name: "PageViews");

            migrationBuilder.DropTable(
                name: "PathCodes");

            migrationBuilder.DropTable(
                name: "RoleMenus");

            migrationBuilder.DropTable(
                name: "SiteViews");

            migrationBuilder.DropTable(
                name: "SystemConfigs");

            migrationBuilder.DropTable(
                name: "TaskTemplateForm");

            migrationBuilder.DropTable(
                name: "TaskTemplateStepOperate");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "TaskTemplateStep");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "TaskTemplate");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
