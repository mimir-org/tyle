# syntax=docker/dockerfile:1.3

# In order to restore and properly cache all dependencies, ALL csproj need to be copied into container.
# Docker does not offer good support for glob pattern, so the csproj files are all copied in cwd, and a inline script is used to recreate correct folder structure for restore.

FROM mcr.microsoft.com/dotnet/sdk:7.0-alpine AS base
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}/ && mv $file ${file%.*}/; done
RUN dotnet restore TypeLibrary.Api/TypeLibrary.Api.csproj
COPY . .

# FIXME: dotnet-format is broken for 6.0.200 https://github.com/dotnet/format/issues/1519
# globally install dotnet-format as workaround
# When fix is in place, can be replaced with only "dotnet format . --verify-no-changes --verbosity diagnostic --no-restore"

FROM base as format
RUN dotnet tool install -g dotnet-format --version "7.*" --add-source https://pkgs.dev.azure.com/dnceng/public/_packaging/dotnet7/nuget/v3/index.json
RUN /root/.dotnet/tools/dotnet-format . --verify-no-changes --verbosity diagnostic --no-restore

FROM base as build
RUN dotnet test -c Release
RUN dotnet build TypeLibrary.Api/TypeLibrary.Api.csproj -c Release --no-restore

FROM build as publish
RUN dotnet publish TypeLibrary.Api/TypeLibrary.Api.csproj -c Release --no-restore --no-build -o /app/publish

# icu-libs are not included in alpine runtime images
# see: https://github.com/dotnet/dotnet-docker/blob/main/samples/selecting-tags.md#alpine-globalization and https://github.com/dotnet/dotnet-docker/blob/main/samples/dotnetapp/Dockerfile.alpine-x64#L19
# This need to be added, because of globalization issues.

FROM mcr.microsoft.com/dotnet/aspnet:7.0-alpine AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV \
  DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false \
  LC_ALL=en_US.UTF-8 \
  LANG=en_US.UTF-8
RUN apk add --no-cache icu-libs
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TypeLibrary.Api.dll"]
