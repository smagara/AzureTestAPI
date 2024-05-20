FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5105

ENV ASPNETCORE_URLS=http://+:5105

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["azure-test-api.csproj", "./"]
RUN dotnet restore "azure-test-api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "azure-test-api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "azure-test-api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "azure-test-api.dll"]
