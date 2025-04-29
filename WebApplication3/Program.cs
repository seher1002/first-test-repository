

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;

var builder = WebApplication.CreateBuilder(args);

// 🔌 Configurare DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🌐 Acces extern pe portul 5253
builder.WebHost.UseUrls("http://0.0.0.0:5253");

// ✅ Adăugare Identity + Roluri
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>() // 👈 Activăm roluri
.AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<WebApplication3.Services.MapboxService>();
builder.Services.AddScoped<WebApplication3.Services.RouteOptimizer>(); // ✅ AICI


var app = builder.Build();

// 🔐 Seeding pentru utilizatorul Admin
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedAdmin(userManager, roleManager);
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // 👈 IMPORTANT!
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // 

app.Run();

// ✅ Funcție pentru creare Admin
static async Task SeedAdmin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
{
    string adminEmail = "admin@test.com";
    string adminPassword = "Admin123!";

    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var user = await userManager.FindByEmailAsync(adminEmail);
    if (user == null)
    {
        var newUser = new IdentityUser { UserName = adminEmail, Email = adminEmail };
        var result = await userManager.CreateAsync(newUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(newUser, "Admin");
        }
    }
}


