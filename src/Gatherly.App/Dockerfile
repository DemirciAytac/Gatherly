
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443



FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gatherly.App/Gatherly.App.csproj", "src/Gatherly.App/"]
COPY ["src/Gatherly.Presentation/Gatherly.Presentation.csproj", "src/Gatherly.Presentation/"]
COPY ["src/Gatherly.Infrastructure/Gatherly.Infrastructure.csproj", "src/Gatherly.Infrastructure/"]
COPY ["src/Gatherly.Persistence/Gatherly.Persistence.csproj", "src/Gatherly.Persistence/"]
COPY ["src/Gatherly.Application/Gatherly.Application.csproj", "src/Gatherly.Application/"]
COPY ["src/Gatherly.Domain/Gatherly.Domain.csproj", "src/Gatherly.Domain/"]
RUN dotnet restore "./src/Gatherly.App/Gatherly.App.csproj"
COPY . .
WORKDIR "/src/src/Gatherly.App"
RUN dotnet build "./Gatherly.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Gatherly.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gatherly.App.dll"]