using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentMigrator;
using MyOrg.HelloWorld.Domain;
using Smartstore.Core.Data.Migrations;

namespace MyOrg.HelloWorld.Migrations
{
    [MigrationVersion("2024-07-23 14:30:00", "HelloWorld: Initial")]
    public class _20240723143000_Initial : MigrationBase
    {
        public override void Up()
        {
            var tableName = "Notification";

            if (!Schema.Table(tableName).Exists())
            {
                Create.Table(tableName)
                .WithIdColumn()
                .WithColumn(nameof(Notification.AuthorId))
                .AsInt32()
                .NotNullable()
                .Indexed("IX_Notification_AuthorId")
                .WithColumn(nameof(Notification.Published))
                .AsDateTime()
                .NotNullable()
                .Indexed("IX_Notification.Published")
                .WithColumn(nameof(Notification.Message))
                .AsMaxString()
                .NotNullable();

            }
        }

        public override void Down() { 
        }
    }
}
