eval $(minikube docker-env)
docker build -t gachamoon-webapi -f Dockerfile ../..
eval $(minikube docker-env -u)
kubectl apply -f Deployment/deployment.yml
