#import "LocalyticsUnity.h"

// Converts C style string to NSString
NSString* LLCreateNSString (const char* string)
{
    if (string)
    {
        return [NSString stringWithUTF8String: string];
    }
    else
    {
        return [NSString stringWithUTF8String: ""];
    }
}

// Helper method to create C string copy
char* LLMakeStringCopy (const char* string)
{
    if (string == NULL)
    {
        return NULL;
    }
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

// Converts JSON array into NSArray
NSArray* LLMakeNSArray(const char* string)
{
    NSData *data = [LLCreateNSString(string) dataUsingEncoding:NSUTF8StringEncoding];
    NSError *e;
    NSArray *array = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&e];
    return array;
}

// Converts JSON dictionary into NSDictionary
NSDictionary* LLMakeNSDictionary(const char* string)
{
    NSData *data = [LLCreateNSString(string) dataUsingEncoding:NSUTF8StringEncoding];
    NSError *e;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&e];
    return dictionary;
}

LLCustomer* LLMakeLLCustomer(const char* string)
{
    NSDictionary *dict = LLMakeNSDictionary(string);
    if (dict)
    {
        return [LLCustomer customerWithBlock:^(LLCustomerBuilder *builder) {
            builder.customerId = dict[@"customer_id"];
            builder.firstName = dict[@"first_name"];
            builder.lastName = dict[@"last_name"];
            builder.fullName = dict[@"full_name"];
            builder.emailAddress = dict[@"email_address"];
        }];
    }
    return nil;
}

NSArray* LLMakeNSArrayFromRegions(NSArray<LLRegion *> *regions) {
    NSMutableArray *dictArray = [NSMutableArray new];
    for (LLRegion *region in regions) {
        if ([region isKindOfClass:[LLGeofence class]]) {
            LLGeofence *fence = (LLGeofence *) region;
            [dictArray addObject:@{@"unique_id": fence.region.identifier,
                                   @"latitude": @(fence.region.center.latitude),
                                   @"longitude": @(fence.region.center.longitude),
                                   @"radius": @(fence.region.radius),
                                   @"name": fence.name ?: [NSNull null],
                                   @"type": @"geofence",
                                   @"attributes": fence.attributes ?: [NSNull null]
                                   }];
        }
    }
    return [dictArray copy];
}

char* LLMakeJsonFromRegions(NSArray<LLRegion *> *regions) {
    NSArray *dictArray = LLMakeNSArrayFromRegions(regions);
    NSError *error;
    NSData *data = [NSJSONSerialization dataWithJSONObject:dictArray options:0 error:&error];
    NSString *jsonString = [[NSString alloc] initWithData:data encoding:NSUTF8StringEncoding];
    return LLMakeStringCopy([jsonString UTF8String]);
}

NSDictionary* LLMakeNSDictionaryFromLocation(CLLocation *location) {
    return @{@"latitude": @(location.coordinate.latitude),
             @"longitude": @(location.coordinate.longitude),
             @"altitude": @(location.altitude),
             @"horizontalAccuracy": @(location.horizontalAccuracy),
             @"verticalAccuracy": @(location.horizontalAccuracy),
             @"timestamp": @([location.timestamp timeIntervalSince1970])
             };
}

@implementation AnalyticsDelegate

static LocalyticsAnalyticsCallback _analyticsCallback;
static AnalyticsDelegate *_analyticsInstance;
+ (AnalyticsDelegate *)instance
{
    @synchronized(self)
    {
        if (!_analyticsInstance)
        {
            _analyticsInstance = [[AnalyticsDelegate alloc] init];
        }

        return _analyticsInstance;
    }
}

+ (LocalyticsAnalyticsCallback)callback
{
    return _analyticsCallback;
}

+ (void)setCallback:(LocalyticsAnalyticsCallback)value
{
    _analyticsCallback = value;
}

- (id)init {
    self = [super init];

    return self;
}

