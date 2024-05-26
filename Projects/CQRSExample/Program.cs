using CQRSExample.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using CQRSExample.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// veritabanına bağlanma 
Console.WriteLine("Database açılıyor.");
builder.Services.AddDbContext<BookShopContext>(opt =>
{
    opt.UseSqlServer("Data Source=localhost;Initial Catalog=SQRS_Example;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
    opt.UseLoggerFactory(LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
        builder.SetMinimumLevel(LogLevel.Warning); //açılıştaki bir sürü konsol çıktısını önlemek için
    }));
    opt.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });


//mediatr ekleme
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));




// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//varsa veritabanını siler ve yeniden üretir
using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<BookShopContext>())
{
   if(context == null) { Console.WriteLine("context boş"); }
   else
   {
       if (await context.Database.CanConnectAsync())
       {
           Console.WriteLine("Database Siliniyor");
           await context.Database.EnsureDeletedAsync();
           Console.WriteLine("Database Slindi");
           Console.WriteLine("Database Üretiliyor");
           await context.Database.EnsureCreatedAsync();
           Console.WriteLine("Database Üretildi");
       }
       else
       {
           Console.WriteLine("Database bağlanılamadı.");
       }
   }

}


// yeniden üretilen veritabanını doldurur
using (var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<BookShopContext>())
{
   Console.WriteLine("Database seed işlemi başlıyor.");
   if (await context.Database.CanConnectAsync())
   {
       Random random = new Random();
       List<Author> authors = new List<Author>();
       for (int i = 0; i < 100; i++)
       {
           authors.Add(new Author { Name = $"Author {i}" });
       }
       await context.Authors.AddRangeAsync(authors);
       await context.SaveChangesAsync();
       List<Book> books = new List<Book>();
       for (int i = 0; i < 1000; i++)
       {
           books.Add(new Book { Title = $"Title {i}", AuthorId = random.Next(1, 99) });
       }
       await context.Books.AddRangeAsync(books);
       await context.SaveChangesAsync();
   }
}



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
