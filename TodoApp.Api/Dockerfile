FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy project files
COPY ["TodoApp.Api/TodoApp.Api.csproj", "TodoApp.Api/"]
COPY ["TodoApp.Core/TodoApp.Core.csproj", "TodoApp.Core/"]
COPY ["TodoApp.Infrastructure/TodoApp.Infrastructure.csproj", "TodoApp.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "TodoApp.Api/TodoApp.Api.csproj"

# Copy the rest of the code
COPY . .

# Build the application
WORKDIR "/src/TodoApp.Api"
RUN dotnet build "TodoApp.Api.csproj" -c Release -o /app/build

# Publish the application
FROM build AS publish
RUN dotnet publish "TodoApp.Api.csproj" -c Release -o /app/publish

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "TodoApp.Api.dll"] 