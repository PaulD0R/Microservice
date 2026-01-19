FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 3000
EXPOSE 3001

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["HotelService.API/HotelService.API.csproj", "HotelService.API/"]
COPY ["HotelService.Infrastructure/HotelService.Infrastructure.csproj", "HotelService.Infrastructure/"]
COPY ["HotelService.Application/HotelService.Application.csproj", "HotelService.Application/"]
COPY ["HotelService.Domain/HotelService.Domain.csproj", "HotelService.Domain/"]
RUN dotnet restore "HotelService.API/HotelService.API.csproj"
COPY . .
WORKDIR "/src/HotelService.API"
RUN dotnet build "./HotelService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HotelService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelService.API.dll"]
