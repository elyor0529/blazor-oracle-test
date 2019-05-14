@echo off
 
SET ASPNETCORE_URLS=http://0.0.0.0:5000 
SET ASPNETCORE_ENVIRONMENT=Development

dotnet BlazorDemo.Server.dll

start "http://0.0.0.0:5000"
