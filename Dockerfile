# =========================
# BUILD STAGE
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution và csproj trước (tối ưu cache)
COPY QLBAOCAOCV.slnx .
COPY QLBAOCAOCV.DAL/*.csproj QLBAOCAOCV.DAL/
COPY QLBAOCAOCV.BLL/*.csproj QLBAOCAOCV.BLL/
COPY QLBAOCAOCV.Web/*.csproj QLBAOCAOCV.Web/

# Restore
RUN dotnet restore QLBAOCAOCV.Web/QLBAOCAOCV.Web.csproj

# Copy toàn bộ source
COPY . .

# Publish
WORKDIR /src/QLBAOCAOCV.Web
RUN dotnet publish -c Release -o /app/publish

# =========================
# RUNTIME STAGE
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "QLBAOCAOCV.Web.dll"]
