using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger = new LoggerConfiguration()
//    .ReadFrom.Configuration(builder.Configuration)
//    .CreateLogger();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Warning()
            .WriteTo.Console()
            .WriteTo.File("logs/myapp.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();


builder.Host.UseSerilog();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
