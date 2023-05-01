#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FunnyGame.Api/FunnyGame.Api.csproj", "FunnyGame.Api/"]
COPY ["FunnyGame.Application/FunnyGame.Application.csproj", "FunnyGame.Application/"]
COPY ["FunnyGame.Data/FunnyGame.Data.csproj", "FunnyGame.Data/"]
RUN dotnet restore "FunnyGame.Api/FunnyGame.Api.csproj"
COPY . .
WORKDIR "/src/FunnyGame.Api"
RUN dotnet build "FunnyGame.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FunnyGame.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FunnyGame.Api.dll", "--environment=Development"]