using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace projet_net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill{Id=1,Name="hassen", Damage=34},
                new Skill{Id=2, Name="ps",Damage=34},
                new Skill{Id=3, Name="cp",Damage=45}
            );
        }

        public DbSet<Character> Characters => Set<Character>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Weapon> Weapons => Set<Weapon>();
        public DbSet<Skill> Skills => Set<Skill>();
    }
}