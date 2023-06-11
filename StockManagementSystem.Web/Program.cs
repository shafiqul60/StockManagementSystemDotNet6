using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Core.Domains.IRepositories;
using StockManagementSystem.Core.IServices;
using StockManagementSystem.Core.Security;
using StockManagementSystem.Core.Services;
using StockManagementSystem.Infrastructure.DbContext;
using StockManagementSystem.Infrastructure.Repositories;
using StockManagementSystem.Web.Configuration;
using WebMarkupMin.AspNetCore6;

var builder = WebApplication.CreateBuilder(args);


// Html minification.
builder.Services.AddWebMarkupMin().AddHtmlMinification(option =>
{
    option.MinificationSettings.RemoveRedundantAttributes = true;
    option.MinificationSettings.MinifyInlineJsCode = true;
    option.MinificationSettings.MinifyInlineCssCode = true;
}).AddXmlMinification().AddHttpCompression();

 

// Hide the version information.
builder.WebHost.ConfigureKestrel(serverOption =>
{
    serverOption.AddServerHeader = false;
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
     .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddControllersWithViews();



// Notification
builder.Services.AddNotyf(config => { config.DurationInSeconds = 15; config.IsDismissable = true; config.Position = NotyfPosition.TopRight; });




//Services
builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<ISupplierRepo, SupplierRepo>();
builder.Services.AddScoped<ISupplierService, SupplierService>();

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();


builder.Services.AddScoped<ICustomerRepo, CustomerRepo>();
builder.Services.AddScoped<ICustomerService, CustomerService>();



builder.Services.AddSingleton<DataProtectionPurposeString>();


builder.Services.AddScoped<ICustomerProductPriceRepo, CustomerProductPriceRepo>();
builder.Services.AddScoped<ICustomerProductPriceService, CustomerProductPriceService>();


//AutoMapper Config
builder.Services.AddAutoMapper(typeof(MapConfig));



//If you receive an error regarding the IActionContextAccessor dependency, you should register it in the Program or Startup class:
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseWebMarkupMin();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();



app.Run();
