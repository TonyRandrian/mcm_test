# licence2425

{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000", "http://localhost:5282"]
  },
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=mcm_db3;Username=havotrauser;Password=havotrauser"
  },
  "FileConfiguration": {
    "document": {
      "storage": "assets/documents"
    },
    "image": {
      "storage": "assets/images"
    }
  },
  "JwtSettings": {
    "SecretKey": "secret-key-project-licence-2425-andrianantenaina",
    "Issuer": "mcm_api",
    "Audience": "mcm_api",
    "ExpiryMinutes": 60
  }
}
