FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["authentication-bypass-cookie.Mvc/authentication-bypass-cookie.Mvc.csproj", "authentication-bypass-cookie.Mvc/"]
RUN dotnet restore "authentication-bypass-cookie.Mvc/authentication-bypass-cookie.Mvc.csproj"
COPY . .
WORKDIR "/src/authentication-bypass-cookie.Mvc"
RUN dotnet build "authentication-bypass-cookie.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "authentication-bypass-cookie.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authentication-bypass-cookie.Mvc.dll"]
