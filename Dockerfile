FROM microsoft/aspnet:1.0.0-beta4

ADD . /app

WORKDIR /app

RUN ["dnu", "restore"]

ENV SERVICE_PORT=5012

ENTRYPOINT sleep 99999999999 | dnx . Microsoft.AspNet.Hosting --server Kestrel --server.urls http://localhost:$SERVICE_PORT
