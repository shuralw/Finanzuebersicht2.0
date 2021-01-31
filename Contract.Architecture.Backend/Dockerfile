FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore "Contract.Architecture.Contract/Contract.Architecture.Contract.csproj"
RUN dotnet restore "Contract.Architecture.API/Contract.Architecture.API.csproj"
RUN dotnet restore "Contract.Architecture.Logic/Contract.Architecture.Logic.csproj"
RUN dotnet restore "Contract.Architecture.Persistence/Contract.Architecture.Persistence.csproj"

RUN dotnet build "Contract.Architecture.Contract/Contract.Architecture.Contract.csproj" -c Release
RUN dotnet build "Contract.Architecture.API/Contract.Architecture.API.csproj" -c Release
RUN dotnet build "Contract.Architecture.Logic/Contract.Architecture.Logic.csproj" -c Release
RUN dotnet build "Contract.Architecture.Persistence/Contract.Architecture.Persistence.csproj" -c Release

FROM build AS publish
RUN dotnet publish "Contract.Architecture.Contract/Contract.Architecture.Contract.csproj" -c Release -o /app
RUN dotnet publish "Contract.Architecture.API/Contract.Architecture.API.csproj" -c Release -o /app
RUN dotnet publish "Contract.Architecture.Logic/Contract.Architecture.Logic.csproj" -c Release -o /app
RUN dotnet publish "Contract.Architecture.Persistence/Contract.Architecture.Persistence.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .

RUN sed -i 's/MinProtocol = TLSv1.2/MinProtocol = TLSv1/' /etc/ssl/openssl.cnf &&\
    sed -i 's/DEFAULT@SECLEVEL=2/DEFAULT@SECLEVEL=1/g' /etc/ssl/openssl.cnf 

ENTRYPOINT ["dotnet", "Contract.Architecture.API.dll"]