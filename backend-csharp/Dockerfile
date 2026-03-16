FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY UserShiftsApiService.sln ./
COPY UserShiftsApiService/UserShiftsApiService.csproj ./UserShiftsApiService/
RUN dotnet restore

COPY . .
RUN dotnet publish UserShiftsApiService/UserShiftsApiService.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:5029
EXPOSE 5029

ENTRYPOINT ["dotnet", "UserShiftsApiService.dll"]
