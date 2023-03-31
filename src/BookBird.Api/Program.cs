using BookBird.Api;
using BookBird.Application;
using BookBird.Application.IntegrationEvents;
using BookBird.Infrastructure;
using BookBird.Infrastructure.Extensions;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((ctx, cfg) => cfg.ReadFrom.Configuration(ctx.Configuration));
ValidatorOptions.Global.LanguageManager.Enabled = false;

builder.Services
    .AddControllers()
    .AddJsonOptions(opt => opt.AddCustomConverters());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.AddCustomTypeMappings();
    opt.AddCustomFilters();
});

builder.Services
    .AddApplication(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApi()
    .AddMassTransit(x =>
    {
        x.UseRabbit(builder.Configuration);
        EndpointConventions.AddConventions(builder.Configuration);
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCustomMiddlewares();
app.UseAuthorization();
app.MapControllers();

app.RunWithLogging();
