using API.Extensions;
using BusinessLogic.JoggingTracker;
using Contracts.GlobalExeptionHandler;
using DataAccess.SQL.ApplicationDbContext;
using DataAccess.SQL.Entities;
using DataAccess.SQL.UnitOfWork;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(GlobalExceptionHandler));
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJoggingTrackerManager, JoggingTrackerManager>();
builder.Services.AddMvc()
    .AddNewtonsoftJson();
builder.Services.AddDbContextFactory<AppDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services
    .AddIdentity<UserEntity, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.WithOrigins("*");
    builder.AllowAnyHeader();
    builder.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<UserEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await SeedData.InitializeRoles(roleManager);
        await SeedData.Initialize(userManager);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();
