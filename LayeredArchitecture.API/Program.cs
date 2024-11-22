using LayeredArchitecture.API.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<HandleErrorMiddleware>();
if (!app.Environment.IsDevelopment())
{
    app.UseMiddleware<CustomAuthorizationMiddleware>();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
