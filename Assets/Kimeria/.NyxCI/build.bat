
set BUILD_NAME=NyxTester
set UNITY_VERSION=2018.4.36f1
set BUILD_TARGET=StandaloneWindows64
set CI_PROJECT_NAME=NyxTester
set CI_PROJECT_DIR=

D:\Softs\Unitys\%UNITY_VERSION%\Editor\Unity.exe -projectPath %CI_PROJECT_DIR% -logfile D:\Gitlab_runner\Logs\%BUILD_TARGET%.log -customBuildPath %BUILD_PATH% -customProjectName %CI_PROJECT_NAME% -customBuildTarget %BUILD_TARGET% -pipelineId %CI_PIPELINE_ID% -batchmode -nographics -executeMethod Nyx.Builder.BuildDefaultProject -quit
