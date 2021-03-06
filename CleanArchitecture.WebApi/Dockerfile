#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj", "CleanArchitecture.WebApi/"]
COPY ["Services/CleanArchitecture.Services.csproj", "Services/"]
COPY ["Abstractions/CleanArchitecture.Abstractions.csproj", "Abstractions/"]
COPY ["Entities/CleanArchitecture.Entities.csproj", "Entities/"]
COPY ["Application/CleanArchitecture.Application.csproj", "Application/"]
COPY ["Repository/CleanArchitecture.Repository.csproj", "Repository/"]
COPY ["DataAccess/CleanArchitecture.DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj"
COPY . .
WORKDIR "/src/CleanArchitecture.WebApi"
RUN dotnet build "CleanArchitecture.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CleanArchitecture.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.WebApi.dll"]