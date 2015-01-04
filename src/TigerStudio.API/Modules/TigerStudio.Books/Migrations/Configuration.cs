using TigerStudio.Books.Data;
using System.Data.Entity.Migrations;

namespace TigerStudio.Books.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<BookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookContext context)
        {
            //  This method will be called after migrating to the latest version.

            InitializeBooksData.Init(context);
        }
    }
}
