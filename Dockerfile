FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine 

WORKDIR /app

COPY . .

RUN dotnet build

ENTRYPOINT ["dotnet","test"]