# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:5.0 AS build
WORKDIR /source

# install extra cloud firestore dependencies
RUN apt-get update \
    && apt-get install -y --allow-unauthenticated \
    libc6-dev \
    libgdiplus \
    libx11-dev \
    && rm -rf /var/lib/apt/lists/*

# copy csproj and restore as distinct layers
COPY src/Injhinuity.Backend/*.csproj Injhinuity.Backend/
COPY src/Injhinuity.Backend.Core/*.csproj Injhinuity.Backend.Core/
COPY src/Injhinuity.Backend.Model/*.csproj Injhinuity.Backend.Model/
RUN dotnet restore Injhinuity.Backend/Injhinuity.Backend.csproj

# copy and build app and libraries
COPY src/Injhinuity.Backend/ Injhinuity.Backend/
COPY src/Injhinuity.Backend.Core/ Injhinuity.Backend.Core/
COPY src/Injhinuity.Backend.Model/ Injhinuity.Backend.Model/
WORKDIR /source/Injhinuity.Backend
RUN dotnet build -c release

FROM build AS publish
RUN dotnet publish -c release --no-build -o /app

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:5.0
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Injhinuity.Backend.dll"]