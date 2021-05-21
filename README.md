# Unity Sample Project with Plugins (iOS & Android)

# Simplified Mode 

The latest versions of the SDK (6.2.x) include an option (enabled by default) in the Build Config tool int the Unity Localytics menu which drastically simplifies the build process at the cost of losing access to functionality like messaging and push notifications. In simplified mode a user can simply import the package into a project, add the Localytics App Key to localytics.options.android.json and localytics.options.ios.json and they are ready to start triggering analytics events.

# Instructions

To use these Localytics Plugins for Unity:
- Import the Unity Packages from the LocalyticsPlugin Project into your Unity Project
- Setup the SDK within Unity and the native development platforms (setting Localytics App Key and Push notification)
- Add the Localytics App Key to localytics.options.android.json and localytics.options.ios.json
- If using firebase push notifications on android then fill in strings.xml with values from the firebase control panel
- Open the tutorial scene Tutorial1_ConfiguringTheSDK.unity to check your configuration
- Start calling the Localytics API from any MonoBehavior

# Migrating from Older Versions

Please delete the files from the old version of the plugin and then once the new plugin is imported then access the new build config tool from the Localytics top bar menu. Older versions did not require a config file; whereas these newer versions have ios and android specific config files, as well as there being build specific options in the config tool. An image of the build config tool is given here:

https://github.com/AdbC99/localytics-unity/blob/master/docs/build-config-tool.png

## Importing the Unity Plugin Packages

You will need to open either the Sample Project or your own project and then import the Unity Packages by:

  Going to "Asset" -> "Import Package" -> "Custom Package..." and Navigate to the **.unitypackage** for Android or AndroidX and/or iOS
  
If you do not have a specific reason to chose Android over AndroidX then chose the AndroidX package for use on Android devices.

## Using the Sample Project

The sample project contains scenes that will assist you in configuring the SDK for your project. It is highly recommended that you start by configuring the sample project and running throught the Tutorial scenes before integrating the package into your app.

## Setup the SDK 

### iOS
1. Ensure in the Player Settings that Company Name, Product Name and iOS bundle identifier are appropriately set

2. Inside Unity, navigate to the Localytics/Resources folder and edit the file:
  ```
  localytics.options.ios.json
  ```
  Replace <LOCALYTICS_API_KEY> with your api key

3. On the top menu bar go to the Localytics menu entry and select Build Config, here you can select whether to include Location Monitoring or no, the Info.plist file will automaticaly be configured when you build. There is a build button at the bottom of the Build Config which is a convenience button and is no different to building from PlayerSettings as usual.

### Android

#### Note: On versions of Unity prior to Unity 2018.4, if you wish to use push notifications then you will need to export a gradle project, edit the build.gradle dependencies to include implementation ('androidx.legacy:legacy-support-v4:1.0.0') alongside the other aar files.
#### Note: On earlier versions of Unity, due to a Unity [bug](https://forum.unity3d.com/threads/android-deployment-error.444133/#post-2876464), you may not be able to build and run with the latest version of Android sdk tools. This has been tested with sdk tools version 25.2.2.

1. Ensure in the Player Settings that Company Name, Product Name and Android Package Name are appropriately set

2. Inside Unity, navigate to the Localytics/Resources folder and edit the file:
  ```
  localytics.options.ios.json
  ```
  Replace <LOCALYTICS_API_KEY> with your api key
  
 3. If you are using firebase push notifications then you will need to acquire a google-services.json file from your firebase console and use the values in it to populate the file:
   ```
   Assets/Plugins/Android/res/values/strings.xml
   ```
   A free online converter exists here:
  
   https://dandar3.github.io/android/google-services-json-to-xml.html
   
   
4. On the top menu bar go to the Localytics menu entry and select Build Config, here you can select whether to build for Android or AndroidX which must match the package you imported. You may select to enable or disable push notifications in the manifest when you configure the options file. You must either click Configure Manifest to build an android manifest or if you have one already then you will need to manually integrate the changes into your existing manifest. You can tick Ignore Manifest Issues if you are using a custom configuration. There is a build button at the bottom of the Build Config which is a convenience button and is no different to building from PlayerSettings as usual.

