# Seteo de imagén base
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 AS build
WORKDIR /src


# Copiar paquetes a la imagén y restaurarlos
COPY *.sln .
COPY Ahorcado.MVC/*.csproj ./Ahorcado.MVC/
COPY Ahorcado.MVC/*.config ./Ahorcado.MVC/
RUN nuget restore

# Copiar todo lo restante y buildear el proyecto
COPY Ahorcado.MVC/. ./Ahorcado.MVC/
WORKDIR /src/Ahorcado.MVC/
RUN msbuild /p:Configuration=Release -r:False

FROM mcr.microsoft.com/dotnet/framework/aspnet:4.8 AS runtime
WORKDIR /inetpub/wwwroot
COPY --from=build /src/Ahorcado.MVC/. ./
