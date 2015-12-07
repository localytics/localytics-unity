#import "LocalyticsUnity.h"

@implementation AnalyticsDelegate

static LocalyticsAnalyticsCallback _analyticsCallback;
static AnalyticsDelegate *_analyticsInstance;
+ (AnalyticsDelegate *)instance
{
    @synchronized(self)
    {
        if (!_analyticsInstance)
            _analyticsInstance = [[AnalyticsDelegate alloc] init];
        
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
            _messagingInstance = [[MessagingDelegate alloc] init];
        
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


// Converts C style string to NSString
NSString* CreateNSString (const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return [NSString stringWithUTF8String: ""];
}

// Helper method to create C string copy
char* MakeStringCopyLocalytics (const char* string)
{
    if (string == NULL)
        return NULL;
    
    char* res = (char*)malloc(strlen(string) + 1);
    strcpy(res, string);
    return res;
}

// Converts JSON array into NSArray
NSArray* MakeNSArray(const char* string)
{
    NSData *data = [CreateNSString(string) dataUsingEncoding:NSUTF8StringEncoding];
    NSError *e;
    NSArray *array = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&e];
    return array;
}

// Converts JSON dictionary into NSDictionary
NSDictionary* MakeNSDictionary(const char* string)
{
    NSData *data = [CreateNSString(string) dataUsingEncoding:NSUTF8StringEncoding];
    NSError *e;
    NSDictionary *dictionary = [NSJSONSerialization JSONObjectWithData:data options:NSJSONReadingMutableContainers error:&e];
    return dictionary;
}

extern "C"
{
    const char* _analyticsHost()
    {
        return MakeStringCopyLocalytics([[Localytics analyticsHost] UTF8String]);
    }
    
    void _setAnalyticsHost(const char* analyticsHost)
    {
        [Localytics setAnalyticsHost:CreateNSString(analyticsHost)];
    }
    
    const char* _appKey()
    {
        return MakeStringCopyLocalytics([[Localytics appKey] UTF8String]);
    }
    
    const char* _customerId()
    {
        return MakeStringCopyLocalytics([[Localytics customerId] UTF8String]);
    }
    
    void _setInAppMessageDismissButtonLocation(uint location)
    {
        [Localytics setInAppMessageDismissButtonLocation:(LLInAppMessageDismissButtonLocation)location];
    }
    
    uint _inAppMessageDismissButtonLocation()
    {
        return (uint)[Localytics inAppMessageDismissButtonLocation];
    }
    
    void _setCustomerId(const char* customerId)
    {
        [Localytics setCustomerId:CreateNSString(customerId)];
    }
    
    const char* _installId()
    {
        return MakeStringCopyLocalytics([[Localytics installId] UTF8String]);
    }
    
    bool _isCollectingAdvertisingIdentifier()
    {
        return [Localytics isCollectingAdvertisingIdentifier];
    }
    
    void _setCollectingAdvertisingIdentifier(bool collectingAdvertisingIdentifier)
    {
        [Localytics setCollectAdvertisingIdentifier:collectingAdvertisingIdentifier];
    }
    
    const char* _libraryVersion()
    {
        return MakeStringCopyLocalytics([[Localytics libraryVersion] UTF8String]);
    }
    
    bool _isLoggingEnabled()
    {
        return [Localytics isLoggingEnabled];
    }
    
    void _setLoggingEnabled(bool loggingEnabled)
    {
        [Localytics setLoggingEnabled:loggingEnabled];
    }
    
    const char* _messagingHost()
    {
        return MakeStringCopyLocalytics([[Localytics messagingHost] UTF8String]);
    }
    
    void _setMessagingHost(const char* messagingHost)
    {
        [Localytics setMessagingHost:CreateNSString(messagingHost)];
    }
    
    bool _isOptedOut()
    {
        return [Localytics isOptedOut];
    }
    
    void _setOptedOut(bool optedOut)
    {
        [Localytics setOptedOut:optedOut];
    }
    
    const char* _profilesHost()
    {
        return MakeStringCopyLocalytics([[Localytics profilesHost] UTF8String]);
    }
    
    void _setProfilesHost(const char* profilesHost)
    {
        [Localytics setProfilesHost:CreateNSString(profilesHost)];
    }
    
    const char* _pushToken()
    {
        return MakeStringCopyLocalytics([[Localytics pushToken] UTF8String]);
    }
    
    bool _testModeEnabled()
    {
        return [Localytics isTestModeEnabled];
    }
    
    void _setTestModeEnabled(bool enabled)
    {
        [Localytics setTestModeEnabled:enabled];
    }
    
    double _sessionTimeoutInterval()
    {
        return [Localytics sessionTimeoutInterval];
    }
    
    void _setSessionTimeoutInterval(double timeoutInterval)
    {
        [Localytics setSessionTimeoutInterval:timeoutInterval];
    }
    
    void _addProfileAttributesToSet(const char* attribute, const char* values, int scope)
    {
        [Localytics addValues:MakeNSArray(values) toSetForProfileAttribute:CreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _closeSession()
    {
        [Localytics closeSession];
    }
    
    void _decrementProfileAttribute(const char* attribute, long value, int scope)
    {
        [Localytics decrementValueBy:value forProfileAttribute:CreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _deleteProfileAttribute(const char* attribute, int scope)
    {
        [Localytics deleteProfileAttribute:CreateNSString(attribute) withScope:(LLProfileScope)scope];
    }
    
    void _setCustomerEmail(const char* email)
    {
        [Localytics setCustomerEmail:CreateNSString(email)];
    }
    
    void _setCustomerFirstName(const char* firstName)
    {
        [Localytics setCustomerFirstName:CreateNSString(firstName)];
    }
    
    void _setCustomerLastName(const char* lastName)
    {
        [Localytics setCustomerLastName:CreateNSString(lastName)];
    }
    
    void _setCustomerFullName(const char* fullName)
    {
        [Localytics setCustomerFullName:CreateNSString(fullName)];
    }
    
    void _dismissCurrentInAppMessage()
    {
        [Localytics dismissCurrentInAppMessage];
    }
    
    const char* _getCustomDimension(int dimension)
    {
        return MakeStringCopyLocalytics([[Localytics valueForCustomDimension:dimension] UTF8String]);
    }
    
    const char* _getIdentifier(const char* identifier)
    {
        return MakeStringCopyLocalytics([[Localytics valueForIdentifier:CreateNSString(identifier)] UTF8String]);
    }
    
    void _incrementProfileAttribute(const char* attributeName, long attributeValue, int scope)
    {
        [Localytics incrementValueBy:attributeValue forProfileAttribute:CreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _removeProfileAttributesFromSet(const char* attributeName, const char* values, int scope)
    {
        [Localytics removeValues:MakeNSArray(values) fromSetForProfileAttribute:CreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _setCustomDimension(int dimension, const char* value)
    {
        [Localytics setValue:CreateNSString(value) forCustomDimension:dimension];
    }
    
    void _setIdentifier(const char* key, const char* value)
    {
        [Localytics setValue:CreateNSString(key) forIdentifier:CreateNSString(value)];
    }
    
    void _setLocation(double latitude, double longitude)
    {
        [Localytics setLocation:CLLocationCoordinate2DMake(latitude, longitude)];
    }
    
    void _setProfileAttribute(const char* attributeName, const char* values, int scope)
    {
        [Localytics setValue:MakeNSArray(values) forProfileAttribute:CreateNSString(attributeName) withScope:(LLProfileScope)scope];
    }
    
    void _tagEvent(const char* eventName, const char* attributes, long customerValueIncrease)
    {
        [Localytics tagEvent:CreateNSString(eventName) attributes:MakeNSDictionary(attributes) customerValueIncrease:[NSNumber numberWithLong:customerValueIncrease]];
    }
    
    void _tagScreen(const char* screenName)
    {
        [Localytics tagScreen:CreateNSString(screenName)];
    }
    
    void _triggerInAppMessage(const char* triggerName, const char* attributes)
    {
        [Localytics triggerInAppMessage:CreateNSString(triggerName) withAttributes:MakeNSDictionary(attributes)];
    }
    
    void _upload()
    {
        [Localytics upload];
    }
    
    void _openSession()
    {
        [Localytics openSession];
    }
    
    void _registerReceiveAnalyticsCallback(LocalyticsAnalyticsCallback callback)
    {
        [AnalyticsDelegate setCallback:callback];
        
        [Localytics addAnalyticsDelegate:[AnalyticsDelegate instance]];
    }
    
    void _removeAnalyticsCallback()
    {
        [Localytics removeAnalyticsDelegate:[AnalyticsDelegate instance]];
    }
    
    void _registerReceiveMessagingCallback(LocalyticsMessagingCallback callback)
    {
        [MessagingDelegate setCallback:callback];
        
        [Localytics addMessagingDelegate:[MessagingDelegate instance]];
    }
    
    void _removeMessagingCallback()
    {
        [Localytics removeMessagingDelegate:[MessagingDelegate instance]];
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