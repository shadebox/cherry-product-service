## Product Service Api
Manage and serve product and product related data

## Getting Started

-   Requires .Net Core 3.1

## Developing

### Steps to setup user secrets:
-   Create folder UserSecret under C:\Users\{username}\AppData\Roaming\Microsoft
-   Create folder 163a88cb-54c0-4db5-a8f5-44387a7b5ed3 under UserSecrets
-   Add secrets.json with connection string for database

### Running the project

To run the project from a CLI:
-   Open terminal and ```cd``` to the ProductService.Rest directory
-   Run ```dotnet restore```
-   Run ```dotnet run```

To call api:
-   Get Api: curl -X GET https://localhost:5001/product/1 -H 'Content-Type: application/json' -k
-   Get All Api: curl -X GET https://localhost:5001/product -H 'Content-Type: application/json' -k
-   Post Api: curl -X POST https://localhost:5001/product -H 'Content-Type: application/json' -d '' -k
-   Put Api: curl -X PUT https://localhost:5001/product/1 -H 'Content-Type: application/json' -d '' -k
-   Delete Api: curl -X DELETE https://localhost:5001/product/1 -H 'Content-Type: application/json' -k

## Deployment

TBD