#!/usr/bin/env bash
# Start both dev servers in parallel (macOS / Linux)
set -e

echo "Starting backend on http://localhost:5000 ..."
(cd backend/CarRecommendation.Api && dotnet run) &

echo "Starting frontend on http://localhost:4200 ..."
(cd frontend && npm start) &

wait
