FROM mcr.microsoft.com/dotnet/core/sdk:2.2
WORKDIR /app
EXPOSE 9090
COPY . .
RUN dotnet publish -c Release -o out

ENTRYPOINT [ "dotnet", "out/MiniprojectSoapService.dll" ]