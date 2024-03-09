FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /VATQuery

COPY *.sln .
COPY VATQuery.Bot/*.csproj ./VATQuery.Bot/
COPY KookBotCraft.BotMarket/*.csproj ./KookBotCraft.BotMarket/
COPY VATQuery.Application/*.csproj ./VATQuery.Application/
COPY VATQuery.Core/*.csproj ./VATQuery.Core/
COPY KookBotCraft.Database/*.csproj ./KookBotCraft.Database/
RUN dotnet restore

COPY VATQuery.Bot/. ./VATQuery.Bot/
COPY KookBotCraft.BotMarket/. ./KookBotCraft.BotMarket/
COPY VATQuery.Application/. ./VATQuery.Application/
COPY VATQuery.Core/. ./VATQuery.Core/
COPY KookBotCraft.Database/. ./KookBotCraft.Database/

WORKDIR /VATQuery/VATQuery.Bot
RUN dotnet publish -c release -o /app --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

ENTRYPOINT ["dotnet", "VATQuery.Bot.dll"]