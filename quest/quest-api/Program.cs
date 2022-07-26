using Microsoft.EntityFrameworkCore;
using quest_api.Models;
using quest_api.Services;
using quest_entity;

var builder = WebApplication.CreateBuilder(args);

// Add quest configuration
builder.Configuration.AddJsonFile("quest-configuration.json", false, true);
var questSettings = builder.Configuration.GetSection("QuestConfiguration")
            .Get<QuestConfiguration>(options => options.BindNonPublicProperties = true);

var milestoneSettings = builder.Configuration.GetSection("QuestConfiguration").GetSection("Milestones").Get<MilestoneConfiguration[]>();

builder.Services.AddSingleton(questSettings);
builder.Services.AddSingleton(milestoneSettings);

// Add database
builder.Services.AddDbContext<QuestContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add DI
builder.Services.AddScoped<IPlayerService, PlayerService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka .ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
