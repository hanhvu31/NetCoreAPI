using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MvcMovie.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<MvcMovie.Models.Employee> Employee { get; set; } = default!;
        public DbSet<MvcMovie.Models.DaiLy> DaiLy { get; set; } = default!;
        public DbSet<MvcMovie.Models.HeThongPhanPhoi> HeThongPhanPhoi { get; set; } = default!;

    }

}