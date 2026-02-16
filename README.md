# SVGL Club Assetto Corsa League Site

Blazor webapp for Assetto Corsa AMP server. Tracks sessions, laptimes, and user stats for leaderboards. 

Example appsettings.json for configuration
```{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  "ContentManager": {
    "ConnectionURL": ""
  },
  "AMPServer": {
    "APIBase": "(site)/API",
    "User": "",
    "Pass": "",
    "Dir": "(instance directory)/assetto-corsa/302550",
    "ConfigDir": "cfg",
    "ResultDir": "results",
    "ConfigFile": "server_cfg.ini"
  },
  "CarClasses": [
    {
      "Name": "(Car Class)",
      "Cars": [ "(Car Model)" ]
    },
  ]
}
```
