FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

COPY . .
RUN dotnet restore ./testApp/testApp.csproj
RUN dotnet publish ./testApp/testApp.csproj  -o out /p:Version=1.0.0

FROM build
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "testApp.dll"]
