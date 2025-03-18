FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
COPY mydatabase.db /tmp/mydatabase.db
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["stored-xss-file-upload-comment.Mvc/stored-xss-file-upload-comment.Mvc.csproj", "stored-xss-file-upload-comment.Mvc/"]
COPY ["stored-xss-file-upload-comment.Core/stored-xss-file-upload-comment.Core.csproj", "stored-xss-file-upload-comment.Core/"]
COPY ["stored-xss-file-upload-comment.Infrastructure/stored-xss-file-upload-comment.Infrastructure.csproj", "stored-xss-file-upload-comment.Infrastructure/"]
RUN dotnet restore "stored-xss-file-upload-comment.Mvc/stored-xss-file-upload-comment.Mvc.csproj"
COPY . .
WORKDIR "/src/stored-xss-file-upload-comment.Mvc"
RUN dotnet build "stored-xss-file-upload-comment.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "stored-xss-file-upload-comment.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "stored-xss-file-upload-comment.Mvc.dll"]