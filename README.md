# Unity Plugins (iOS & Android)

To use these Localytics Plugins for Unity:
- Import (or optionally build and import) the Unity Packages from the LocalyticsPlugin Project into your Unity Project
- Setup the SDK within Unity and the native development platforms (setting Localytics App Key and Push notification)
- Start calling the Localytics API from any MonoBehavior

## Building and Importing the Unity Plugin Packages

If you have Unity 2017.3 you should be able to use the unitypackage files that come checked in to this repository inside the `release` folder.

If you want to customize the localytics plugin (for a newer or older version of unity, for instance) keep reading this section.

You will need development environment setup for Unity, Android and/or iOS. After that,

1. Build the Plugins (or use the generated ones from the `release` folder)

When creating the Android plug-in you may either supply your own unity.jar (`unity-classes.jar`) file or use the one already contained in the project (which came from Unity 2017.3).  Different versions of unity will require different versions of the `unity-classes.jar` to link against (and may not provide a consistent interface with the provided code).  If these files are not present or the wrong version is used you may experience un-anticipated problems in the android build.  This jar file is needed for the plugin to be built correctly and it's interface must match that in your completed android project.

The easiest way to get the `unity-classes.jar` file that I know of on MacOS is to create an empty unity project and export the project to Android Studio.  The `unity-classes.jar` file will be contained inside this project.  Before attempting to generate the pacakages, find the file and copy it over the existing version at: 
`localytics_android_builder/app/libs/unity-classes.jar`

  Call `generate_packages.sh` or `generate_packages.bat` contained in the root of this repository, depending on your system (OSX or Windows). Make sure the LocalyticsPlugin project is closed when executing these scripts. Otherwise, you may experience issues. The generated packages will be in the 'packages' folder with the respective version x.x.x. (i.e. localytics-unity-android-x.x.x.unitypackage and localytics-unity-ios-x.x.x.unitypackage)

When these scripts are executed they first build the android studio project, `localytics_android_builder` (which produces the compiled localytics code), copy the compiled results into a different folder, and then run a packaging unity script (unity project is located at 'LocalyticsPlugin') to create the '.unitypackage' files.

2. Import the Unity Packages

  Go to "Asset" -> "Import Package" -> "Custom Package..." and Navigate to the generated **.unitypackage** for Android and/or iOS in Step 1.


## Setup the SDK

### iOS
1. Inside Unity, enable **AdSupport** framework for **libLocalytics.a**:
  * Within Unity navigate to "Assets" -> "Plugins" -> "iOS".
  * Select **libLocalytics.a**, and within the **Import Settings** section of the **Inspector** tab, enable **AdSupport**.
  * Click **Apply**.