Also for Android you will need to set the global gradle variables:

`android.enableJetifier=true`

`android.useAndroidX=true`

This can be done in Android Studio.
  
## Calling the SDK in C\# #

After successfully setting up the SDK, the static function for the Localytics C\# class should be available within any MonoBehaviour of the project under the `LocalyticsUnity` namespace.
```
using LocalyticsUnity;

// or

namespace LocalyticsUnity {
	...
}
```

Localytics will automatically initiate when the application loads  (i.e. auto integrate and setup any Messaging specified in previous steps) and you can call it at any time.  Note that the scripts should not be called from the editor, so preprocessor directives may be necessary.

When targeting iOS or Android only, most methods can be called without preprocessor directives:
```
// within a MonoBehaviour
Localytics.LoggingEnabled = true;
Localytics.RegisterForAnalyticsEvents();
Localytics.RegisterForMessagingEvents();

Localytics.TagEvent("click");

// e.g. with preprocessor directives
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
    Localytics.TagScreen("xxx");
    Localytics.Upload();
#endif

// It can also be just
#if !UNITY_EDITOR && UNITY_IOS
    ...
#elif !UNITY_EDITOR && UNITY_ANDROID
    ...
#endif
```
When targeting platforms other than iOS or Android, a `NotImplementedException` will be thrown if these SDK method are called. Therefore you should considering using preprocessor directives regardless if you support other platforms.

A small subset of the API requires preprocessor directives:
```
// within a MonoBehaviour
#if UNITY_IOS

Localytics.HandleTestModeURL(string url);
Localytics.InAppAdIdParameterEnabled;
Localytics.PushToken;
Localytics.PersistLocationMonitoring(bool persist)
Localytics.setCustomerIdWithOptedOut(string value, bool optedOut);

#elif UNITY_ANDROID

Localytics.RegisterPush;
Localytics.NotificationsDisabled;
Localytics.PushRegistrationId;
Localytics.SetLocationMonitoringEnabled(bool enabled, bool persist)
Localytics.ClearInAppMessageDisplayActivity();
Localytics.DisplayPushNotification(Dictionary<string, string> data);
Localytics.setCustomerIdWithPrivacyOptedOut(string value, bool optedOut)

#endif
```

## Reducing the Localytics Footprint

It is best not to included unused files in your build and unused entries in your android manifest. The Localytics Build Config tool allows iOS features to be removed easily at build time. For android some of the .aar files can be removed depending on your configuration:

   * If you are not using the collect adid feature then you can remove the play-services-ads-.... files
   * If you are not using push notifications then you can remove the firebase-.... files
   * If you are not using location then you can remove the play-services-location and places files
   * If you are not using install referall receiving then you can remove the installreferrer file
   
The AndroidManifest.xml generated by the Localytics build tool will have several features set to enabled or disabled in the andorid manifest based on what you have selected in your options files. If you would like to keep your manifest size down then you can remove any section that has the tag: android:enabled="false"

## API References

### API Documentation

The Localytics.cs file contains doxygen style comments for all methods, for your convenience the generated doxygen documentation is available here:

https://localytics.github.io/localytics-unity/html/class_localytics_unity_1_1_localytics.html

### Sample Application (SampleUnityProject)

To run the project, you need to follow the steps above (Build/Import .unitypackage and setup the SDK).  Inside TestLocalytics.cs, there are examples of how to call most of the available API. Note that the sample application call through most of the available API on start up for testing and demo purpose; this is unlikely to be how it fits into a real application. As mention earlier, Localytics will automatically initiate when the application loads (i.e. auto integrate and setup any Messaging specified in previous steps) and you can call the other methods at any time.


### Available API

