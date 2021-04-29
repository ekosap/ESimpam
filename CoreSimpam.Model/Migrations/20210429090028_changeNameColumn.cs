using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreSimpam.Model.Migrations
{
    public partial class changeNameColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleScreen_Roles_RoleID",
                schema: "Common",
                table: "RoleScreen");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleScreen_Screen_ScreenID",
                schema: "Common",
                table: "RoleScreen");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleID",
                schema: "Common",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "Common",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrxItem",
                schema: "Billing",
                table: "TrxItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trx",
                schema: "Billing",
                table: "Trx");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Screen",
                schema: "Common",
                table: "Screen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleScreen",
                schema: "Common",
                table: "RoleScreen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                schema: "Common",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resident",
                schema: "Customer",
                table: "Resident");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customer",
                schema: "Customer",
                table: "Customer");

            migrationBuilder.EnsureSchema(
                name: "customer");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.EnsureSchema(
                name: "billing");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Common",
                newName: "users",
                newSchema: "common");

            migrationBuilder.RenameTable(
                name: "TrxItem",
                schema: "Billing",
                newName: "trxitem",
                newSchema: "billing");

            migrationBuilder.RenameTable(
                name: "Trx",
                schema: "Billing",
                newName: "trx",
                newSchema: "billing");

            migrationBuilder.RenameTable(
                name: "Screen",
                schema: "Common",
                newName: "screen",
                newSchema: "common");

            migrationBuilder.RenameTable(
                name: "RoleScreen",
                schema: "Common",
                newName: "rolescreen",
                newSchema: "common");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Common",
                newName: "roles",
                newSchema: "common");

            migrationBuilder.RenameTable(
                name: "Resident",
                schema: "Customer",
                newName: "resident",
                newSchema: "customer");

            migrationBuilder.RenameTable(
                name: "Customer",
                schema: "Customer",
                newName: "customer",
                newSchema: "customer");

            migrationBuilder.RenameColumn(
                name: "UserName",
                schema: "common",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Salt",
                schema: "common",
                table: "users",
                newName: "salt");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "common",
                table: "users",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "Password",
                schema: "common",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                schema: "common",
                table: "users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UserID",
                schema: "common",
                table: "users",
                newName: "userid");

            migrationBuilder.RenameIndex(
                name: "IX_Users_RoleID",
                schema: "common",
                table: "users",
                newName: "IX_users_roleid");

            migrationBuilder.RenameColumn(
                name: "TrxID",
                schema: "billing",
                table: "trxitem",
                newName: "trxid");

            migrationBuilder.RenameColumn(
                name: "Qty",
                schema: "billing",
                table: "trxitem",
                newName: "qty");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "billing",
                table: "trxitem",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "IsClear",
                schema: "billing",
                table: "trxitem",
                newName: "isclear");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                schema: "billing",
                table: "trxitem",
                newName: "customerid");

            migrationBuilder.RenameColumn(
                name: "BeforeAmount",
                schema: "billing",
                table: "trxitem",
                newName: "beforeamount");

            migrationBuilder.RenameColumn(
                name: "AfterAmount",
                schema: "billing",
                table: "trxitem",
                newName: "afteramount");

            migrationBuilder.RenameColumn(
                name: "TrxItemID",
                schema: "billing",
                table: "trxitem",
                newName: "trxitemid");

            migrationBuilder.RenameColumn(
                name: "UpdatedDate",
                schema: "billing",
                table: "trx",
                newName: "updateddate");

            migrationBuilder.RenameColumn(
                name: "UpdatedBy",
                schema: "billing",
                table: "trx",
                newName: "updatedby");

            migrationBuilder.RenameColumn(
                name: "TrxDate",
                schema: "billing",
                table: "trx",
                newName: "trxdate");

            migrationBuilder.RenameColumn(
                name: "Total",
                schema: "billing",
                table: "trx",
                newName: "total");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                schema: "billing",
                table: "trx",
                newName: "createddate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                schema: "billing",
                table: "trx",
                newName: "createdby");

            migrationBuilder.RenameColumn(
                name: "TrxID",
                schema: "billing",
                table: "trx",
                newName: "trxid");

            migrationBuilder.RenameColumn(
                name: "ScreenName",
                schema: "common",
                table: "screen",
                newName: "screenname");

            migrationBuilder.RenameColumn(
                name: "ParentID",
                schema: "common",
                table: "screen",
                newName: "parentid");

            migrationBuilder.RenameColumn(
                name: "IsMenu",
                schema: "common",
                table: "screen",
                newName: "ismenu");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "common",
                table: "screen",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "IconCss",
                schema: "common",
                table: "screen",
                newName: "iconcss");

            migrationBuilder.RenameColumn(
                name: "ControllerName",
                schema: "common",
                table: "screen",
                newName: "controllername");

            migrationBuilder.RenameColumn(
                name: "ActionName",
                schema: "common",
                table: "screen",
                newName: "actionname");

            migrationBuilder.RenameColumn(
                name: "ScreenID",
                schema: "common",
                table: "screen",
                newName: "screenid");

            migrationBuilder.RenameColumn(
                name: "WriteFlag",
                schema: "common",
                table: "rolescreen",
                newName: "writeflag");

            migrationBuilder.RenameColumn(
                name: "ScreenID",
                schema: "common",
                table: "rolescreen",
                newName: "screenid");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "common",
                table: "rolescreen",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ReadFlag",
                schema: "common",
                table: "rolescreen",
                newName: "readflag");

            migrationBuilder.RenameColumn(
                name: "DeleteFlag",
                schema: "common",
                table: "rolescreen",
                newName: "deleteflag");

            migrationBuilder.RenameColumn(
                name: "RoleScreenID",
                schema: "common",
                table: "rolescreen",
                newName: "rolescreenid");

            migrationBuilder.RenameIndex(
                name: "IX_RoleScreen_ScreenID",
                schema: "common",
                table: "rolescreen",
                newName: "IX_rolescreen_screenid");

            migrationBuilder.RenameIndex(
                name: "IX_RoleScreen_RoleID",
                schema: "common",
                table: "rolescreen",
                newName: "IX_rolescreen_roleid");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                schema: "common",
                table: "roles",
                newName: "rolename");

            migrationBuilder.RenameColumn(
                name: "IsEnabled",
                schema: "common",
                table: "roles",
                newName: "isenabled");

            migrationBuilder.RenameColumn(
                name: "RoleID",
                schema: "common",
                table: "roles",
                newName: "roleid");

            migrationBuilder.RenameColumn(
                name: "ResidentNumber",
                schema: "customer",
                table: "resident",
                newName: "residentnumber");

            migrationBuilder.RenameColumn(
                name: "ResidentName",
                schema: "customer",
                table: "resident",
                newName: "residentname");

            migrationBuilder.RenameColumn(
                name: "Price",
                schema: "customer",
                table: "resident",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                schema: "customer",
                table: "resident",
                newName: "isactive");

            migrationBuilder.RenameColumn(
                name: "ResidentID",
                schema: "customer",
                table: "resident",
                newName: "residentid");

            migrationBuilder.RenameColumn(
                name: "ResidentID",
                schema: "customer",
                table: "customer",
                newName: "residentid");

            migrationBuilder.RenameColumn(
                name: "Phone",
                schema: "customer",
                table: "customer",
                newName: "phone");

            migrationBuilder.RenameColumn(
                name: "CustomerNumber",
                schema: "customer",
                table: "customer",
                newName: "customernumber");

            migrationBuilder.RenameColumn(
                name: "CustomerName",
                schema: "customer",
                table: "customer",
                newName: "customername");

            migrationBuilder.RenameColumn(
                name: "CustomerAddress",
                schema: "customer",
                table: "customer",
                newName: "customeraddress");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                schema: "customer",
                table: "customer",
                newName: "customerid");

            migrationBuilder.AddColumn<long>(
                name: "createdby",
                schema: "common",
                table: "rolescreen",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "createddate",
                schema: "common",
                table: "rolescreen",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "updatedby",
                schema: "common",
                table: "rolescreen",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddate",
                schema: "common",
                table: "rolescreen",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "createdby",
                schema: "common",
                table: "roles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "createddate",
                schema: "common",
                table: "roles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "updatedby",
                schema: "common",
                table: "roles",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updateddate",
                schema: "common",
                table: "roles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                schema: "common",
                table: "users",
                column: "userid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trxitem",
                schema: "billing",
                table: "trxitem",
                column: "trxitemid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_trx",
                schema: "billing",
                table: "trx",
                column: "trxid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_screen",
                schema: "common",
                table: "screen",
                column: "screenid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_rolescreen",
                schema: "common",
                table: "rolescreen",
                column: "rolescreenid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roles",
                schema: "common",
                table: "roles",
                column: "roleid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_resident",
                schema: "customer",
                table: "resident",
                column: "residentid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_customer",
                schema: "customer",
                table: "customer",
                column: "customerid");

            migrationBuilder.AddForeignKey(
                name: "FK_rolescreen_roles_roleid",
                schema: "common",
                table: "rolescreen",
                column: "roleid",
                principalSchema: "common",
                principalTable: "roles",
                principalColumn: "roleid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_rolescreen_screen_screenid",
                schema: "common",
                table: "rolescreen",
                column: "screenid",
                principalSchema: "common",
                principalTable: "screen",
                principalColumn: "screenid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_users_roles_roleid",
                schema: "common",
                table: "users",
                column: "roleid",
                principalSchema: "common",
                principalTable: "roles",
                principalColumn: "roleid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_rolescreen_roles_roleid",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropForeignKey(
                name: "FK_rolescreen_screen_screenid",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropForeignKey(
                name: "FK_users_roles_roleid",
                schema: "common",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                schema: "common",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trxitem",
                schema: "billing",
                table: "trxitem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trx",
                schema: "billing",
                table: "trx");

            migrationBuilder.DropPrimaryKey(
                name: "PK_screen",
                schema: "common",
                table: "screen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_rolescreen",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roles",
                schema: "common",
                table: "roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_resident",
                schema: "customer",
                table: "resident");

            migrationBuilder.DropPrimaryKey(
                name: "PK_customer",
                schema: "customer",
                table: "customer");

            migrationBuilder.DropColumn(
                name: "createdby",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropColumn(
                name: "createddate",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropColumn(
                name: "updatedby",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropColumn(
                name: "updateddate",
                schema: "common",
                table: "rolescreen");

            migrationBuilder.DropColumn(
                name: "createdby",
                schema: "common",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "createddate",
                schema: "common",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "updatedby",
                schema: "common",
                table: "roles");

            migrationBuilder.DropColumn(
                name: "updateddate",
                schema: "common",
                table: "roles");

            migrationBuilder.EnsureSchema(
                name: "Customer");

            migrationBuilder.EnsureSchema(
                name: "Common");

            migrationBuilder.EnsureSchema(
                name: "Billing");

            migrationBuilder.RenameTable(
                name: "users",
                schema: "common",
                newName: "Users",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "trxitem",
                schema: "billing",
                newName: "TrxItem",
                newSchema: "Billing");

            migrationBuilder.RenameTable(
                name: "trx",
                schema: "billing",
                newName: "Trx",
                newSchema: "Billing");

            migrationBuilder.RenameTable(
                name: "screen",
                schema: "common",
                newName: "Screen",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "rolescreen",
                schema: "common",
                newName: "RoleScreen",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "roles",
                schema: "common",
                newName: "Roles",
                newSchema: "Common");

            migrationBuilder.RenameTable(
                name: "resident",
                schema: "customer",
                newName: "Resident",
                newSchema: "Customer");

            migrationBuilder.RenameTable(
                name: "customer",
                schema: "customer",
                newName: "Customer",
                newSchema: "Customer");

            migrationBuilder.RenameColumn(
                name: "username",
                schema: "Common",
                table: "Users",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "salt",
                schema: "Common",
                table: "Users",
                newName: "Salt");

            migrationBuilder.RenameColumn(
                name: "roleid",
                schema: "Common",
                table: "Users",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "password",
                schema: "Common",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                schema: "Common",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "userid",
                schema: "Common",
                table: "Users",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_users_roleid",
                schema: "Common",
                table: "Users",
                newName: "IX_Users_RoleID");

            migrationBuilder.RenameColumn(
                name: "trxid",
                schema: "Billing",
                table: "TrxItem",
                newName: "TrxID");

            migrationBuilder.RenameColumn(
                name: "qty",
                schema: "Billing",
                table: "TrxItem",
                newName: "Qty");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "Billing",
                table: "TrxItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "isclear",
                schema: "Billing",
                table: "TrxItem",
                newName: "IsClear");

            migrationBuilder.RenameColumn(
                name: "customerid",
                schema: "Billing",
                table: "TrxItem",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "beforeamount",
                schema: "Billing",
                table: "TrxItem",
                newName: "BeforeAmount");

            migrationBuilder.RenameColumn(
                name: "afteramount",
                schema: "Billing",
                table: "TrxItem",
                newName: "AfterAmount");

            migrationBuilder.RenameColumn(
                name: "trxitemid",
                schema: "Billing",
                table: "TrxItem",
                newName: "TrxItemID");

            migrationBuilder.RenameColumn(
                name: "updateddate",
                schema: "Billing",
                table: "Trx",
                newName: "UpdatedDate");

            migrationBuilder.RenameColumn(
                name: "updatedby",
                schema: "Billing",
                table: "Trx",
                newName: "UpdatedBy");

            migrationBuilder.RenameColumn(
                name: "trxdate",
                schema: "Billing",
                table: "Trx",
                newName: "TrxDate");

            migrationBuilder.RenameColumn(
                name: "total",
                schema: "Billing",
                table: "Trx",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "createddate",
                schema: "Billing",
                table: "Trx",
                newName: "CreatedDate");

            migrationBuilder.RenameColumn(
                name: "createdby",
                schema: "Billing",
                table: "Trx",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "trxid",
                schema: "Billing",
                table: "Trx",
                newName: "TrxID");

            migrationBuilder.RenameColumn(
                name: "screenname",
                schema: "Common",
                table: "Screen",
                newName: "ScreenName");

            migrationBuilder.RenameColumn(
                name: "parentid",
                schema: "Common",
                table: "Screen",
                newName: "ParentID");

            migrationBuilder.RenameColumn(
                name: "ismenu",
                schema: "Common",
                table: "Screen",
                newName: "IsMenu");

            migrationBuilder.RenameColumn(
                name: "isactive",
                schema: "Common",
                table: "Screen",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "iconcss",
                schema: "Common",
                table: "Screen",
                newName: "IconCss");

            migrationBuilder.RenameColumn(
                name: "controllername",
                schema: "Common",
                table: "Screen",
                newName: "ControllerName");

            migrationBuilder.RenameColumn(
                name: "actionname",
                schema: "Common",
                table: "Screen",
                newName: "ActionName");

            migrationBuilder.RenameColumn(
                name: "screenid",
                schema: "Common",
                table: "Screen",
                newName: "ScreenID");

            migrationBuilder.RenameColumn(
                name: "writeflag",
                schema: "Common",
                table: "RoleScreen",
                newName: "WriteFlag");

            migrationBuilder.RenameColumn(
                name: "screenid",
                schema: "Common",
                table: "RoleScreen",
                newName: "ScreenID");

            migrationBuilder.RenameColumn(
                name: "roleid",
                schema: "Common",
                table: "RoleScreen",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "readflag",
                schema: "Common",
                table: "RoleScreen",
                newName: "ReadFlag");

            migrationBuilder.RenameColumn(
                name: "deleteflag",
                schema: "Common",
                table: "RoleScreen",
                newName: "DeleteFlag");

            migrationBuilder.RenameColumn(
                name: "rolescreenid",
                schema: "Common",
                table: "RoleScreen",
                newName: "RoleScreenID");

            migrationBuilder.RenameIndex(
                name: "IX_rolescreen_screenid",
                schema: "Common",
                table: "RoleScreen",
                newName: "IX_RoleScreen_ScreenID");

            migrationBuilder.RenameIndex(
                name: "IX_rolescreen_roleid",
                schema: "Common",
                table: "RoleScreen",
                newName: "IX_RoleScreen_RoleID");

            migrationBuilder.RenameColumn(
                name: "rolename",
                schema: "Common",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "isenabled",
                schema: "Common",
                table: "Roles",
                newName: "IsEnabled");

            migrationBuilder.RenameColumn(
                name: "roleid",
                schema: "Common",
                table: "Roles",
                newName: "RoleID");

            migrationBuilder.RenameColumn(
                name: "residentnumber",
                schema: "Customer",
                table: "Resident",
                newName: "ResidentNumber");

            migrationBuilder.RenameColumn(
                name: "residentname",
                schema: "Customer",
                table: "Resident",
                newName: "ResidentName");

            migrationBuilder.RenameColumn(
                name: "price",
                schema: "Customer",
                table: "Resident",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "isactive",
                schema: "Customer",
                table: "Resident",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "residentid",
                schema: "Customer",
                table: "Resident",
                newName: "ResidentID");

            migrationBuilder.RenameColumn(
                name: "residentid",
                schema: "Customer",
                table: "Customer",
                newName: "ResidentID");

            migrationBuilder.RenameColumn(
                name: "phone",
                schema: "Customer",
                table: "Customer",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "customernumber",
                schema: "Customer",
                table: "Customer",
                newName: "CustomerNumber");

            migrationBuilder.RenameColumn(
                name: "customername",
                schema: "Customer",
                table: "Customer",
                newName: "CustomerName");

            migrationBuilder.RenameColumn(
                name: "customeraddress",
                schema: "Customer",
                table: "Customer",
                newName: "CustomerAddress");

            migrationBuilder.RenameColumn(
                name: "customerid",
                schema: "Customer",
                table: "Customer",
                newName: "CustomerID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "Common",
                table: "Users",
                column: "UserID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrxItem",
                schema: "Billing",
                table: "TrxItem",
                column: "TrxItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trx",
                schema: "Billing",
                table: "Trx",
                column: "TrxID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Screen",
                schema: "Common",
                table: "Screen",
                column: "ScreenID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleScreen",
                schema: "Common",
                table: "RoleScreen",
                column: "RoleScreenID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                schema: "Common",
                table: "Roles",
                column: "RoleID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resident",
                schema: "Customer",
                table: "Resident",
                column: "ResidentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customer",
                schema: "Customer",
                table: "Customer",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_RoleScreen_Roles_RoleID",
                schema: "Common",
                table: "RoleScreen",
                column: "RoleID",
                principalSchema: "Common",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleScreen_Screen_ScreenID",
                schema: "Common",
                table: "RoleScreen",
                column: "ScreenID",
                principalSchema: "Common",
                principalTable: "Screen",
                principalColumn: "ScreenID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleID",
                schema: "Common",
                table: "Users",
                column: "RoleID",
                principalSchema: "Common",
                principalTable: "Roles",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
