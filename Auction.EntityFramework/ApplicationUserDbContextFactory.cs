using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Auction.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Auction.EntityFramework
{
    public class ApplicationUserDbContextFactory : IDesignTimeDbContextFactory<ApplicationUserDbContext>
    {
        public ApplicationUserDbContext CreateDbContext(string[] args)
        {
            var dbContext = new ApplicationUserDbContext(
                new DbContextOptionsBuilder<ApplicationUserDbContext>()
                    .UseMySql(
                        new ConfigurationBuilder()
                            .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                            .Build()
                            .GetConnectionString("AuctionConnection")
                    ).Options
            );
            return dbContext;
        }
    }
}
