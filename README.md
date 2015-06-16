# Cover Service

Microservice to retrieve CD cover image URLs

## Usage

```
GET http://46.101.191.124:5012/images/{mbid} // mbid = MusicBrainz ID

// Returns an array of image URLs, like:
// ["http://domain.org/6158.jpg", "http://domain.org/1589.jpg"]
```

## Health Check

* [Cover Service - Health Check](http://46.101.191.124:5012/healthcheck)

## Configuration Parameters

```
SERVICE_PORT=5012
DEFAULT_IMAGE_URL=http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png
```

## Build container

```
docker build -t cover-service .
```

## Run container locally

```
docker run -t -d -p 5012:5012 cover-service
```

## Push container into private registry

```
docker tag cover-service:latest 46.101.191.124:5000/cover-service:0.0.1
docker push 46.101.191.124:5000/cover-service:0.0.1
```

## Deploy container from Shipyard

### OSX/Linux

```
curl -X POST \
-H 'Content-Type: application/json' \
-H 'X-Service-Key: pdE4.JVg43HyxCEMWvsFvu6bdFV7LwA7YPii' \
http://46.101.191.124:8080/api/containers?pull=true \
-d '{  
  "name": "46.101.191.124:5000/cover-service:0.0.1",
  "cpus": 0.1,
  "memory": 64,
  "environment": {
    "SERVICE_CHECK_SCRIPT": "curl -s http://46.101.191.124:5012/healthcheck",
    "SERVICE_PORT": "5012",
    "DEFAULT_IMAGE_URL": "http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png"
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
       "port": 5012,
       "container_port": 5012
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

### Windows

```
$Uri = "http://46.101.191.124:8080/api/containers?pull=true"

$Headers = @{
  "X-Service-Key" = "pdE4.JVg43HyxCEMWvsFvu6bdFV7LwA7YPii"
  "Content-Type" = "application/json"
}

$Body = @"
{  
  "name": "46.101.191.124:5000/cover-service:0.0.1",
  "cpus": 0.1,
  "memory": 64,
  "environment": {
    "SERVICE_CHECK_SCRIPT": "curl -s http://46.101.191.124:5012/healthcheck",
    "SERVICE_PORT": "5012",
    "DEFAULT_IMAGE_URL": "http://www.pdclipart.org/albums/Computers/etiquette_cd-rom_01.png"
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
       "port": 5012,
       "container_port": 5012
    }
  ],
  "labels": [],
  "publish": false,
  "privileged": false,
  "restart_policy": {  
    "name": "no"
  }
}
"@

Invoke-RestMethod -Uri $Uri -Method Post -Headers $Headers -Body $Body
```