2. Initialization in UnityAppController

  Inside Unity, `LocalyticsAppController.mm` is included within 'Assets/Plugins/iOS' as a template; if the application already has another UnityAppController, they need to be merged. You can also modify the file within the exported XCode project, but the changes won't persist if the project is regenerated from Unity.
  * Call `autoIntegrate` with your Locatlyics App Key

    ```
    [Localytics autoIntegrate:@"xxxxxxxxxxxxxxxxxxxxxxx-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx" launchOptions:launchOptions];
    ```
  * (Optional) Push notification setup (see `LocalyticsAppController.mm` and the [push messaging documentation](http://docs.localytics.com/dev/ios.html#push-messaging-ios)).

3. Inside Unity, configure test mode by adding the URL scheme:
  * Open **Player Settings** by navigating to "Edit" -> "Project Settings" -> "Player".
  * Select the **Settings for iOS** tab within the **Inspector** tab.
  * Within the **Other Settings** section, set the **Supported URL Schemes** size value to `1`, and then set the element value to "amp" followed by your App Key (e.g. "ampYOUR_APP_KEY").

4. After you generate and open the XCode project, add the `libsqlite3.tbd` and `libz.tbd` libraries (located under "General" -> "Linked Frameworks and Libraries" in the .xcodeproj settings)

### Android
#### Note: The android platform, unity and this plugin may fall out of lock-step with one-another which may cause compatibility issues

1. Inside Unity, `AndroidManifest.xml` is included within "Assets/Plugins/Android" as a sample; you may provide your own. You can also modify the file within the exported Android project, but the changes won't persist if the project is regenerated from Unity. The following changes are needed in the sample `AndroidManifest.xml`, or they need to be added to your existing `AndroidManifest.xml`:
  * Replace the android:value in the sample with your Localytics App Key

    ```
    <meta-data android:name="LOCALYTICS_APP_KEY" android:value="xxxxxxxxxxxxxxxxxxxxxxx-xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx"/>
  ```
  * Replace YOUR_APP_KEY with your Localytics App Key in the test mode `IntentFilter` within the main `Activity` declaration. The value should be "amp" followed by your App Key (e.g. "ampYOUR_APP_KEY").
  ```
  <intent-filter>
      <action android:name="android.intent.action.VIEW"/>
      <category android:name="android.intent.category.DEFAULT"/>
      <category android:name="android.intent.category.BROWSABLE"/>
      <data
        android:host="testMode"
        android:scheme="ampYOUR_APP_KEY"/>
  </intent-filter>
  ```
  * Replace YOUR.PACKAGE.NAME with your application's package identifier in all locations where this placeholder exists:

    ```
    // e.g.,
    <permission android:name="YOUR.PACKAGE.NAME.permission.C2D_MESSAGE" android:protectionLevel="signature" />
    <uses-permission android:name="YOUR.PACKAGE.NAME.permission.C2D_MESSAGE" />
    ...
    ```
  * (Optional) Setup Push with your Project Number (not the Project ID string).

    ```
    // Important: You need to escape a space in front of the value.
    // Otherwise it will be interpreted as an int,
    // which is not a large enough data type to hold a project ID
    <meta-data android:name="LOCALYTICS_PUSH_PROJECT_ID" android:value="\ #################"/>
    ```

2. Modifying Application launcher

  If the existing Unity Android Project already has a main Application/Activity defined, you will need to merge this with our class.  For references on what is needed, refer the Application and Activity class inside "localytics-android/src" within this repository; you can also inspect the class inside Android Studio). Basically the following is needed.
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

Localytics.NotificationsDisabled;
Localytics.PushRegistrationId;
Localytics.ClearInAppMessageDisplayActivity();
Localytics.DisplayPushNotification(Dictionary<string, string> data);

#endif
```

## API References

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
public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;

#if UNITY_ANDROID
public static event LocalyticsShouldShowPushNotification OnLocalyticsShouldShowPushNotification;
public static event LocalyticsShouldShowPlacesPushNotification OnLocalyticsShouldShowPlacesPushNotification;
public static event LocalyticsWillShowPlacesPushNotification OnLocalyticsWillShowPlacesPushNotification;
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
#if UNITY_ANDROID
Localytics.OnLocalyticsShouldShowPushNotification += Localytics_OnLocalyticsShouldShowPushNotification;
Localytics.OnLocalyticsShouldShowPlacesPushNotification += Localytics_OnLocalyticsShouldShowPlacesPushNotification;
Localytics.OnLocalyticsWillShowPushNotification += Localytics_OnLocalyticsWillShowPushNotification;
Localytics.OnLocalyticsWillShowPlacesPushNotification += Localytics_OnLocalyticsWillShowPlacesPushNotification;
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
```

### Localytics C\# Class Members List

```
// Version 4.2.0
public enum ProfileScope { Application, Organization }
public enum InAppMessageDismissButtonLocation { Left, Right }
public enum RegionEvent { Enter, Exit }

// Analytics Events
public static event LocalyticsDidTagEvent OnLocalyticsDidTagEvent;
public static event LocalyticsSessionDidOpen OnLocalyticsSessionDidOpen;
public static event LocalyticsSessionWillClose OnLocalyticsSessionWillClose;
public static event LocalyticsSessionWillOpen OnLocalyticsSessionWillOpen;

// Messaging Events
public static event LocalyticsDidDismissInAppMessage OnLocalyticsDidDismissInAppMessage;
public static event LocalyticsDidDisplayInAppMessage OnLocalyticsDidDisplayInAppMessage;
public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;
#if UNITY_ANDROID
public static event LocalyticsShouldShowPushNotification OnLocalyticsShouldShowPushNotification;
public static event LocalyticsShouldShowPlacesPushNotification OnLocalyticsShouldShowPlacesPushNotification;
public static event LocalyticsWillShowPlacesPushNotification OnLocalyticsWillShowPlacesPushNotification;
public static event LocalyticsWillShowPushNotification OnLocalyticsWillShowPushNotification;
#endif

// Location Events
public static event LocalyticsDidUpdateLocation OnLocalyticsDidUpdateLocation;
public static event LocalyticsDidTriggerRegions OnLocalyticsDidTriggerRegions;
public static event LocalyticsDidUpdateMonitoredGeofences OnLocalyticsDidUpdateMonitoredGeofences;

// Integration
public static void Upload()

// Analytics
public static void OpenSession()
public static void CloseSession()
public static void TagEvent(string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
public static void TagPurchased(string itemName, string itemId, string itemType, long itemPrice, Dictionary<string, string> attributes)
public static void TagAddedToCart(string itemName, string itemId, string itemType, long itemPrice, Dictionary<string, string> attributes)
public static void TagStartedCheckout(long totalPrice, long itemCount, Dictionary<string, string> attributes)
public static void TagCompletedCheckout(long totalPrice, long itemCount, Dictionary<string, string> attributes)
public static void TagContentViewed(string contentName, string contentId, string contentType, Dictionary<string, string> attributes)
public static void TagSearched(string queryText, string contentType, long resultCount, Dictionary<string, string> attributes)
public static void TagShared(string contentName, string contentId, string contentType, string methodName, Dictionary<string, string> attributes)
public static void TagContentRated(string contentName, string contentId, string contentType, long rating, Dictionary<string, string> attributes)
public static void TagCustomerRegistered(CustomerInfo customer, string methodName, Dictionary<string, string> attributes)
public static void TagCustomerLoggedIn(CustomerInfo customer, string methodName, Dictionary<string, string> attributes)
public static void TagCustomerLoggedOut(Dictionary<string, string> attributes)
public static void TagInvited(string methodName, Dictionary<string, string> attributes)
public static void TagScreen(string screen)
public static string GetCustomDimension(int dimension)
public static void SetCustomDimension(int dimension, string value)
public static bool OptedOut
public static void RegisterForAnalyticsEvents()
public static void UnregisterForAnalyticsEvents()

// Profiles
public static void SetProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
public static void SetProfileAttribute(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void DeleteProfileAttribute(string attributeName, ProfileScope scope = ProfileScope.Application)
public static void AddProfileAttributesToSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void AddProfileAttributesToSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void RemoveProfileAttributesFromSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void RemoveProfileAttributesFromSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
public static void IncrementProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
public static void DecrementProfileAttribute(string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
public static void SetCustomerEmail(string email)
public static void SetCustomerFirstName(string firstName)
public static void SetCustomerLastName(string lastName)
public static void SetCustomerFullName(string fullName)

// Marketing
public static void TriggerInAppMessage(string triggerName, Dictionary<string, string> attributes = null)
public static void DismissCurrentInAppMessage()
public static InAppMessageDismissButtonLocation InAppMessageDismissButtonLocationEnum
public static bool TestModeEnabled
#if UNITY_ANDROID
public static void ClearInAppMessageDisplayActivity() // Android only
public static void RegisterPush(string senderId) // Android only
public static void DisplayPushNotification(Dictionary<string, string> data) // Android only
public static string PushRegistrationId // Android only
public static bool NotificationsDisabled // Android only
#elif UNITY_IOS
public static string PushToken
public static bool InAppAdIdParameterEnabled
#endif
public static void RegisterForMessagingEvents()
public static void UnregisterForMessagingEvents()

// Location
public static bool LocationMonitoringEnabled
public static List<CircularRegionInfo> GetGeofencesToMonitor(double latitude, double longitude)
public static void TriggerRegions(List<CircularRegionInfo> regions, RegionEvent regionEvent)
public static void RegisterForLocationEvents()
public static void UnregisterForLocationEvents()

// User Information
public static string CustomerId
public static string GetIdentifier(string key)
public static void SetIdentifier(string key, string value)
public static void SetLocation(LocationInfo location)

// Developer Options
public static void SetOption(string key, string stringValue)
public static void SetOption(string key, long longValue)
public static void SetOption(string key, bool boolValue)
public static string AppKey
public static string InstallId
public static string LibraryVersion
public static bool LoggingEnabled
```

### Other Public Classes
```
public class CustomerInfo
{
  public string CustomerId
  {
    get
    {
      return customerId;
    }
    set
    {
      customerId = value;
    }
  }

  public string FirstName
  {
    get
    {
      return firstName;
    }
    set
    {
      firstName = value;
    }
  }

  public string LastName
  {
    get
    {
      return lastName;
    }
    set
    {
      lastName = value;
    }
  }

  public string FullName
  {
    get
    {
      return fullName;
    }
    set
    {
      fullName = value;
    }
  }

  public string EmailAddress
  {
    get
    {
      return emailAddress;
    }
    set
    {
      emailAddress = value;
    }
  }

  public Dictionary<string, string> ToDictionary()
  {
    Dictionary<string, string> dict = new Dictionary<string, string>();
    if (customerId != null)
    {
      dict.Add("customer_id", customerId);
    }
    if (firstName != null)
    {
      dict.Add("first_name", firstName);
    }
    if (lastName != null)
    {
      dict.Add("last_name", lastName);
    }
    if (fullName != null)
    {
      dict.Add("full_name", fullName);
    }
    if (emailAddress != null)
    {
      dict.Add("email_address", emailAddress);
    }
    return dict;
  }
}

public class RegionInfo
{
  public string UniqueId
  {
    get
    {
      return uniqueId;
    }
    set
    {
      uniqueId = value;
    }
  }

  public double Latitude
  {
    get
    {
      return latitude;
    }
    set
    {
      latitude = value;
    }
  }

  public double Longitude
  {
    get
    {
      return longitude;
    }
    set
    {
      longitude = value;
    }
  }

  public string Name
  {
    get
    {
      return name;
    }
    set
    {
      name = value;
    }
  }

  public string Type
  {
    get
    {
      return type;
    }
    set
    {
      type = value;
    }
  }

  public Dictionary<string, string> Attributes
  {
    get
    {
      return attributes;
    }
    set
    {
      attributes = value;
    }
  }
}

public class CircularRegionInfo : RegionInfo
{
  public int Radius
  {
    get
    {
      return radius;
    }
    set
    {
      radius = value;
    }
  }
}

public class CampaignInfo
{
  public long CampaignId
  {
    get
    {
      return campaignId;
    }
    set
    {
      campaignId = value;
    }
  }

  public string Name
  {
    get
    {
      return name;
    }
    set
    {
      name = value;
    }
  }

  public Dictionary<string, string> Attributes
  {
    get
    {
      return attributes;
    }
    set
    {
      attributes = value;
    }
  }
}

public class NotificationCampaignInfo : CampaignInfo
{
  public long CreativeId
  {
    get
    {
      return creativeId;
    }
    set
    {
      creativeId = value;
    }
  }

  public string CreativeType
  {
    get
    {
      return creativeType;
    }
    set
    {
      creativeType = value;
    }
  }

  public string Message
  {
    get
    {
      return message;
    }
    set
    {
      message = value;
    }
  }

  public string SoundFilename
  {
    get
    {
      return soundFilename;
    }
    set
    {
      soundFilename = value;
    }
  }
}

public class PushCampaignInfo : NotificationCampaignInfo
{
}

public class PlacesCampaignInfo : NotificationCampaignInfo
{
  public CircularRegionInfo Region
  {
    get
    {
      return region;
    }
    set
    {
      region = value;
    }
  }

  public Localytics.RegionEvent TriggerEvent
  {
    get
    {
      return triggerEvent;
    }
    set
    {
      triggerEvent = value;
    }
  }
}
```
