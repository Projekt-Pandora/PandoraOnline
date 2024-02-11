CLS
@ECHO OFF
ECHO.
ECHO  Willkommen im Ceemas-Toolkit Installer
ECHO ----------------------------------------
ECHO  Installiert werden Projektvorlagen
ECHO.
ECHO [31m
ECHO Während der Installation können Fehlermeldungen auftreten. Vorallem wenn die Installation
ECHO bereits einmal ausgeführt wurde.
ECHO [37m
ECHO.
PAUSE

REM Projektvorlagen installieren
DOTNET new install contrib\ProjektTemplates\Pandora.ClassLibrary.Template --force
DOTNET new install contrib\ProjektTemplates\Pandora.ConsoleApp.Template --force
DOTNET new install contrib\ProjektTemplates\Pandora.WebApiApp.Template --force