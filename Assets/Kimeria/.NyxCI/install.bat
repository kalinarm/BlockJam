@echo off

call config.bat

set SOURCE_FOLDER=%CD%
set DEST_FOLDER=%CD%\\..\\..\\..\\

echo Copy destination : %DEST_FOLDER%

echo Copy .gitignore
xcopy %SOURCE_FOLDER%\\.gitignore %DEST_FOLDER%

echo Copy config.bat
xcopy %SOURCE_FOLDER%\\config.bat %DEST_FOLDER%

echo Copy build.bat
xcopy %SOURCE_FOLDER%\\build.bat %DEST_FOLDER%

echo Copy deployItch.bat
xcopy %SOURCE_FOLDER%\\deployItch.bat %DEST_FOLDER%

echo Copy gitlab-ci.yml
xcopy %SOURCE_FOLDER%\\.gitlab-ci.yml %DEST_FOLDER%

