FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5086

ENV ASPNETCORE_URLS=http://+:5086

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/Services/OrderService/Order.API/Order.API.csproj", "src/Services/OrderService/Order.API/"]
RUN dotnet restore "src/Services/OrderService/Order.API/Order.API.csproj"
COPY . .
WORKDIR "/src/src/Services/OrderService/Order.API"
RUN dotnet build "Order.API.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Order.API.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Order.API.dll"]
