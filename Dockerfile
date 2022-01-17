# Set the base image
FROM mcr.microsoft.com/dotnet/framework/sdk:4.8 as build
WORKDIR "/src"


# Copy packages to your image and restore them
COPY TDD_Ahorcado.sln .
COPY Ahorcado.MVC/Ahorcado.MVC.csproj Ahorcado.MVC/Ahorcado.MVC.csproj
COPY Ahorcado.MVC/packages.config Ahorcado.MVC/packages.config
RUN nuget restore Ahorcado.MVC/packages.config -PackagesDirectory packages


# Add files from source to the current directory and publish the deployment files to the folder profile
COPY . .
WORKDIR /src/Ahorcado.MVC
RUN msbuild Ahorcado.MVC.csproj /p:Configuration=Release /m /p:DeployOnBuild=true /p:PublishProfile=FolderProfile
