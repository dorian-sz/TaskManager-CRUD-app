FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TaskManagerApi.csproj", "."]
RUN dotnet restore "./TaskManagerApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "TaskManagerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskManagerApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskManagerApi.dll"]