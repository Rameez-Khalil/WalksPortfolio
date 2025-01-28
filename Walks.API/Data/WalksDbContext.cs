using Microsoft.EntityFrameworkCore;
using Walks.API.Models.Domain;

namespace Walks.API.Data
{
    public class WalksDbContext : DbContext
    {
        public WalksDbContext(DbContextOptions options) : base(options) { }
       


        //generate tables.
        public DbSet<Region> Regions { get; set; } 
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulty { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for difficulties.
            var difficulties = new List<Difficulty>()
            {
               new Difficulty()
               {
                   Id = Guid.Parse("e6c508b4-e944-46e0-9c52-1a0ec86374ad"),
                   Name = "Easy"
               },
                new Difficulty()
               {
                   Id = Guid.Parse("e6c508b4-e944-46e0-9c52-1a0ec86374aa"),
                   Name = "Medium"
               },
                 new Difficulty()
               {
                   Id = Guid.Parse("e6c508b4-e944-46e0-9c52-1a0ec86374ab"),
                   Name = "Hard"
               },
            }; 

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions.
            var regions = new List<Region>()
            {
               new Region()
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Name = "Missouri",
                    code = "MS",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
               new Region
                {
                    Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f7"),
                    Name = "Michigan",
                    code = "MI",
                    RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },


            }; 

            modelBuilder.Entity<Region>().HasData(regions);

        }

    }
}
