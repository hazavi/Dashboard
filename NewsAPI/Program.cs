using NewsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddHttpClient<NewsService>();

// Register the NewsService and configure the HttpClient
builder.Services.AddHttpClient<NewsService>(client =>
{
    client.BaseAddress = new Uri("https://api.worldnewsapi.com"); // Base address for the API
    client.DefaultRequestHeaders.Add("x-api-key", builder.Configuration["NewsApiKey"]); // API key from configuration
});

// Add Swagger/OpenAPI documentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS (if needed)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS
app.UseCors();

// Enable HTTPS redirection
app.UseHttpsRedirection();

// Enable authorization (if you have authentication/authorization in place)
app.UseAuthorization();

// Map controllers to endpoints
app.MapControllers();

app.Run();
