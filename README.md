# Unity Plugins (iOS & Android)

To use these Localytics Plugins for Unity:
- Build and Import the Unity Packages from the LocalyticsPlugin Project into your Unity Project
- Setup the SDK within Unity and the native development platforms (setting Localytics App Key and Push notification)
- Start calling the Localytics API from any MonoBehavior

## Building and Importing the Unity Plugin Packages

You will need development environment setup for Unity, Android and/or iOS. After that,

1. Build the Plugins (or use the generated ones from /release folder)

  Call `generate_packages.sh` or `generate_packages.bat` contained in the root of this repository, depending on your system (OSX or Windows). Make sure the LocalyticsPlugin project is not currently opened in Unity, or it may cause some issues. The generated packages will be in the 'packages' folder with the respective version x.x.x. (i.e. localytics-unity-android-x.x.x.unitypackage and localytics-unity-ios-x.x.x.unitypackage)

2. Import the Unity Packages

  Go to "Asset" -> "Import Package" -> "Custom Package..." and Navigate to the generated .unitypackage for Android and/or iOS in Step 1.


## Setup the SDK

### iOS
1. Inside Unity, enable AdSupport framework within **Import Settings** of 'Assets/Plugins/iOS/libLocalytics.a'

2. Initialization in UnityAppController

  Inside Unity, LocalyticsAppController.mm is included within 'Assets/Plugins/iOS' as a template; if the application already have another UnityAppController, they need to be merged. You can also modified the file within the exported XCode project, but the changes won't persist if the project is regenerated from Unity.
  * call autoIntegrate with your Locatlyics App Key

    ```
    [Localytics autoIntegrate:@"xxxxxxxxxxxxxxxxxxxxxxx-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" launchOptions:launchOptions];
    ```
  * (Optional) Push notification hookup (see LocalyticsAppController.mm)
  * (Optional) Other Delegate Methods (see LocalyticsAppController.mm `application:handleWatchKitExtensionRequest:reply:`)

3. After you generate and open the XCode project. Within XCode, add `libsqlite3.dylib` and `libz.tbd` (e.g. under "General" -> "Linked Frameworks and Libraries" in the .xcodeproj settings)

### Android
1. Inside Unity, AndroidManifest.xml is included within 'Assets/Plugins/Android' as a sample; you may provide your own. You can also modified the file within the exported Android project, but the changes won't persist if the project is regenerated from Unity. The following changes are needed in the sample AndroidManifest.xml, or they need to be added to your existing AndroidManifest.xml:
  * Replace the android:value in the sample with your Localytics App Key

    ```
    <meta-data android:name="LOCALYTICS_APP_KEY" android:value="xxxxxxxxxxxxxxxxxxxxxxx-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"/>
  ```
  * Replace YOUR.PACKAGE.NAME with your application's package identifier

    ```
    // e.g.,
    <permission android:name="YOUR.PACKAGE.NAME.permission.C2D_MESSAGE" android:protectionLevel="signature" />
    <uses-permission android:name="YOUR.PACKAGE.NAME.permission.C2D_MESSAGE" />
    ...
    ```
  * (Optional) Setup Push with your Project Number (not the Project ID string).

    ```
    // Important: You need to escape a space in front
    // Otherwise it will be interpreted as an int,
    // which is not a large enough data type to hold a project ID
    <meta-data android:name="LOCALYTICS_PUSH_PROJECT_ID" android:value="\ #################"/>
    ```

2. Modifying Application launcher

  If the existing Unity Android Project already has a main Application/Activity defined, you will need to merge this with our class.  For references on what is needed refer the application and activity class inside localytics-android/src within this repository; you can also inspect the class inside Android Studio). Basically the following is needed.
  * Inside `onCreate()` of com.localytics.android.unity.LocalyticsApplication.java or your main application, call this

    ```
    registerActivityLifecycleCallbacks(new LocalyticsActivityLifecycleCallbacks(this));
    ```
  * Inside `onCreate()` of com.localytics.android.unity.LocalyticsUnityPlayerActivity.java or your main activity, you may want to register Push. Most of the other code within this file are the same as the template Activity generated (i.e. UnityPlayerActivity).

    ```
    try
    {
        ApplicationInfo appInfo = getPackageManager().getApplicationInfo(getPackageName(), PackageManager.GET_META_DATA);

        String pushId = appInfo.metaData.getString("LOCALYTICS_PUSH_PROJECT_ID");
        Localytics.registerPush(pushId.trim());
    }
    catch (NameNotFoundException e)
    {
        ...
    }
    ```

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

Localytics.InAppAdIdParameterEnabled;
Localytics.PushToken;

#elif UNITY_ANDROID

Localytics.PushDisabled;
Localytics.PushRegistrationId;
Localytics.ClearInAppMessageDisplayActivity();

