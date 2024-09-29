using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification.Migrations
{
    [Migration(1)]
    internal class _1_CreateAccountTable : Migration
    {
        public override void Down()
        {
            Delete.Table("accounts");
        }

        public override void Up()
        {
            Create.Table("accounts")
                .WithColumn("account_id").AsInt64().PrimaryKey().Identity()
                .WithColumn("account_type").AsInt32().NotNullable()
                .WithColumn("account_name").AsFixedLengthString(100).NotNullable()
                .WithColumn("account_email").AsFixedLengthString(100).NotNullable()
                .WithColumn("account_passwordHash").AsFixedLengthString(200).NotNullable()
                .WithColumn("account_createDate").AsDateTime().NotNullable()
                .WithColumn("account_changeDate").AsDateTime().NotNullable()
                .WithColumn("account_deleteDate").AsDateTime().WithDefaultValue(null)
                .WithColumn("account_enabled").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("account_validated").AsBoolean().NotNullable().WithDefaultValue(false);

            Insert.IntoTable("accounts").Row(new
            {
                account_type            = AccountType.Normal,
                account_name            = "admin",
                account_email           = "admin@adin.to",
                account_passwordHash    = new byte[] { 0 },
                account_createDate      =  DateTime.Now,
                account_changeDate      = DateTime.Now,
                account_enabled         = true,
                account_validated       = true
            });
        }
    }
}
