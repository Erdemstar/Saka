FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["information-leakage-js-file/information-leakage-js-file.csproj", "information-leakage-js-file/"]
RUN dotnet restore "information-leakage-js-file/information-leakage-js-file.csproj"
COPY . .
WORKDIR "/src/information-leakage-js-file"
RUN dotnet build "information-leakage-js-file.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "information-leakage-js-file.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "information-leakage-js-file.dll"]
