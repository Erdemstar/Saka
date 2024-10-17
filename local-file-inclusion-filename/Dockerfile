FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["local-file-inclusion-filename.Mvc/local-file-inclusion-filename.Mvc.csproj", "local-file-inclusion-filename.Mvc/"]
COPY ["local-file-inclusion-filename.Core/local-file-inclusion-filename.Core.csproj", "local-file-inclusion-filename.Core/"]
RUN dotnet restore "local-file-inclusion-filename.Mvc/local-file-inclusion-filename.Mvc.csproj"
COPY . .
WORKDIR "/src/local-file-inclusion-filename.Mvc"
RUN dotnet build "local-file-inclusion-filename.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "local-file-inclusion-filename.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "local-file-inclusion-filename.Mvc.dll"]