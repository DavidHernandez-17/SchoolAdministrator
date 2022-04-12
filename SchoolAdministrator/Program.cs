using Microsoft.EntityFrameworkCore;
using SchoolAdministrator.Data;

var builder = WebApplication.CreateBuilder(args);

//Servicio de base de datos
builder.Services.AddDbContext<DataContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});



builder.Services.AddControllersWithViews();

builder.Services.AddTransient<SeedDB>();

var app = builder.Build();

SeedData();

void SeedData()
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        SeedDB? service = scope.ServiceProvider.GetService<SeedDB>();
        service.SeedAsync().Wait();
    }

}

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
