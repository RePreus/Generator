FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["Generator.API/Generator.API.csproj", "Generator.API/"]
COPY ["Generator.Infrastructure/Generator.Infrastructure.csproj", "Generator.Infrastructure/"]
COPY ["Generator.Application/Generator.Application.csproj", "Generator.Application/"]
COPY ["Generator.Domain/Generator.Domain.csproj", "Generator.Domain/"]
RUN dotnet restore "Generator.API/Generator.API.csproj"
COPY . .
WORKDIR "/src/Generator.API"
RUN dotnet build "Generator.API.csproj" -c Release -o /app

FROM build AS dev
RUN dotnet test -c Release --logger "trx;LogFileName=testresults.trx"

FROM build AS publish
RUN dotnet publish "Generator.API.csproj" -c Release -o /app

FROM base AS prod
WORKDIR /app
EXPOSE 80
EXPOSE 443
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Generator.API.dll"]