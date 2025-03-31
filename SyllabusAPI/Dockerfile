# ------------------------------
# Base Image
# ------------------------------
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# ------------------------------
# Build Stage
# ------------------------------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy all csproj files from solution
COPY ["SyllabusAPI/SyllabusAPI.csproj", "SyllabusAPI/"]
COPY ["Syllabus.Application/Syllabus.Application.csproj", "Syllabus.Application/"]
COPY ["Syllabus.Domain/Syllabus.Domain.csproj", "Syllabus.Domain/"]
COPY ["Syllabus.Infrastructure/Syllabus.Infrastructure.csproj", "Syllabus.Infrastructure/"]
COPY ["Syllabus.ApiContracts/Syllabus.ApiContracts.csproj", "Syllabus.ApiContracts/"]
COPY ["Syllabus.Util/Syllabus.Util.csproj", "Syllabus.Util/"]

# Restore dependencies
RUN dotnet restore "SyllabusAPI/SyllabusAPI.csproj"

# Copy entire source
COPY . .

# Build the app
WORKDIR "/src/SyllabusAPI"
RUN dotnet build "SyllabusAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# ------------------------------
# Publish Stage
# ------------------------------
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "SyllabusAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# ------------------------------
# Final Image
# ------------------------------
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "SyllabusAPI.dll"]
