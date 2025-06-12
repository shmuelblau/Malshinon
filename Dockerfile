# שלב ראשון: Build
# שלב ראשון: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# העתקת קובץ csproj
COPY Malshinon/Malshinon.csproj ./Malshinon/
# העתקת שאר הקבצים
COPY Malshinon/. ./Malshinon/

WORKDIR /src/Malshinon
RUN dotnet restore
RUN dotnet publish -c Release -o /app/out

# שלב שני: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "Malshinon.dll"]