Refer to the iOS and Android Documentation for details of each call. The available API contains almost all of the native API of the same version. Generally, the Pascal Case function name is C\# correspond to the native function with the Camel Case name (e.g. `Localytics.TagEvent(...)` calls `tagEvent(...)` in the iOS and Android library). The implementation is in Localytics.cs, within the LocalyticsPlugin/Assets/Localytics folder when you imported the .unitypackage.

#### C\# Listeners

These are C\# level events that can be dispatched from Localytics service.

```
// Analytics
public static event LocalyticsDidTagEvent OnLocalyticsDidTagEvent;
public static event LocalyticsSessionDidOpen OnLocalyticsSessionDidOpen;
public static event LocalyticsSessionWillClose OnLocalyticsSessionWillClose;
public static event LocalyticsSessionWillOpen OnLocalyticsSessionWillOpen;

// Messaging
public static event LocalyticsDidDismissInAppMessage OnLocalyticsDidDismissInAppMessage;
public static event LocalyticsDidDisplayInAppMessage OnLocalyticsDidDisplayInAppMessage;
public static event LocalyticsShouldShowInAppMessage OnLocalyticsShouldShowInAppMessage;
public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;
public static event LocalyticsShouldShowPlacesPushNotification OnLocalyticsShouldShowPlacesPushNotification;

#if UNITY_ANDROID
public static event LocalyticsWillShowPlacesPushNotification OnLocalyticsWillShowPlacesPushNotification;
public static event LocalyticsShouldShowPushNotification OnLocalyticsShouldShowPushNotification;
public static event LocalyticsWillShowPushNotification OnLocalyticsWillShowPushNotification;
#endif

// Location
public static event LocalyticsDidUpdateLocation OnLocalyticsDidUpdateLocation;
public static event LocalyticsDidTriggerRegions OnLocalyticsDidTriggerRegions;
public static event LocalyticsDidUpdateMonitoredGeofences OnLocalyticsDidUpdateMonitoredGeofences;
```

In order to listen to them,

