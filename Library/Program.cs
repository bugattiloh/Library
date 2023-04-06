using Library.Data;
using Library.Middleware;
using Library.Repositories.Books;
using Library.Repository;
using Library.Services;
using Library.Services.Book;
using Library.Services.Books;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<LibraryContext>(options => options.UseNpgsql(builder.Configuration["ConnectionString"]));

builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IReaderService, ReaderService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();