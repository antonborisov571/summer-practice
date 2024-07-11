FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5201

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kfoodle.WEB/Kfoodle.WEB.csproj", "Kfoodle.WEB/"]
COPY ["Kfoodle.Core/Kfoodle.Core.csproj", "Kfoodle.Core/"]
COPY ["Kfoodle.Contracts/Kfoodle.Contracts.csproj", "Kfoodle.Contracts/"]
COPY ["Kfoodle.Data.PostgreSQL/Kfoodle.Data.PostgreSQL.csproj", "Kfoodle.Data.PostgreSQL/"]
COPY ["Kfoodle.Worker/Kfoodle.Worker.csproj", "Kfoodle.Worker/"]
COPY ["Kfoodle.SFTP/Kfoodle.SFTP.csproj", "Kfoodle.SFTP/"]
RUN dotnet restore "Kfoodle.WEB/Kfoodle.WEB.csproj"
COPY . .
WORKDIR "/src/Kfoodle.WEB"
RUN dotnet build "Kfoodle.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Kfoodle.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kfoodle.WEB.dll"]
