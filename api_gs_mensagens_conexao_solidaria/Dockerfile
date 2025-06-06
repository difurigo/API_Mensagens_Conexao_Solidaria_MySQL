# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Criação de usuário não-root
RUN useradd -m -u 1001 appuser
ENV ASPNETCORE_ENVIRONMENT=Production

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["api_gs_mensagens_conexao_solidaria.csproj", "./"]
RUN dotnet restore "api_gs_mensagens_conexao_solidaria.csproj"
COPY . .
RUN dotnet build "api_gs_mensagens_conexao_solidaria.csproj" -c Release -o /app/build

# Publicação
FROM build AS publish
RUN dotnet publish "api_gs_mensagens_conexao_solidaria.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Imagem final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER appuser
ENTRYPOINT ["dotnet", "api_gs_mensagens_conexao_solidaria.dll"]
