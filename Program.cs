using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

var builder = WebApplication.CreateBuilder(args);

// Добавьте строку подключения к PostgreSQL
var connectionString = "Host=localhost;Port=5432;Database=catatonia;Username=postgres;Password=1";

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
    var request = await context.Request.ReadFromJsonAsync<RequestModel>();
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


// Включаем просмотр директорий (только для разработки!)
// Например: при запросе к /images отображается список файлов
// ВНИМАНИЕ: Небезопасно в продакшене — лучше отключить
// app.UseFileServer(enableDirectoryBrowsing: true); — альтернатива, объединяющая UseDefaultFiles, UseStaticFiles и UseDirectoryBrowser

// Маршрутизация SPA: все нераспознанные запросы направляются в index.html
// Это необходимо для клиентских маршрутов (например, Vue Router, React Router в режиме history)
app.MapFallbackToFile("index.html");

app.Run();


public class ApplicationDbContext : DbContext
{
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Настройка связей между таблицами
        modelBuilder.Entity<Field_elem>()
            .HasKey(fe => new { fe.field_id, fe.elem_id });

        modelBuilder.Entity<Field_elem>()
            .HasOne(fe => fe.field)
            .WithMany(f => f.field_elems)
            .HasForeignKey(fe => fe.field_id);

        modelBuilder.Entity<Field_elem>()
            .HasOne(fe => fe.elem)
            .WithMany(e => e.field_elems)
            .HasForeignKey(fe => fe.elem_id);

        // Можно добавить другие настройки...
    }

    public DbSet<Elem> elem { get; set; }
    public DbSet<Field> field { get; set; }
    public DbSet<Field_elem> field_elem { get; set; }
}

// RequestModel.cs
public class RequestModel
{
    public string? did { get; set; }
    public string? time_fishing { get; set; }
}
public class Elem
{
    [Key]
    public int elem_id { get; set; }
    public required string elem_name { get; set; }
    public ICollection<Field_elem> field_elems { get; set; }
}
public class Field
{
    [Key]
    public int field_id { get; set; }
    public required string field_name { get; set; }
    public ICollection<Field_elem> field_elems { get; set; }
}
public class Field_elem
{
    [Key]
    public int field_elem_id { get; set; }
    public required int elem_id { get; set; }
    public required int field_id { get; set; }
    public required int x { get; set; }
    public required int y { get; set; }
    public required int field_order { get; set; }

    // Навигационные свойства
    public required Field field { get; set; }
    public required Elem elem { get; set; }
}

