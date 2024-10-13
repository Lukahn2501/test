using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MovieRanker.Business;
using MovieRanker.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services au conteneur.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurer la chaîne de connexion et enregistrer le contexte de la base de données
builder.Services.AddDbContext<MovieRankerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer")));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Enregistrer les services business
builder.Services.AddScoped<PersonBusiness>();
builder.Services.AddScoped<MovieBusiness>();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configurer le pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

#region Seed Data (FOR TESTING PURPOSES ONLY)
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    var roles = new[] { "Admin", "Contributor", "Spectator" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var userTest = await userManager.FindByNameAsync("cont@cont.cont");
    await userManager.RemoveFromRoleAsync(userTest!, "Admin");

    var users = new[]
    {
        new { Username = "admin@admin.admin", Password = "adminP@ssw0rd", Role = "Admin" },
        new { Username = "cont@cont.cont", Password = "contP@ssw0rd", Role = "Contributor"  },
        new { Username = "spec@spec.spec", Password = "specP@ssw0rd", Role = "Spectator"  }
    };

    foreach(var userSeed in users)
    {
        var user = await userManager.FindByNameAsync(userSeed.Username);
        if (user == null)
        {
            user = new IdentityUser { UserName = userSeed.Username, Email = userSeed.Username };
            var result = await userManager.CreateAsync(user, userSeed.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, userSeed.Role);
            }
        }
        else
        {
            if (!await userManager.IsInRoleAsync(user, userSeed.Role))
            {
                await userManager.AddToRoleAsync(user, userSeed.Role);
            }
        }
    }
}
#endregion

app.Run();