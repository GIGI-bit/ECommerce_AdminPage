using ECommerce.Business.Abstract;
using ECommerce.Business.Concrete;
using ECommerce.DataAccess.Abstraction;
using ECommerce.DataAccess.Concrete.EFEntityFramework;
using ECommerce.Entities.Models;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IProductDal, EFProductDal>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryDal, EFCategoryDal>();
builder.Services.AddScoped<IOrderDetailsDal, EFOrderDetailDal>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartSessionService, CartSessionService>();

builder.Services.AddSession();

var con = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<NorthwindContext>(opt =>
{
    opt.UseSqlServer(con);
});


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
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
