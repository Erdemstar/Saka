FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["css-injection-input.Mvc/css-injection-input.Mvc.csproj", "css-injection-input.Mvc/"]
RUN dotnet restore "css-injection-input.Mvc/css-injection-input.Mvc.csproj"
COPY . .
WORKDIR "/src/css-injection-input.Mvc/"
RUN dotnet build "css-injection-input.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "css-injection-input.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "css-injection-input.Mvc.dll"]
