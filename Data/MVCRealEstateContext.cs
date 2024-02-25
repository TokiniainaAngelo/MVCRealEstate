using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCRealEstate.Models;

namespace MVCRealEstate.Data
{
    public class MVCRealEstateContext : DbContext
    {
        public MVCRealEstateContext (DbContextOptions<MVCRealEstateContext> options)
            : base(options)
        {
        }

        public DbSet<MVCRealEstate.Models.User> User { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Agency> Agency { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.OfferMedia> OfferMedia { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Location> Location { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.OwnerInfo> OwnerInfo { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Interest> Interest { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Offer> Offer { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Appointment> Appointment { get; set; } = default!;
        public DbSet<MVCRealEstate.Models.Message> Message { get; set; } = default!;
    }
}
