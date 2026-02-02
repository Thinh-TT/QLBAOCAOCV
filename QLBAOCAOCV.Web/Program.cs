using Microsoft.EntityFrameworkCore;
using QLBAOCAOCV.BLL.Interfaces;
using QLBAOCAOCV.BLL.Services;
using QLBAOCAOCV.DAL;
using QLBAOCAOCV.DAL.Entities;


var builder = WebApplication.CreateBuilder(args);
// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

// BLL Services
builder.Services.AddScoped<IBaoCaoService, BaoCaoService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
