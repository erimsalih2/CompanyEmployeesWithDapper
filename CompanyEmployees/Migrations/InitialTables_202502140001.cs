using FluentMigrator;
using Microsoft.AspNetCore.Http.HttpResults;
namespace CompanyEmployees.Migrations
{
    [Migration(202501140001)]
    public class InitialTables_202502140001:Migration
    {
        public override void Down()
        {
            Delete.Table("Employees");
            Delete.Table("Companies");
        }
        public override void Up()
        {
            Create.Table("Companies")
            .WithColumn("CompanyId").AsGuid().NotNullable()
            .PrimaryKey().WithDefaultValue("NEWID()")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Address").AsString(60).NotNullable()
            .WithColumn("Country").AsString(50).NotNullable();
            Create.Table("Employees")
            .WithColumn("EmployeeId").AsGuid().NotNullable()
            .PrimaryKey().WithDefaultValue("NEWID()")
            .WithColumn("Name").AsString(50).NotNullable()
            .WithColumn("Age").AsInt32().NotNullable()
            .WithColumn("Position").AsString(50).NotNullable()
            .WithColumn("CompanyId").AsGuid().NotNullable()
            .ForeignKey("Companies", "CompanyId")
            .OnDelete(System.Data.Rule.Cascade);
        }

    }
}
