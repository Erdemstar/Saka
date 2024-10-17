FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["css-injection-input/css-injection-input.csproj", "css-injection-input/"]
RUN dotnet restore "css-injection-input/css-injection-input.csproj"
COPY . .
WORKDIR "/src/css-injection-input"
RUN dotnet build "css-injection-input.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "css-injection-input.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "css-injection-input.dll"]
