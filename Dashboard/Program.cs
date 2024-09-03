using Dashboard.Components;
using Dashboard.Connect;
using Dashboard.Services;
using Dashboard.Services.Movies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services for dependency injection( a way to give a class the things it needs (its dependencies) from the outside)

// Adding CORS policy(lets your server decide which websites can access its resources from different domains.)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7262") // Replace with your Blazor app URL
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

// Registering Movies Services to be injected where needed
builder.Services.AddScoped<TopRatedMovies>();
builder.Services.AddScoped<PopularMovies>();
builder.Services.AddScoped<NowPlayingMovies>();
builder.Services.AddScoped<UpcomingMovies>();

// Registering CalendarService with HTTP client for making API requests
builder.Services.AddHttpClient<CalendarService>();
builder.Services.AddScoped<CalendarService>();


// Registering NewsService with HTTP client and setting the base address

builder.Services.AddHttpClient<NewsService>(client =>
{
    client.BaseAddress = new Uri("https://newsdata.io");
});
builder.Services.AddSingleton<NewsService>();


// Registering WeatherApiService with HTTP client and setting the base address for API requests
builder.Services.AddHttpClient<WeatherApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    // Optionally add other default headers or configurations
});

// Register user and login services
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<LoginService>();

//Admin Service

// Adding Blazor Bootstrap and Razor components for UI
builder.Services.AddBlazorBootstrap();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


// Configuring Entity Framework with SQL Server, using a connection string from configuration
builder.Services.AddDbContext<DashboardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));


// Add QuickGrid Entity Framework Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Middleware configuration for handling HTTP requests
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
    app.UseMigrationsEndPoint();
}
// Applying the CORS policy configured earlier
app.UseCors("AllowSpecificOrigins");

// Middleware for HTTPS redirection and serving static files
app.UseHttpsRedirection();
app.UseStaticFiles();

// Middleware for routing and authorization
app.UseRouting();
app.UseAuthorization();

// Middleware for antiforgery protection
app.UseAntiforgery();

// Map Razor components and configure server-side rendering mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
