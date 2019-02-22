FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["Aspnetcore.Demo/Aspnetcore.Demo.csproj", "Aspnetcore.Demo/"]
RUN dotnet restore "Aspnetcore.Demo/Aspnetcore.Demo.csproj"
COPY . .
WORKDIR "/src/Aspnetcore.Demo"
RUN dotnet build "Aspnetcore.Demo.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Aspnetcore.Demo.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Aspnetcore.Demo.dll"]