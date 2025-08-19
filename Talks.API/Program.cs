using Application._Interfaces.Repositories;
using Application._Interfaces.Services;
using Application.Handlers;
using Application.Services;
using Infrastructure.Contexts;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Add Entity Framework
builder.Services.AddDbContext<TalksDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TalksDb")));

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(AddSpeakerCommandHandler).Assembly);
});

builder.Services.AddScoped<ISessionApprovalService, SessionApprovalService>();

builder.Services.AddScoped<ISpeakerValidationService, SpeakerValidationService>();
builder.Services.AddScoped<ISpeakerPricingService, SpeakerPricingService>();

builder.Services.AddScoped<ISpeakerRepository, SpeakerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();