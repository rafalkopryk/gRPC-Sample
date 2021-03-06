#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["BookShop.BookService.Rpc/BookShop.BookService.Rpc.csproj", "BookShop.BookService.Rpc/"]
COPY ["BookShop.BookService.Application/BookShop.BookService.Application.csproj", "BookShop.BookService.Application/"]
COPY ["BookShop.BookService.Domain/BookShop.BookService.Domain.csproj", "BookShop.BookService.Domain/"]
COPY ["BookShop.Common/BookShop.Common.csproj", "BookShop.Common/"]
RUN dotnet restore "BookShop.BookService.Rpc/BookShop.BookService.Rpc.csproj"
COPY . .
WORKDIR "/src/BookShop.BookService.Rpc"
RUN dotnet build "BookShop.BookService.Rpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BookShop.BookService.Rpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookShop.BookService.Rpc.dll"]
