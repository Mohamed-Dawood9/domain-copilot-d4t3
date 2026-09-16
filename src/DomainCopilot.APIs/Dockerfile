FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["src/DomainCopilot.APIs/DomainCopilot.APIs.csproj", "src/DomainCopilot.APIs/"]
COPY ["src/DomainCopilot.Core/DomainCopilot.Core.csproj", "src/DomainCopilot.Core/"]
COPY ["src/DomainCopilot.Repository/DomainCopilot.Repository.csproj", "src/DomainCopilot.Repository/"]
COPY ["src/DomainCopilot.Service/DomainCopilot.Service.csproj", "src/DomainCopilot.Service/"]
RUN dotnet restore "src/DomainCopilot.APIs/DomainCopilot.APIs.csproj"
COPY . .
WORKDIR "/src/src/DomainCopilot.APIs"
RUN dotnet build "DomainCopilot.APIs.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DomainCopilot.APIs.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DomainCopilot.APIs.dll"]
