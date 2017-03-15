using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using LocalyticsUnity;

public class TestLocalytics : MonoBehaviour
{
    [SerializeField]
    private Text _analyticsHost;
    [SerializeField]
    private Text _appKey;
    [SerializeField]
    private Text _installId;
    [SerializeField]
    private Text _libraryVersion;
    [SerializeField]
    private Text _loggingEnabled;
    [SerializeField]
	private Text _messagingHost;
	[SerializeField]
	private Text _inAppIdAdEnabled;
    [SerializeField]
    private Text _optedOut;
    [SerializeField]
    private Text _profilesHost;
    [SerializeField]
    private Text _pushDisabled;
    [SerializeField]
    private Text _testModeEnabled;
    [SerializeField]
    private Text _sessionTimeoutInterval;
    [SerializeField]
    private Text _pushRegistrationId;
    [SerializeField]
    private Button _openSession;
    [SerializeField]
    private Button _closeSession;
    [SerializeField]
    private Button _tagEventClick;
    [SerializeField]
    private Button _tagScreen1;
    [SerializeField]
    private Button _tagScreen2;

    // Use this for initialization
    void Start()
    {
        Localytics.LoggingEnabled = true;

        Localytics.RegisterForAnalyticsEvents();
		Localytics.RegisterForMessagingEvents();
		Localytics.RegisterForLocationEvents();

		// This is just for testing purpose
		Localytics.UnregisterForAnalyticsEvents();
		Localytics.UnregisterForMessagingEvents();
		Localytics.UnregisterForLocationEvents();

		Localytics.RegisterForAnalyticsEvents();
		Localytics.RegisterForMessagingEvents();
		Localytics.RegisterForLocationEvents();

		// Analytics events
        Localytics.OnLocalyticsDidTagEvent += Localytics_OnLocalyticsDidTagEvent;
        Localytics.OnLocalyticsSessionWillOpen += Localytics_OnLocalyticsSessionWillOpen;
        Localytics.OnLocalyticsSessionDidOpen += Localytics_OnLocalyticsSessionDidOpen;
        Localytics.OnLocalyticsSessionWillClose += Localytics_OnLocalyticsSessionWillClose;

		// Messaging events
		Localytics.OnLocalyticsDidDismissInAppMessage += Localytics_OnLocalyticsDidDismissInAppMessage;
		Localytics.OnLocalyticsDidDisplayInAppMessage += Localytics_OnLocalyticsDidDisplayInAppMessage;
		Localytics.OnLocalyticsWillDismissInAppMessage += Localytics_OnLocalyticsWillDismissInAppMessage;
		Localytics.OnLocalyticsWillDisplayInAppMessage += Localytics_OnLocalyticsWillDisplayInAppMessage;
		Localytics.OnLocalyticsShouldShowPushNotification += Localytics_OnLocalyticsShouldShowPushNotification;
		Localytics.OnLocalyticsShouldShowPlacesPushNotification += Localytics_OnLocalyticsShouldShowPlacesPushNotification;
		Localytics.OnLocalyticsWillShowPushNotification += Localytics_OnLocalyticsWillShowPushNotification;
		Localytics.OnLocalyticsWillShowPlacesPushNotification += Localytics_OnLocalyticsWillShowPlacesPushNotification;

		// Location events
		Localytics.OnLocalyticsDidUpdateLocation += Localytics_OnLocalyticsDidUpdateLocation;
		Localytics.OnLocalyticsDidTriggerRegions += Localytics_OnLocalyticsDidTriggerRegions;
		Localytics.OnLocalyticsDidUpdateMonitoredGeofences += Localytics_OnLocalyticsDidUpdateMonitoredGeofences;

        _openSession.onClick.AddListener(() => {
			Localytics.OpenSession();
			Localytics.Upload();
		});
        _closeSession.onClick.AddListener(() => {
			Localytics.CloseSession();
			Localytics.Upload();
		});
        _tagEventClick.onClick.AddListener(() => {
			Dictionary<string, string> attributes = new Dictionary<string, string>();
			attributes.Add("attr", "value");
			Localytics.TagEvent("test", attributes);
			Localytics.Upload();

		});
        _tagScreen1.onClick.AddListener(() => {
			Localytics.TagScreen("screen1");
			Localytics.Upload();
		});

        _tagScreen2.onClick.AddListener(() => {
			Localytics.TagScreen("screen2");
			Localytics.Upload();
		});

        Localytics.CustomerId = "user1";

		Localytics.SetIdentifier("test_identifier", "test setIdentifier");
		Localytics.GetIdentifier("test_identifier");

		Localytics.InAppMessageDismissButtonLocationEnum = Localytics.InAppMessageDismissButtonLocation.Right;

		Localytics.TriggerInAppMessage("Sample Startup", null);

        // to set a profile attribute:
        Localytics.SetProfileAttribute("Age", 45, Localytics.ProfileScope.Organization);
        Localytics.SetProfileAttribute("Lucky numbers", new long[] { 8, 13 }, Localytics.ProfileScope.Application);
        Localytics.SetProfileAttribute("Hometown", "New York, New York", Localytics.ProfileScope.Organization);
        Localytics.SetProfileAttribute("States visited", new String[] { "New York", "California", "South Dakota" }, Localytics.ProfileScope.Application);

        // to remove a profile attribute:
        Localytics.DeleteProfileAttribute("Days until graduation", Localytics.ProfileScope.Application);

        // to add a set of values to an already-defined set of values:
        Localytics.AddProfileAttributesToSet("Lucky numbers", new long[] { 666 }, Localytics.ProfileScope.Application);
        Localytics.AddProfileAttributesToSet("States visited", new String[] { "North Dakota" }, Localytics.ProfileScope.Application);

        // to remove a set of values from an already-defined set of values:
        Localytics.RemoveProfileAttributesFromSet("Lucky numbers", new long[] { 8, 666 }, Localytics.ProfileScope.Application);
        Localytics.RemoveProfileAttributesFromSet("States visited", new String[] { "California" }, Localytics.ProfileScope.Application);

        // to increment or decrement an already-defined value:
        Localytics.IncrementProfileAttribute("Age", 1, Localytics.ProfileScope.Organization);
        Localytics.DecrementProfileAttribute("Days until graduation", 3, Localytics.ProfileScope.Application);

		Localytics.SetCustomerEmail("Convenient Email");
		Localytics.SetCustomerFirstName("Convenient FirstName");
		Localytics.SetCustomerLastName("Convenient LastName");
		Localytics.SetCustomerFullName("Convenient Full Name");

		Localytics.SetCustomDimension(1,"testCD1");
		Localytics.GetCustomDimension(1);

        Localytics.Upload();

		Input.location.Start();
		_lastStatus = Input.location.status;

        UpdateLabels();
    }

