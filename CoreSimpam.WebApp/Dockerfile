#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["CoreSimpam.WebApp/CoreSimpam.WebApp.csproj", "CoreSimpam.WebApp/"]
COPY ["CoreSimpam.Repo/CoreSimpam.Repo.csproj", "CoreSimpam.Repo/"]
COPY ["CoreSimpam.Model/CoreSimpam.Model.csproj", "CoreSimpam.Model/"]
COPY ["CoreSimpam.ViewModel/CoreSimpam.ViewModel.csproj", "CoreSimpam.ViewModel/"]
RUN dotnet restore "CoreSimpam.WebApp/CoreSimpam.WebApp.csproj"
COPY . .
WORKDIR "/src/CoreSimpam.WebApp"
RUN dotnet build "CoreSimpam.WebApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CoreSimpam.WebApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CoreSimpam.WebApp.dll"]