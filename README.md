# AuthentikSharp
A client lib for Authentik to get information.

### Example
Install the package from NuGet https://www.nuget.org/packages/AuthentikSharp
```cs
AuthentikClient Auth = new AuthentikClient("https://auth.domain.com", "YourTokenHere");
```

### Create Token
You need to create an API token to access your Authentik instance.
> 
Go to directory app tokens.
>
![image](https://github.com/FluxpointDev/AuthentikSharp/assets/17956143/d6da6165-af1d-44a4-9c17-699713dfd762)
> 
Then create a non expiring token and add it to YouTokenHere
> 
![image](https://github.com/FluxpointDev/AuthentikSharp/assets/17956143/a1c4b5c0-898b-47d4-bcc7-79b50151e61c)
