using Microsoft.EntityFrameworkCore;
using Universidade_Api.Models;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<UniversidadeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UniversidadeContext") ?? throw new InvalidOperationException("Connection string 'UniversidadeContext' not found.")));


builder.Services.AddControllers();
builder.Services.AddDbContext<AlunoContext>(opt =>
    opt.UseInMemoryDatabase("Aluno"));
builder.Services.AddDbContext<CursoContext>(opt =>
    opt.UseInMemoryDatabase("Curso"));

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();