using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Walks.API.Data
{
    public class WalksAuthDbContext : IdentityDbContext
    {
        public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /*
             * Identity user: Use for authenticate ex : login user
               Identity role : Use for authorization ex: Administrator (above user belongs to administrator role)
               Users have roles, roles have permissions. Like create app
             */

            //list of roles.

            var readerRoleId = "bad8d325-06dd-45f6-9e0d-36fd07863a05";
            var writerRoleId = "9992583f-5664-4a43-bb22-9a1028d63d08";


            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                   Id = readerRoleId,
                   ConcurrencyStamp = readerRoleId,
                   Name = "Reader",
                   NormalizedName = "Reader".ToUpper() //helpful for case-insensitivity.
                },

                new IdentityRole
                {
                    Id =writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper() //helpful for case-insensitivity.


                }
            };  

            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
