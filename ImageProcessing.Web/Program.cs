using ImageProcessing.Data.DataContext;
using ImageProcessing.Data.Interface;
using ImageProcessing.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMvcCore();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();   
builder.Services.AddDbContext<ImageProcessingDBContext>(options => options.UseSqlServer(builder.Configuration
    .GetConnectionString("IPConectionString")));
 

builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
    options.AppendTrailingSlash = true;
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
app.UseAuthentication();;

app.UseAuthorization();

app.MapAreaControllerRoute(
            name: "MyAreaAdmin",
            areaName: "Admin",
            pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//   name: "areas",
//      pattern: "{area:}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Signin}/{id?}");

app.MapRazorPages();    

app.Run();
