FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["server-side-request-forgery-health-check.Mvc/server-side-request-forgery-health-check.Mvc.csproj", "server-side-request-forgery-health-check.Mvc/"]
RUN dotnet restore "server-side-request-forgery-health-check.Mvc/server-side-request-forgery-health-check.Mvc.csproj"
COPY . .
WORKDIR "/src/server-side-request-forgery-health-check.Mvc"
RUN dotnet build "server-side-request-forgery-health-check.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "server-side-request-forgery-health-check.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "server-side-request-forgery-health-check.Mvc.dll"]
