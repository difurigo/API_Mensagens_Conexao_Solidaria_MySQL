using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using api_gs_mensagens_conexao_solidaria.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Conex�o com MySQL - especificando vers�o para evitar erro ao gerar migrations
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // Substitua por sua vers�o real do MySQL se necess�rio
    )
);

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Conex�o Solid�ria API",
        Version = "v1",
        Description = "API para gerenciamento de mensagens de emerg�ncia com MySQL + MVC"
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conex�o Solid�ria API v1");
    c.RoutePrefix = "swagger";
});

// Aplica automaticamente as migrations na inicializa��o (pode ser removido em produ��o)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
}

app.Run();