    void UpdateLabels()
    {
		_appKey.text = "AppKey: " + Localytics.AppKey;
		_installId.text = "Install Id:" + Localytics.InstallId;
		_libraryVersion.text = "Library Version: " + Localytics.LibraryVersion;
		_loggingEnabled.text = "Logging Enabled: " + Localytics.LoggingEnabled;
		_optedOut.text = "Opted Out: " + Localytics.OptedOut;		
		_testModeEnabled.text = "Test Mode Enabled: " + Localytics.TestModeEnabled;

#if UNITY_IOS
		_inAppIdAdEnabled.text = "InAppAdIdParameter Enabled: " + Localytics.InAppAdIdParameterEnabled;
		_pushDisabled.text = "IsCollectingAdvertisingIdentifier: " + Localytics.IsCollectingAdvertisingIdentifier;
        _pushRegistrationId.text = "Push Token: " + Localytics.PushToken;
#elif UNITY_ANDROID
		_inAppIdAdEnabled.text = "InAppAdIdParameter Enabled: -";
		_pushDisabled.text = "Push Disabled: " + Localytics.NotificationsDisabled;
        _pushRegistrationId.text = "Push RegId: " + Localytics.PushRegistrationId;
#endif
    }

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

	bool Localytics_OnLocalyticsShouldShowPushNotification(PushCampaignInfo campaign)
	{
		Debug.Log ("ShouldShowPushNotification");
		printPushCampaign (campaign);
		return true;
	}

	bool Localytics_OnLocalyticsShouldShowPlacesPushNotification(PlacesCampaignInfo campaign)
	{
		Debug.Log ("ShouldShowPlacesPushNotification");
		printPlacesCampaign (campaign);
		return true;
	}

	AndroidJavaObject Localytics_OnLocalyticsWillShowPushNotification(AndroidJavaObject notificationBuilder, PushCampaignInfo campaign)
	{
		Debug.Log ("WillShowPushNotification");
		printPushCampaign (campaign);
		//notificationBuilder.Call<AndroidJavaObject> ("setContentTitle", "New Title");
		return notificationBuilder;
	}

