FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["client-side-restriction-file-extension/client-side-restriction-file-extension.csproj", "client-side-restriction-file-extension/"]
RUN dotnet restore "client-side-restriction-file-extension/client-side-restriction-file-extension.csproj"
COPY . .
WORKDIR "/src/client-side-restriction-file-extension"
RUN dotnet build "client-side-restriction-file-extension.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "client-side-restriction-file-extension.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "client-side-restriction-file-extension.dll"]
