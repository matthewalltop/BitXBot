#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BitXBot.Console/BitXBot.Console.csproj", "BitXBot.Console/"]
COPY ["BitXBot.External/BitXBot.External.csproj", "BitXBot.External/"]

RUN dotnet restore "BitXBot.Console/BitXBot.Console.csproj"
COPY . .
WORKDIR "/src/BitXBot.Console"
RUN dotnet build "BitXBot.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BitXBot.Console.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ASPNETCORE_URLS=http://*:$PORT dotnet BitXBot.Console.dll
# ENTRYPOINT ["dotnet", "BitXBot.Console.dll"]