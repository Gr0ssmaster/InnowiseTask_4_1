using AuthorsWebAPI.Data;
using AuthorsWebAPI.MIddleWare;
using AuthorsWebAPI.Services.AuthorService;
using AuthorsWebAPI.Services.BookService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DataContainer>();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddSingleton<IBookService, BookService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
