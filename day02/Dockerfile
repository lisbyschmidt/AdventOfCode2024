FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build

# Install NativeAOT build prerequisites
RUN apk update
RUN apk add clang build-base zlib-dev

WORKDIR /source
COPY day02.csproj .
COPY input.txt .
COPY Program.cs .
RUN dotnet publish -c release -o publish

FROM alpine
WORKDIR /app
COPY --from=build /source/publish .
ENTRYPOINT [ "./day02" ]