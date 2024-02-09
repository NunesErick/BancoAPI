#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["APIBancoChu/APIBancoChu.csproj", "APIBancoChu/"]
COPY ["CHU.Data/CHU.Data.csproj", "CHU.Data/"]
COPY ["CHU.Domain/CHU.Domain.csproj", "CHU.Domain/"]
COPY ["CHU.Infrastructure/CHU.Infrastructure.csproj", "CHU.Infrastructure/"]
COPY ["CHU.Services/CHU.Service.csproj", "CHU.Services/"]
RUN dotnet restore "./APIBancoChu/./APIBancoChu.csproj"
COPY . .
WORKDIR "/src/APIBancoChu"
RUN dotnet build "./APIBancoChu.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./APIBancoChu.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "APIBancoChu.dll"]