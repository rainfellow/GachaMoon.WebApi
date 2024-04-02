#!/bin/bash
COMMIT=$(git rev-parse --verify HEAD)
docker login 192.168.31.95:32768  --username admin --password LFzSN#9MR#KLH7v
docker build --platform linux/arm64 . -t gachamoon-webapi -f src/gachamoon.WebApi/Dockerfile
#kubectl create secret generic gachamoon-webapi --namespace=gachamoon  --from-file=app.json=./src/gachamoon.WebApi/appsettings.Production.json --dry-run=client -o yaml | kubectl apply -f -
docker tag gachamoon-webapi 192.168.31.95:32768/gachamoon-webapi/gachamoon-webapi:latest 
docker push 192.168.31.95:32768/gachamoon-webapi/gachamoon-webapi:latest