	AndroidJavaObject Localytics_OnLocalyticsWillShowPlacesPushNotification(AndroidJavaObject notificationBuilder, PlacesCampaignInfo campaign)
	{
		Debug.Log ("WillShowPlacesPushNotification");
		printPlacesCampaign (campaign);
		//notificationBuilder.Call<AndroidJavaObject> ("setContentTitle", "New Title");
		return notificationBuilder;
	}

#if UNITY_ANDROID
	void Localytics_OnLocalyticsDidUpdateLocation(AndroidJavaObject location)
	{
		Debug.Log ("DidUpdateLocation");
		double latitude = location.Call<double> ("getLatitude");
		double longitude = location.Call<double> ("getLongitude");
		Debug.Log ("lat=" + latitude + " lng=" + longitude);
	}
#else
	void Localytics_OnLocalyticsDidUpdateLocation(LocationInfo location)
	{
	}
#endif

	void Localytics_OnLocalyticsDidTriggerRegions(List<CircularRegionInfo> regions, Localytics.RegionEvent regionEvent)
	{
		Debug.Log ("DidTriggerRegions for event: " + regionEvent);
		foreach (CircularRegionInfo info in regions)
		{
			printCircularRegion(info);
		}
	}

	void Localytics_OnLocalyticsDidUpdateMonitoredGeofences(List<CircularRegionInfo> added, List<CircularRegionInfo> removed)
	{
		Debug.Log ("DidUpdateMonitoredGeofences");
		Debug.Log ("--- added ---");
		foreach (CircularRegionInfo info in added)
		{
			printCircularRegion(info);
		}
		Debug.Log ("--- removed ---");
		foreach (CircularRegionInfo info in removed)
		{
			printCircularRegion(info);
		}
	}

	private void printPushCampaign(PushCampaignInfo info)
	{
		Debug.Log("-------");
		Debug.Log("PushCampaignInfo");
		Debug.Log("campaignId: " + info.CampaignId);
		Debug.Log("name: " + info.Name);
		Debug.Log("creativeId: " + info.CreativeId);
		Debug.Log("creativeType: " + info.CreativeType);
		Debug.Log("message: " + info.Message);
		Debug.Log("soundFilename: " + info.SoundFilename);
		Debug.Log("attributes:");
		foreach (KeyValuePair<string, string> kvp in info.Attributes)
		{
			Debug.Log(kvp.Key + "=" + kvp.Value);
		}
	}

	private void printPlacesCampaign(PlacesCampaignInfo info)
	{
		Debug.Log("-------");
		Debug.Log("PlacesCampaignInfo");
		Debug.Log("campaignId: " + info.CampaignId);
		Debug.Log("name: " + info.Name);
		Debug.Log("creativeId: " + info.CreativeId);
		Debug.Log("creativeType: " + info.CreativeType);
		Debug.Log("message: " + info.Message);
		Debug.Log("soundFilename: " + info.SoundFilename);
		Debug.Log("triggerEvent: " + info.TriggerEvent);
		Debug.Log("attributes:");
		foreach (KeyValuePair<string, string> kvp in info.Attributes)
		{
			Debug.Log(kvp.Key + "=" + kvp.Value);
		}
		printCircularRegion(info.Region);
	}

	private void printCircularRegion(CircularRegionInfo info)
	{
		Debug.Log("-------");
		Debug.Log("CircularRegionInfo");
		Debug.Log("uniqueId: " + info.UniqueId);
		Debug.Log("latitude: " + info.Latitude);
		Debug.Log("longitude: " + info.Longitude);
		Debug.Log("radius: " + info.Radius);
		Debug.Log("name: " + info.Name);
		Debug.Log("type: " + info.Type);
		Debug.Log("attributes:");
		foreach (KeyValuePair<string, string> kvp in info.Attributes)
		{
			Debug.Log(kvp.Key + "=" + kvp.Value);
		}
	}
	
	private LocationServiceStatus _lastStatus;
    // Update is called once per frame
    void Update()
    {
		if (Input.location.status != _lastStatus)
		{
			_lastStatus = Input.location.status;
			if (_lastStatus == LocationServiceStatus.Running)
			{
				Debug.Log ("Setting location lat: " + Input.location.lastData.latitude + " long: " + Input.location.lastData.longitude);
				Localytics.SetLocation(Input.location.lastData);
			}
		}
    }
}
