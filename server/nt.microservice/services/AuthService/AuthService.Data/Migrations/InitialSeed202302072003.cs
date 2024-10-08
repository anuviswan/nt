﻿using FluentMigrator;

namespace AuthService.Data.Migrations;

[Migration(202302072003)]
public class InitialSeed202302072003 : Migration
{
    public override void Down()
    {
        Delete.FromTable(User.TABLE_NAME)
            .AllRows();
        Delete.Table(User.TABLE_NAME);
    }

    public override void Up()
    {
        Create.Table(User.TABLE_NAME)
            .WithColumn("id").AsGuid().NotNullable().WithDefaultValue(SystemMethods.NewGuid).PrimaryKey()
            .WithColumn("username").AsString().NotNullable()
            .WithColumn("password").AsString().NotNullable();


        Insert.IntoTable(User.TABLE_NAME)
            .Row(new { username = "jia.anu", password = "bXlwYXNz" })
            .Row(new { username = "naina.anu", password = "bXlwYXNz" });
    }
}
