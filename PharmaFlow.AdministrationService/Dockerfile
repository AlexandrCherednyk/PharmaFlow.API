#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["PharmaFlow.AdministrationService/PharmaFlow.AdministrationService.csproj", "PharmaFlow.AdministrationService/"]
COPY ["PharmaFlow.AdministrationService.Grpc/PharmaFlow.AdministrationService.Grpc.csproj", "PharmaFlow.AdministrationService.Grpc/"]
RUN dotnet restore "PharmaFlow.AdministrationService/PharmaFlow.AdministrationService.csproj"
COPY . .
WORKDIR "/src/PharmaFlow.AdministrationService"
RUN dotnet build "PharmaFlow.AdministrationService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PharmaFlow.AdministrationService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PharmaFlow.AdministrationService.dll"]