#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BookShop.Getway.Rest/BookShop.Getway.Rest.csproj", "BookShop.Getway.Rest/"]
COPY ["BookShop.Getway.Application/BookShop.Getway.Application.csproj", "BookShop.Getway.Application/"]
COPY ["BookShop.Common/BookShop.Common.csproj", "BookShop.Common/"]
RUN dotnet restore "BookShop.Getway.Rest/BookShop.Getway.Rest.csproj"
COPY . .
WORKDIR "/src/BookShop.Getway.Rest"
RUN dotnet build "BookShop.Getway.Rest.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.Getway.Rest.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.Getway.Rest.dll"]