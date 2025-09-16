# Base image لتشغيل التطبيق
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5000

# Base image لبناء المشروع
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# نسخ csproj فقط واستعادة الحزم لتسريع البناء
COPY ["FoodRush.API.csproj", "./"]
RUN dotnet restore "FoodRush.API.csproj"

# نسخ كل الملفات وبناء المشروع
COPY . .
RUN dotnet publish "FoodRush.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# المرحلة النهائية لتشغيل التطبيق
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "FoodRush.API.dll"]
