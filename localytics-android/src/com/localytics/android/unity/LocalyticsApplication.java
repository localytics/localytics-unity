package com.localytics.android.unity;

import android.app.Application;
import com.localytics.android.*;

public class LocalyticsApplication extends Application {
	@Override
	public void onCreate()
	{
		super.onCreate();
		
		registerActivityLifecycleCallbacks(new LocalyticsActivityLifecycleCallbacks(this));
	}
}
