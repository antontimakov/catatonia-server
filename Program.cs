using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавьте строку подключения к PostgreSQL
var connectionString = "Host=localhost;Port=5432;Database=nodedemo;Username=postgres;Password=1";

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
    var request = context.Request.ReadFromJsonAsync<RequestModel>().Result;
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

        // Получаем все фильмы (только поле Title)
        var films = await db.films
            .OrderBy(f => f.id)
            .Select(f => new { f.did, f.time_fishing })
            .FirstOrDefaultAsync();

        // Возвращаем как JSON
        var response = new {
            time = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.000Z"),
            status = "ok",
            received = films
        };

        return Results.Ok(response);
    }
    catch (Exception ex)
    {
        return Results.Problem($"Ошибка: {ex.Message}", statusCode: 500);
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

// ... остальной код ...

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    // Пример таблицы: если у вас есть таблица "Items"
    public DbSet<Film> films { get; set; }
}

public class Film
{
    public int id { get; set; }
    public string title { get; set; }
    public int? did { get; set; }
    public DateTime? time_fishing { get; set; }
}

// RequestModel.cs
public class RequestModel
{
    public string? did { get; set; }
    public string? time_fishing { get; set; }
}
