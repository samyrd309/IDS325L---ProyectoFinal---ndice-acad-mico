using Microsoft.EntityFrameworkCore;
using IDS325L___ProyectoFinal___Índice_académico.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<IndiceContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Asignaturas}/{action=Index}/{id?}");

app.Run();
