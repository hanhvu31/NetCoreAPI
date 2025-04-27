using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using Microsoft.AspNetCore.Identity;
using MvcMovie.Models;
using VicemMVCIdentity.Data;


var builder = WebApplication.CreateBuilder(args);

// Cấu hình DbContext với SQLite và Identity
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Cấu hình Identity
builder.Services.AddDefaultIdentity<ApplicationUser>(options=>options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Thêm các dịch vụ cần thiết cho ứng dụng
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Đảm bảo tệp tĩnh được phục vụ

app.UseRouting();

app.UseAuthorization();

// Cấu hình đường dẫn cho controller và action
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();