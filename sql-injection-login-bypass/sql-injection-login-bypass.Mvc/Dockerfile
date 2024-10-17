FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["sql-injection-login-bypass/sql-injection-login-bypass.csproj", "sql-injection-login-bypass/"]
RUN dotnet restore "sql-injection-login-bypass/sql-injection-login-bypass.csproj"
COPY . .
WORKDIR "/src/sql-injection-login-bypass"
RUN dotnet build "sql-injection-login-bypass.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "sql-injection-login-bypass.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "sql-injection-login-bypass.dll"]
