FROM mcr.microsoft.com/dotnet/core/sdk:2.2 as base
WORKDIR /app
EXPOSE 9090
COPY . .
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2 as final
WORKDIR /app
COPY --from=base /app/out .

ENTRYPOINT [ "dotnet", "MiniprojectSoapService.dll" ]