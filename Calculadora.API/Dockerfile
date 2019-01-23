FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 53773
EXPOSE 44351

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY Calculadora.API/Calculadora.API.csproj Calculadora.API/
RUN dotnet restore Calculadora.API/Calculadora.API.csproj
COPY . .
WORKDIR /src/Calculadora.API
RUN dotnet build Calculadora.API.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Calculadora.API.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Calculadora.API.dll"]
