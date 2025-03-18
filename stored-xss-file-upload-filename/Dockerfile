FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
COPY mydatabase.db /tmp/mydatabase.db
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["stored-xss-file-upload-filename.Mvc/stored-xss-file-upload-filename.Mvc.csproj", "stored-xss-file-upload-filename.Mvc/"]
COPY ["stored-xss-file-upload-filename.Core/stored-xss-file-upload-filename.Core.csproj", "stored-xss-file-upload-filename.Core/"]
COPY ["stored-xss-file-upload-filename.Infrastructure/stored-xss-file-upload-filename.Infrastructure.csproj", "stored-xss-file-upload-filename.Infrastructure/"]
RUN dotnet restore "stored-xss-file-upload-filename.Mvc/stored-xss-file-upload-filename.Mvc.csproj"
COPY . .
WORKDIR "/src/stored-xss-file-upload-filename.Mvc"
RUN dotnet build "stored-xss-file-upload-filename.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "stored-xss-file-upload-filename.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "stored-xss-file-upload-filename.Mvc.dll"]