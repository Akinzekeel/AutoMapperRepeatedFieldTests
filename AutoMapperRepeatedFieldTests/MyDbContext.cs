using AutoMapperRepeatedFieldTests.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperRepeatedFieldTests
{
    public class MyDbContext : DbContext
    {
        private static readonly DbContextOptions options;

        static MyDbContext()
        {
            var builder = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("mydb");

            options = builder.Options;
        }

        public MyDbContext() : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasMany(x => x.Pets)
                .WithOne();

            base.OnModelCreating(modelBuilder);
        }
    }
}
