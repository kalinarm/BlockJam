@echo off

REM --------------------------
REM edit here
set PROJECT_NAME=NyxTester
set PROJECT_SLUG=nyxTester
set PROJECT_PATH=%CD%
REM --------------------------

REM auto config
set BUILD_PATH=%PROJECT_PATH%\Build
set LOG_PATH=%PROJECT_PATH%\Log
set GAME_VERSION_FILE=%BUILD_PATH%\%PROJECT_NAME%_Data\StreamingAssets\GameVersion.txt


echo ------------------------------------------
echo Project Name : %PROJECT_NAME%
echo Project Path : %PROJECT_PATH%
echo Deploy Path : %BUILD_PATH%
echo Game Version File : %GAME_VERSION_FILE%
echo ------------------------------------------


