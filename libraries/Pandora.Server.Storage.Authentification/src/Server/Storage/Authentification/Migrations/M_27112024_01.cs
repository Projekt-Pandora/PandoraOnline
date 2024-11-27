using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification.Migrations
{
    [Migration(2024112701, description: "Create Account Table")]
    internal class _27112024_01 : Migration
    {
        public override void Down()
        {
            Delete.Table("accounts");
        }

        public override void Up()
        {
            Create.Table("accounts")
                .WithColumn("account_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("account_name").AsString(100).NotNullable().Unique()
                .WithColumn("account_mail").AsString(100).NotNullable().Unique()
                .WithColumn("account_password_hash").AsString(200).NotNullable()
                .WithColumn("account_password_salt").AsString(200).NotNullable()
                .WithColumn("account_type").AsInt16().NotNullable().WithDefaultValue(0)
                .WithColumn("account_enabled").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("account_verified").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("account_createDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("account_changeDate").AsDateTime().NotNullable().WithDefault(SystemMethods.CurrentDateTime)
                .WithColumn("account_deleteDate").AsDateTime().WithDefaultValue(null);
        }
    }
}
