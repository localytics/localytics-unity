@echo off

::  %1 Requires Unity Path
:: e.g generate_packages.bat D:\Unity5.5.3\Editor\Unity.exe

set ANDROID_PROJECT_NAME="localytics_android_builder"

:: build the java ("android") project

echo "Building jar files..."
cd %ANDROID_PROJECT_NAME%
echo dir
CALL gradlew clean
CALL gradlew jarRelease
cd ..

echo "Copying jar files into place..."
copy %ANDROID_PROJECT_NAME%\app\libs\localytics.jar LocalyticsPlugin\Assets\Plugins\Android
copy %ANDROID_PROJECT_NAME%\app\build\libs\app.jar LocalyticsPlugin\Assets\Plugins\Android\localytics-unity.jar

echo "Generating Packages..."
%1 -batchmode -executeMethod GenerateBundles.GenerateiOSBundle -projectPath %~dp0\LocalyticsPlugin -quit
%1 -batchmode -executeMethod GenerateBundles.GenerateAndroidBundle -projectPath %~dp0\LocalyticsPlugin -quit
echo "Done!"