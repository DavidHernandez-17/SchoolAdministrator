

using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data;

var builder = WebApplication.CreateBuilder(args);

//Servicio de base de datos
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
