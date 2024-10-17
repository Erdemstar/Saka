FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["error-based-sql-injection-profile/error-based-sql-injection-profile.csproj", "error-based-sql-injection-profile/"]
RUN dotnet restore "error-based-sql-injection-profile/error-based-sql-injection-profile.csproj"
COPY . .
WORKDIR "/src/error-based-sql-injection-profile"
RUN dotnet build "error-based-sql-injection-profile.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "error-based-sql-injection-profile.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "error-based-sql-injection-profile.dll"]
