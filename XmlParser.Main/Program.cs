using System.Globalization;

using XmlParser.Main.Mappers;
using XmlParser.Main.Models;
using XmlParser.Main.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IMapper<WebOrder, WebOrderViewModel>, WebOrderMapper>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

var cultureInfo = CultureInfo.CreateSpecificCulture("da-DK");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
