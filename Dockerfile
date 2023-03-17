# build stage
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /source
COPY . .
RUN dotnet publish opentickets-backend.csproj --configuration "Release" --output "dist"

# production stage
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /source/dist ./
ENV ASPNETCORE_URLS http://0.0.0.0:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "opentickets-backend.dll"]