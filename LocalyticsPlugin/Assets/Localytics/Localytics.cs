using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;
using System.Collections;

#if (UNITY_IOS || UNITY_ANDROID) && !UNITY_EDITOR
public class Localytics
{
    public delegate void LocalyticsDidTagEvent(string eventName, Dictionary<string, string> attributes, long customerValueIncrease);
    public delegate void LocalyticsSessionDidOpen(bool isFirst, bool isUpgrade, bool isResume);
    public delegate void LocalyticsSessionWillClose();
    public delegate void LocalyticsSessionWillOpen(bool isFirst, bool isUpgrade, bool isResume);
    public delegate void LocalyticsDidDismissInAppMessage();
    public delegate void LocalyticsDidDisplayInAppMessage();
    public delegate void LocalyticsWillDismissInAppMessage();
    public delegate void LocalyticsWillDisplayInAppMessage();

    public static event LocalyticsDidTagEvent OnLocalyticsDidTagEvent;
    public static event LocalyticsSessionDidOpen OnLocalyticsSessionDidOpen;
    public static event LocalyticsSessionWillClose OnLocalyticsSessionWillClose;
    public static event LocalyticsSessionWillOpen OnLocalyticsSessionWillOpen;
    public static event LocalyticsDidDismissInAppMessage OnLocalyticsDidDismissInAppMessage;
    public static event LocalyticsDidDisplayInAppMessage OnLocalyticsDidDisplayInAppMessage;
    public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
    public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;

    public enum ProfileScope
    {
        Application,
        Organization,
    }

