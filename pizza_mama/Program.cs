using System.Globalization;
using System.Web;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using pizza_mama.Data;

var builder = WebApplication.CreateBuilder(args);

var cultureInfo = new CultureInfo("fr-FR");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();


builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

/*builder.Services.AddDbContext<DataContext>(options =>
 options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnectionSqLite")));*/

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Administration ==> Page de login
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options =>
{
    options.LoginPath = "/Admin";
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
    app.UseExceptionHandler("/Home/Error");
}


// log errors 
if (string.IsNullOrWhiteSpace(app.Environment.WebRootPath))
{
    app.Environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
}
// Create log folder if not exists
if (!Directory.Exists(Path.Combine(app.Environment.WebRootPath, "logs"))) Directory.CreateDirectory(Path.Combine(app.Environment.WebRootPath, "logs"));


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Administration
app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();

app.Run();

