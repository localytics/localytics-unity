using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;
using System.Collections;


namespace LocalyticsUnity {

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

		public static string AnalyticsHost
	    {
	        get
	        {
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getAnalyticsHost");
				#elif UNITY_IOS
				return _analyticsHost();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
	        }
	        set
	        {
				#if UNITY_ANDROID
	            LocalyticsClass.CallStatic("setAnalyticsHost", value);
				#elif UNITY_IOS
				_setAnalyticsHost(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
	        }
	    }

		public static string AppKey
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getAppKey");
				#elif UNITY_IOS
				return _appKey();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static string CustomerId
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getCustomerId");
				#elif UNITY_IOS
				return _customerId();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setCustomerId", value);
				#elif UNITY_IOS
				_setCustomerId(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static InAppMessageDismissButtonLocation InAppMessageDismissButtonLocationEnum
		{
			get
			{
				#if UNITY_ANDROID
				string name = LocalyticsClass.CallStatic<AndroidJavaObject>("getInAppMessageDismissButtonLocation").Call<string>("name");
				switch (name)
				{
				default:
				case "LEFT":
					return InAppMessageDismissButtonLocation.Left;
				case "RIGHT":
					return InAppMessageDismissButtonLocation.Right;
				}
				#elif UNITY_IOS
				return (InAppMessageDismissButtonLocation)_inAppMessageDismissButtonLocation();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setInAppMessageDismissButtonLocation", GetInAppMessageDismissButtonLocationEnum(value));
				#elif UNITY_IOS
				_setInAppMessageDismissButtonLocation((uint)value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static string InstallId
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getInstallId");
				#elif UNITY_IOS
				return _installId();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}



		public static string LibraryVersion
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getLibraryVersion");
				#elif UNITY_IOS
				return _libraryVersion();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static bool LoggingEnabled
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<bool>("isLoggingEnabled");
				#elif UNITY_IOS
				return _isLoggingEnabled();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setLoggingEnabled", value);
				#elif UNITY_IOS
				_setLoggingEnabled(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static string MessagingHost
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getMessagingHost");
				#elif UNITY_IOS
				return _messagingHost();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setMessagingHost", value);
				#elif UNITY_IOS
				_setMessagingHost(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static bool OptedOut
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<bool>("isOptedOut");
				#elif UNITY_IOS
				return _isOptedOut();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setOptedOut", value);
				#elif UNITY_IOS
				_setOptedOut(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static string ProfilesHost
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getProfilesHost");
				#elif UNITY_IOS
				return _profilesHost();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setProfilesHost", value);
				#elif UNITY_IOS
				_setProfilesHost(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}



		public static bool TestModeEnabled
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<bool>("isTestModeEnabled");
				#elif UNITY_IOS
				return _testModeEnabled();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setTestModeEnabled", value);
				#elif UNITY_IOS
				_setTestModeEnabled(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static double SessionTimeoutInterval
		{
			get
			{
				#if UNITY_ANDROID
				return Convert.ToDouble(LocalyticsClass.CallStatic<long>("getSessionTimeoutInterval"));
				#elif UNITY_IOS
				return _sessionTimeoutInterval();
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setSessionTimeoutInterval", Convert.ToInt64(value));
				#elif UNITY_IOS
				_setSessionTimeoutInterval(value);
				#else
				throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}
		
		public static void AddProfileAttributesToSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
				values = MiniJSON.jsonEncode (attributeValue);
			_addProfileAttributesToSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void AddProfileAttributesToSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
				values = MiniJSON.jsonEncode (attributeValue);
			_addProfileAttributesToSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void CloseSession()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("closeSession");
			#elif UNITY_IOS
			_closeSession ();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void DecrementProfileAttribute(string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("decrementProfileAttribute", attributeName, decrementValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_decrementProfileAttribute (attributeName, decrementValue, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void DeleteProfileAttribute(string attributeName, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("deleteProfileAttribute", attributeName, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_deleteProfileAttribute (attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void SetCustomerEmail(string email)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerEmail", email);
			#elif UNITY_IOS
			_setCustomerEmail(email);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetCustomerFirstName(string firstName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerFirstName", firstName);
			#elif UNITY_IOS
			_setCustomerFirstName(firstName);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetCustomerLastName(string lastName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerLastName", lastName);
			#elif UNITY_IOS
			_setCustomerLastName(lastName);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetCustomerFullName(string fullName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerFullName", fullName);
			#elif UNITY_IOS
			_setCustomerFullName(fullName);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void DismissCurrentInAppMessage()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("dismissCurrentInAppMessage");
			#elif UNITY_IOS
			_dismissCurrentInAppMessage ();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static string GetCustomDimension(int dimension)
		{
			#if UNITY_ANDROID
			return LocalyticsClass.CallStatic<string>("getCustomDimension", dimension);
			#elif UNITY_IOS
			return _getCustomDimension (dimension);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static string GetIdentifier(string key)
		{
			#if UNITY_ANDROID
			return LocalyticsClass.CallStatic<string>("getIdentifier", key);
			#elif UNITY_IOS
			return _getIdentifier (key);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void IncrementProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("incrementProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_incrementProfileAttribute (attributeName, attributeValue, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void OpenSession()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("openSession");
			#elif UNITY_IOS
			_openSession();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void RegisterForAnalyticsEvents()
		{
			#if UNITY_ANDROID
			if (_analyticsListener == null) _analyticsListener = new AnalyticsListener();
			
			LocalyticsClass.CallStatic("addAnalyticsListener", _analyticsListener);
			#elif UNITY_IOS
			_registerReceiveAnalyticsCallback (ReceiveAnalyticsMessage);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void UnregisterForAnalyticsEvents()
		{
			#if UNITY_ANDROID
			if (_analyticsListener == null) return;
			
			LocalyticsClass.CallStatic("removeAnalyticsListener", _analyticsListener);
			#elif UNITY_IOS
			_removeAnalyticsCallback ();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void RegisterForMessagingEvents()
		{
			#if UNITY_ANDROID
			if (_messagingListener == null)
				_messagingListener = new MessagingListener();
			
			LocalyticsClass.CallStatic("addMessagingListener", _messagingListener);
			#elif UNITY_IOS
			_registerReceiveMessagingCallback (ReceiveMessagingMessage);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void UnregisterForMessagingEvents()
		{
			#if UNITY_ANDROID
			if (_messagingListener == null) return;
			
			LocalyticsClass.CallStatic("removeMessagingListener", _messagingListener);
			#elif UNITY_IOS
			_removeMessagingCallback ();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void RemoveProfileAttributesFromSet(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
				values = MiniJSON.jsonEncode (attributeValue);
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void RemoveProfileAttributesFromSet(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
				values = MiniJSON.jsonEncode (attributeValue);
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void SetCustomDimension(int dimension, string value)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomDimension", dimension, value);
			#elif UNITY_IOS
			_setCustomDimension (dimension, value);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void SetIdentifier(string key, string value)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setIdentifier", key, value);
			#elif UNITY_IOS
			_setIdentifier (key, value);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void SetLocation(LocationInfo location)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setLocation", LocationInfoToLocation(location));
			#elif UNITY_IOS
			_setLocation(location.latitude, location.longitude);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		
		public static void SetProfileAttribute(string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (new long[] { attributeValue });
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetProfileAttribute(string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (attributeValue);
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetProfileAttribute(string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (new string[] { attributeValue });
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void SetProfileAttribute(string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (attributeValue);
			_removeProfileAttributesFromSet (values, attributeName, (int)scope);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}
		
		public static void TagEvent(string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagEvent", eventName, DictionaryToMap(attributes), customerValueIncrease);
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
				values = MiniJSON.jsonEncode (attributes);
			_tagEvent(eventName, values, customerValueIncrease);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		public static void TagScreen(string screen)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagScreen", screen);
			#elif UNITY_IOS
			_tagScreen(screen);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TriggerInAppMessage(string triggerName, Dictionary<string, string> attributes = null)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("triggerInAppMessage", triggerName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
				values = MiniJSON.jsonEncode (attributes);
			_triggerInAppMessage (triggerName, values);
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void Upload()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("upload");
			#elif UNITY_IOS
			_upload();
			#else
			throw new NotImplementedException("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		
		/*
		 * Platform specific Native SDK Calls
		 * 
		 * */
#if UNITY_ANDROID

		public static void ClearInAppMessageDisplayActivity()
		{
			LocalyticsClass.CallStatic("clearInAppMessageDisplayActivity");
		}

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
#elif UNITY_IOS
		public static string PushToken
		{
			get
			{
				return _pushToken();
			}
		}

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
		
		/* 
		 * Android Helpers
		 * */
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

#if UNITY_IOS
		/* 
		 * iOS Helpers
		 * - DLL Imports
		 * - iOS Specific delegates
		 * - MonoPInvokeCallback
		 * */
		[DllImport("__Internal")] private static extern string _appKey();
		[DllImport("__Internal")] private static extern string _analyticsHost();
		[DllImport("__Internal")] private static extern void _setAnalyticsHost(string analyticsHost);
		[DllImport("__Internal")] private static extern string _customerId();
		[DllImport("__Internal")] private static extern void _setCustomerId(string customerId);
		[DllImport("__Internal")] private static extern uint _inAppMessageDismissButtonLocation();
		[DllImport("__Internal")] private static extern void _setInAppMessageDismissButtonLocation(uint location);
		[DllImport("__Internal")] private static extern string _installId();
		[DllImport("__Internal")] private static extern bool _isCollectingAdvertisingIdentifier();
		[DllImport("__Internal")] private static extern void _setCollectingAdvertisingIdentifier(bool collectingAdvertisingIdentifier);
		[DllImport("__Internal")] private static extern string _libraryVersion();
		[DllImport("__Internal")] private static extern bool _isLoggingEnabled();
		[DllImport("__Internal")] private static extern void _setLoggingEnabled(bool loggingEnabled);
		[DllImport("__Internal")] private static extern string _messagingHost();
		[DllImport("__Internal")] private static extern void _setMessagingHost(string messagingHost);
		[DllImport("__Internal")] private static extern bool _isOptedOut();
		[DllImport("__Internal")] private static extern void _setOptedOut(bool optedOut);
		[DllImport("__Internal")] private static extern string _profilesHost();
		[DllImport("__Internal")] private static extern void _setProfilesHost(string profilesHost);
		[DllImport("__Internal")] private static extern string _pushToken();
		[DllImport("__Internal")] private static extern bool _testModeEnabled();
		[DllImport("__Internal")] private static extern void _setTestModeEnabled(bool enabled);
		[DllImport("__Internal")] private static extern double _sessionTimeoutInterval();
		[DllImport("__Internal")] private static extern void _setSessionTimeoutInterval(double timeoutInterval);
		[DllImport("__Internal")] private static extern void _addProfileAttributesToSet(string attribute, string values, int scope);
		[DllImport("__Internal")] private static extern void _closeSession();
		[DllImport("__Internal")] private static extern void _decrementProfileAttribute(string attributeName, long value, int scope);
		[DllImport("__Internal")] private static extern void _deleteProfileAttribute(string attributeName, int scope);
		[DllImport("__Internal")] private static extern void _setCustomerEmail(string email);
		[DllImport("__Internal")] private static extern void _setCustomerFirstName(string firstName);
		[DllImport("__Internal")] private static extern void _setCustomerLastName(string lastName);
		[DllImport("__Internal")] private static extern void _setCustomerFullName(string fullName);
		[DllImport("__Internal")] private static extern void _dismissCurrentInAppMessage();
		[DllImport("__Internal")] private static extern string _getCustomDimension(int dimension);
		[DllImport("__Internal")] private static extern string _getIdentifier(string key);
		[DllImport("__Internal")] private static extern void _incrementProfileAttribute(string attributeName, long attributeValue, int scope);
		[DllImport("__Internal")] public static extern void _openSession();
		[DllImport("__Internal")] private static extern void _registerReceiveAnalyticsCallback(ReceiveAnalyticsDelegate callback);
		[DllImport("__Internal")] private static extern void _removeAnalyticsCallback ();
		[DllImport("__Internal")] private static extern bool _isInAppAdIdParameterEnabled();
		[DllImport("__Internal")] private static extern void _setInAppAdIdParameterEnabled(bool appAdIdEnabled);
		[DllImport("__Internal")] private static extern void _registerReceiveMessagingCallback(ReceiveMessagingDelegate callback);
		[DllImport("__Internal")] private static extern void _removeMessagingCallback ();
		[DllImport("__Internal")] private static extern void _removeProfileAttributesFromSet(string attributeName, string attributeValue, int scope);
		[DllImport("__Internal")] private static extern void _setCustomDimension(int dimension, string value);
		[DllImport("__Internal")] private static extern void _setIdentifier(string key, string value);
		[DllImport("__Internal")] private static extern void _setLocation(double latitude, double longitude);
		[DllImport("__Internal")] private static extern void _setProfileAttribute (string attributeName, string values, int scope);
		[DllImport("__Internal")] private static extern void _tagEvent(string eventName, string attributes, long customerValueIncrease);
		[DllImport("__Internal")] private static extern void _tagScreen(string screen);
		[DllImport("__Internal")] private static extern void _triggerInAppMessage(string triggerName, string attributes);
		[DllImport("__Internal")] private static extern void _upload();


		private delegate void ReceiveAnalyticsDelegate(string message);
		private delegate void ReceiveMessagingDelegate(string message);
		
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
#endif
		
	}

}