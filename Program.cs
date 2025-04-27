using Farmify_Api;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(db => 
{ db.UseSqlServer(builder.Configuration.GetConnectionString("Connection")); });
builder.Services.AddControllers();
builder.Services.AddCors(o =>
{
    o.AddPolicy("AllowAll",
        builder => builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()
        );
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();  

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
