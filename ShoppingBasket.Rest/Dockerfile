FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ShoppingBasket.Rest/ShoppingBasket.Rest.csproj", "ShoppingBasket.Rest/"]
COPY ["ShoppingBasket.Infrastructure.Persistence/ShoppingBasket.Infrastructure.Persistence.csproj", "ShoppingBasket.Infrastructure.Persistence/"]
COPY ["ShoppingBasket.Application/ShoppingBasket.Application.csproj", "ShoppingBasket.Application/"]
COPY ["ShoppingBasket.Core/ShoppingBasket.Core.csproj", "ShoppingBasket.Core/"]
COPY ["ShoppingBasket.Domain/ShoppingBasket.Domain.csproj", "ShoppingBasket.Domain/"]
COPY ["ShoppingBasket.Infrastructure/ShoppingBasket.Infrastructure.csproj", "ShoppingBasket.Infrastructure/"]
RUN dotnet restore "ShoppingBasket.Rest/ShoppingBasket.Rest.csproj"
COPY . .
WORKDIR "/src/ShoppingBasket.Rest"
RUN dotnet build "ShoppingBasket.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ShoppingBasket.Rest.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingBasket.Rest.dll"]