- (void)localyticsSessionWillOpen:(BOOL)isFirst isUpgrade:(BOOL)isUpgrade isResume:(BOOL)isResume
{
    if (_analyticsCallback != NULL)
    {
        NSMutableDictionary *params = [NSMutableDictionary dictionary];
        [params setValue:[NSNumber numberWithBool:isFirst] forKey:@"isFirst"];
        [params setValue:[NSNumber numberWithBool:isUpgrade] forKey:@"isUpgrade"];
        [params setValue:[NSNumber numberWithBool:isResume] forKey:@"isResume"];

        NSMutableDictionary *call = [NSMutableDictionary dictionary];
        [call setValue:params forKey:@"params"];
        [call setValue:@"localyticsSessionWillOpen" forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _analyticsCallback([result UTF8String]);
    }
}

- (void)localyticsSessionDidOpen:(BOOL)isFirst isUpgrade:(BOOL)isUpgrade isResume:(BOOL)isResume
{
    if (_analyticsCallback != NULL)
    {
        NSMutableDictionary *params = [NSMutableDictionary dictionary];
        [params setValue:[NSNumber numberWithBool:isFirst] forKey:@"isFirst"];
        [params setValue:[NSNumber numberWithBool:isUpgrade] forKey:@"isUpgrade"];
        [params setValue:[NSNumber numberWithBool:isResume] forKey:@"isResume"];

        NSMutableDictionary *call = [NSMutableDictionary dictionary];
        [call setValue:params forKey:@"params"];
        [call setValue:@"localyticsSessionDidOpen" forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _analyticsCallback([result UTF8String]);
    }
}

- (void)localyticsDidTagEvent:(NSString *)eventName
                   attributes:(NSDictionary *)attributes
        customerValueIncrease:(NSNumber *)customerValueIncrease
{
    if (_analyticsCallback != NULL)
    {
        NSMutableDictionary *params = [NSMutableDictionary dictionary];
        [params setValue:eventName forKey:@"eventName"];
        [params setValue:attributes forKey:@"attributes"];
        [params setValue:customerValueIncrease forKey:@"customerValueIncrease"];

        NSMutableDictionary *call = [NSMutableDictionary dictionary];
        [call setValue:params forKey:@"params"];
        [call setValue:@"localyticsDidTagEvent" forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _analyticsCallback([result UTF8String]);
    }
}

- (void)localyticsSessionWillClose
{
    if (_analyticsCallback != NULL)
    {
        NSDictionary *call = [NSDictionary dictionaryWithObject:@"localyticsSessionWillClose"
                                                         forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _analyticsCallback([result UTF8String]);
    }
}

@end

@implementation MessagingDelegate

static LocalyticsMessagingCallback _messagingCallback;
static MessagingDelegate *_messagingInstance;
+ (MessagingDelegate *)instance
{
    @synchronized(self)
    {
        if (!_messagingInstance)
        {
            _messagingInstance = [[MessagingDelegate alloc] init];
        }

        return _messagingInstance;
    }
}

+ (LocalyticsMessagingCallback)callback
{
    return _messagingCallback;
}

+ (void)setCallback:(LocalyticsMessagingCallback)value
{
    _messagingCallback = value;
}

- (id)init {
    self = [super init];

    return self;
}

- (void)localyticsWillDisplayInAppMessage
{
    if (_messagingCallback != NULL)
    {
        NSDictionary *call = [NSDictionary dictionaryWithObject:@"localyticsWillDisplayInAppMessage"
                                                         forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _messagingCallback([result UTF8String]);
    }
}

- (void)localyticsDidDisplayInAppMessage
{
    if (_messagingCallback != NULL)
    {
        NSDictionary *call = [NSDictionary dictionaryWithObject:@"localyticsDidDisplayInAppMessage"
                                                         forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _messagingCallback([result UTF8String]);
    }
}

- (void)localyticsWillDismissInAppMessage
{
    if (_messagingCallback != NULL)
    {
        NSDictionary *call = [NSDictionary dictionaryWithObject:@"localyticsWillDismissInAppMessage"
                                                         forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _messagingCallback([result UTF8String]);
    }
}

- (void)localyticsDidDismissInAppMessage
{
    if (_messagingCallback != NULL)
    {
        NSDictionary *call = [NSDictionary dictionaryWithObject:@"localyticsDidDismissInAppMessage"
                                                         forKey:@"event"];

        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _messagingCallback([result UTF8String]);
    }
}

@end

@implementation LocationDelegate

static LocalyticsLocationCallback _locationCallback;
static LocationDelegate *_locationInstance;
+ (LocationDelegate *)instance
{
    @synchronized(self)
    {
        if (!_locationInstance)
        {
            _locationInstance = [[LocationDelegate alloc] init];
        }
        
        return _locationInstance;
    }
}

+ (LocalyticsLocationCallback)callback
{
    return _locationCallback;
}

+ (void)setCallback:(LocalyticsLocationCallback)value
{
    _locationCallback = value;
}

- (id)init {
    self = [super init];
    
    return self;
}

- (void)localyticsDidTriggerRegions:(NSArray<LLRegion *> *)regions withEvent:(LLRegionEvent)event
{
    if (_locationCallback != NULL)
    {
        NSDictionary *params = @{@"regions": LLMakeNSArrayFromRegions(regions),
                                 @"regionEvent": @(event)};
        NSDictionary *call = @{@"event": @"localyticsDidTriggerRegions:withEvent",
                               @"params": params};
        
        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _locationCallback([result UTF8String]);
    }
}

- (void)localyticsDidUpdateMonitoredRegions:(NSArray<LLRegion *> *)addedRegions removeRegions:(NSArray<LLRegion *> *)removedRegions
{
    if (_locationCallback != NULL)
    {
        NSDictionary *params = @{@"addedRegions": LLMakeNSArrayFromRegions(addedRegions),
                                 @"removedRegions": LLMakeNSArrayFromRegions(removedRegions)};
        NSDictionary *call = @{@"event": @"localyticsDidUpdateMonitoredRegions:removeRegions",
                               @"params": params};
        
        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _locationCallback([result UTF8String]);
    }
}

- (void)localyticsDidUpdateLocation:(CLLocation *)location
{
    if (_locationCallback != NULL)
    {
        NSDictionary *params = @{@"location": LLMakeNSDictionaryFromLocation(location)};
        NSDictionary *call = @{@"event": @"localyticsDidUpdateLocation",
                               @"params": params};
        
        NSError *error;
        NSData *jsonData = [NSJSONSerialization dataWithJSONObject:call
                                                           options:0
                                                             error:&error];
        NSString *result = [[NSString alloc] initWithData:jsonData encoding:NSUTF8StringEncoding];
        _locationCallback([result UTF8String]);
    }
}

@end

extern "C"
{
    void _upload()
    {
        [Localytics upload];
    }

    void _openSession()
    {
        [Localytics openSession];
    }

    void _closeSession()
    {
        [Localytics closeSession];
    }

    void _tagEvent(const char* eventName, const char* attributes, long customerValueIncrease)
    {
        [Localytics tagEvent:LLCreateNSString(eventName) attributes:LLMakeNSDictionary(attributes) customerValueIncrease:[NSNumber numberWithLong:customerValueIncrease]];
    }

    void _tagPurchased(const char* itemName, const char* itemId, const char* itemType, long itemPrice, const char* attributes)
    {
        [Localytics tagPurchased:LLCreateNSString(itemName) itemId:LLCreateNSString(itemId) itemType:LLCreateNSString(itemType) itemPrice:[NSNumber numberWithLong:itemPrice] attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagAddedToCart(const char* itemName, const char* itemId, const char* itemType, long itemPrice, const char* attributes)
    {
        [Localytics tagAddedToCart:LLCreateNSString(itemName) itemId:LLCreateNSString(itemId) itemType:LLCreateNSString(itemType) itemPrice:[NSNumber numberWithLong:itemPrice] attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagStartedCheckout(long totalPrice, long itemCount, const char* attributes)
    {
        [Localytics tagStartedCheckout:[NSNumber numberWithLong:totalPrice] itemCount:[NSNumber numberWithLong:itemCount] attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagCompletedCheckout(long totalPrice, long itemCount, const char* attributes)
    {
        [Localytics tagCompletedCheckout:[NSNumber numberWithLong:totalPrice] itemCount:[NSNumber numberWithLong:itemCount] attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagContentViewed(const char* contentName, const char* contentId, const char* contentType, const char* attributes)
    {
        [Localytics tagContentViewed:LLCreateNSString(contentName) contentId:LLCreateNSString(contentId) contentType:LLCreateNSString(contentType) attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagSearched(const char* queryText, const char* contentType, long resultCount, const char* attributes)
    {
        [Localytics tagSearched:LLCreateNSString(queryText) contentType:LLCreateNSString(contentType) resultCount:[NSNumber numberWithLong:resultCount] attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagShared(const char* contentName, const char* contentId, const char* contentType, const char* methodName, const char* attributes)
    {
        [Localytics tagShared:LLCreateNSString(contentName) contentId:LLCreateNSString(contentId) contentType:LLCreateNSString(contentType) methodName:LLCreateNSString(methodName) attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagContentRated(const char* contentName, const char* contentId, const char* contentType, long rating, const char* attributes)
    {
        [Localytics tagContentRated:LLCreateNSString(contentName) contentId:LLCreateNSString(contentId) contentType:LLCreateNSString(contentType) rating:[NSNumber numberWithLong:rating] attributes:LLMakeNSDictionary(attributes)];
    }
    
    void _tagCustomerRegistered(const char* customer, const char* methodName, const char* attributes)
    {
        [Localytics tagCustomerRegistered:LLMakeLLCustomer(customer) methodName:LLCreateNSString(methodName) attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagCustomerLoggedIn(const char* customer, const char* methodName, const char* attributes)
    {
        [Localytics tagCustomerLoggedIn:LLMakeLLCustomer(customer) methodName:LLCreateNSString(methodName) attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagCustomerLoggedOut(const char* attributes)
    {
        [Localytics tagCustomerLoggedOut:LLMakeNSDictionary(attributes)];
    }

    void _tagInvited(const char* methodName, const char* attributes)
    {
        [Localytics tagInvited:LLCreateNSString(methodName) attributes:LLMakeNSDictionary(attributes)];
    }

    void _tagScreen(const char* screenName)
    {
        [Localytics tagScreen:LLCreateNSString(screenName)];
    }
    
    const char* _getCustomDimension(int dimension)
    {
        return LLMakeStringCopy([[Localytics valueForCustomDimension:dimension] UTF8String]);
    }
    
    void _setCustomDimension(int dimension, const char* value)
    {
        [Localytics setValue:LLCreateNSString(value) forCustomDimension:dimension];
    }
    
    bool _isOptedOut()
    {
        return [Localytics isOptedOut];
    }
    
    void _setOptedOut(bool optedOut)
    {
        [Localytics setOptedOut:optedOut];
    }
    
    void _registerReceiveAnalyticsCallback(LocalyticsAnalyticsCallback callback)
    {
        [AnalyticsDelegate setCallback:callback];
        [Localytics setAnalyticsDelegate:[AnalyticsDelegate instance]];
    }
    
    void _removeAnalyticsCallback()
    {
        [Localytics setAnalyticsDelegate:nil];
    }
    
    void _setProfileAttributeLong(const char* attributeName, long value, int scope)
    {
        [Localytics setValue:[NSNumber numberWithLong:value] forProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _setProfileAttributeLongArray(const char* attributeName, const char* values, int scope)
    {
        NSArray *stringArray = LLMakeNSArray(values);
        NSMutableArray *mutArray = [NSMutableArray new];
        for (NSString *val in stringArray)
        {
            [mutArray addObject:[NSNumber numberWithInteger:[val integerValue]]];
        }
        [Localytics setValue:[mutArray copy] forProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _setProfileAttributeString(const char* attributeName, const char* value, int scope)
    {
        [Localytics setValue:LLCreateNSString(value) forProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _setProfileAttributeStringArray(const char* attributeName, const char* values, int scope)
    {
        [Localytics setValue:LLMakeNSArray(values) forProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _deleteProfileAttribute(const char* attribute, int scope)
    {
        [Localytics deleteProfileAttribute:LLCreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _addProfileAttributesToSet(const char* attribute, const char* values, int scope)
    {
        [Localytics addValues:LLMakeNSArray(values) toSetForProfileAttribute:LLCreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _removeProfileAttributesFromSet(const char* attributeName, const char* values, int scope)
    {
        [Localytics removeValues:LLMakeNSArray(values) fromSetForProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _incrementProfileAttribute(const char* attributeName, long attributeValue, int scope)
    {
        [Localytics incrementValueBy:attributeValue forProfileAttribute:LLCreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _decrementProfileAttribute(const char* attribute, long value, int scope)
    {
        [Localytics decrementValueBy:value forProfileAttribute:LLCreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _setCustomerEmail(const char* email)
    {
        [Localytics setCustomerEmail:LLCreateNSString(email)];
    }
    
    void _setCustomerFirstName(const char* firstName)
    {
        [Localytics setCustomerFirstName:LLCreateNSString(firstName)];
    }
    
    void _setCustomerLastName(const char* lastName)
    {
        [Localytics setCustomerLastName:LLCreateNSString(lastName)];
    }
    
    void _setCustomerFullName(const char* fullName)
    {
        [Localytics setCustomerFullName:LLCreateNSString(fullName)];
    }
    
    void _triggerInAppMessage(const char* triggerName, const char* attributes)
    {
        [Localytics triggerInAppMessage:LLCreateNSString(triggerName) withAttributes:LLMakeNSDictionary(attributes)];
    }
    
    void _dismissCurrentInAppMessage()
    {
        [Localytics dismissCurrentInAppMessage];
    }
    
    void _setInAppMessageDismissButtonLocation(uint location)
    {
        [Localytics setInAppMessageDismissButtonLocation:(LLInAppMessageDismissButtonLocation)location];
    }
    
    uint _inAppMessageDismissButtonLocation()
    {
        return (uint)[Localytics inAppMessageDismissButtonLocation];
    }
    
    bool _testModeEnabled()
    {
        return [Localytics isTestModeEnabled];
    }
    
    void _setTestModeEnabled(bool enabled)
    {
        [Localytics setTestModeEnabled:enabled];
    }
    
    void _registerReceiveMessagingCallback(LocalyticsMessagingCallback callback)
    {
        [MessagingDelegate setCallback:callback];
        [Localytics setMessagingDelegate:[MessagingDelegate instance]];
    }
    
    void _removeMessagingCallback()
    {
        [Localytics setMessagingDelegate:nil];
    }
    
    void _setLocationMonitoringEnabled(bool enabled)
    {
        [Localytics setLocationMonitoringEnabled:enabled];
    }
    
    const char* _getGeofencesToMonitor(double latitude, double longitude)
    {
        CLLocationCoordinate2D coordinate = CLLocationCoordinate2DMake(latitude, longitude);
        NSArray<LLRegion *> *geofencesToMonitor = [Localytics geofencesToMonitor:coordinate];
        return LLMakeJsonFromRegions(geofencesToMonitor);
    }
    
    void _triggerRegions(const char* regions, int regionEvent)
    {
        NSArray *regionIds = LLMakeNSArray(regions);
        NSMutableArray *toTrigger = [NSMutableArray new];
        for (NSString *uniqueId in regionIds)
        {
            CLCircularRegion *circularRegion = [[CLCircularRegion alloc] initWithCenter:CLLocationCoordinate2DMake(0.0, 0.0)
                                                                                 radius:1
                                                                             identifier:uniqueId];
            [toTrigger addObject:circularRegion];
        }
        [Localytics triggerRegions:[toTrigger copy] withEvent:(LLRegionEvent)regionEvent];
    }
    
    void _registerReceiveLocationCallback(LocalyticsLocationCallback callback)
    {
        [LocationDelegate setCallback:callback];
        [Localytics setLocationDelegate:[LocationDelegate instance]];
    }
    
    void _removeLocationCallback()
    {
        [Localytics setLocationDelegate:nil];
    }
    
    const char* _customerId()
    {
        return LLMakeStringCopy([[Localytics customerId] UTF8String]);
    }
    
    void _setCustomerId(const char* customerId)
    {
        [Localytics setCustomerId:LLCreateNSString(customerId)];
    }
    
    const char* _getIdentifier(const char* identifier)
    {
        return LLMakeStringCopy([[Localytics valueForIdentifier:LLCreateNSString(identifier)] UTF8String]);
    }
    
    void _setIdentifier(const char* key, const char* value)
    {
        [Localytics setValue:LLCreateNSString(key) forIdentifier:LLCreateNSString(value)];
    }
    
    void _setLocation(double latitude, double longitude)
    {
        [Localytics setLocation:CLLocationCoordinate2DMake(latitude, longitude)];
    }
    
    void _setStringOption(const char* key, const char* value)
    {
        [Localytics setOptions:@{LLCreateNSString(key): LLCreateNSString(value)}];
    }
    
    void _setBoolOption(const char* key, bool value)
    {
        [Localytics setOptions:@{LLCreateNSString(key): [NSNumber numberWithBool:value]}];
    }
    
    void _setLongOption(const char* key, long value)
    {
        [Localytics setOptions:@{LLCreateNSString(key): [NSNumber numberWithLong:value]}];
    }

    const char* _appKey()
    {
        return LLMakeStringCopy([[Localytics appKey] UTF8String]);
    }

    const char* _installId()
    {
        return LLMakeStringCopy([[Localytics installId] UTF8String]);
    }

    const char* _libraryVersion()
    {
        return LLMakeStringCopy([[Localytics libraryVersion] UTF8String]);
    }

    bool _isLoggingEnabled()
    {
        return [Localytics isLoggingEnabled];
    }

    void _setLoggingEnabled(bool loggingEnabled)
    {
        [Localytics setLoggingEnabled:loggingEnabled];
    }

    const char* _pushToken()
    {
        return LLMakeStringCopy([[Localytics pushToken] UTF8String]);
    }

    bool _isInAppAdIdParameterEnabled()
    {
        return [Localytics isInAppAdIdParameterEnabled];
    }

    void _setInAppAdIdParameterEnabled(bool inAppAdIdEnabled)
    {
        [Localytics setInAppAdIdParameterEnabled:inAppAdIdEnabled];
    }
}
