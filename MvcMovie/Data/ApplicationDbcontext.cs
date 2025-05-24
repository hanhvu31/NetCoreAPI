using Microsoft.EntityFrameworkCore;
using MvcMovie.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcMovie.Models.Entities;
using EntitiesEmployee = MvcMovie.Models.Entities.Employee;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<MemberUnit> MemberUnits { get; set; }
        public DbSet<EntitiesEmployee> Employees { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<MvcMovie.Models.Employee> Employee { get; set; } = default!;
        public DbSet<MvcMovie.Models.DaiLy> DaiLy { get; set; } = default!;
        public DbSet<MvcMovie.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;

    }

}