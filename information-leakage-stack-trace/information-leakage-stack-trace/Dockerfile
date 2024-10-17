FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["information-leakage-stack-trace/information-leakage-stack-trace.csproj", "information-leakage-stack-trace/"]
RUN dotnet restore "information-leakage-stack-trace/information-leakage-stack-trace.csproj"
COPY . .
WORKDIR "/src/information-leakage-stack-trace"
RUN dotnet build "information-leakage-stack-trace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "information-leakage-stack-trace.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "information-leakage-stack-trace.dll"]
