#!/bin/bash

DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )

ANDROID_PROJECT_NAME="localytics_android_builder"

#if a path is supplied - use that for the unity executable otherwise default
if (($# == 0))
then
UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
else
UNITY_PATH=$1
fi

#build the java ("android") project
cd $ANDROID_PROJECT_NAME
./gradlew clean
./gradlew build
cd ..

#extract the needed .jar files
unzip $ANDROID_PROJECT_NAME/app/build/outputs/aar/app-release.aar -d $ANDROID_PROJECT_NAME/app/build/outputs/aar/tmp_files

#copy jar files
cp $ANDROID_PROJECT_NAME/app/build/outputs/aar/tmp_files//libs/localytics.jar LocalyticsPlugin/Assets/Plugins/Android/
cp $ANDROID_PROJECT_NAME/app/build/outputs/aar/tmp_files/classes.jar LocalyticsPlugin/Assets/Plugins/Android/localytics-unity.jar


#execute the custom packaging/bundling scripts in the plugin project
echo -n "Generating packages..."
$UNITY_PATH -batchmode -executeMethod GenerateBundles.GenerateiOSBundle -projectPath $DIR/LocalyticsPlugin -logFile -quit
$UNITY_PATH -batchmode -executeMethod GenerateBundles.GenerateAndroidBundle -projectPath $DIR/LocalyticsPlugin -logFile -quit
echo "Done!"


