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

// Enregistrer les services business
builder.Services.AddScoped<PersonBusiness>();
builder.Services.AddScoped<MovieBusiness>();

var app = builder.Build();

// Configurer le pipeline HTTP.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();