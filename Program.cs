using Microsoft.EntityFrameworkCore;
using System.Collections;
using Microsoft.OpenApi.Models;
using AboutUs.Data;
using AboutUs.Services;
using System.Text.Json;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProfileContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data:profile_context:ConnectionString")));
builder.Services.AddDbContext<AddressContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Data:address_context:ConnectionString")));

// Add services to the container.
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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
} else {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//custom error routing
app.Use(async (context, next) =>
	{
		await next.Invoke();
		//Pass all successful http response codes:
        HttpResponse response = context.Response;
        if (response.StatusCode >= 400)
        {
            Dictionary<int, string> StatusCodes = new Dictionary<int, string>
            {
                {400, "Bad Request, Please go back and check your details."},
                {401, "Unauthorized Access. You don't have permissions for this content."},
                {403, "Forbidden by server."},
                {404, "Page Not Found. Try again."},
                {405, "Not Allowed Here."},
                {408, "Timed-Out!"},
                {410, "This resource is no longer available!"},
                {413, "Oversized Request"},
                {415, "Media Type Not Acceptable"},
                {426, "Outdated Protocol, Upgrade your browser."},
                {429, "Too Many Requests!"},
                {500, "Internal Error"},
                {501, "Not Supported"},
                {503, "Unavailable"},
                {504, "Gateway Timed-Out!"},
                {507, "Oops..No more room!"},
                {700, "Username Not Found. If you aren't a user, please create and account."},
                {701, "Oops...! Your credentials are invalid."},
                {702, "Oops...! This username already exists. Are you trying to sign-in?"}
            };
            string msgStr = "Internal Access Error.";
            var value="";
            if(StatusCodes.TryGetValue(response.StatusCode, out value))
            {
                msgStr = value;
            }
            string msg = new SecureAddress().singleCode(msgStr);
            context.Response.Redirect($"https://aboutusmvc.azurewebsites.net/Home/ErrorPage?code={response.StatusCode}&msg={msg}", false);
            return;
        }
        Console.WriteLine($"Using address: {context.Connection.RemoteIpAddress}");
	});
//ip getter
app.Use(async (context, next) => {
    await next.Invoke();
    SessionService.remoteIp = context.Connection.RemoteIpAddress.ToString();
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );
app.Run();
