#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["reflected-xss-hidden-input.csproj", "."]
RUN dotnet restore "./reflected-xss-hidden-input.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "reflected-xss-hidden-input.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "reflected-xss-hidden-input.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "reflected-xss-hidden-input.dll"]