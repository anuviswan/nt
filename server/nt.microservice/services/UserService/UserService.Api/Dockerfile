#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
# EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["services/UserService/UserService.Api/UserService.Api.csproj", "services/UserService/UserService.Api/"]
COPY ["services/UserService/UserService.Service/UserService.Service.csproj", "services/UserService/UserService.Service/"]
COPY ["services/UserService/UserService.Data/UserService.Data.csproj", "services/UserService/UserService.Data/"]
COPY ["services/UserService/UserService.Domain/UserService.Domain.csproj", "services/UserService/UserService.Domain/"]
RUN dotnet restore "services/UserService/UserService.Api/UserService.Api.csproj"
COPY . .
WORKDIR "/src/services/UserService/UserService.Api"
RUN dotnet build "UserService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserService.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserService.Api.dll"]
