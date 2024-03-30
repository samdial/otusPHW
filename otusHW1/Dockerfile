# Уточняем базовый образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY otusHW1.csproj .
RUN dotnet restore

COPY . .
RUN dotnet build -c Release -o /app/build

# Опубликуем приложение
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS final
WORKDIR /app

# Копируем сборку из предыдущего этапа
COPY --from=publish /app/publish .

# Указываем точку входа для запуска приложения
ENTRYPOINT ["dotnet", "otusHW1.dll"]
