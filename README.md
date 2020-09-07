# Unity Sample Project with Plugins (iOS & Android)

To use these Localytics Plugins for Unity:
- Import the Unity Packages from the LocalyticsPlugin Project into your Unity Project
- Setup the SDK within Unity and the native development platforms (setting Localytics App Key and Push notification)
- Add the Localytics App Key to localytics.options.android.json and localytics.options.ios.json
- If using firebase push notifications on android then fill in strings.xml with values from
- Open the tutorial scene Tutorial1_ConfiguringTheSDK.unity to check your configuration
- Start calling the Localytics API from any MonoBehavior

## Importing the Unity Plugin Packages

You will need to open either the Sample Project or your own project and then import the Unity Packages by:

  Going to "Asset" -> "Import Package" -> "Custom Package..." and Navigate to the **.unitypackage** for Android or AndroidX and/or iOS
  
If you do not have a specific reason to chose Android over AndroidX then chose the AndroidX package for use on Android devices.

## Using the Sample Project

The sample project contains scenes that will assist you in configuring the SDK for your project. It is highly recommended that you start by configuring the sample project and running throught the Tutorial scenes before integrating the package into your app.

## Setup the SDK 

### iOS
1. Inside Unity, navigate to the Localytics/Resources folder and edit the file:
  ```
  localytics.options.ios.json
  ```
  Replace <LOCALYTICS_API_KEY> with your api key

2. On the top menu bar go to the Localytics menu entry and select Build Config, here you can select whether to include Location Monitoring or no, the Info.plist file will automaticaly be configured when you build. There is a build button at the bottom of the Build Config which is a convenience button and is no different to building from PlayerSettings as usual.

### Android

#### Note: On versions of Unity prior to Unity 2018.4, if you wish to use push notifications then you will need to export a gradle project, edit the build.gradle dependencies to include implementation ('androidx.legacy:legacy-support-v4:1.0.0') alongside the other aar files.
#### Note: On earlier versions of Unity, due to a Unity [bug](https://forum.unity3d.com/threads/android-deployment-error.444133/#post-2876464), you may not be able to build and run with the latest version of Android sdk tools. This has been tested with sdk tools version 25.2.2.
ation's package identifier in all locations where this placeholder exists:

1. Inside Unity, navigate to the Localytics/Resources folder and edit the file:
  ```
  localytics.options.ios.json
  ```
  Replace <LOCALYTICS_API_KEY> with your api key
  
 2. If you are using firebase push notifications then you will need to acquire a google-services.json file from your firebase console and use the values in it to populate the file:
   ```
   Assets/Plugins/Android/res/values/strings.xml
   ```
   A free online converter exists here:
   ```
   https://dandar3.github.io/android/google-services-json-to-xml.html
   ```
   
3. On the top menu bar go to the Localytics menu entry and select Build Config, here you can select whether to build for Android or AndroidX which must match the package you imported. You may select to enable or disable push notifications in the manifest when you configure the options file. You must either click Configure Manifest to build an android manifest or if you have one already then you will need to manually integrate the changes into your existing manifest. You can tick Ignore Manifest Issues if you are using a custom configuration. There is a build button at the bottom of the Build Config which is a convenience button and is no different to building from PlayerSettings as usual.
  
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
