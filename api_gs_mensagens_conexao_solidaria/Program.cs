using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using api_gs_mensagens_conexao_solidaria.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Conexão com MySQL - especificando versão para evitar erro ao gerar migrations
builder.Services.AddDbContext<ApplicationContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 36)) // Substitua por sua versão real do MySQL se necessário
    )
);

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Conexão Solidária API",
        Version = "v1",
        Description = "API para gerenciamento de mensagens de emergência com MySQL + MVC"
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
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Conexão Solidária API v1");
    c.RoutePrefix = "swagger";
});

// Aplica automaticamente as migrations na inicialização (pode ser removido em produção)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
    db.Database.Migrate();
}

app.Run();
