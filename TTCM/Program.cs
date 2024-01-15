using Microsoft.EntityFrameworkCore;
using TTCM._2Repository;
using TTCM.Models;
using TTCM.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var conectionString = builder.Configuration.GetConnectionString("QldoAn4Context");
builder.Services.AddDbContext<QldoAn4Context>(x => x.UseSqlServer(conectionString));
builder.Services.AddScoped<ILoaiDoAnRepository, LoaiDoAnRepository>();
builder.Services.AddScoped<INhaHang2Repository, NhaHang2Repository>();
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();

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
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
