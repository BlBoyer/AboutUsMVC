using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using AboutUs.Data;
using AboutUs.Services;
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) => loggerConfiguration
    .WriteTo.Console());
builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("profile_context")));
builder.Services.AddDbContext<AddressContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("address_context")));

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IdentityProvider>();
builder.Services.AddTransient<SessionService>();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo{ Title = "AboutUs API", Description = "Api Testing for CRUD endpoints", Version = "v1" });
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI(options => 
options.SwaggerEndpoint("/swagger/v1/swagger.json", "AboutUs API V1"));
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseErrorHandler();
app.UseAuthHandler();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.Run();
