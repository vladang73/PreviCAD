using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
using System.Data.Entity.Migrations.Model;
using System.Data.Entity.Migrations.Utilities;
using System.Data.Entity.SqlServer;

namespace Previgesst.DataContexts.Extensions
{
    public static class DbMigrationExtensions
    {
        public static void CreateView(this DbMigration migration, string viewName, string viewqueryString)
        {
            ((IDbMigration)migration).AddOperation(new CreateViewOperation(viewName, viewqueryString));
        }
    }

    public class CreateViewOperation : MigrationOperation
    {
        public CreateViewOperation(string viewName, string viewQueryString)
          : base(null)
        {
            ViewName = viewName;
            ViewString = viewQueryString;
        }

        public string ViewName { get; private set; }
        public string ViewString { get; private set; }
        public override bool IsDestructiveChange
        {
            get { return false; }
        }
    }

    public class CustomSqlServerMigrationSqlGenerator : SqlServerMigrationSqlGenerator
    {
        protected override void Generate(MigrationOperation migrationOperation)
        {
            var operation = migrationOperation as CreateViewOperation;
            if (operation != null)
            {
                using (IndentedTextWriter writer = Writer())
                {
                    writer.WriteLine("CREATE VIEW {0} AS {1} ; ",
                                      operation.ViewName,
                                      operation.ViewString);
                    Statement(writer);
                }
            }
        }
    }
}
