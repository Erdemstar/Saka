FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["file-upload-basic.Mvc/file-upload-basic.Mvc.csproj", "file-upload-basic.Mvc/"]
RUN dotnet restore "file-upload-basic.Mvc/file-upload-basic.Mvc.csproj"
COPY . .
WORKDIR "/src/file-upload-basic.Mvc"
RUN dotnet build "file-upload-basic.Mvc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "file-upload-basic.Mvc.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "file-upload-basic.Mvc.dll"]
