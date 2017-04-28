#!/bin/bash

DIR=$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )
ANDROID_PROJECT_NAME="localytics-android"

if (($# == 0))
then
	UNITY_PATH="/Applications/Unity/Unity.app/Contents/MacOS/Unity"
else
	UNITY_PATH=$1
fi

ant -buildfile $ANDROID_PROJECT_NAME/build.xml release

cp $ANDROID_PROJECT_NAME/bin/classes.jar LocalyticsPlugin/Assets/Plugins/Android/localytics-unity.jar
cp $ANDROID_PROJECT_NAME/libs/localytics.jar LocalyticsPlugin/Assets/Plugins/Android

echo -n "Generating packages..."
$UNITY_PATH -batchmode -executeMethod GenerateBundles.GenerateiOSBundle -projectPath $DIR/LocalyticsPlugin -logFile -quit
$UNITY_PATH -batchmode -executeMethod GenerateBundles.GenerateAndroidBundle -projectPath $DIR/LocalyticsPlugin -logFile -quit
echo "Done!"
