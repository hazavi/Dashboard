using Dashboard.Components;
using Dashboard.Connect;
using Dashboard.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Services Configuration
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

//Calendar API
builder.Services.AddHttpClient<CalendarService>();
builder.Services.AddScoped<CalendarService>();


// NewsAPI Configuration

builder.Services.AddHttpClient<NewsService>(client =>
{
    client.BaseAddress = new Uri("https://newsdata.io");
});
builder.Services.AddSingleton<NewsService>();


// WeatherAPI Configuration
builder.Services.AddHttpClient<WeatherApiService>(client =>
{
    client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");
    // Optionally add other default headers or configurations
});

// Register services
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<LoginService>();

// Bootstrap and Blazor Components
builder.Services.AddBlazorBootstrap();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Database Context Configuration
builder.Services.AddDbContext<DashboardDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

// Add QuickGrid Entity Framework Adapter
builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseCors("AllowSpecificOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