#endif
```

## API References

### Sample Application (SampleUnityProject)

To run the project, you need to follow the steps above (Build/Import .unitypackage and setup the SDK).  Inside TestLocalytics.cs, there are examples of how to call most of the available API. Note that the sample application call through most of the available API on start up for testing and demo purpose; this is unlikely to be how it fits into a real application. As mention earlier, Localytics will automatically initiate when the application loads (i.e. auto integrate and setup any Messaging specified in previous steps) and you can call the other methods at any time.


### Available API

Refer to the iOS and Android Documentation for details of each call. The available API contains almost all of the native API of the same version. Generally, the Pascal Case function name is C\# correspond to the native function with the Camel Case name (e.g. `Localytics.TagEvent(...)` calls `tagEvent(...)` in the iOS and Android library). The implementation is in Localytics.cs, within the LocalyticsPlugin/Assets/Localytics folderor when you imported the .unitypackage.

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
public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;
```

In order to listen to them,

```
// Register for Analytics and Messaging Events
Localytics.RegisterForAnalyticsEvents();
Localytics.RegisterForMessagingEvents();


//Add Analytics Event Handlers
Localytics.OnLocalyticsDidTagEvent += Localytics_OnLocalyticsDidTagEvent;
Localytics.OnLocalyticsSessionWillOpen += Localytics_OnLocalyticsSessionWillOpen;
Localytics.OnLocalyticsSessionDidOpen += Localytics_OnLocalyticsSessionDidOpen;
Localytics.OnLocalyticsSessionWillClose += Localytics_OnLocalyticsSessionWillClose;

//Add Messaging Event Handlers
Localytics.OnLocalyticsDidDismissInAppMessage += Localytics_OnLocalyticsDidDismissInAppMessage;
Localytics.OnLocalyticsDidDisplayInAppMessage += Localytics_OnLocalyticsDidDisplayInAppMessage;
Localytics.OnLocalyticsWillDismissInAppMessage += Localytics_OnLocalyticsWillDismissInAppMessage;
Localytics.OnLocalyticsWillDisplayInAppMessage += Localytics_OnLocalyticsWillDisplayInAppMessage;

...

// Sample Event Handlers to demonstrate the  parameters.  There's no parameters for the Messaging callbacks.
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
```


### Localytics C\# Class Members List

```
// Version 3.4.0
public enum ProfileScope { Application, Organization }
public enum InAppMessageDismissButtonLocation { Left, Right }

public static event LocalyticsDidTagEvent OnLocalyticsDidTagEvent;
public static event LocalyticsSessionDidOpen OnLocalyticsSessionDidOpen;
public static event LocalyticsSessionWillClose OnLocalyticsSessionWillClose;
public static event LocalyticsSessionWillOpen OnLocalyticsSessionWillOpen;
public static event LocalyticsDidDismissInAppMessage OnLocalyticsDidDismissInAppMessage;
public static event LocalyticsDidDisplayInAppMessage OnLocalyticsDidDisplayInAppMessage;
public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;

public static string AnalyticsHost
public static string AppKey
public static string CustomerId
public static InAppMessageDismissButtonLocation InAppMessageDismissButtonLocationEnum
public static string InstallId
public static string LibraryVersion
public static bool LoggingEnabled
public static string MessagingHost
public static bool OptedOut
public static string ProfilesHost
public static bool TestModeEnabled


#if UNITY_ANDROID /// Android Only
public static bool PushDisabled
public static string PushRegistrationId
public static void ClearInAppMessageDisplayActivity()
#elif UNITY_IOS /// iOS Only
public static string PushToken
public static bool InAppAdIdParameterEnabled
#endif


public static void OpenSession()
public static void CloseSession()

public static void TagEvent(string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
public static void TagScreen(string screen)

public static void Upload()

public static void RegisterForAnalyticsEvents()
public static void UnregisterForAnalyticsEvents()

public static void RegisterForMessagingEvents()
public static void UnregisterForMessagingEvents()


public static void SetCustomDimension(int dimension, string value)
public static string GetCustomDimension(int dimension)

public static string GetIdentifier(string key)

public static void IncrementProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
public static void DecrementProfileAttribute(string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
public static void DeleteProfileAttribute(string attributeName, ProfileScope scope = ProfileScope.Application)
public static void AddProfileAttributesToSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void AddProfileAttributesToSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void RemoveProfileAttributesFromSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void RemoveProfileAttributesFromSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)

public static void SetIdentifier(string key, string value)
public static void SetLocation(LocationInfo location)
public static void SetProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetCustomerEmail(string email)
public static void SetCustomerFirstName(string firstName)
public static void SetCustomerLastName(string lastName)
public static void SetCustomerFullName(string fullName)

public static void TriggerInAppMessage(string triggerName, Dictionary<string, string> attributes = null)
public static void DismissCurrentInAppMessage()

```
