using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Ngo.Models;
using System.Collections.Generic;

namespace Ngo.Data
{
    public class ApplicationDbContext: IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<Event> Events { get; set; }
        //public DbSet<Donar> Donars { get; set; }    
        public DbSet<CampaignCategory> CampaignCategories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<DonationI> DonationIs { get; set; }

        public DbSet<Volunteer> Volunteers { get; set; }    
        public DbSet<NgoEvent> NgoEvents { get; set; }
       
        
    }
}
