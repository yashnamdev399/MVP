# Car Recommendation MVP

Helps confused car buyers confidently shortlist a vehicle.

## Tech Stack

| Layer    | Tech                        |
|----------|-----------------------------|
| Backend  | .NET 10 Web API (C#)       |
| Frontend | Angular 19 (standalone)    |
| Database | MongoDB Atlas (cloud)      |

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js 22+](https://nodejs.org/) & npm
- Angular CLI: `npm i -g @angular/cli`
- A MongoDB Atlas connection string (update `backend/.../appsettings.json`)

## Quick Start (local dev)

**Windows:**
```
start-dev.bat
```

**macOS / Linux:**
```bash
chmod +x start-dev.sh
./start-dev.sh
```

Or use Docker Compose (update env vars in `docker-compose.yml` first):
```
docker compose up
```

| Service  | URL                        |
|----------|----------------------------|
| API      | http://localhost:5000/api/cars |
| Frontend | http://localhost:4200       |

## Project Structure

```
├── backend/
│   └── CarRecommendation.Api/   # .NET Web API
├── frontend/                    # Angular workspace
├── docker-compose.yml
├── start-dev.bat                # Windows launcher
└── start-dev.sh                 # Unix launcher
```
