using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;
using Microsoft.EntityFrameworkCore;

namespace JukeBoxd
{
    public class DiaryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }


        /// <summary>
        /// Default constructor for the DiaryDbContext class.
        /// </summary>
        public DiaryDbContext( )
        {
            // nope
        }

        /// <summary>
        /// Constructor for the DiaryDbContext class that accepts DbContextOptions.
        /// </summary>
        /// <param name="options"></param>
        public DiaryDbContext(DbContextOptions<DiaryDbContext> options) : base(options)
        {
            // nope
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Diary;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            
        }
    }
}
