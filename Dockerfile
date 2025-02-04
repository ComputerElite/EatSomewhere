# Include dotnet6 sdk

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
# Start to run Flutter commands
# doctor to see if all was installes ok
COPY . .

WORKDIR /app/EatSomewhere
RUN rm -rf bin/ obj/
WORKDIR /app/app/
RUN bash ./build_frontend.sh
WORKDIR /app/EatSomewhere
RUN dotnet build
RUN mkdir -p bin/Debug/net9.0/
RUN mkdir -p bin/Release/net9.0/
RUN dotnet tool install --global --version 9.0.0 dotnet-ef
RUN cp /app/EatSomewhere/docker_config.json /app/EatSomewhere/bin/Debug/net9.0/config.json
RUN cp /app/EatSomewhere/docker_config.json /app/EatSomewhere/bin/Release/net9.0/config.json
RUN cp /app/EatSomewhere/docker_config.json /app/EatSomewhere/config.json
WORKDIR /app/EatSomewhere
CMD ["bash", "migrate_db_and_start.sh"]