@echo off

REM set project path
call config.bat

set LOG_FILE=%LOG_PATH%\deployItch.log
set ITCH_KEY=%APPDATA%/itch/butler_creds

set USER=kalinka
set GAME=%PROJECT_SLUG%
set CHANNEL=Win64
set VERSION_FILE=%GAME_VERSION_FILE%

echo ---------------------------------------------------------------
echo      Deploy to Itch.io
echo Current dir = %CD%
echo Path to project = %PROJECT_PATH%
echo Export log to = %LOG_FILE%
echo ---------------------------------------------------------------

echo pushing to itch %USER%/%GAME%:%CHANNEL% 

set BUILD_FOLDER=%BUILD_PATH%\Dashboalder
echo build finded here : %BUILD_FOLDER%

butler push %BUILD_FOLDER% %USER%/%GAME%:%CHANNEL% --userversion-file %VERSION_FILE%



