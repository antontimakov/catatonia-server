using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using CatatoniaServer.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Считываем строку подключения из appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(connectionString));

// Добавляем поддержку MIME-типов для файлов Unity
var provider = new FileExtensionContentTypeProvider();
provider.Mappings[".data"] = "application/octet-stream";
provider.Mappings[".wasm"] = "application/wasm";
provider.Mappings[".js"] = "application/javascript";

// Добавляем сервис для отображения содержимого директорий (если нужно)
builder.Services.AddDirectoryBrowser(); // Убедитесь, что это необходимо в продакшене
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Urls.Add("http://0.0.0.0:5074");

// Обслуживание статических файлов с расширенными MIME-типами
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// Обслуживание статических файлов из папки wwwroot
app.UseDefaultFiles(); // Поддержка default-файлов (index.html, default.html и т.д.)
app.UseStaticFiles();  // Отдача статических файлов (CSS, JS, изображения)

// Подключаем маршрутизацию для MVC
app.MapControllers();

// Маршрутизация SPA: все нераспознанные запросы направляются в index.html
// Это необходимо для клиентских маршрутов (например, Vue Router, React Router в режиме history)
app.MapFallbackToFile("index.html");

app.Run();
