# Use the official ASP.NET Core runtime as a base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine3.18-amd64 AS base
EXPOSE 5000
WORKDIR /app
# https://+:5005; if have certificate
ENV ASPNETCORE_URLS=http://+:5000;https://+:5005
# Else will throw an error for localization
ENV ASPNETCORE_ENVIRONMENT=Production

# Copy the published application to the container
FROM mcr.microsoft.com/dotnet/sdk:8.0.100-1-alpine3.18-amd64 AS build
WORKDIR /src
COPY ["Elipse.csproj", "./"]
RUN dotnet restore "Elipse.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "Elipse.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Elipse.csproj" -c Release -o /app/publish

# Build runtime image
FROM base AS final
WORKDIR /app
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
RUN dotnet dev-certs https
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Elipse.dll"]