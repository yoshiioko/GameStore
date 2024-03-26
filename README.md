# GameStore API

## Starting DB Server
```terminal
docker compose up
```

## Setting the connection string to secret manager
```
dotnet user-secrets init

host="localhost"
username="admin"
password="Your-Password-Here"
database="GameStoreDB"

dotnet user-secrets set "ConnectionStrings:GameStoreContext" "Host=$host; Username=$username; Password=$password; Database=$database"

dotnet user-secrets list
```
