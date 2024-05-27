FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["src/bookApi.Core/bookApi.Core.csproj", "bookApi.Core/"]
COPY ["src/bookApi.Domain/bookApi.Domain.csproj", "bookApi.Domain/"]
COPY ["src/bookApi.Infrastructure/bookApi.Infrastructure.csproj", "bookApi.Infrastructure/"]
COPY ["src/bookApi.Persistence.Postgres/bookApi.Persistence.Postgres.csproj", "bookApi.Persistence.Postgres/"]
COPY ["src/bookApi.Persistence.SqlServer/bookApi.Persistence.SqlServer.csproj", "bookApi.Persistence.SqlServer/"]
COPY ["src/bookApi.WebApi/bookApi.WebApi.csproj", "bookApi.WebApi/"]
COPY ["src/Shared/bookApi.Shared.Abstractions/bookApi.Shared.Abstractions.csproj", "Shared/bookApi.Shared.Abstractions/"]
COPY ["src/Shared/bookApi.Shared.Infrastructure/bookApi.Shared.Infrastructure.csproj", "Shared/bookApi.Shared.Infrastructure/"]

RUN dotnet restore "bookApi.WebApi/bookApi.WebApi.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "src/bookApi.WebApi/bookApi.WebApi.csproj" -c Release -o /app/build

FROM build AS publish

RUN dotnet publish "src/bookApi.WebApi/bookApi.WebApi.csproj" -c Release -o /app/publish
RUN dotnet dev-certs https -ep /app/publish/radyalabs.pfx -p pa55w0rd!


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=pa55w0rd!
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=radyalabs.pfx

ENTRYPOINT ["dotnet", "bookApi.WebApi.dll"]