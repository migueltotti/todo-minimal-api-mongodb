FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["to-do-minimal-api-mongodb/to-do-minimal-api-mongodb.csproj", "to-do-minimal-api-mongodb/"]
RUN dotnet restore "to-do-minimal-api-mongodb/to-do-minimal-api-mongodb.csproj"
COPY . .
WORKDIR "/src/to-do-minimal-api-mongodb"
RUN dotnet build "to-do-minimal-api-mongodb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "to-do-minimal-api-mongodb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "to-do-minimal-api-mongodb.dll"]
