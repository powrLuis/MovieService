using MovieService.Models;
namespace MovieService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieService.Data.MovieServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieService.Data.MovieServiceContext context)
        {
            context.Movies.AddOrUpdate(x => x.Id,
                new Movie { Id = 1, Titulo = "Star wars", A�o = 1999, Precio = 10.9M },
                new Movie { Id = 2, Titulo = "Star wars 2", A�o = 2002, Precio = 12.9M },
                new Movie { Id = 3, Titulo = "Star wars 3", A�o = 2005, Precio = 14.9M },
                new Movie { Id = 4, Titulo = "Star wars 4", A�o = 1977, Precio = 16.9M },
                new Movie { Id = 5, Titulo = "Star wars 5", A�o = 1980, Precio = 18.9M },
                new Movie { Id = 6, Titulo = "Star wars 6", A�o = 1983, Precio = 20.9M },
                new Movie { Id = 7, Titulo = "Star wars 7", A�o = 2015, Precio = 5.9M },
                new Movie { Id = 8, Titulo = "Star wars 8", A�o = 2017, Precio = 7.9M },
                new Movie { Id = 9, Titulo = "Star wars 9", A�o = 2019, Precio = 8.9M }
                );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
