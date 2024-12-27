# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["App1.csproj", "./"]
RUN dotnet restore "./App1.csproj"

# Copy the remaining application files
COPY . ./

# Build the application
RUN dotnet build "App1.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "App1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "App1.dll"]
