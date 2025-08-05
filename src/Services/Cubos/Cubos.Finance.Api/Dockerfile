#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Cubos/Cubos.Finance.Api/Cubos.Finance.Api.csproj", "Services/Cubos/Cubos.Finance.Api/"]
COPY ["Services/Cubos/Cubos.Finance.Application/Cubos.Finance.Application.csproj", "Services/Cubos/Cubos.Finance.Application/"]
COPY ["Shared/Cubos.Finance.Shared/Cubos.Finance.Shared.csproj", "Shared/Cubos.Finance.Shared/"]
COPY ["Services/Cubos/Cubos.Finance.Domain/Cubos.Finance.Domain.csproj", "Services/Cubos/Cubos.Finance.Domain/"]
COPY ["Services/Cubos/Cubos.Finance.External/Cubos.Finance.External.csproj", "Services/Cubos/Cubos.Finance.External/"]
COPY ["Services/Cubos/Cubos.Finance.Data/Cubos.Finance.Data.csproj", "Services/Cubos/Cubos.Finance.Data/"]

RUN dotnet restore "Services/Cubos/Cubos.Finance.Api/"
COPY . .
WORKDIR "Services/Cubos/Cubos.Finance.Api"
RUN dotnet build "Cubos.Finance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Cubos.Finance.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Cubos.Finance.Api.dll"]