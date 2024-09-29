CLS
@ECHO OFF
ECHO.
ECHO  Pandora pre requirements installer
ECHO ----------------------------------------
ECHO  1. Pandora Project-Templates
ECHO.
ECHO [31m
ECHO Press enter to start.
ECHO [37m
ECHO.
PAUSE

git submodule update --progress

REM Projektvorlagen installieren
DOTNET new install contrib\ProjektTemplates\Pandora.ClassLibrary.Template --force
DOTNET new install contrib\ProjektTemplates\Pandora.ConsoleApp.Template --force
DOTNET new install contrib\ProjektTemplates\Pandora.WebApiApp.Template --force
DOTNET new install contrib\ProjektTemplates\Pandora.WinFormApp.Template --force