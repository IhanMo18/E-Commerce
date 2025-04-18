
using Dashboard.Areas.Admin.Controllers;
using Dashboard.Filters;
using Dashboard.Models.Hub;
using Dashboard.Repository;
using Ecommerce.Data.Data;
using Ecommerce.Domain.Interface.Repository;
using Ecommerce.Domain.Interface.Service;
using Ecommerce.Domain.Models;
using Ecommerce.Repository.Repositories;
using Ecommerce.Services;
using Ecommerce.Services.CartService;
using Ecommerce.Services.CategoryService;
using Ecommerce.Services.ProductService;
using Ecommerce.Services.ReviewService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add(typeof(SessionIdCookieFilter));
});

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("PostgresConnection")));



builder.Services.AddIdentity<User, IdentityRole>(options => options.SignIn.RequireConfirmedAccount=false)
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<INotificationsRepository,NotificationsRepository>();
builder.Services.AddScoped<IOrderDetailRepository,OrderDetailRepository>();
builder.Services.AddScoped<IOrderRepository,OrderRepository>();
builder.Services.AddScoped<IReviewsRepository,ReviewsRepository>();
builder.Services.AddScoped<IUserRepository,UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<ICategoryService,CategoryService>();
builder.Services.AddScoped<IReviewService,ReviewService>();
builder.Services.AddSignalR();

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await AdminController.CreateRoles(services);
    await AdminController.MakeStaticSuperAdminAsync(services);
}
app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();


app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapHub<MessageHub>("/messageHub");
app.MapControllerRoute(
        name: "default",
        pattern: "{area=Client}/{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.Run();