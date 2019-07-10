FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build
WORKDIR /app

# copy csproj and restore as distinct layers
COPY DeliverySystem/*.csproj ./DeliverySystem/
WORKDIR /app/DeliverySystem
RUN dotnet restore

# copy and publish app and libraries
WORKDIR /app
COPY DeliverySystem/. ./DeliverySystem/
WORKDIR /app/DeliverySystem
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/runtime:2.2 AS runtime
WORKDIR /app
COPY --from=build /app/DeliverySystem/out ./
ENTRYPOINT ["dotnet", "DeliverySystem.dll"]