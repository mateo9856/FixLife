FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["FixApp.WebAPI/FixApp.WebAPI.csproj", "FixApp.WebAPI/"]
RUN dotnet restore "FixApp.WebAPI/FixApp.WebAPI.csproj" --no-cache
COPY . .
WORKDIR "/src/FixApp.WebAPI"
RUN dotnet build "FixApp.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FixApp.WebAPI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FixApp.WebAPI.dll"]