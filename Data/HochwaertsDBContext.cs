using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hochwaerts.Models;

namespace Hochwaerts.Models
{
    public class HochwaertsDBContext : DbContext
    {
        public HochwaertsDBContext (DbContextOptions<HochwaertsDBContext> options)
            : base(options)
        {
        }

        public DbSet<Hochwaerts.Models.Bibliothek> Bibliothek { get; set; } = default!;
        public DbSet<Hochwaerts.Models.Autor> Autor { get; set; } = default!;
        public DbSet<Hochwaerts.Models.Buch> Buch { get; set; } = default!;
        public DbSet<Hochwaerts.Models.Status> Status { get; set; } = default!;
        public DbSet<Hochwaerts.Models.Verleihobjekt> Verleihobjekt { get; set; } = default!;
        public DbSet<Hochwaerts.Models.Zauberer> Zauberer { get; set; } = default!;
    }
}
