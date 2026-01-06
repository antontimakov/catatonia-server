В `appsettings.Development.json` после секции `"Logging"` добавить:

```,
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=catatonia;Username=postgres;Password=__PASSWORD__"
  }
```
http://localhost:5074/swagger/index.html