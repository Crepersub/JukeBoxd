using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxd
{
    /// <summary>
    /// Represents the database context for the Diary application.
    /// </summary>
    public class DiaryDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet of users in the database.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet of entries in the database.
        /// </summary>
        public DbSet<Entry> Entries { get; set; }

        /// <summary>
        /// Default constructor for the DiaryDbContext class.
        /// </summary>
        public DiaryDbContext()
        {
            // Default constructor logic
        }

        /// <summary>
        /// Constructor for the DiaryDbContext class that accepts DbContextOptions.
        /// </summary>
        /// <param name="options">The options to configure the DbContext.</param>
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options)
        {
            // Constructor logic with options
        }

        /// <summary>
        /// Configures the database context with specific options.
        /// </summary>
        /// <param name="optionsBuilder">The options builder used to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Diary;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
    }
}
