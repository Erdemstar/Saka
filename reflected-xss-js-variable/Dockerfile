MAINTAINER Erdemstar <erdem-yildiz@windowslive.com>

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["reflected-xss-js-variable.csproj", "."]
RUN dotnet restore "./reflected-xss-js-variable.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "reflected-xss-js-variable.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "reflected-xss-js-variable.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "reflected-xss-js-variable.dll"]