using ManageStock.Data;
using ManageStock.Data.Services.Categorie;
using ManageStock.Data.Services.EntreeStock;
using ManageStock.Data.Services.Entrepot;
using ManageStock.Data.Services.Fournisseur;
using ManageStock.Data.Services.Produit;
using ManageStock.Data.Services.SortieStock;
using ManageStock.Data.Services.Stock;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Add mon builder avec Postgressql
builder.Services.AddDbContext<ManageStockContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection")));

builder.Services.AddScoped<ICategorieService, CategorieService>();
builder.Services.AddScoped<IFournisseurService, FournisseurService>();
builder.Services.AddScoped<IEntrepotService, EntrepotService>();
builder.Services.AddScoped<IProduitService, ProduitService>();
builder.Services.AddScoped<IStockService, StockService>();
builder.Services.AddScoped<IEntreeStockService, EntreeStockService>();
builder.Services.AddScoped<ISortieStockService, SortieStockService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
