using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using Makeev_lab2.Models;

namespace Makeev_lab2.Data
{
    public class Makeev_lab2Context : DbContext
    {
        public Makeev_lab2Context(DbContextOptions<Makeev_lab2Context> options)
         : base(options)
        {
        }
        public DbSet<Doctor> Doctors { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<Speciality> Specialities { get; set; } = null!;
    }
}
