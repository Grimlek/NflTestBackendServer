# Ef Core Migration Notes
Perform all actions from the solution directory

- Generate Initial Migration (When Separate startup and infrastructure projects)
	```
	dotnet ef --startup-project ./AngularTestBackendServer --project ./AngularTestBackendServer.Data migrations add INITIAL
	```

- Run Latest Migration (When Separate startup and infrastructure projects)
	```
	dotnet ef --project ./AngularTestBackendServer.Data database update
	```


# Sql Server

- Azure Managed Instance
	csexton-admin
	63b44068-6a7d-4373-845a-da4e133d0732

	
# Kubernetes
## Install
### Windows
- Prerequisite install docker desktop
- Install kubectl and add to path
- Install minikube and add to path


## Minikube
- Dashboard and will display url for access
	```
	minikube dashboard
	```
- Dashboard will show errors for the pods based on namespaces (dropdown to the left of search at the top of the page)


## Namespace
Namespaces are generally not needed for smaller companies are it groups kubernetes resources.

- cd into C:/Users/c.sexton/_dev/_k8
	```
	kubectl create -f ./namespaces/angular-test-projects-namespace.yml
	```
 
- Optional: set namespace as default context, prevents from having to add --namespace to commands
	```
    kubectl config set-context --current --namespace=angular-test-projects
	```

## Pod

- cd into C:/Users/c.sexton/_dev/_k8
  ```
  kubectl create -f ./pods/angular-test-backend-server-pod.yml
  ```

- expose the pod
  ```
  kubectl expose pod angular-test-backend-server --port=5000 --target-port 5000	
  ```

- check the existence of the pod
  ```
  kubectl get svc jenkins-pod -o yaml
  ```


