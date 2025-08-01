# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Build.props", "."]
COPY ["services/ReviewService/ReviewService.Presentation.Api/ReviewService.Presenation.Api.csproj", "services/ReviewService/ReviewService.Presentation.Api/"]
COPY ["infrastructure/nt.orchestrator.ServiceDefaults/nt.orchestrator.ServiceDefaults.csproj", "infrastructure/nt.orchestrator.ServiceDefaults/"]
COPY ["services/ReviewService/ReviewService.Application.Interfaces/ReviewService.Application.Interfaces.csproj", "services/ReviewService/ReviewService.Application.Interfaces/"]
COPY ["services/ReviewService/ReviewService.Application.DTO/ReviewService.Application.DTO.csproj", "services/ReviewService/ReviewService.Application.DTO/"]
COPY ["services/ReviewService/ReviewService.Domain/ReviewService.Domain.csproj", "services/ReviewService/ReviewService.Domain/"]
COPY ["services/ReviewService/ReviewService.Application.Orchestration/ReviewService.Application.Orchestration.csproj", "services/ReviewService/ReviewService.Application.Orchestration/"]
RUN dotnet restore "./services/ReviewService/ReviewService.Presentation.Api/ReviewService.Presenation.Api.csproj"
COPY . .
WORKDIR "/src/services/ReviewService/ReviewService.Presentation.Api"
RUN dotnet build "./ReviewService.Presenation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ReviewService.Presenation.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReviewService.Presenation.Api.dll"]