FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["local-file-inclusion-null-byte.Mvc/local-file-inclusion-null-byte.Mvc.csproj", "local-file-inclusion-null-byte.Mvc/"]
COPY ["local-file-inclusion-null-byte.Core/local-file-inclusion-null-byte.Core.csproj", "local-file-inclusion-null-byte.Core/"]
RUN dotnet restore "local-file-inclusion-null-byte.Mvc/local-file-inclusion-null-byte.Mvc.csproj"
COPY . .
WORKDIR "/src/local-file-inclusion-null-byte.Mvc"
RUN dotnet build "local-file-inclusion-null-byte.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "local-file-inclusion-null-byte.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "local-file-inclusion-null-byte.Mvc.dll"]