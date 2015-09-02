@echo off

::  %1 Requires Unity Path

%1 -batchmode -executeMethod GenerateBundles.GenerateiOSBundle -projectPath %~dp0\SampleUnityProject -quit
%1 -batchmode -executeMethod GenerateBundles.GenerateAndroidBundle -projectPath %~dp0\SampleUnityProject -quit