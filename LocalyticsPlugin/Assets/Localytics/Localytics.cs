using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using UnityEngine;
using AOT;
using System.Collections;


namespace LocalyticsUnity
{

	public class CustomerInfo
	{
		private string customerId;
		private string firstName;
		private string lastName;
		private string fullName;
		private string emailAddress;

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

		public Dictionary<string, string> ToDictionary ()
		{
			Dictionary<string, string> dict = new Dictionary<string, string> ();
			if (customerId != null)
			{
				dict.Add ("customer_id", customerId);
			}
			if (firstName != null)
			{
				dict.Add ("first_name", firstName);
			}
			if (lastName != null)
			{
				dict.Add ("last_name", lastName);
			}
			if (fullName != null)
			{
				dict.Add ("full_name", fullName);
			}
			if (emailAddress != null)
			{
				dict.Add ("email_address", emailAddress);
			}
			return dict;
		}
	}

	public class RegionInfo
	{
		private string uniqueId;
		private double latitude;
		private double longitude;
		private string name;
		private string type;
		private Dictionary<string, string> attributes;

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
		private int radius;

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
		private long campaignId;
		private string name;
		private Dictionary<string, string> attributes;

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
		private long creativeId;
		private string creativeType;
		private string message;
		private string soundFilename;

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
		private CircularRegionInfo region;
		private Localytics.RegionEvent triggerEvent;

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

	public class Localytics
	{
		// Analytics
		public delegate void LocalyticsDidTagEvent (string eventName, Dictionary<string, string> attributes, long customerValueIncrease);
		public delegate void LocalyticsSessionDidOpen (bool isFirst, bool isUpgrade, bool isResume);
		public delegate void LocalyticsSessionWillClose ();
		public delegate void LocalyticsSessionWillOpen (bool isFirst, bool isUpgrade, bool isResume);

		public static event LocalyticsDidTagEvent OnLocalyticsDidTagEvent;
		public static event LocalyticsSessionDidOpen OnLocalyticsSessionDidOpen;
		public static event LocalyticsSessionWillClose OnLocalyticsSessionWillClose;
		public static event LocalyticsSessionWillOpen OnLocalyticsSessionWillOpen;

		// Messaging
		public delegate void LocalyticsDidDismissInAppMessage ();
		public delegate void LocalyticsDidDisplayInAppMessage ();
		public delegate void LocalyticsWillDismissInAppMessage ();
		public delegate void LocalyticsWillDisplayInAppMessage ();

		public static event LocalyticsDidDismissInAppMessage OnLocalyticsDidDismissInAppMessage;
		public static event LocalyticsDidDisplayInAppMessage OnLocalyticsDidDisplayInAppMessage;
		public static event LocalyticsWillDismissInAppMessage OnLocalyticsWillDismissInAppMessage;
		public static event LocalyticsWillDisplayInAppMessage OnLocalyticsWillDisplayInAppMessage;

#if UNITY_ANDROID
		public delegate bool LocalyticsShouldShowPushNotification(PushCampaignInfo campaign);
		public delegate bool LocalyticsShouldShowPlacesPushNotification(PlacesCampaignInfo campaign);
		public delegate AndroidJavaObject LocalyticsWillShowPlacesPushNotification(AndroidJavaObject notificationBuilder, PlacesCampaignInfo campaign);
		public delegate AndroidJavaObject LocalyticsWillShowPushNotification(AndroidJavaObject notificationBuilder, PushCampaignInfo campaign);

		public static event LocalyticsShouldShowPushNotification OnLocalyticsShouldShowPushNotification;
		public static event LocalyticsShouldShowPlacesPushNotification OnLocalyticsShouldShowPlacesPushNotification;
		public static event LocalyticsWillShowPlacesPushNotification OnLocalyticsWillShowPlacesPushNotification;
		public static event LocalyticsWillShowPushNotification OnLocalyticsWillShowPushNotification;
#endif

		// Location
		public delegate void LocalyticsDidTriggerRegions (List<CircularRegionInfo> regions, RegionEvent regionEvent);
		public delegate void LocalyticsDidUpdateMonitoredGeofences (List<CircularRegionInfo> added, List<CircularRegionInfo> removed);
#if UNITY_ANDROID
		public delegate void LocalyticsDidUpdateLocation(AndroidJavaObject location);
#else
		public delegate void LocalyticsDidUpdateLocation (Dictionary<string, object> locationDict);
#endif

		public static event LocalyticsDidUpdateLocation OnLocalyticsDidUpdateLocation;
		public static event LocalyticsDidTriggerRegions OnLocalyticsDidTriggerRegions;
		public static event LocalyticsDidUpdateMonitoredGeofences OnLocalyticsDidUpdateMonitoredGeofences;

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

		public enum RegionEvent
		{
			Enter,
			Exit
		}

		// #################################################
		// Integration
		// #################################################

		public static void Upload ()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("upload");
			#elif UNITY_IOS
			_upload();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// Analytics
		// #################################################

