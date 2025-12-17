using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using CatatoniaServer.Models;


var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();
app.Urls.Add("http://0.0.0.0:5074");

// Обслуживание статических файлов с расширенными MIME-типами
app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider
});

// Обслуживание статических файлов из папки wwwroot
app.UseDefaultFiles(); // Поддержка default-файлов (index.html, default.html и т.д.)
app.UseStaticFiles();  // Отдача статических файлов (CSS, JS, изображения)

app.MapPost("/getdb", async (ApplicationDbContext db, HttpContext context) => 
{
    RequestModel? request = await context.Request.ReadFromJsonAsync<RequestModel>();

    if (request == null)
    {
        return Results.BadRequest("Не удалось прочитать данные");
    }
    // Данные уже автоматически десериализованы
    string? did = request.did;
    string? time_fishing = request.time_fishing;

    Console.WriteLine($"Получено: Id={did}, Action={time_fishing}");

    try
    {
        // Проверяем подключение к БД
        var canConnect = await db.Database.CanConnectAsync();
        if (!canConnect)
            return Results.Problem("Не удалось подключиться к базе данных", statusCode: 500);

        var result = await db.field_elem
            .Where(fe => fe.field_id == 2)
            .Select(fe => new
            {
                fe.elem.elem_name,
                fe.x,
                fe.y
            })
            .ToListAsync();

        // Возвращаем как JSON
        var response = new {
            time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
            status = "ok",
            received = result
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Ошибка: {ex.Message}", statusCode: 500);
    }
});
app.MapPost("/setdb", async (ApplicationDbContext db, HttpContext context) =>
{
    // Читаем и десериализуем JSON
    var request = await context.Request.ReadFromJsonAsync<SetDbRequest>();
    if (request == null){
        return Results.BadRequest("Некорректные данные");
    }
    Console.WriteLine($"x={request.elem_id}");

    try
    {
        // Находим запись в field_elem по координатам x и y
        var fieldElem = await db.field_elem
            .FirstOrDefaultAsync(fe => fe.x == request.x && fe.y == request.y);

        if (fieldElem == null)
            return Results.NotFound($"Элемент с координатами ({request.x}, {request.y}) не найден");

        // Обновляем elem_id
        fieldElem.elem_id = request.elem_id;

        // Сохраняем изменения в БД
        await db.SaveChangesAsync();

        return Results.Ok(new
        {
            message = "Обновлено успешно",
            updated = new { request.x, request.y, request.elem_id }
        });
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка в /setdb: " + ex);
        return Results.Problem($"Ошибка при обновлении: {ex.Message}", statusCode: 500);
    }
});



// Включаем просмотр директорий (только для разработки!)
// Например: при запросе к /images отображается список файлов
// ВНИМАНИЕ: Небезопасно в продакшене — лучше отключить
// app.UseFileServer(enableDirectoryBrowsing: true); — альтернатива, объединяющая UseDefaultFiles, UseStaticFiles и UseDirectoryBrowser

// Маршрутизация SPA: все нераспознанные запросы направляются в index.html
// Это необходимо для клиентских маршрутов (например, Vue Router, React Router в режиме history)
app.MapFallbackToFile("index.html");

app.Run();
