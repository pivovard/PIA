FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY Bank/Bank.csproj Bank/
RUN dotnet restore Bank/Bank.csproj
COPY . .
WORKDIR /src/Bank
RUN dotnet build Bank.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Bank.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY BankContext-afea5610-d4cd-47d8-8ae0-6e4372c48298.mdf ../var/opt/mssql/data
ENTRYPOINT ["dotnet", "Bank.dll"]
