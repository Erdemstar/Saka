FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["sql-injection-search.Mvc/sql-injection-search.Mvc.csproj", "sql-injection-search.Mvc/"]
RUN dotnet restore "sql-injection-search.Mvc/sql-injection-search.Mvc.csproj"
COPY . .
WORKDIR "/src/sql-injection-search.Mvc"
RUN dotnet build "sql-injection-search.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sql-injection-search.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sql-injection-search.Mvc.dll"]
