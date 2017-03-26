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
                              "Assets/Plugins/iOS/LLCampaignBase.h",
                              "Assets/Plugins/iOS/LLCustomer.h",
                              "Assets/Plugins/iOS/LLGeofence.h",
                              "Assets/Plugins/iOS/LLInboxCampaign.h",
                              "Assets/Plugins/iOS/LLInboxDetailViewController.h",
                              "Assets/Plugins/iOS/LLInboxViewController.h",
                              "Assets/Plugins/iOS/LLPlacesCampaign.h",
                              "Assets/Plugins/iOS/LLRegion.h",
                              "Assets/Plugins/iOS/LLWebViewCampaign.h",
                              "Assets/Plugins/iOS/Localytics.h",
                              "Assets/Plugins/iOS/LocalyticsTypes.h",
                              "Assets/Plugins/iOS/LocalyticsAppController.mm",
                              "Assets/Plugins/iOS/LocalyticsUnity.mm",
                              "Assets/Plugins/iOS/LocalyticsUnity.h",
                          };

		System.IO.Directory.CreateDirectory ("../packages");
        AssetDatabase.ExportPackage(assets, "../packages/localytics-unity-ios-4.2.0.unitypackage");
    }

    [MenuItem("Localytics/Build Android")]
    public static void GenerateAndroidBundle()
    {
        string[] assets = {
                              "Assets/Localytics/Localytics.cs",
                              "Assets/Plugins/Android/android-support-v4.aar",
                              "Assets/Plugins/Android/support-annotations.jar",
                              "Assets/Plugins/Android/AndroidManifest.xml",
                              "Assets/Plugins/Android/localytics.jar",
                              "Assets/Plugins/Android/localytics-unity.jar",
                              "Assets/Plugins/Android/play-services-ads.aar",
                              "Assets/Plugins/Android/play-services-base.aar",
                              "Assets/Plugins/Android/play-services-basement.aar",
                              "Assets/Plugins/Android/play-services-gcm.aar",
                              "Assets/Plugins/Android/play-services-location.aar",
                              "Assets/Plugins/Android/play-services-maps.aar",
                              "Assets/Plugins/Android/play-services-measurement.aar"
                          };

		System.IO.Directory.CreateDirectory ("../packages");
        AssetDatabase.ExportPackage(assets, "../packages/localytics-unity-android-4.2.1.unitypackage");
    }
}
