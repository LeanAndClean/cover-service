# Cover Service

Microservice to retrieve CD cover image URLs

##Configuration

```
export SERVICE_PORT=5012
export DEFAULT_IMAGE_URL=http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png
export PUBLISH_SERVICE=<ip>:<port>
export SERVICE_VERSION=0.0.4
```

## Build container

```
docker build -t cover-service .
```

## Run locally

```
docker run -t -d -p $SERVICE_PORT:$SERVICE_PORT cover-service
```

## Publish into private registry

```
docker tag cover-service:latest $PUBLISH_SERVICE/cover-service:$SERVICE_VERSION
docker push $PUBLISH_SERVICE/cover-service:$SERVICE_VERSION
```

## API

```
GET http://localhost:$SERVICE_PORT/images/{mbid} // mbid = MusicBrainz ID

// Returns an array of image URLs, like:
// ["http://domain.org/6158.jpg", "http://domain.org/1589.jpg"]
```
