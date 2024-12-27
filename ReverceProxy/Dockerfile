
# Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8484

# Base image for building the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file and restore dependencies
COPY ["ReverseProxydemo.csproj", "./"]
RUN dotnet restore "./ReverseProxydemo.csproj"

# Copy the remaining application files
COPY . ./

# Build the application
RUN dotnet build "ReverseProxydemo.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "ReverseProxydemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final image for runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReverseProxydemo.dll"]


#docker build -t reverse_proxy:latest .
#docker run -d -p 8484:8484 --name reverse_proxy_container reverse_proxy:latest