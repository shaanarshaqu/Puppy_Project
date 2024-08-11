FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Puppy_Project/Puppy_Project.csproj", "Puppy_Project/"]
RUN dotnet restore "./Puppy_Project/Puppy_Project.csproj"
COPY . .
WORKDIR "/src/Puppy_Project"
RUN dotnet build "./Puppy_Project.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./Puppy_Project.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Puppy_Project.dll"]
