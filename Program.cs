using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Muntean_Radu_Lab2.Data;
using Microsoft.AspNetCore.Identity;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<Muntean_Radu_Lab2Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Muntean_Radu_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Muntean_Radu_Lab2Context' not found.")));

builder.Services.AddDbContext<LibraryIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Muntean_Radu_Lab2Context") ?? throw new InvalidOperationException("Connection string 'Muntean_Radu_Lab2Context' not found.")));
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LibraryIdentityContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
