FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["authentication-bypass-session-fixation/authentication-bypass-session-fixation.csproj", "authentication-bypass-session-fixation/"]
RUN dotnet restore "authentication-bypass-session-fixation/authentication-bypass-session-fixation.csproj"
COPY . .
WORKDIR "/src/authentication-bypass-session-fixation"
RUN dotnet build "authentication-bypass-session-fixation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "authentication-bypass-session-fixation.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "authentication-bypass-session-fixation.dll"]
