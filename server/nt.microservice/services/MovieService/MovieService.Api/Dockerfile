#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["services/MovieService/MovieService.Api/MovieService.Api.csproj", "services/MovieService/MovieService.Api/"]
COPY ["services/MovieService/MovieService.Service/MovieService.Service.csproj", "services/MovieService/MovieService.Service/"]
COPY ["services/MovieService/MovieService.Domain/MovieService.Domain.csproj", "services/MovieService/MovieService.Domain/"]
RUN dotnet restore "./services/MovieService/MovieService.Api/MovieService.Api.csproj"
COPY . .
WORKDIR "/src/services/MovieService/MovieService.Api"
RUN dotnet build "./MovieService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MovieService.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MovieService.Api.dll"]