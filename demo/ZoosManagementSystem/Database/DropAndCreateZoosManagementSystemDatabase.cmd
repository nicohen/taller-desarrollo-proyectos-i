@echo off
setlocal 
%~d0
cd "%~dp0"

Set SERVERNAME="%1%"
if "%1%"=="" set SERVERNAME=.\SQLEXPRESS

echo.
echo =====================================
echo Creating ZoosManagementSystem database
echo =====================================
echo.

echo Droping and creating the ZooManagementSystem database...

@call SqlCmd -S %SERVERNAME% -E -i ".\DropAndCreateZoosManagementSystemDatabase.sql"
IF ERRORLEVEL 1 GOTO ERROR

echo Creating Stored Procedures into AdventureWorks2008 database...

@call SqlCmd -S %SERVERNAME% -E -i ".\PopulateZoosManagementSystemDatabase.sql"
IF ERRORLEVEL 1 GOTO ERROR

echo.
echo ================================================================
echo ZoosManagementSystem database created and populated successfully!
echo ================================================================
GOTO FINISH

:ERROR
echo.
echo ======================================
echo An error occured. 
echo Please review messages above.
echo ======================================

:FINISH
@PAUSE
EXIT %ERRORLEVEL%