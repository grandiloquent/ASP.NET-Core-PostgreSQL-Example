# ASP.NET Core PostgreSQL Example


## 初始化数据库

1. 打开 `appsettings.json`,修改:

        "DefaultConnection": "Server=127.0.0.1;Port=5432;Database=npgsql_test;User Id=postgres;Password=postgres;CommandTimeout=20;"

2. 执行命令:

        dotnet ef migrations add Initial -v
        dotnet ef database update
        # 或
        dotnet ef database drop --force --context EFDatabaseContext && dotnet ef migrations remove -c EfDatabaseContext && dotnet ef migrations add Initial -v -c EfDatabaseContext && dotnet ef database update --context EFDatabaseContext
        
## 依赖项

* [Npgsql](https://github.com/npgsql/npgsql)
