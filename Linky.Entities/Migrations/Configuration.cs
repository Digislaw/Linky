using Linky.Entities.Identity;
using Linky.Entities.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Linky.Entities.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            ApplicationUser user = context.Users.FirstOrDefault(u => u.Email == "user@example.com");

            if(user == null)
            {
                var store = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(store);

                user = new ApplicationUser() { Email = "user@example.com", UserName = "user@example.com" };
                userManager.Create(user, "Test123@");
            }

            DateTime day = DateTime.Parse("22/01/2021 12:15:07");

            var link = new Link()
            {
                Clicks = 46,
                CreatedAt = day,
                Label = "seed",
                URL = "http://example.com",
                ApplicationUserId = user.Id
            };

            context.Links.AddOrUpdate(x => x.Label, link);

            var countries = new []{ "Poland", "United States", "France", "Germany" };
            var clicks = new[] { 8, 12, 20, 6 };

            for (int i = 0; i < 4; i++)
            {
                var countryStats = new CountryCounter()
                {
                    Id = i + 1,
                    Clicks = clicks[i],
                    CountryName = countries[i],
                    LinkId = link.Id
                };

                context.CountryCounters.AddOrUpdate(x => x.Id, countryStats);
            }

            for (int i = 1; i < 3; i++)
            {
                var dailyStats = new DailyCounter()
                {
                    Id = i,
                    Clicks = i * 2,
                    Day = day.Date,
                    LinkId = link.Id
                };

                context.DailyCounters.AddOrUpdate(x => x.Id, dailyStats);
                day = day.AddDays(1);
            }

            for (int i = 3; i <= 7; i++)
            {
                var dailyStats = new DailyCounter()
                {
                    Id = i,
                    Clicks = i + 3,
                    Day = day.Date,
                    LinkId = link.Id
                };

                context.DailyCounters.AddOrUpdate(x => x.Id, dailyStats);
                day = day.AddDays(1);
            }
        }
    }
}
