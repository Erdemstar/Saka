FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["command-injection-image-resize.Mvc/command-injection-image-resize.Mvc.csproj", "command-injection-image-resize.Mvc/"]
RUN dotnet restore "command-injection-image-resize.Mvc/command-injection-image-resize.Mvc.csproj"
COPY . .
WORKDIR "/src/command-injection-image-resize.Mvc"
RUN dotnet build "command-injection-image-resize.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "command-injection-image-resize.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "command-injection-image-resize.Mvc.dll"]
