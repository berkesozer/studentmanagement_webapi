using System;
using studentmanagement_webapi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace studentmanagement_webapi.Data
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options){ }
        public DbSet<User> Users { get; set; }
        private readonly IConfiguration Configuration;
        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Lesson> Lessons { get; set; }

    }
}

 


