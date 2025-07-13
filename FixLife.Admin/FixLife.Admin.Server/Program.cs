using FixLife.Admin.Db;
using FixLife.Admin.Db.Exceptions;
using FixLife.Admin.Users.Abstraction;
using FixLife.Admin.Users.Implementation;
using FixLife.Admin.Users.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connString = builder.Configuration.GetConnectionString("AdminDB")
    ?? throw new ConnectionStringException("AdminDB");

builder.Services.AddDatabase(connString);

// Register HttpClient for API calls
builder.Services.AddHttpClient();

// Register Admin services
builder.Services.AddScoped<IClientUserService, ClientUserService>();
builder.Services.AddScoped<HttpClientService>();

builder.Services.AddAuthentication()
    .AddCookie("AuthCookie", options =>
    {
        options.Cookie.Name = "AuthCookie";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/access-denied";
        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization(options =>
    options.AddPolicy("AdminRole", policy =>
    policy.RequireRole("Admin")));

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

//TODO: Prepare enpoint classes to identity

app.MapFallbackToFile("/index.html");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
