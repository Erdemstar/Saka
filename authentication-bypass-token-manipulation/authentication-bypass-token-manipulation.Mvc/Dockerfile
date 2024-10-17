FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["authentication-bypass-token-manipulation.Mvc/authentication-bypass-token-manipulation.Mvc.csproj", "authentication-bypass-token-manipulation.Mvc/"]
RUN dotnet restore "authentication-bypass-token-manipulation.Mvc/authentication-bypass-token-manipulation.Mvc.csproj"
COPY . .
WORKDIR "/src/authentication-bypass-token-manipulation.Mvc"
RUN dotnet build "authentication-bypass-token-manipulation.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "authentication-bypass-token-manipulation.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authentication-bypass-token-manipulation.Mvc.dll"]
