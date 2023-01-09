#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/AuthService/AuthService.Api/AuthService.Api.csproj", "services/AuthService/AuthService.Api/"]
COPY ["services/AuthService/AuthService.Data/AuthService.Data.csproj", "services/AuthService/AuthService.Data/"]
COPY ["services/AuthService/AuthService.Domain/AuthService.Domain.csproj", "services/AuthService/AuthService.Domain/"]
COPY ["services/AuthService/AuthService.Service/AuthService.Service.csproj", "services/AuthService/AuthService.Service/"]
RUN dotnet restore "services/AuthService/AuthService.Api/AuthService.Api.csproj"
COPY . .
WORKDIR "/src/services/AuthService/AuthService.Api"
RUN dotnet build "AuthService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthService.Api.dll"]
