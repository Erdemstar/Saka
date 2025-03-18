FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
COPY mydatabase.db /tmp/mydatabase.db
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["stored-xss-comment.Mvc/stored-xss-comment.Mvc.csproj", "stored-xss-comment.Mvc/"]
COPY ["stored-xss-comment.Core/stored-xss-comment.Core.csproj", "stored-xss-comment.Core/"]
COPY ["stored-xss-comment.Infrastructure/stored-xss-comment.Infrastructure.csproj", "stored-xss-comment.Infrastructure/"]
RUN dotnet restore "stored-xss-comment.Mvc/stored-xss-comment.Mvc.csproj"
COPY . .
WORKDIR "/src/stored-xss-comment.Mvc"
RUN dotnet build "stored-xss-comment.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "stored-xss-comment.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "stored-xss-comment.Mvc.dll"]