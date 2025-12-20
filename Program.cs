using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
//using System.ComponentModel.DataAnnotations;
using CatatoniaServer.Services;
using CatatoniaServer.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры
builder.Services.AddControllers();

// Подключаем БД
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
// Регистрируем сервис
builder.Services.AddScoped<FillFieldService>();
// Регистрируем репозиторий
builder.Services.AddScoped<FillFieldRepository>();

// Добавляем Swagger (только для разработки)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем Swagger только в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Настройка MIME-типов для Unity
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".js"] = "application/javascript";

// Обслуживание статики
app.UseDefaultFiles(); // Поддержка default-файлов (index.html, default.html и т.д.)
app.UseStaticFiles();  // Отдача статических файлов (CSS, JS, изображения)
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// Маршрутизация
app.MapControllers();

// Маршрутизация SPA: все нераспознанные запросы направляются в index.html
// Это необходимо для клиентских маршрутов (например, Vue Router, React Router в режиме history)
app.MapFallbackToFile("index.html");

app.Run();
