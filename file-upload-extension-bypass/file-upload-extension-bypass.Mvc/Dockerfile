FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["file-upload-extension-bypass.Mvc/file-upload-extension-bypass.Mvc.csproj", "file-upload-extension-bypass.Mvc/"]
RUN dotnet restore "file-upload-extension-bypass.Mvc/file-upload-extension-bypass.Mvc.csproj"
COPY . .
WORKDIR "/src/file-upload-extension-bypass.Mvc"
RUN dotnet build "file-upload-extension-bypass.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "file-upload-extension-bypass.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "file-upload-extension-bypass.Mvc.dll"]
