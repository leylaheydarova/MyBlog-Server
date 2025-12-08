using Microsoft.EntityFrameworkCore;
using MyBlog.App.Contexts;
using MyBlog.App.Repositories.Implements;
using MyBlog.App.Repositories.Interfaces;
using MyBlog.App.Services.Implements;
using MyBlog.App.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option =>
option.UseNpgsql(builder.Configuration["ConnectionString:Default"]));

//Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Services
builder.Services.AddScoped<ICategoryService, CategoryService>();

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

app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
       name: "admin",
        areaName: "admin",
        pattern: "admin/{controller=AdminHome}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();