    public enum InAppMessageDismissButtonLocation
    {
        Left,
        Right,
    }

#if UNITY_ANDROID
    public static string AnalyticsHost
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getAnalyticsHost");
        }
        set
        {
            LocalyticsClass.CallStatic("setAnalyticsHost", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _analyticsHost();

    [DllImport("__Internal")]
    private static extern void _setAnalyticsHost(string analyticsHost);

    public static string AnalyticsHost
    {
        get 
        {
            return _analyticsHost();
        }
        set
        {
            _setAnalyticsHost(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string AppKey
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getAppKey");
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _appKey();

    public static string AppKey
    {
        get 
        {
            return _appKey();
        }
    }
#endif

#if UNITY_ANDROID
    public static string CustomerId
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getCustomerId");
        }
        set
        {
            LocalyticsClass.CallStatic("setCustomerId", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _customerId();

    [DllImport("__Internal")]
    private static extern void _setCustomerId(string customerId);

    public static string CustomerId
    {
        get
        {
            return _customerId();
        }
        set
        {
            _setCustomerId(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static InAppMessageDismissButtonLocation InAppMessageDismissButtonLocationEnum
    {
        get
        {
            string name = LocalyticsClass.CallStatic<AndroidJavaObject>("getInAppMessageDismissButtonLocation").Call<string>("name");
            switch (name)
            {
                default:
                case "LEFT":
                    return InAppMessageDismissButtonLocation.Left;
                case "RIGHT":
                    return InAppMessageDismissButtonLocation.Right;
            }
        }
        set
        {
            LocalyticsClass.CallStatic("setInAppMessageDismissButtonLocation", GetInAppMessageDismissButtonLocationEnum(value));
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern uint _inAppMessageDismissButtonLocation();

    [DllImport("__Internal")]
    private static extern void _setInAppMessageDismissButtonLocation(uint location);

    public static InAppMessageDismissButtonLocation InAppMessageDismissButtonLocationEnum
    {
        get
        {
            return (InAppMessageDismissButtonLocation)_inAppMessageDismissButtonLocation();
        }
        set
        {
            _setInAppMessageDismissButtonLocation((uint)value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string InstallId
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getInstallId");
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _installId();

    public static string InstallId
    {
        get 
        {
            return _installId();
        }
    }
#endif

#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool _isCollectingAdvertisingIdentifier();

    [DllImport("__Internal")]
    private static extern void _setCollectingAdvertisingIdentifier(bool collectingAdvertisingIdentifier);

    public static bool IsCollectingAdvertisingIdentifier
    {
        get
        {
            return _isCollectingAdvertisingIdentifier();
        }
        set
        {
            _setCollectingAdvertisingIdentifier(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string LibraryVersion
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getLibraryVersion");
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _libraryVersion();

    public static string LibraryVersion
    {
        get 
        {
            return _libraryVersion();
        }
    }
#endif

#if UNITY_ANDROID
    public static bool LoggingEnabled
    {
        get
        {
            return LocalyticsClass.CallStatic<bool>("isLoggingEnabled");
        }
        set
        {
            LocalyticsClass.CallStatic("setLoggingEnabled", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool _isLoggingEnabled();

    [DllImport("__Internal")]
    private static extern void _setLoggingEnabled(bool loggingEnabled);

    public static bool LoggingEnabled
    {
        get 
        {
            return _isLoggingEnabled();
        }
        set
        {
            _setLoggingEnabled(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string MessagingHost
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getMessagingHost");
        }
        set
        {
            LocalyticsClass.CallStatic("setMessagingHost", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _messagingHost();

    [DllImport("__Internal")]
    private static extern void _setMessagingHost(string messagingHost);

    public static string MessagingHost
    {
        get 
        {
            return _messagingHost();
        }
        set 
        {
            _setMessagingHost(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static bool OptedOut
    {
        get
        {
            return LocalyticsClass.CallStatic<bool>("isOptedOut");
        }
        set
        {
            LocalyticsClass.CallStatic("setOptedOut", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool _isOptedOut();

    [DllImport("__Internal")]
    private static extern void _setOptedOut(bool optedOut);

    public static bool OptedOut
    {
        get 
        {
            return _isOptedOut();
        }
        set 
        {
            _setOptedOut(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string ProfilesHost
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getProfilesHost");
        }
        set
        {
            LocalyticsClass.CallStatic("setProfilesHost", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _profilesHost();

    [DllImport("__Internal")]
    private static extern void _setProfilesHost(string profilesHost);

    public static string ProfilesHost
    {
        get 
        {
            return _profilesHost();
        }
        set 
        {
            _setProfilesHost(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static bool PushDisabled
    {
        get
        {
            return LocalyticsClass.CallStatic<bool>("isPushDisabled");
        }
        set
        {
            LocalyticsClass.CallStatic("setPushDisabled", value);
        }
    }
#endif

#if UNITY_ANDROID
    public static string PushRegistrationId
    {
        get
        {
            return LocalyticsClass.CallStatic<string>("getPushRegistrationId");
        }
        set
        {
            LocalyticsClass.CallStatic("setPushRegistrationId", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern string _pushToken();

    public static string PushToken
    {
        get 
        {
            return _pushToken();
        }
    }
#endif

#if UNITY_ANDROID
    public static bool TestModeEnabled
    {
        get
        {
            return LocalyticsClass.CallStatic<bool>("isTestModeEnabled");
        }
        set
        {
            LocalyticsClass.CallStatic("setTestModeEnabled", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern bool _testModeEnabled();

    [DllImport("__Internal")]
    private static extern void _setTestModeEnabled(bool enabled);

    public static bool TestModeEnabled
    {
        get 
        {
            return _testModeEnabled();
        }
        set
        {
            _setTestModeEnabled(value);
        }
    }
#endif

#if UNITY_ANDROID
    public static long SessionTimeoutInterval
    {
        get
        {
            return LocalyticsClass.CallStatic<long>("getSessionTimeoutInterval");
        }
        set
        {
            LocalyticsClass.CallStatic("setSessionTimeoutInterval", value);
        }
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern double _sessionTimeoutInterval();

    [DllImport("__Internal")]
    private static extern void _setSessionTimeoutInterval(double timeoutInterval);

    public static double SessionTimeoutInterval
    {
        get
        {
            return _sessionTimeoutInterval();
        }
        set 
        {
            _setSessionTimeoutInterval(value);
        }
    }
#endif

#if UNITY_ANDROID
    
	
    public static void AddProfileAttributesToSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }

    public static void AddProfileAttributesToSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _addProfileAttributesToSet(string attribute, string values, int scope);
	
    

    public static void AddProfileAttributesToSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = "";
		if (attributeValue != null)
			values = MiniJSON.jsonEncode (attributeValue);
		_addProfileAttributesToSet (attributeName, values, (int)scope);
	}
    public static void AddProfileAttributesToSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = "";
		if (attributeValue != null)
			values = MiniJSON.jsonEncode (attributeValue);
		_addProfileAttributesToSet (attributeName, values, (int)scope);
	}
#endif

#if UNITY_ANDROID
    public static void ClearInAppMessageDisplayActivity()
    {
        LocalyticsClass.CallStatic("clearInAppMessageDisplayActivity");
    }
#elif UNITY_IOS

#endif

#if UNITY_ANDROID
    public static void CloseSession()
    {
        LocalyticsClass.CallStatic("closeSession");
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _closeSession();

    public static void CloseSession()
    {
		_closeSession ();
	}
#endif

#if UNITY_ANDROID
    public static void DecrementProfileAttribute(string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("decrementProfileAttribute", attributeName, decrementValue, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _decrementProfileAttribute(string attributeName, long value, int scope);

	public static void DecrementProfileAttribute(string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
	{
		_decrementProfileAttribute (attributeName, decrementValue, (int)scope);
	}
#endif

#if UNITY_ANDROID
    public static void DeleteProfileAttribute(string attributeName, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("deleteProfileAttribute", attributeName, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _deleteProfileAttribute(string attributeName, int scope);

    public static void DeleteProfileAttribute(string attributeName, ProfileScope scope = ProfileScope.Application)
	{
		_deleteProfileAttribute (attributeName, (int)scope);
	}
#endif

#if UNITY_ANDROID
	public static void SetCustomerEmail(string email)
	{
		LocalyticsClass.CallStatic("setCustomerEmail", email);
	}

	public static void SetCustomerFirstName(string firstName)
	{
		LocalyticsClass.CallStatic("setCustomerFirstName", firstName);
	}

	public static void SetCustomerLastName(string lastName)
	{
		LocalyticsClass.CallStatic("setCustomerLastName", lastName);
	}

	public static void SetCustomerFullName(string fullName)
	{
		LocalyticsClass.CallStatic("setCustomerFullName", fullName);
	}
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _setCustomerEmail(string email);

	[DllImport("__Internal")]
	private static extern void _setCustomerFirstName(string firstName);

	[DllImport("__Internal")]
	private static extern void _setCustomerLastName(string lastName);

	[DllImport("__Internal")]
	private static extern void _setCustomerFullName(string fullName);

	public static void SetCustomerEmail(string email)
	{
		_setCustomerEmail(email);
	}
	public static void SetCustomerFirstName(string firstName)
	{
		_setCustomerFirstName(firstName);
	}

	public static void SetCustomerLastName(string lastName)
	{
		_setCustomerLastName(lastName);
	}

	public static void SetCustomerFullName(string fullName)
	{
		_setCustomerFullName(fullName);
	}
#endif

#if UNITY_ANDROID
    public static void DismissCurrentInAppMessage()
    {
        LocalyticsClass.CallStatic("dismissCurrentInAppMessage");
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _dismissCurrentInAppMessage();

	public static void DismissCurrentInAppMessage()
	{
		_dismissCurrentInAppMessage ();
	}
#endif

#if UNITY_ANDROID
    public static string GetCustomDimension(int dimension)
    {
        return LocalyticsClass.CallStatic<string>("getCustomDimension", dimension);
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern string _getCustomDimension(int dimension);

	public static string GetCustomDimension(int dimension)
	{
		return _getCustomDimension (dimension);
	}
#endif

#if UNITY_ANDROID
    public static string GetIdentifier(string key)
    {
        return LocalyticsClass.CallStatic<string>("getIdentifier", key);
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern string _getIdentifier(string key);

	public static string GetIdentifier(string key)
	{
		return _getIdentifier (key);
	}
#endif

#if UNITY_ANDROID
    public static void IncrementProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("incrementProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _incrementProfileAttribute(string attributeName, long attributeValue, int scope);

    public static void IncrementProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		_incrementProfileAttribute (attributeName, attributeValue, (int)scope);
	}
#endif

#if UNITY_ANDROID
    public static void OpenSession()
    {
        LocalyticsClass.CallStatic("openSession");
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    public static extern void _openSession();

    public static void OpenSession()
    {
        _openSession();
    }
#endif
	
#if UNITY_ANDROID
    public static void RegisterForAnalyticsEvents()
    {
        if (_analyticsListener == null)
            _analyticsListener = new AnalyticsListener();

        LocalyticsClass.CallStatic("addAnalyticsListener", _analyticsListener);
    }

	public static void UnregisterForAnalyticsEvents()
	{
		if (_analyticsListener == null) return;
		
		LocalyticsClass.CallStatic("removeAnalyticsListener", _analyticsListener);
	}
#elif UNITY_IOS
	private delegate void ReceiveAnalyticsDelegate(string message);
	
	[MonoPInvokeCallback(typeof(ReceiveAnalyticsDelegate))]
	private static void ReceiveAnalyticsMessage(string message)
	{
		try 
		{
			var json = (Hashtable)MiniJSON.jsonDecode (message);
			string e = json["event"].ToString();
			switch(e)
			{
				case "localyticsSessionWillOpen":
					var willOpen = (Hashtable)json["params"];
					bool willIsFirst = Boolean.Parse (willOpen["isFirst"].ToString ());
					bool willIsUpgrade = Boolean.Parse (willOpen["isUpgrade"].ToString ());
					bool willIsResume = Boolean.Parse (willOpen["isResume"].ToString ());
					if (OnLocalyticsSessionWillOpen != null)
						OnLocalyticsSessionWillOpen(willIsFirst, willIsUpgrade, willIsResume);
						break;
				case "localyticsSessionDidOpen":
					var didOpen = (Hashtable)json["params"];
					bool didIsFirst = Boolean.Parse (didOpen["isFirst"].ToString ());
					bool didIsUpgrade = Boolean.Parse (didOpen["isUpgrade"].ToString ());
					bool didIsResume = Boolean.Parse (didOpen["isResume"].ToString ());
					if (OnLocalyticsSessionDidOpen != null)
						OnLocalyticsSessionDidOpen(didIsFirst, didIsUpgrade, didIsResume);
					break;
				case "localyticsDidTagEvent":
					var tagParams = (Hashtable)json["params"];
					string eventName = tagParams["eventName"].ToString ();

					var attributesDictionary = new Dictionary<string, string>();
					if (tagParams.ContainsKey("attributes"))
					{
						var attributes = ((Hashtable)tagParams["attributes"]);
						foreach(var key in attributes.Keys)
						{
							attributesDictionary.Add ((string)key, (string)attributes[key]);
						}
					}
					long customerValueIncrease = 0;
					if (tagParams.ContainsKey("customerValueIncrease"))
					{
						customerValueIncrease = long.Parse(tagParams["customerValueIncrease"].ToString());
					}
					if (OnLocalyticsDidTagEvent != null)
						OnLocalyticsDidTagEvent(eventName, attributesDictionary, customerValueIncrease);

					break;
				case "localyticsSessionWillClose":
					if (OnLocalyticsSessionWillClose != null)
						OnLocalyticsSessionWillClose();
					break;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError ("There was a problem decoding an analytics message from the Localytics plugin: " + ex.Message);
		}
	}
	
	[DllImport("__Internal")]
	private static extern void _registerReceiveAnalyticsCallback(ReceiveAnalyticsDelegate callback);
	[DllImport("__Internal")]
	private static extern void _removeAnalyticsCallback ();

    public static void RegisterForAnalyticsEvents()
    {
		_registerReceiveAnalyticsCallback (ReceiveAnalyticsMessage);
    }

	public static void UnregisterForAnalyticsEvents()
	{
		_removeAnalyticsCallback ();
	}
#endif

#if UNITY_ANDROID
#elif UNITY_IOS
[DllImport("__Internal")]
private static extern bool _isInAppAdIdParameterEnabled();

[DllImport("__Internal")]
private static extern void _setInAppAdIdParameterEnabled(bool appAdIdEnabled);

public static bool InAppAdIdParameterEnabled
{
	get 
	{
		return _isInAppAdIdParameterEnabled();
	}
	set
	{
		_setInAppAdIdParameterEnabled(value);
	}
}
#endif

#if UNITY_ANDROID
    public static void RegisterForMessagingEvents()
    {
        if (_messagingListener == null)
            _messagingListener = new MessagingListener();

        LocalyticsClass.CallStatic("addMessagingListener", _messagingListener);
    }

	public static void UnregisterForMessagingEvents()
	{
		if (_messagingListener == null) return;
		
		LocalyticsClass.CallStatic("removeMessagingListener", _messagingListener);
	}
#elif UNITY_IOS
	private delegate void ReceiveMessagingDelegate(string message);
	
	[MonoPInvokeCallback(typeof(ReceiveMessagingDelegate))]
	private static void ReceiveMessagingMessage(string message)
	{
		try 
		{
			var json = (Hashtable)MiniJSON.jsonDecode (message);
			string e = json["event"].ToString();
			switch(e)
			{
				case "localyticsWillDisplayInAppMessage":
					if (OnLocalyticsWillDisplayInAppMessage != null)
						OnLocalyticsWillDisplayInAppMessage();
					break;
				case "localyticsDidDisplayInAppMessage":
					if (OnLocalyticsDidDisplayInAppMessage != null)
						OnLocalyticsDidDisplayInAppMessage();
					break;
				case "localyticsWillDismissInAppMessage":
					if (OnLocalyticsWillDismissInAppMessage != null)
						OnLocalyticsWillDismissInAppMessage();
					break;
				case "localyticsDidDismissInAppMessage":
					if (OnLocalyticsDidDismissInAppMessage != null)
						OnLocalyticsDidDismissInAppMessage();
					break;
			}
		}
		catch (Exception ex)
		{
			Debug.LogError ("There was a problem decoding an analytics message from the Localytics plugin: " + ex.Message);
		}
	}
	
	[DllImport("__Internal")]
	private static extern void _registerReceiveMessagingCallback(ReceiveMessagingDelegate callback);
	[DllImport("__Internal")]
	private static extern void _removeMessagingCallback ();
	
	public static void RegisterForMessagingEvents()
	{
		_registerReceiveMessagingCallback (ReceiveMessagingMessage);
	}
	
	public static void UnregisterForMessagingEvents()
	{
		_removeMessagingCallback ();
	}
#endif

#if UNITY_ANDROID
    

    public static void RemoveProfileAttributesFromSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }

    public static void RemoveProfileAttributesFromSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _removeProfileAttributesFromSet(string attributeName, string attributeValue, int scope);

    

    public static void RemoveProfileAttributesFromSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = "";
		if (attributeValue != null)
			values = MiniJSON.jsonEncode (attributeValue);
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}

    public static void RemoveProfileAttributesFromSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = "";
		if (attributeValue != null)
			values = MiniJSON.jsonEncode (attributeValue);
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}
#endif

#if UNITY_ANDROID
    public static void SetCustomDimension(int dimension, string value)
    {
        LocalyticsClass.CallStatic("setCustomDimension", dimension, value);
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _setCustomDimension(int dimension, string value);

    public static void SetCustomDimension(int dimension, string value)
	{
		_setCustomDimension (dimension, value);
	}
#endif

#if UNITY_ANDROID
    public static void SetIdentifier(string key, string value)
    {
        LocalyticsClass.CallStatic("setIdentifier", key, value);
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _setIdentifier(string key, string value);

	public static void SetIdentifier(string key, string value)
	{
		_setIdentifier (key, value);
	}
#endif

#if UNITY_ANDROID
    // setLocation
    public static void SetLocation(LocationInfo location)
    {
        LocalyticsClass.CallStatic("setLocation", LocationInfoToLocation(location));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _setLocation(double latitude, double longitude);

	public static void SetLocation(LocationInfo location)
	{
		_setLocation(location.latitude, location.longitude);
	}
#endif

#if UNITY_ANDROID
    

    public static void SetProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }

    public static void SetProfileAttribute(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }

    public static void SetProfileAttribute(string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }

    public static void SetProfileAttribute(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
    {
        LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _setProfileAttribute (string attributeName, string values, int scope);
	


	public static void SetProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = MiniJSON.jsonEncode (new long[] { attributeValue });
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}

    public static void SetProfileAttribute(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = MiniJSON.jsonEncode (attributeValue);
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}

    public static void SetProfileAttribute(string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = MiniJSON.jsonEncode (new string[] { attributeValue });
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}

    public static void SetProfileAttribute(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
	{
		string values = MiniJSON.jsonEncode (attributeValue);
		_removeProfileAttributesFromSet (values, attributeName, (int)scope);
	}
#endif

#if UNITY_ANDROID
    public static void TagEvent(string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
    {
        LocalyticsClass.CallStatic("tagEvent", eventName, DictionaryToMap(attributes), customerValueIncrease);
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _tagEvent(string eventName, string attributes, long customerValueIncrease);

    public static void TagEvent(string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
	{
		string values = "";
		if (attributes != null)
			values = MiniJSON.jsonEncode (attributes);
		_tagEvent(eventName, values, customerValueIncrease);
	}
#endif

#if UNITY_ANDROID
    public static void TagScreen(string screen)
    {
        LocalyticsClass.CallStatic("tagScreen", screen);
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _tagScreen(string screen);

    public static void TagScreen(string screen)
    {
        _tagScreen(screen);
    }
#endif

#if UNITY_ANDROID
    public static void TriggerInAppMessage(string triggerName, Dictionary<string, string> attributes = null)
    {
        LocalyticsClass.CallStatic("triggerInAppMessage", triggerName, DictionaryToMap(attributes));
    }
#elif UNITY_IOS
	[DllImport("__Internal")]
	private static extern void _triggerInAppMessage(string triggerName, string attributes);

    public static void TriggerInAppMessage(string triggerName, Dictionary<string, string> attributes = null)
	{
		string values = "";
		if (attributes != null)
			values = MiniJSON.jsonEncode (attributes);
		_triggerInAppMessage (triggerName, values);
	}
#endif

#if UNITY_ANDROID
    public static void Upload()
    {
        LocalyticsClass.CallStatic("upload");
    }
#elif UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _upload();

    public static void Upload()
    {
        _upload();
    }
#endif
	
#if UNITY_ANDROID
    private static AnalyticsListener _analyticsListener;
    private static MessagingListener _messagingListener;

    private static AndroidJavaClass _localyticsClass;
    private static AndroidJavaClass LocalyticsClass
    {
        get
        {
            if (_localyticsClass == null)
            {
                _localyticsClass = new AndroidJavaClass("com.localytics.android.Localytics");
            }
            return _localyticsClass;
        }
    }

    private static AndroidJavaObject DictionaryToMap(Dictionary<string, string> dict)
    {
        var map = new AndroidJavaObject("java.util.HashMap");

        if (dict == null || dict.Count == 0) return map;

        IntPtr putMethod = AndroidJNIHelper.GetMethodID(map.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");

        var args = new object[2];
        foreach (KeyValuePair<string, string> kvp in dict)
        {
            using (var k = new AndroidJavaObject("java.lang.String", kvp.Key))
            {
                using (var v = new AndroidJavaObject("java.lang.String", kvp.Value))
                {
                    args[0] = k;
                    args[1] = v;
                    AndroidJNI.CallObjectMethod(map.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
                }
            }
        }
        return map;
    }

    public static Dictionary<string, string> MapToDictionary(AndroidJavaObject obj)
    {
        var mapClazz = new AndroidJavaObject("java.util.HashMap");
        var setClazz = new AndroidJavaObject("java.util.HashSet");

        IntPtr keySetMethod = AndroidJNIHelper.GetMethodID(obj.GetRawClass(), "keySet", "()Ljava/util/Set;");
        IntPtr set = AndroidJNI.CallObjectMethod(obj.GetRawObject(), keySetMethod, new jvalue[] { });
        IntPtr toArrayMethod = AndroidJNI.GetMethodID(setClazz.GetRawClass(), "toArray", "()[Ljava/lang/Object;");
        IntPtr array = AndroidJNI.CallObjectMethod(set, toArrayMethod, new jvalue[] { });

        var dict = new Dictionary<string, string>();
        var keys = AndroidJNIHelper.ConvertFromJNIArray<string[]>(array);
        IntPtr getMethod = AndroidJNIHelper.GetMethodID(mapClazz.GetRawClass(), "get", "(Ljava/lang/Object;)Ljava/lang/Object;");
        foreach (var k in keys)
        {
            string v = AndroidJNI.CallStringMethod(obj.GetRawObject(), getMethod, new jvalue[] { new jvalue() { l = AndroidJNI.NewStringUTF(k) } });
            dict.Add(k, v);
        }

        return dict;
    }

    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    private static Array DateTimeArrayToDateArray(DateTime[] dates)
    {
        if (dates == null || dates.Length == 0) return new object[0];

        var arr = new AndroidJavaObject[dates.Length];
        for (int i = 0; i < dates.Length; i++)
        {
            arr[i] = DateTimeToDate(dates[i]);
        }

        return arr;
    }

    private static AndroidJavaObject DateTimeToDate(DateTime date)
    {
        return new AndroidJavaObject("java.util.Date", Convert.ToInt64((date - UnixEpoch).TotalMilliseconds));
    }

    private static AndroidJavaObject LocationInfoToLocation(LocationInfo location)
    {
        var l = new AndroidJavaObject("android.location.Location", "unity");
        l.Call("setLatitude", (double)location.latitude);
        l.Call("setLongitude", (double)location.longitude);
        l.Call("setAltitude", (double)location.altitude);
        l.Call("setTime", (long)location.timestamp * 1000);
        l.Call("setAccuracy", (location.horizontalAccuracy + location.verticalAccuracy) / 2.0f);
        return l;
    }

    private static AndroidJavaObject GetProfileScopeEnum(ProfileScope scope)
    {
        string name = "";

        switch (scope)
        {
            case ProfileScope.Application:
                name = "APPLICATION";
                break;
            case ProfileScope.Organization:
                name = "ORGANIZATION";
                break;
        }
        using (var c = new AndroidJavaClass("com.localytics.android.Localytics$ProfileScope"))
        {
            return c.GetStatic<AndroidJavaObject>(name);
        }
    }

    private static AndroidJavaObject GetInAppMessageDismissButtonLocationEnum(InAppMessageDismissButtonLocation buttonLocation)
    {
        string name = "";

        switch (buttonLocation)
        {
            case InAppMessageDismissButtonLocation.Left:
                name = "LEFT";
                break;
            case InAppMessageDismissButtonLocation.Right:
                name = "RIGHT";
                break;
        }

        using (var c = new AndroidJavaClass("com.localytics.android.Localytics$InAppMessageDismissButtonLocation"))
        {
            return c.GetStatic<AndroidJavaObject>(name);
        }
    }

    class AnalyticsListener : AndroidJavaProxy
    {
        public AnalyticsListener()
            : base("com.localytics.android.AnalyticsListener")
        {

        }

        public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
        {
            // hack to intercept Map for attributes
            if (methodName == "localyticsDidTagEvent")
            {
                string e = javaArgs[0].Call<string>("toString");
                var attributes = Localytics.MapToDictionary(javaArgs[1]);
                string c = javaArgs[2].Call<string>("toString");

                if (Localytics.OnLocalyticsDidTagEvent != null)
                    Localytics.OnLocalyticsDidTagEvent(e, attributes, Int64.Parse(c));
            }

            return base.Invoke(methodName, javaArgs);
        }


        void localyticsSessionDidOpen(bool isFirst, bool isUpgrade, bool isResume)
        {
            if (Localytics.OnLocalyticsSessionDidOpen != null)
                Localytics.OnLocalyticsSessionDidOpen(isFirst, isUpgrade, isResume);
        }

        void localyticsSessionWillClose()
        {
            if (Localytics.OnLocalyticsSessionWillClose != null)
                Localytics.OnLocalyticsSessionWillClose();
        }

        void localyticsSessionWillOpen(bool isFirst, bool isUpgrade, bool isResume)
        {
            if (Localytics.OnLocalyticsSessionWillOpen != null)
                Localytics.OnLocalyticsSessionWillOpen(isFirst, isUpgrade, isResume);
        }

		// hack for object comparison
		static bool isSelf;
		int hashCode() {
			isSelf = true;
			return this.GetHashCode();
		}
		
		bool equals(AndroidJavaObject o) {
			isSelf = false;
			o.Call<int>("hashCode");
			return isSelf;
		}
    }

    class MessagingListener : AndroidJavaProxy
    {
        public MessagingListener()
            : base("com.localytics.android.MessagingListener")
        {
            
        }

        void localyticsDidDismissInAppMessage()
        {
            if (Localytics.OnLocalyticsDidDismissInAppMessage != null)
                Localytics.OnLocalyticsDidDismissInAppMessage();
        }

        void localyticsDidDisplayInAppMessage()
        {
            if (Localytics.OnLocalyticsDidDisplayInAppMessage != null)
                Localytics.OnLocalyticsDidDisplayInAppMessage();
        }

        void localyticsWillDismissInAppMessage()
        {
            if (Localytics.OnLocalyticsWillDismissInAppMessage != null)
                Localytics.OnLocalyticsWillDismissInAppMessage();
        }

        void localyticsWillDisplayInAppMessage()
        {
            if (Localytics.OnLocalyticsWillDisplayInAppMessage != null)
                Localytics.OnLocalyticsWillDisplayInAppMessage();
        }

		// hack for object comparison
		static bool isSelf;
		int hashCode() {
			isSelf = true;
			return this.GetHashCode();
		}

		bool equals(AndroidJavaObject o) {
			isSelf = false;
			o.Call<int>("hashCode");
			return isSelf;
		}
    }
#endif
	
}
#endif