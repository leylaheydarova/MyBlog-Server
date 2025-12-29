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
builder.Services.AddSession();
//Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteItemRepository, FavoriteItemRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

//Services
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddScoped<IFavoriteItemService, FavoriteItemService>();
builder.Services.AddScoped<ICommentService, CommentService>();

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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(name: "Default",
    pattern: "/{controller=Home}/{action=Index}/{Id?}");

app.Run();