		public static void OpenSession ()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("openSession");
			#elif UNITY_IOS
			_openSession();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void CloseSession ()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("closeSession");
			#elif UNITY_IOS
			_closeSession ();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagEvent (string eventName, Dictionary<string, string> attributes = null, long customerValueIncrease = 0)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagEvent", eventName, DictionaryToMap(attributes), customerValueIncrease);
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode (attributes);
			}
			_tagEvent(eventName, values, customerValueIncrease);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagPurchased (string itemName, string itemId, string itemType, long itemPrice, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagPurchased", itemName, itemId, itemType, new AndroidJavaObject("java/lang/Long", itemPrice), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagPurchased(itemName, itemId, itemType, itemPrice, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagAddedToCart (string itemName, string itemId, string itemType, long itemPrice, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagAddedToCart", itemName, itemId, itemType, new AndroidJavaObject("java/lang/Long", itemPrice), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagAddedToCart(itemName, itemId, itemType, itemPrice, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagStartedCheckout (long totalPrice, long itemCount, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagStartedCheckout", new AndroidJavaObject("java/lang/Long", totalPrice), new AndroidJavaObject("java/lang/Long", itemCount), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			Dictionary<string, string> copy = null;
			if (attributes == null)
			{
				copy = new Dictionary<string, string>();
			}
			else
			{
				copy = new Dictionary<string, string>(attributes);
			}
			copy.Add("ll_total_price", totalPrice+"");
			copy.Add("ll_item_count", itemCount+"");
			values = MiniJSON.jsonEncode(copy);
			_tagStartedCheckout(values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagCompletedCheckout (long totalPrice, long itemCount, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagCompletedCheckout", new AndroidJavaObject("java/lang/Long", totalPrice), new AndroidJavaObject("java/lang/Long", itemCount), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			Dictionary<string, string> copy = null;
			if (attributes == null)
			{
				copy = new Dictionary<string, string>();
			}
			else
			{
				copy = new Dictionary<string, string>(attributes);
			}
			copy.Add("ll_total_price", totalPrice+"");
			copy.Add("ll_item_count", itemCount+"");
			values = MiniJSON.jsonEncode(copy);
			_tagCompletedCheckout(values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagContentViewed (string contentName, string contentId, string contentType, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagContentViewed", contentName, contentId, contentType, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagContentViewed(contentName, contentId, contentType, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagSearched (string queryText, string contentType, long resultCount, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagSearched", queryText, contentType, new AndroidJavaObject("java/lang/Long", resultCount), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagSearched(queryText, contentType, resultCount, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagShared (string contentName, string contentId, string contentType, string methodName, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagShared", contentName, contentId, contentType, methodName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagShared(contentName, contentId, contentType, methodName, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagContentRated (string contentName, string contentId, string contentType, long rating, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagContentRated", contentName, contentId, contentType, new AndroidJavaObject("java/lang/Long", rating), DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagContentRated(contentName, contentId, contentType, rating, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagCustomerRegistered (CustomerInfo customer, string methodName, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagCustomerRegistered", ConvertCustomerInfo(customer), methodName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string customerValues = "";
			if (customer != null)
			{
				customerValues = MiniJSON.jsonEncode(customer.ToDictionary());
			}
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagCustomerRegistered(customerValues, methodName, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagCustomerLoggedIn (CustomerInfo customer, string methodName, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagCustomerLoggedIn", ConvertCustomerInfo(customer), methodName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string customerValues = "";
			if (customer != null)
			{
				customerValues = MiniJSON.jsonEncode(customer.ToDictionary());
			}
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagCustomerLoggedIn(customerValues, methodName, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagCustomerLoggedOut (Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagCustomerLoggedOut", DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagCustomerLoggedOut(values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagInvited (string methodName, Dictionary<string, string> attributes)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagInvited", methodName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode(attributes);
			}
			_tagInvited(methodName, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TagScreen (string screen)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("tagScreen", screen);
			#elif UNITY_IOS
			_tagScreen(screen);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static string GetCustomDimension (int dimension)
		{
			#if UNITY_ANDROID
			return LocalyticsClass.CallStatic<string>("getCustomDimension", dimension);
			#elif UNITY_IOS
			return _getCustomDimension (dimension);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetCustomDimension (int dimension, string value)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomDimension", dimension, value);
			#elif UNITY_IOS
			_setCustomDimension (dimension, value);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setOptedOut", value);
				#elif UNITY_IOS
				_setOptedOut(value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static void RegisterForAnalyticsEvents ()
		{
			#if UNITY_ANDROID
			if (_analyticsListener == null)
			{
				_analyticsListener = new AnalyticsListener();
			}

			LocalyticsClass.CallStatic("setAnalyticsListener", _analyticsListener);
			#elif UNITY_IOS
			_registerReceiveAnalyticsCallback (ReceiveAnalyticsMessage);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void UnregisterForAnalyticsEvents ()
		{
			#if UNITY_ANDROID
			if (_analyticsListener == null)
			{
				return;
			}

			LocalyticsClass.CallStatic("setAnalyticsListener", null);
			_analyticsListener = null;
			#elif UNITY_IOS
			_removeAnalyticsCallback ();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// Profiles
		// #################################################

		public static void SetProfileAttribute (string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_setProfileAttributeLong (attributeName, attributeValue, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetProfileAttribute (string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (attributeValue);
			_setProfileAttributeLongArray (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetProfileAttribute (string attributeName, string attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_setProfileAttributeString (attributeName, attributeValue, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetProfileAttribute (string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = MiniJSON.jsonEncode (attributeValue);
			_setProfileAttributeStringArray (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void DeleteProfileAttribute (string attributeName, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("deleteProfileAttribute", attributeName, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_deleteProfileAttribute (attributeName, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void AddProfileAttributesToSet (string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
			{
				values = MiniJSON.jsonEncode (attributeValue);
			}
			_addProfileAttributesToSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void AddProfileAttributesToSet (string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("addProfileAttributesToSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
			{
				values = MiniJSON.jsonEncode (attributeValue);
			}
			_addProfileAttributesToSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void RemoveProfileAttributesFromSet (string attributeName, long[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
			{
				values = MiniJSON.jsonEncode (attributeValue);
			}
			_removeProfileAttributesFromSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void RemoveProfileAttributesFromSet (string attributeName, string[] attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("removeProfileAttributesFromSet", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			string values = "";
			if (attributeValue != null)
			{
				values = MiniJSON.jsonEncode (attributeValue);
			}
			_removeProfileAttributesFromSet (attributeName, values, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void IncrementProfileAttribute (string attributeName, long attributeValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("incrementProfileAttribute", attributeName, attributeValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_incrementProfileAttribute (attributeName, attributeValue, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void DecrementProfileAttribute (string attributeName, long decrementValue, ProfileScope scope = ProfileScope.Application)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("decrementProfileAttribute", attributeName, decrementValue, GetProfileScopeEnum(scope));
			#elif UNITY_IOS
			_decrementProfileAttribute (attributeName, decrementValue, (int)scope);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetCustomerEmail (string email)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerEmail", email);
			#elif UNITY_IOS
			_setCustomerEmail(email);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetCustomerFirstName (string firstName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerFirstName", firstName);
			#elif UNITY_IOS
			_setCustomerFirstName(firstName);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetCustomerLastName (string lastName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerLastName", lastName);
			#elif UNITY_IOS
			_setCustomerLastName(lastName);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetCustomerFullName (string fullName)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setCustomerFullName", fullName);
			#elif UNITY_IOS
			_setCustomerFullName(fullName);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// Marketing
		// #################################################

		public static void TriggerInAppMessage (string triggerName, Dictionary<string, string> attributes = null)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("triggerInAppMessage", triggerName, DictionaryToMap(attributes));
			#elif UNITY_IOS
			string values = "";
			if (attributes != null)
			{
				values = MiniJSON.jsonEncode (attributes);
			}
			_triggerInAppMessage (triggerName, values);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void DismissCurrentInAppMessage ()
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("dismissCurrentInAppMessage");
			#elif UNITY_IOS
			_dismissCurrentInAppMessage ();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setInAppMessageDismissButtonLocation", GetInAppMessageDismissButtonLocationEnum(value));
				#elif UNITY_IOS
				_setInAppMessageDismissButtonLocation((uint)value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setTestModeEnabled", value);
				#elif UNITY_IOS
				_setTestModeEnabled(value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

#if UNITY_ANDROID
		
		public static void ClearInAppMessageDisplayActivity()
		{
			LocalyticsClass.CallStatic("clearInAppMessageDisplayActivity");
		}

		public static void RegisterPush(string senderId)
		{
			LocalyticsClass.CallStatic("registerPush", senderId);
		}

		public static void DisplayPushNotification(Dictionary<string, string> data)
		{
			LocalyticsClass.CallStatic("displayPushNotification", DictionaryToBundle(data));
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

		public static bool NotificationsDisabled
		{
			get
			{
				return LocalyticsClass.CallStatic<bool>("areNotificationsDisabled");
			}
			set
			{
				LocalyticsClass.CallStatic("setNotificationsDisabled", value);
			}
		}
#endif

#if UNITY_IOS
		public static string PushToken
		{
			get
			{
				return _pushToken();
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

		public static void RegisterForMessagingEvents ()
		{
			#if UNITY_ANDROID
			if (_messagingListener == null)
			{
				_messagingListener = new MessagingListener();
			}

			LocalyticsClass.CallStatic("setMessagingListener", _messagingListener);
			#elif UNITY_IOS
			_registerReceiveMessagingCallback (ReceiveMessagingMessage);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void UnregisterForMessagingEvents ()
		{
			#if UNITY_ANDROID
			if (_messagingListener == null)
			{
				return;
			}

			LocalyticsClass.CallStatic("setMessagingListener", null);
			_messagingListener = null;
			#elif UNITY_IOS
			_removeMessagingCallback ();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// Location
		// #################################################

		public static bool LocationMonitoringEnabled
		{
#if UNITY_ANDROID
			get
			{
				return LocalyticsClass.CallStatic<bool>("isLocationMonitoringEnabled");
			}
#endif
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setLocationMonitoringEnabled", value);
				#elif UNITY_IOS
				_setLocationMonitoringEnabled(value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static List<CircularRegionInfo> GetGeofencesToMonitor (double latitude, double longitude)
		{
			#if UNITY_ANDROID
			AndroidJavaObject listObject = LocalyticsClass.CallStatic<AndroidJavaObject>("getGeofencesToMonitor", latitude, longitude);
			return ConvertGeofencesList(listObject);
			#elif UNITY_IOS
			string json = _getGeofencesToMonitor(latitude, longitude);
			return ConvertGeofencesListFromJson(json);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void TriggerRegions (List<CircularRegionInfo> regions, RegionEvent regionEvent)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("triggerRegions", ConvertRegionInfoList(regions), GetRegionEventEnum(regionEvent));
			#elif UNITY_IOS
			string values = "";
			if (regions != null)
			{
				values = ConvertRegionInfoList(regions);
			}
			_triggerRegions(values, (int)regionEvent);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void RegisterForLocationEvents ()
		{
			#if UNITY_ANDROID
			if (_locationListener == null)
			{
				_locationListener = new LocationListener();
			}

			LocalyticsClass.CallStatic("setLocationListener", _locationListener);
			#elif UNITY_IOS
			_registerReceiveLocationCallback (ReceiveLocationMessage);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void UnregisterForLocationEvents ()
		{
			#if UNITY_ANDROID
			if (_locationListener == null)
			{
				return;
			}

			LocalyticsClass.CallStatic("setLocationListener", null);
			_locationListener = null;
			#elif UNITY_IOS
			_removeLocationCallback ();
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// User Information
		// #################################################

		public static string CustomerId
		{
			get
			{
				#if UNITY_ANDROID
				return LocalyticsClass.CallStatic<string>("getCustomerId");
				#elif UNITY_IOS
				return _customerId();
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setCustomerId", value);
				#elif UNITY_IOS
				_setCustomerId(value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

		public static string GetIdentifier (string key)
		{
			#if UNITY_ANDROID
			return LocalyticsClass.CallStatic<string>("getIdentifier", key);
			#elif UNITY_IOS
			return _getIdentifier (key);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetIdentifier (string key, string value)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setIdentifier", key, value);
			#elif UNITY_IOS
			_setIdentifier (key, value);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetLocation (LocationInfo location)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setLocation", LocationInfoToLocation(location));
			#elif UNITY_IOS
			_setLocation(location.latitude, location.longitude);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		// #################################################
		// Developer Options
		// #################################################

		public static void SetOption (string key, string stringValue)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setOption", key, stringValue);
			#elif UNITY_IOS
			_setStringOption(key, stringValue);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetOption (string key, long longValue)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setOption", key, new AndroidJavaObject("java/lang/Long", longValue));
			#elif UNITY_IOS
			_setLongOption(key, longValue);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
		}

		public static void SetOption (string key, bool boolValue)
		{
			#if UNITY_ANDROID
			LocalyticsClass.CallStatic("setOption", key, new AndroidJavaObject("java/lang/Boolean", boolValue));
			#elif UNITY_IOS
			_setBoolOption(key, boolValue);
			#else
			throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
			#endif
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
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
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
			set
			{
				#if UNITY_ANDROID
				LocalyticsClass.CallStatic("setLoggingEnabled", value);
				#elif UNITY_IOS
				_setLoggingEnabled(value);
				#else
				throw new NotImplementedException ("Localytics Unity SDK only supports iOS or Android");
				#endif
			}
		}

#if UNITY_ANDROID
		
		/*
		 * Android Helpers
		 *
		 **/
		private static AnalyticsListener _analyticsListener;
		private static MessagingListener _messagingListener;
		private static LocationListener _locationListener;

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

			if (dict == null || dict.Count == 0)
			{
				return map;
			}

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

		private static AndroidJavaObject DictionaryToBundle(Dictionary<string, string> dict)
		{
			var bundle = new AndroidJavaObject("android.os.Bundle");

			if (dict == null || dict.Count == 0)
			{
				return bundle;
			}

			IntPtr putMethod = AndroidJNIHelper.GetMethodID(bundle.GetRawClass(), "putString", "(Ljava/lang/String;Ljava/lang/String;)V;");

			var args = new object[2];
			foreach (KeyValuePair<string, string> kvp in dict)
			{
				using (var k = new AndroidJavaObject("java.lang.String", kvp.Key))
				{
					using (var v = new AndroidJavaObject("java.lang.String", kvp.Value))
					{
						args[0] = k;
						args[1] = v;
						AndroidJNI.CallObjectMethod(bundle.GetRawObject(), putMethod, AndroidJNIHelper.CreateJNIArgArray(args));
					}
				}
			}
			return bundle;
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

		private static AndroidJavaObject ConvertCustomerInfo(CustomerInfo obj)
		{
			var builder = new AndroidJavaObject("com.localytics.android.Customer$Builder");
			if (obj != null)
			{
				var args = new object[1];
				if (obj.CustomerId != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setCustomerId", "(Ljava/lang/String;)Lcom.localytics.android.Customer$Builder;");
					args[0] = obj.CustomerId;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}

				if (obj.FirstName != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setFirstName", "(Ljava/lang/String;)Lcom.localytics.android.Customer$Builder;");
					args[0] = obj.FirstName;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}

				if (obj.LastName != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setLastName", "(Ljava/lang/String;)Lcom.localytics.android.Customer$Builder;");
					args[0] = obj.LastName;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}

				if (obj.FullName != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setFullName", "(Ljava/lang/String;)Lcom.localytics.android.Customer$Builder;");
					args[0] = obj.FullName;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}

				if (obj.EmailAddress != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setEmailAddress", "(Ljava/lang/String;)Lcom.localytics.android.Customer$Builder;");
					args[0] = obj.EmailAddress;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}
			}

			return builder.Call<AndroidJavaObject> ("build", new object[]{});
		}

		private static List<CircularRegionInfo> ConvertGeofencesList(AndroidJavaObject listObject)
		{
			int size = listObject.Call<int>("size");

			List<CircularRegionInfo> regionInfos = new List<CircularRegionInfo>();
			for (int i = 0; i < size; i++)
			{
				AndroidJavaObject regionObj = listObject.Call<AndroidJavaObject>("get", i);
				CircularRegionInfo info = ConvertCircularRegionInfoFromJava(regionObj);
				regionInfos.Add(info);
			}
			return regionInfos;
		}

		private static AndroidJavaObject ConvertRegionInfoList(List<CircularRegionInfo> regions)
		{
			var list = new AndroidJavaObject("java.util.ArrayList");

			if (regions == null || regions.Count == 0)
			{
				return list;
			}

			foreach (CircularRegionInfo info in regions)
			{
				list.Call<bool>("add", ConvertCircularRegionInfoToJava(info));
			}
			return list;
		}

		private static CircularRegionInfo ConvertCircularRegionInfoFromJava(AndroidJavaObject obj)
		{
			CircularRegionInfo info = new CircularRegionInfo();
			info.UniqueId = obj.Call<string>("getUniqueId");
			info.Latitude = obj.Call<double>("getLatitude");
			info.Longitude = obj.Call<double>("getLongitude");
			info.Radius = obj.Call<int>("getRadius");
			info.Name = obj.Call<string>("getName");
			info.Type = obj.Call<string>("getType");
			info.Attributes = MapToDictionary(obj.Call<AndroidJavaObject>("getAttributes"));
			return info;
		}

		private static AndroidJavaObject ConvertCircularRegionInfoToJava(CircularRegionInfo info)
		{
			var builder = new AndroidJavaObject("com.localytics.android.CircularRegion$Builder");
			if (info != null)
			{
				var args = new object[1];
				// Only care about uniqueId b/c triggerRegions will pull the populated CircularRegion
				// object from the DB
				if (info.UniqueId != null)
				{
					IntPtr method = AndroidJNIHelper.GetMethodID(builder.GetRawClass(), "setUniqueId", "(Ljava/lang/String;)Lcom.localytics.android.CircularRegion$Builder;");
					args[0] = info.UniqueId;
					AndroidJNI.CallObjectMethod(builder.GetRawObject(), method, AndroidJNIHelper.CreateJNIArgArray(args));
				}
			}

			return builder.Call<AndroidJavaObject> ("build", new object[]{});
		}

		private static PushCampaignInfo ConvertPushCampaign(AndroidJavaObject obj)
		{
			PushCampaignInfo info = new PushCampaignInfo();
			info.CampaignId = obj.Call<long>("getCampaignId");
			info.Name = obj.Call<string>("getName");
			info.Attributes = MapToDictionary(obj.Call<AndroidJavaObject>("getAttributes"));
			info.CreativeId = obj.Call<long>("getCreativeId");
			info.CreativeType = obj.Call<string>("getCreativeType");
			info.Message = obj.Call<string>("getMessage");
			info.SoundFilename = obj.Call<string>("getSoundFilename");
			return info;
		}

		private static PlacesCampaignInfo ConvertPlacesCampaign(AndroidJavaObject obj)
		{
			PlacesCampaignInfo info = new PlacesCampaignInfo();
			info.CampaignId = obj.Call<long>("getCampaignId");
			info.Name = obj.Call<string>("getName");
			info.Attributes = MapToDictionary(obj.Call<AndroidJavaObject>("getAttributes"));
			info.CreativeId = obj.Call<long>("getCreativeId");
			info.CreativeType = obj.Call<string>("getCreativeType");
			info.Message = obj.Call<string>("getMessage");
			info.SoundFilename = obj.Call<string>("getSoundFilename");
			info.Region = ConvertCircularRegionInfoFromJava(obj.Call<AndroidJavaObject>("getRegion"));
			info.TriggerEvent = RegionEventFromJava(obj.Call<AndroidJavaObject>("getTriggerEvent"));
			return info;
		}

		private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private static Array DateTimeArrayToDateArray(DateTime[] dates)
		{
			if (dates == null || dates.Length == 0)
			{
				return new object[0];
			}

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

		private static AndroidJavaObject GetRegionEventEnum(RegionEvent regionEvent)
		{
			string name = "";

			switch (regionEvent)
			{
				case RegionEvent.Enter:
					name = "ENTER";
					break;
				case RegionEvent.Exit:
					name = "EXIT";
					break;
			}

			using (var c = new AndroidJavaClass("com.localytics.android.Region$Event"))
			{
				return c.GetStatic<AndroidJavaObject>(name);
			}
		}

		private static RegionEvent RegionEventFromJava(AndroidJavaObject obj)
		{
			string name = obj.Call<string>("name");
			switch (name)
			{
				default:
				case "ENTER":
					return RegionEvent.Enter;
				case "EXIT":
					return RegionEvent.Exit;
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
					{
						Localytics.OnLocalyticsDidTagEvent(e, attributes, Int64.Parse(c));
						return null;
					}
				}

				return base.Invoke(methodName, javaArgs);
			}

			void localyticsSessionDidOpen(bool isFirst, bool isUpgrade, bool isResume)
			{
				if (Localytics.OnLocalyticsSessionDidOpen != null)
				{
					Localytics.OnLocalyticsSessionDidOpen(isFirst, isUpgrade, isResume);
				}
			}

			void localyticsSessionWillClose()
			{
				if (Localytics.OnLocalyticsSessionWillClose != null)
				{
					Localytics.OnLocalyticsSessionWillClose();
				}
			}

			void localyticsSessionWillOpen(bool isFirst, bool isUpgrade, bool isResume)
			{
				if (Localytics.OnLocalyticsSessionWillOpen != null)
				{
					Localytics.OnLocalyticsSessionWillOpen(isFirst, isUpgrade, isResume);
				}
			}

			// hack for object comparison
			static bool isSelf;
			int hashCode()
			{
				isSelf = true;
				return this.GetHashCode();
			}

			bool equals(AndroidJavaObject o)
			{
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

			public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
			{
				// hack to intercept campaign objects
				if (methodName == "localyticsShouldShowPushNotification")
				{
					PushCampaignInfo info = ConvertPushCampaign(javaArgs[0]);
					if (Localytics.OnLocalyticsShouldShowPushNotification != null)
					{
						bool should = Localytics.OnLocalyticsShouldShowPushNotification(info);
						return new AndroidJavaObject("java.lang.Boolean", should);
					}
				}
				else if (methodName == "localyticsShouldShowPlacesPushNotification")
				{
					PlacesCampaignInfo info = ConvertPlacesCampaign(javaArgs[0]);
					if (Localytics.OnLocalyticsShouldShowPlacesPushNotification != null)
					{
						bool should = Localytics.OnLocalyticsShouldShowPlacesPushNotification(info);
						return new AndroidJavaObject("java.lang.Boolean", should);
					}
				}
				else if (methodName == "localyticsWillShowPlacesPushNotification")
				{
					AndroidJavaObject notifBuilder = javaArgs[0];
					PlacesCampaignInfo info = ConvertPlacesCampaign(javaArgs[1]);
					if (Localytics.OnLocalyticsWillShowPlacesPushNotification != null)
					{
						return Localytics.OnLocalyticsWillShowPlacesPushNotification(notifBuilder, info);
					}
				}
				else if (methodName == "localyticsWillShowPushNotification")
				{
					AndroidJavaObject notifBuilder = javaArgs[0];
					PushCampaignInfo info = ConvertPushCampaign(javaArgs[1]);
					if (Localytics.OnLocalyticsWillShowPushNotification != null)
					{
						return Localytics.OnLocalyticsWillShowPushNotification(notifBuilder, info);
					}
				}

				return base.Invoke(methodName, javaArgs);
			}

			void localyticsDidDismissInAppMessage()
			{
				if (Localytics.OnLocalyticsDidDismissInAppMessage != null)
				{
					Localytics.OnLocalyticsDidDismissInAppMessage();
				}
			}

			void localyticsDidDisplayInAppMessage()
			{
				if (Localytics.OnLocalyticsDidDisplayInAppMessage != null)
				{
					Localytics.OnLocalyticsDidDisplayInAppMessage();
				}
			}

			void localyticsWillDismissInAppMessage()
			{
				if (Localytics.OnLocalyticsWillDismissInAppMessage != null)
				{
					Localytics.OnLocalyticsWillDismissInAppMessage();
				}
			}

			void localyticsWillDisplayInAppMessage()
			{
				if (Localytics.OnLocalyticsWillDisplayInAppMessage != null)
				{
					Localytics.OnLocalyticsWillDisplayInAppMessage();
				}
			}

			bool localyticsShouldShowPushNotification(PushCampaignInfo campaign)
			{
				if (Localytics.OnLocalyticsShouldShowPushNotification != null)
				{
					return Localytics.OnLocalyticsShouldShowPushNotification(campaign);
				}

				return true;
			}

			bool localyticsShouldShowPlacesPushNotification(PlacesCampaignInfo campaign)
			{
				if (Localytics.OnLocalyticsShouldShowPlacesPushNotification != null)
				{
					return Localytics.OnLocalyticsShouldShowPlacesPushNotification(campaign);
				}

				return true;
			}

			AndroidJavaObject localyticsWillShowPlacesPushNotification(AndroidJavaObject notificationBuilder, PlacesCampaignInfo campaign)
			{
				if (Localytics.OnLocalyticsWillShowPlacesPushNotification != null)
				{
					return Localytics.OnLocalyticsWillShowPlacesPushNotification(notificationBuilder, campaign);
				}

				return notificationBuilder;
			}

			AndroidJavaObject localyticsWillShowPushNotification(AndroidJavaObject notificationBuilder, PushCampaignInfo campaign)
			{
				if (Localytics.OnLocalyticsWillShowPushNotification != null)
				{
					return Localytics.OnLocalyticsWillShowPushNotification(notificationBuilder, campaign);
				}

				return notificationBuilder;
			}

			// hack for object comparison
			static bool isSelf;
			int hashCode()
			{
				isSelf = true;
				return this.GetHashCode();
			}

			bool equals(AndroidJavaObject o)
			{
				isSelf = false;
				o.Call<int>("hashCode");
				return isSelf;
			}
		}

		class LocationListener : AndroidJavaProxy
		{
			public LocationListener()
					: base("com.localytics.android.LocationListener")
			{
			}

			public override AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
			{
				// hack to intercept location and region objects
				if (methodName == "localyticsDidUpdateLocation")
				{
					if (Localytics.OnLocalyticsDidUpdateLocation != null)
					{
						Localytics.OnLocalyticsDidUpdateLocation(javaArgs[0]);
						return null;
					}
				}
				else if (methodName == "localyticsDidTriggerRegions")
				{
					List<CircularRegionInfo> regions = ConvertGeofencesList(javaArgs[0]);
					RegionEvent regionEvent = RegionEventFromJava(javaArgs[1]);
					if (Localytics.OnLocalyticsDidTriggerRegions != null)
					{
						Localytics.OnLocalyticsDidTriggerRegions(regions, regionEvent);
						return null;
					}
				}
				else if (methodName == "localyticsDidUpdateMonitoredGeofences")
				{
					List<CircularRegionInfo> added = ConvertGeofencesList(javaArgs[0]);
					List<CircularRegionInfo> removed = ConvertGeofencesList(javaArgs[1]);
					if (Localytics.OnLocalyticsDidUpdateMonitoredGeofences != null)
					{
						Localytics.OnLocalyticsDidUpdateMonitoredGeofences(added, removed);
						return null;
					}
				}

				return base.Invoke(methodName, javaArgs);
			}

			void localyticsDidUpdateLocation(AndroidJavaObject location)
			{
				if (Localytics.OnLocalyticsDidUpdateLocation != null)
				{
					Localytics.OnLocalyticsDidUpdateLocation(location);
				}
			}

			void localyticsDidTriggerRegions(List<CircularRegionInfo> regions, RegionEvent regionEvent)
			{
				if (Localytics.OnLocalyticsDidTriggerRegions != null)
				{
					Localytics.OnLocalyticsDidTriggerRegions(regions, regionEvent);
				}
			}

			void localyticsDidUpdateMonitoredGeofences(List<CircularRegionInfo> added, List<CircularRegionInfo> removed)
			{
				if (Localytics.OnLocalyticsDidUpdateMonitoredGeofences != null)
				{
					Localytics.OnLocalyticsDidUpdateMonitoredGeofences(added, removed);
				}
			}

			// hack for object comparison
			static bool isSelf;
			int hashCode()
			{
				isSelf = true;
				return this.GetHashCode();
			}

			bool equals(AndroidJavaObject o)
			{
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
		[DllImport("__Internal")] private static extern void _upload();
		[DllImport("__Internal")] private static extern void _openSession();
		[DllImport("__Internal")] private static extern void _closeSession();
		[DllImport("__Internal")] private static extern void _tagEvent(string eventName, string attributes, long customerValueIncrease);
		[DllImport("__Internal")] private static extern void _tagPurchased(string itemName, string itemId, string itemType, long itemPrice, string attributes);
		[DllImport("__Internal")] private static extern void _tagAddedToCart(string itemName, string itemId, string itemType, long itemPrice, string attributes);
		[DllImport("__Internal")] private static extern void _tagStartedCheckout(string attributes);
		[DllImport("__Internal")] private static extern void _tagCompletedCheckout(string attributes);
		[DllImport("__Internal")] private static extern void _tagContentViewed(string contentName, string contentId, string contentType, string attributes);
		[DllImport("__Internal")] private static extern void _tagSearched(string queryText, string contentType, long resultCount, string attributes);
		[DllImport("__Internal")] private static extern void _tagShared(string contentName, string contentId, string contentType, string methodName, string attributes);
		[DllImport("__Internal")] private static extern void _tagContentRated(string contentName, string contentId, string contentType, long rating, string attributes);
		[DllImport("__Internal")] private static extern void _tagCustomerRegistered(string customer, string methodName, string attributes);
		[DllImport("__Internal")] private static extern void _tagCustomerLoggedIn(string customer, string methodName, string attributes);
		[DllImport("__Internal")] private static extern void _tagCustomerLoggedOut(string attributes);
		[DllImport("__Internal")] private static extern void _tagInvited(string methodName, string attributes);
		[DllImport("__Internal")] private static extern void _tagScreen(string screen);
		[DllImport("__Internal")] private static extern string _getCustomDimension(int dimension);
		[DllImport("__Internal")] private static extern void _setCustomDimension(int dimension, string value);
		[DllImport("__Internal")] private static extern bool _isOptedOut();
		[DllImport("__Internal")] private static extern void _setOptedOut(bool optedOut);
		[DllImport("__Internal")] private static extern void _registerReceiveAnalyticsCallback(ReceiveAnalyticsDelegate callback);
		[DllImport("__Internal")] private static extern void _removeAnalyticsCallback ();
		[DllImport("__Internal")] private static extern void _setProfileAttributeLong (string attributeName, long value, int scope);
		[DllImport("__Internal")] private static extern void _setProfileAttributeLongArray (string attributeName, string values, int scope);
		[DllImport("__Internal")] private static extern void _setProfileAttributeString (string attributeName, string value, int scope);
		[DllImport("__Internal")] private static extern void _setProfileAttributeStringArray (string attributeName, string values, int scope);
		[DllImport("__Internal")] private static extern void _deleteProfileAttribute(string attributeName, int scope);
		[DllImport("__Internal")] private static extern void _addProfileAttributesToSet(string attribute, string values, int scope);
		[DllImport("__Internal")] private static extern void _removeProfileAttributesFromSet(string attributeName, string attributeValue, int scope);
		[DllImport("__Internal")] private static extern void _incrementProfileAttribute(string attributeName, long attributeValue, int scope);
		[DllImport("__Internal")] private static extern void _decrementProfileAttribute(string attributeName, long value, int scope);
		[DllImport("__Internal")] private static extern void _setCustomerEmail(string email);
		[DllImport("__Internal")] private static extern void _setCustomerFirstName(string firstName);
		[DllImport("__Internal")] private static extern void _setCustomerLastName(string lastName);
		[DllImport("__Internal")] private static extern void _setCustomerFullName(string fullName);
		[DllImport("__Internal")] private static extern void _triggerInAppMessage(string triggerName, string attributes);
		[DllImport("__Internal")] private static extern void _dismissCurrentInAppMessage();
		[DllImport("__Internal")] private static extern uint _inAppMessageDismissButtonLocation();
		[DllImport("__Internal")] private static extern void _setInAppMessageDismissButtonLocation(uint location);
		[DllImport("__Internal")] private static extern bool _testModeEnabled();
		[DllImport("__Internal")] private static extern void _setTestModeEnabled(bool enabled);
		[DllImport("__Internal")] private static extern void _registerReceiveMessagingCallback(ReceiveMessagingDelegate callback);
		[DllImport("__Internal")] private static extern void _removeMessagingCallback ();
		[DllImport("__Internal")] private static extern void _setLocationMonitoringEnabled(bool enabled);
		[DllImport("__Internal")] private static extern string _getGeofencesToMonitor(double latitude, double longitude);
		[DllImport("__Internal")] private static extern void _triggerRegions(string regions, int regionEvent);
		[DllImport("__Internal")] private static extern void _registerReceiveLocationCallback(ReceiveLocationDelegate callback);
		[DllImport("__Internal")] private static extern void _removeLocationCallback();
		[DllImport("__Internal")] private static extern string _customerId();
		[DllImport("__Internal")] private static extern void _setCustomerId(string customerId);
		[DllImport("__Internal")] private static extern string _getIdentifier(string key);
		[DllImport("__Internal")] private static extern void _setIdentifier(string key, string value);
		[DllImport("__Internal")] private static extern void _setLocation(double latitude, double longitude);
		[DllImport("__Internal")] private static extern void _setStringOption(string key, string value);
		[DllImport("__Internal")] private static extern void _setBoolOption(string key, bool value);
		[DllImport("__Internal")] private static extern void _setLongOption(string key, long value);
		[DllImport("__Internal")] private static extern string _appKey();
		[DllImport("__Internal")] private static extern string _installId();
		[DllImport("__Internal")] private static extern string _libraryVersion();
		[DllImport("__Internal")] private static extern bool _isLoggingEnabled();
		[DllImport("__Internal")] private static extern void _setLoggingEnabled(bool loggingEnabled);
		[DllImport("__Internal")] private static extern string _pushToken();
		[DllImport("__Internal")] private static extern bool _isInAppAdIdParameterEnabled();
		[DllImport("__Internal")] private static extern void _setInAppAdIdParameterEnabled(bool appAdIdEnabled);

		private static double ConvertToDouble(object obj)
		{
			IConvertible convert = obj as IConvertible;
			return convert.ToDouble(null);
		}

		private static int ConvertToInt(object obj)
		{
			IConvertible convert = obj as IConvertible;
			return convert.ToInt32(null);
		}

		private static Dictionary<string, string> ConvertToDictionary(object obj)
		{
			Dictionary<string, string> convert = new Dictionary<string, string>();
			if (obj != null)
			{
				Dictionary<string, object> stringToObj = (Dictionary<string, object>) obj;
				foreach (KeyValuePair<string, object> kvp in stringToObj)
				{
					convert.Add(kvp.Key, kvp.Value.ToString());
				}
			}

			return convert;
		}

		private static string ConvertRegionInfoList(List<CircularRegionInfo> regions)
		{
			List<string> list = new List<string>();
			foreach (CircularRegionInfo info in regions)
			{
				list.Add(info.UniqueId);
			}

			return MiniJSON.jsonEncode(list.ToArray());
		}

		private static List<CircularRegionInfo> ConvertGeofencesList(List<object> geofences)
		{
			List<CircularRegionInfo> geofencesToReturn = new List<CircularRegionInfo>();
			foreach (object obj in geofences)
			{
				Dictionary<string, object> dict = (Dictionary<string, object>) obj;
				CircularRegionInfo info = new CircularRegionInfo();
				info.UniqueId = (string) dict["unique_id"];
				info.Latitude = ConvertToDouble(dict["latitude"]);
				info.Longitude = ConvertToDouble(dict["longitude"]);
				info.Radius = ConvertToInt(dict["radius"]);
				info.Name = (string) dict["name"];
				info.Type = (string) dict["type"];
				info.Attributes = ConvertToDictionary(dict["attributes"]);
				geofencesToReturn.Add(info);
			}
			return geofencesToReturn;
		}

		private static List<CircularRegionInfo> ConvertGeofencesListFromJson(string json)
		{
			List<object> geofences = MiniJSON.jsonDecode(json, true) as List<object>;
			return ConvertGeofencesList(geofences);
		}

		private delegate void ReceiveAnalyticsDelegate(string message);
		private delegate void ReceiveMessagingDelegate(string message);
		private delegate void ReceiveLocationDelegate(string message);

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
					{
						OnLocalyticsSessionWillOpen(willIsFirst, willIsUpgrade, willIsResume);
					}
					break;
				case "localyticsSessionDidOpen":
					var didOpen = (Hashtable)json["params"];
					bool didIsFirst = Boolean.Parse (didOpen["isFirst"].ToString ());
					bool didIsUpgrade = Boolean.Parse (didOpen["isUpgrade"].ToString ());
					bool didIsResume = Boolean.Parse (didOpen["isResume"].ToString ());
					if (OnLocalyticsSessionDidOpen != null)
					{
						OnLocalyticsSessionDidOpen(didIsFirst, didIsUpgrade, didIsResume);
					}
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
							attributesDictionary.Add ((string)key, attributes[key].ToString());
						}
					}
					long customerValueIncrease = 0;
					if (tagParams.ContainsKey("customerValueIncrease"))
					{
						customerValueIncrease = long.Parse(tagParams["customerValueIncrease"].ToString());
					}
					if (OnLocalyticsDidTagEvent != null)
					{
						OnLocalyticsDidTagEvent(eventName, attributesDictionary, customerValueIncrease);
					}

					break;
				case "localyticsSessionWillClose":
					if (OnLocalyticsSessionWillClose != null)
					{
						OnLocalyticsSessionWillClose();
					}
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
					{
						OnLocalyticsWillDisplayInAppMessage();
					}
					break;
				case "localyticsDidDisplayInAppMessage":
					if (OnLocalyticsDidDisplayInAppMessage != null)
					{
						OnLocalyticsDidDisplayInAppMessage();
					}
					break;
				case "localyticsWillDismissInAppMessage":
					if (OnLocalyticsWillDismissInAppMessage != null)
					{
						OnLocalyticsWillDismissInAppMessage();
					}
					break;
				case "localyticsDidDismissInAppMessage":
					if (OnLocalyticsDidDismissInAppMessage != null)
					{
						OnLocalyticsDidDismissInAppMessage();
					}
					break;
				}
			}
			catch (Exception ex)
			{
				Debug.LogError ("There was a problem decoding an analytics message from the Localytics plugin: " + ex.Message);
			}
		}

		[MonoPInvokeCallback(typeof(ReceiveLocationDelegate))]
		private static void ReceiveLocationMessage(string message)
		{
			try
			{
				Dictionary<string, object> json = MiniJSON.jsonDecode(message, true) as Dictionary<string, object>;
				string e = json["event"].ToString();
				switch(e)
				{
				case "localyticsDidTriggerRegions:withEvent":
					Dictionary<string, object> didTrigger = (Dictionary<string, object>) json["params"];
					List<object> regions = (List<object>) didTrigger["regions"];
					int eventInt = ConvertToInt(didTrigger["regionEvent"]);
					List<CircularRegionInfo> infoList = ConvertGeofencesList(regions);
					if (OnLocalyticsDidTriggerRegions != null)
					{
						OnLocalyticsDidTriggerRegions(infoList, (RegionEvent)eventInt);
					}
					break;
				case "localyticsDidUpdateMonitoredRegions:removeRegions":
					Dictionary<string, object> didUpdate = (Dictionary<string, object>) json["params"];
					List<object> added = (List<object>) didUpdate["addedRegions"];
					List<object> removed = (List<object>) didUpdate["removedRegions"];
					List<CircularRegionInfo> addedInfoList = ConvertGeofencesList(added);
					List<CircularRegionInfo> removedInfoList = ConvertGeofencesList(removed);
					if (OnLocalyticsDidUpdateMonitoredGeofences != null)
					{
						OnLocalyticsDidUpdateMonitoredGeofences(addedInfoList, removedInfoList);
					}
					break;
				case "localyticsDidUpdateLocation":
					Dictionary<string, object> updateLocation = (Dictionary<string, object>) json["params"];
					Dictionary<string, object> locationDict = (Dictionary<string, object>) updateLocation["location"];
					if (OnLocalyticsDidUpdateLocation != null)
					{
						OnLocalyticsDidUpdateLocation(locationDict);
					}
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
