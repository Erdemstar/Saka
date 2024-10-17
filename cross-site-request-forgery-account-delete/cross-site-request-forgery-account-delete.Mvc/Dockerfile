FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["cross-site-request-forgery-account-delete.Mvc/cross-site-request-forgery-account-delete.Mvc.csproj", "cross-site-request-forgery-account-delete.Mvc/"]
RUN dotnet restore "cross-site-request-forgery-account-delete.Mvc/cross-site-request-forgery-account-delete.Mvc.csproj"
COPY . .
WORKDIR "/src/cross-site-request-forgery-account-delete.Mvc"
RUN dotnet build "cross-site-request-forgery-account-delete.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "cross-site-request-forgery-account-delete.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "cross-site-request-forgery-account-delete.Mvc.dll"]
