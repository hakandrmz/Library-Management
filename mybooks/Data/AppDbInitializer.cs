using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using mybooks.Data.Models;

namespace mybooks.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                        new Book()
                        {
                            Title = "1st book",
                            Description = "1st book description",
                            IsRead = true,
                            Genre = "Biography",
                            CoverUrl = "https...",
                            DateAdded = DateTime.Now
                        },new Book()
                        {
                            Title = "2st book",
                            Description = "2st book description",
                            IsRead = false,
                            Genre = "Biography",
                            CoverUrl = "https...",
                            DateAdded = DateTime.Now
                        });

                    context.SaveChanges();

                }
            }
        }
    }
}
