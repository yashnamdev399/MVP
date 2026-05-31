@echo off
REM Start both dev servers in parallel (Windows)
echo Starting backend on http://localhost:5000 ...
start "Backend" cmd /c "cd backend\CarRecommendation.Api && dotnet run"

echo Starting frontend on http://localhost:4200 ...
start "Frontend" cmd /c "cd frontend && npm start"

echo Both servers starting. Check the opened terminal windows.
