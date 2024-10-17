FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["command-injection-ping.Mvc/command-injection-ping.Mvc.csproj", "command-injection-ping.Mvc/"]
RUN dotnet restore "command-injection-ping.Mvc/command-injection-ping.Mvc.csproj"
COPY . .
WORKDIR "/src/command-injection-ping.Mvc"
RUN dotnet build "command-injection-ping.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "command-injection-ping.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "command-injection-ping.Mvc.dll"]
