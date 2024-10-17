FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["open-url-redirect.Mvc/open-url-redirect.Mvc.csproj", "open-url-redirect.Mvc/"]
RUN dotnet restore "open-url-redirect.Mvc/open-url-redirect.Mvc.csproj"
COPY . .
WORKDIR "/src/open-url-redirect.Mvc"
RUN dotnet build "open-url-redirect.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "open-url-redirect.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "open-url-redirect.Mvc.dll"]