```
// Register for Analytics, Messaging, and Location Events
Localytics.RegisterForAnalyticsEvents();
Localytics.RegisterForMessagingEvents();
Localytics.RegisterForLocationEvents();


// Add Analytics Event Handlers
Localytics.OnLocalyticsDidTagEvent += Localytics_OnLocalyticsDidTagEvent;
Localytics.OnLocalyticsSessionWillOpen += Localytics_OnLocalyticsSessionWillOpen;
Localytics.OnLocalyticsSessionDidOpen += Localytics_OnLocalyticsSessionDidOpen;
Localytics.OnLocalyticsSessionWillClose += Localytics_OnLocalyticsSessionWillClose;

// Add Messaging Event Handlers
Localytics.OnLocalyticsDidDismissInAppMessage += Localytics_OnLocalyticsDidDismissInAppMessage;
Localytics.OnLocalyticsDidDisplayInAppMessage += Localytics_OnLocalyticsDidDisplayInAppMessage;
Localytics.OnLocalyticsWillDismissInAppMessage += Localytics_OnLocalyticsWillDismissInAppMessage;
Localytics.OnLocalyticsWillDisplayInAppMessage += Localytics_OnLocalyticsWillDisplayInAppMessage;
Localytics.OnLocalyticsShouldShowPushNotification += Localytics_OnLocalyticsShouldShowPushNotification;

#if UNITY_ANDROID
Localytics.OnLocalyticsWillShowPlacesPushNotification += Localytics_OnLocalyticsWillShowPlacesPushNotification;
Localytics.OnLocalyticsShouldShowPlacesPushNotification += Localytics_OnLocalyticsShouldShowPlacesPushNotification;
Localytics.OnLocalyticsWillShowPushNotification += Localytics_OnLocalyticsWillShowPushNotification;
#endif

// Add Location Event Handlers
Localytics.OnLocalyticsDidUpdateLocation += Localytics_OnLocalyticsDidUpdateLocation;
Localytics.OnLocalyticsDidTriggerRegions += Localytics_OnLocalyticsDidTriggerRegions;
Localytics.OnLocalyticsDidUpdateMonitoredGeofences += Localytics_OnLocalyticsDidUpdateMonitoredGeofences;
...

// Sample Event Handlers to demonstrate.
void Localytics_OnLocalyticsDidTagEvent(string eventName, Dictionary<string, string> attributes, long customerValueIncrease)
{
    Debug.Log("Did tag event: name: " + eventName + " attributes.Count: " + attributes.Count + " customerValueIncrease: " + customerValueIncrease);
}

void Localytics_OnLocalyticsSessionWillClose()
{
    Debug.Log("Session will close");
}

void Localytics_OnLocalyticsSessionDidOpen(bool isFirst, bool isUpgrade, bool isResume)
{
    Debug.Log("Session did open: isFirst: " + isFirst + " isUpgrade: " + isUpgrade + " isResume: " + isResume);
}

void Localytics_OnLocalyticsSessionWillOpen(bool isFirst, bool isUpgrade, bool isResume)
{
    Debug.Log("Session will open: isFirst: " + isFirst + " isUpgrade: " + isUpgrade + " isResume: " + isResume);
}

void Localytics_OnLocalyticsDidDismissInAppMessage()
{
    Debug.Log ("DidDismissInAppMessage");
}

void Localytics_OnLocalyticsDidDisplayInAppMessage()
{
    Debug.Log ("DidDisplayInAppMessage");
}

void Localytics_OnLocalyticsWillDismissInAppMessage()
{
    Debug.Log ("WillDismissInAppMessage");
}

void Localytics_OnLocalyticsWillDisplayInAppMessage()
{
	Debug.Log ("WillDisplayInAppMessage");
}

#if UNITY_ANDROID
bool Localytics_OnLocalyticsShouldShowPushNotification(PushCampaignInfo campaign)
{
	Debug.Log ("ShouldShowPushNotification");
	return true; // return false to suppress the notification
}

bool Localytics_OnLocalyticsShouldShowPlacesPushNotification(PlacesCampaignInfo campaign)
{
	Debug.Log ("ShouldShowPlacesPushNotification");
	return true; // return false to suppress the notification
}

AndroidJavaObject Localytics_OnLocalyticsWillShowPushNotification(AndroidJavaObject notificationBuilder, PushCampaignInfo campaign)
{
	Debug.Log ("WillShowPushNotification");
  // ... optionally modify the notificationBuilder
	//notificationBuilder.Call<AndroidJavaObject> ("setContentTitle", "New Title");
	return notificationBuilder;
}

AndroidJavaObject Localytics_OnLocalyticsWillShowPlacesPushNotification(AndroidJavaObject notificationBuilder, PlacesCampaignInfo campaign)
{
	Debug.Log ("WillShowPlacesPushNotification");
  // ... optionally modify the notificationBuilder
	//notificationBuilder.Call<AndroidJavaObject> ("setContentTitle", "New Title");
	return notificationBuilder;
}
#endif

#if UNITY_ANDROID
void Localytics_OnLocalyticsDidUpdateLocation(AndroidJavaObject location)
{
	Debug.Log ("DidUpdateLocation");
	double latitude = location.Call<double> ("getLatitude");
	double longitude = location.Call<double> ("getLongitude");
	Debug.Log ("lat=" + latitude + " lng=" + longitude);
}
#else
void Localytics_OnLocalyticsDidUpdateLocation(Dictionary<string, object> locationDict)
{
	Debug.Log ("DidUpdateLocation");
	Debug.Log ("lat=" + locationDict["latitude"] + " lng=" + locationDict["longitude"]);
}
#endif

void Localytics_OnLocalyticsDidTriggerRegions(List<CircularRegionInfo> regions, Localytics.RegionEvent regionEvent)
{
	Debug.Log ("DidTriggerRegions for event: " + regionEvent);
}

void Localytics_OnLocalyticsDidUpdateMonitoredGeofences(List<CircularRegionInfo> added, List<CircularRegionInfo> removed)
{
	Debug.Log ("DidUpdateMonitoredGeofences");
}
