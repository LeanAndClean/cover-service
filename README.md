# Cover Service

Microservice to retrieve CD cover image URLs

## Service configuration

```
export SERVICE_PORT=5012
export DEFAULT_IMAGE_URL=http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png
```

##Deploy configuration

```
export SERVICE_VERSION=0.0.4
export PUBLISH_SERVICE=<ip>:<port>
export DEPLOY_SERVICE=<ip>:<port>
```

## Build container

```
docker build -t cover-service .
```

## Run container locally

```
docker run -t -d -p $SERVICE_PORT:$SERVICE_PORT cover-service
```

## Push container into private registry

```
docker tag cover-service:latest $PUBLISH_SERVICE/cover-service:$SERVICE_VERSION
docker push $PUBLISH_SERVICE/cover-service:$SERVICE_VERSION
```

## Deploy container from Shipyard

```
curl -X POST \
-H 'Content-Type: application/json' \
-H 'X-Service-Key: pdE4.JVg43HyxCEMWvsFvu6bdFV7LwA7YPii' \
http://$DEPLOY_SERVICE/api/containers?pull=true \
-d '{  
  "name": "'$PUBLISH_SERVICE'/cover-service:'$SERVICE_VERSION'",
  "cpus": 0.1,
  "memory": 64,
  "environment": {
    "SERVICE_CHECK_SCRIPT": "curl -s http://$SERVICE_CONTAINER_IP:$SERVICE_CONTAINER_PORT/healthcheck",
    "SERVICE_PORT": "'$SERVICE_PORT'",
    "DEFAULT_IMAGE_URL": "'$DEFAULT_IMAGE_URL'"
  },
  "hostname": "",
  "domain": "",
  "type": "service",
  "network_mode": "bridge",
  "links": {},
  "volumes": [],
  "bind_ports": [  
    {  
       "proto": "tcp",
       "host_ip": null,
       "port": '$SERVICE_PORT',
       "container_port": '$SERVICE_PORT'
    }
  ],
  "labels": [],
  "publish": false,
  "privileged": false,
  "restart_policy": {  
    "name": "no"
  }
}'
```

## API

```
GET http://localhost:$SERVICE_PORT/images/{mbid} // mbid = MusicBrainz ID

// Returns an array of image URLs, like:
// ["http://domain.org/6158.jpg", "http://domain.org/1589.jpg"]
```
