docker-compose -f client\nt.webclient\vue3\nt\docker-compose.yml up -d
docker-compose -f server/nt.microservice/docker-compose.yml -f server/nt.microservice/docker-compose.override.yml up -d