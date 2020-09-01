using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial2 : MonoBehaviour
{
	public UnityEngine.UI.Button tagEvent;
    public UnityEngine.UI.InputField tagEventInput;
    public UnityEngine.UI.Button tagScreen;
    public UnityEngine.UI.InputField tagScreenInput;
    public UnityEngine.UI.Button tagInvited;
    public UnityEngine.UI.InputField tagInvitedInput;
    public UnityEngine.UI.Button tagPurchased;
    public UnityEngine.UI.InputField tagPurchasedInputName;
    public UnityEngine.UI.InputField tagPurchasedInputId;
    public UnityEngine.UI.InputField tagPurchasedInputType;
    public UnityEngine.UI.InputField tagPurchasedInputPrice;
    public UnityEngine.UI.Button dismissAlert;
    public RectTransform alert;

    // Start is called before the first frame update
    void Start()
    {
        dismissAlert.onClick.AddListener(() =>
        {
            alert.gameObject.SetActive(false);
        });

        tagEvent.onClick.AddListener(() =>
		{
            LocalyticsUnity.Localytics.TagEvent(tagEventInput.text.ToString());
            tagEventInput.text = "";
            alert.gameObject.SetActive(true);
        });

        tagScreen.onClick.AddListener(() =>
        {
            LocalyticsUnity.Localytics.TagScreen(tagScreenInput.text.ToString());
            tagScreenInput.text = "";
            alert.gameObject.SetActive(true);
        });

        tagInvited.onClick.AddListener(() =>
        {
            LocalyticsUnity.Localytics.TagInvited(tagInvitedInput.text.ToString(), null);
            tagInvitedInput.text = "";
            alert.gameObject.SetActive(true);
        });

        tagPurchased.onClick.AddListener(() =>
        {
            string itemName     = tagPurchasedInputName.text;
            string itemId       = tagPurchasedInputId.text;
            string itemType     = tagPurchasedInputType.text;
            long   itemPrice    = long.Parse(tagPurchasedInputPrice.text);

            LocalyticsUnity.Localytics.TagPurchased(itemName, itemId, itemType, itemPrice, null);

            tagPurchasedInputName.text  = "";
            tagPurchasedInputId.text    = "";
            tagPurchasedInputType.text  = "";
            tagPurchasedInputPrice.text = "";

            alert.gameObject.SetActive(true);
        });
    }
}
