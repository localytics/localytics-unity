using UnityEngine;
using System.Collections;
using UnityEditor;

public static class GenerateBundles
{
    [MenuItem("Localytics/Build iOS")]
    public static void GenerateiOSBundle()
    {
        string[] assets = {
                              "Assets/Localytics/Localytics.cs",
                              "Assets/Localytics/MiniJSON.cs",
                              "Assets/Plugins/iOS/libLocalytics.a",
                              "Assets/Plugins/iOS/Localytics.h",
                              "Assets/Plugins/iOS/LocalyticsAppController.mm",
                              "Assets/Plugins/iOS/LocalyticsUnity.mm",
                              "Assets/Plugins/iOS/LocalyticsUnity.h",
                          };

		System.IO.Directory.CreateDirectory ("../packages");
        AssetDatabase.ExportPackage(assets, "../packages/localytics-unity-ios-3.5.0.unitypackage");
    }

    [MenuItem("Localytics/Build Android")]
    public static void GenerateAndroidBundle()
    {
        string[] assets = {
                              "Assets/Localytics/Localytics.cs",
                              "Assets/Plugins/Android/android-support-v4.jar",
                              "Assets/Plugins/Android/AndroidManifest.xml",
                              "Assets/Plugins/Android/localytics.jar",
                              "Assets/Plugins/Android/localytics-unity.jar",
							  "Assets/Plugins/Android/google-play-services.jar"
                          };

		System.IO.Directory.CreateDirectory ("../packages");
        AssetDatabase.ExportPackage(assets, "../packages/localytics-unity-android-3.4.0.unitypackage");
    }
}
