# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "Banco_Sol_Gestion_Financiera/Banco_Sol_Gestion_Financiera.csproj"
RUN dotnet publish "Banco_Sol_Gestion_Financiera/Banco_Sol_Gestion_Financiera.csproj" -c Release -o /app/publish --no-restore

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:10000
EXPOSE 10000

ENTRYPOINT ["dotnet", "Banco_Sol_Gestion_Financiera.dll"]