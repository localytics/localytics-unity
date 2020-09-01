using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial1 : MonoBehaviour
{
	public UnityEngine.UI.Text ios_key;
	public UnityEngine.UI.Text android_key;
    public UnityEngine.UI.Text android_push;
    public UnityEngine.UI.Button android_converter;
    public UnityEngine.UI.Text android_manifest;
    public UnityEngine.UI.Text ios_ready;
    public UnityEngine.UI.Text android_ready;

    const int MAX_COUNT = 10;
	int counter = 0;

    private void Start()
    {
        android_converter.onClick.AddListener(() => {
            Application.OpenURL("https://dandar3.github.io/android/google-services-json-to-xml.html");
        });
    }

    // Update is called once per frame
    void Update()
    {
        // Only update every 10 frames or so
		if (counter == MAX_COUNT)
		{
			counter = 0;

            var iosReader = new LocalyticsUnity.LocalyticsJsonFileReader("localytics.options.ios");
            var androidReader = new LocalyticsUnity.LocalyticsJsonFileReader("localytics.options.android");

            var iosOptions = iosReader.Read();
            var androidOptions = androidReader.Read();

            if (!iosOptions.ContainsKey("app_key"))
            {
                ios_key.color = new Color(0.5f, 0, 0);
                ios_key.text = "iOS: No app key";
                ios_ready.color = new Color(0.5f, 0f, 0);
                ios_ready.text = "iOS: NOT READY";
            }
            else if (iosOptions["app_key"].ToString().Length == 0)
            {
                ios_key.color = new Color(0.5f, 0, 0);
                ios_key.text = "iOS: Empty app key";
                ios_ready.color = new Color(0.5f, 0f, 0);
                ios_ready.text = "iOS: NOT READY";
            }
            else if (iosOptions["app_key"].ToString().ToLower().Contains("localytics"))
            {
                ios_key.color = new Color(0.5f, 0, 0);
                ios_key.text = "iOS: Enter app key";
                ios_ready.color = new Color(0.5f, 0f, 0);
                ios_ready.text = "iOS: NOT READY";
            }
            else
            {
                ios_key.color = new Color(0f, 0.5f, 0);
                ios_key.text = "iOS: " + iosOptions["app_key"].ToString().Substring(0, 10) + "..."; ;
                ios_ready.color = new Color(0f, 0.5f, 0);
                ios_ready.text = "iOS: READY";
            }

            if (!androidOptions.ContainsKey("ll_app_key"))
            {
                android_key.color = new Color(0.5f, 0, 0);
                android_key.text = "Android: No app key";
            }
            else if (androidOptions["ll_app_key"].ToString().Length == 0)
            {
                android_key.color = new Color(0.5f, 0, 0);
                android_key.text = "Android: Empty app key";
            }
            else if (androidOptions["ll_app_key"].ToString().ToLower().Contains("localytics"))
            {
                android_key.color = new Color(0.5f, 0, 0);
                android_key.text = "Android: Enter app key";
            }
            else
            {
                android_key.text = "Android: " + androidOptions["ll_app_key"].ToString().Substring(0, 10) + "...";
                android_key.color = new Color(0f, 0.5f, 0);
            }

            if (this.ValueStringsXMLSensible())
            {
                android_push.color = new Color(0f, 0.5f, 0);
                android_push.text = "Android: firebase configured";
            }
            else
            {
                android_push.color = new Color(0.5f, 0.25f, 0);
                android_push.text = "Android: firebase not configured";
            }

            if (this.AndroidManifestContainsAppKey())
            {
                android_manifest.color = new Color(0f, 0.5f, 0);
                android_manifest.text = "Android: manifest configured";
                android_ready.color = new Color(0f, 0.5f, 0);
                android_ready.text = "Android: READY";
            }
            else
            {
                android_manifest.color = new Color(0.5f, 0f, 0);
                android_manifest.text = "Android: manifest unconfigured";
                android_ready.color = new Color(0.5f, 0f, 0);
                android_ready.text = "Android: NOT READY";
            }
        }

		counter++;
    }

    public bool ValueStringsXMLSensible()
    {
        if (!System.IO.File.Exists("Assets/Plugins/Android/res/values/strings.xml"))
            return false;

        var strings = System.IO.File.ReadAllText("Assets/Plugins/Android/res/values/strings.xml");

        if (strings.Contains("get this from"))
            return false;

        return true;
    }

    public bool AndroidManifestContainsAppKey()
    {
        if (!System.IO.File.Exists("Assets/Plugins/Android/AndroidManifest.xml"))
            return false;

        var manifest = System.IO.File.ReadAllText("Assets/Plugins/Android/AndroidManifest.xml");

        if (!manifest.Contains("ll_app_key"))
            return false;

        if (manifest.Contains("<LOCALYTICS_API_KEY>"))
            return false;

        return true;
    }
}
