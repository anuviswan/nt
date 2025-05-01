using FluentMigrator;

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
                    .Row(new { username = "naina.anu", password = "bXlwYXNz" })
                    .Row(new { username = "priya.smith", password = "bXlwYXNz" })
                    .Row(new { username = "rajan.kumar", password = "bXlwYXNz" })
                    .Row(new { username = "surya.karthik", password = "bXlwYXNz" })
                    .Row(new { username = "anjali.menon", password = "bXlwYXNz" })
                    .Row(new { username = "manju.pillai", password = "bXlwYXNz" })
                    .Row(new { username = "reema.johnson", password = "bXlwYXNz" })
                    .Row(new { username = "sona.roy", password = "bXlwYXNz" })
                    .Row(new { username = "ajay.sharma", password = "bXlwYXNz" })
                    .Row(new { username = "mukesh.singh", password = "bXlwYXNz" })
                    .Row(new { username = "ranjan.verma", password = "bXlwYXNz" })
                    .Row(new { username = "tina.kapoor", password = "bXlwYXNz" })
                    .Row(new { username = "jayan.murali", password = "bXlwYXNz" })
                    .Row(new { username = "shiva.singh", password = "bXlwYXNz" })
                    .Row(new { username = "pradeep.gupta", password = "bXlwYXNz" })
                    .Row(new { username = "seema.khan", password = "bXlwYXNz" })
                    .Row(new { username = "geetha.singh", password = "bXlwYXNz" })
                    .Row(new { username = "krishna.raj", password = "bXlwYXNz" })
                    .Row(new { username = "babu.malhotra", password = "bXlwYXNz" })
                    .Row(new { username = "pavi.kumar", password = "bXlwYXNz" })
                    .Row(new { username = "manu.jain", password = "bXlwYXNz" })
                    .Row(new { username = "rajesh.agarwal", password = "bXlwYXNz" })
                    .Row(new { username = "neha.sharma", password = "bXlwYXNz" })
                    .Row(new { username = "prabha.sen", password = "bXlwYXNz" })
                    .Row(new { username = "krish.kapoor", password = "bXlwYXNz" })
                    .Row(new { username = "sasi.roy", password = "bXlwYXNz" })
                    .Row(new { username = "george.fernandes", password = "bXlwYXNz" })
                    .Row(new { username = "shalini.singh", password = "bXlwYXNz" })
                    .Row(new { username = "varsha.menon", password = "bXlwYXNz" })
                    .Row(new { username = "lalitha.nair", password = "bXlwYXNz" })
                    .Row(new { username = "sunil.singh", password = "bXlwYXNz" })
                    .Row(new { username = "manoj.sharma", password = "bXlwYXNz" })
                    .Row(new { username = "radhika.bansal", password = "bXlwYXNz" })
                    .Row(new { username = "ravi.kapoor", password = "bXlwYXNz" })
                    .Row(new { username = "vijay.gupta", password = "bXlwYXNz" })
                    .Row(new { username = "meera.nair", password = "bXlwYXNz" })
                    .Row(new { username = "sundar.kumar", password = "bXlwYXNz" })
                    .Row(new { username = "ajith.murali", password = "bXlwYXNz" })
                    .Row(new { username = "chitra.singh", password = "bXlwYXNz" })
                    .Row(new { username = "sunitha.khan", password = "bXlwYXNz" })
                    .Row(new { username = "kishore.verma", password = "bXlwYXNz" })
                    .Row(new { username = "rani.pillai", password = "bXlwYXNz" })
                    .Row(new { username = "dinesh.patel", password = "bXlwYXNz" })
                    .Row(new { username = "ragini.singh", password = "bXlwYXNz" })
                    .Row(new { username = "kamal.rajan", password = "bXlwYXNz" })
                    .Row(new { username = "kamini.roy", password = "bXlwYXNz" })
                    .Row(new { username = "vishal.gupta", password = "bXlwYXNz" })
                    .Row(new { username = "anjali.agarwal", password = "bXlwYXNz" })
                    .Row(new { username = "pravin.kumar", password = "bXlwYXNz" })
                    .Row(new { username = "shreya.jain", password = "bXlwYXNz" })
                    .Row(new { username = "subhashini.menon", password = "bXlwYXNz" })
                    .Row(new { username = "nandan.nair", password = "bXlwYXNz" })
                    .Row(new { username = "poornima.sharma", password = "bXlwYXNz" })
                    .Row(new { username = "ranjith.roy", password = "bXlwYXNz" })
                    .Row(new { username = "vijayalaxmi.murali", password = "bXlwYXNz" })
                    .Row(new { username = "prabodh.kapoor", password = "bXlwYXNz" });

    }
}
