using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Ump.Api;
using GoogleMobileAds.Api;
using UnityEngine.UI;

public class GDPRmessage : MonoBehaviour
{
    [SerializeField, Tooltip("Button to show the privacy options form.")]
    public Button _privacyButton;

    // Start is called before the first frame update
    void Start()
    {
        /* 테스트 호출용
        var debugSettings = new ConsentDebugSettings
        {
            // Geography appears as in EEA for debug devices.
            DebugGeography = DebugGeography.EEA,
            TestDeviceHashedIds =
      new List<string>
      {
            "3A85579BA9D246C54E9FAE47FA5D015C"
      }
        };
        */

        // Set tag for under age of consent.
        // Here false means users are not under age of consent.
        ConsentRequestParameters request = new ConsentRequestParameters
        {
            TagForUnderAgeOfConsent = false,
        };

        // Check the current consent information status.
        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }
    void OnConsentInfoUpdated(FormError consentError)
    {
        if (consentError != null)
        {
            // Handle the error.
            UnityEngine.Debug.LogError(consentError);
            return;
        }
        // If the error is null, the consent information state was updated.
        // You are now ready to check if a form is available.
        ConsentForm.LoadAndShowConsentFormIfRequired((FormError formError) =>
        {
            if (formError != null)
            {
                // Consent gathering failed.
                UnityEngine.Debug.LogError(consentError);
                return;
            }
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                // This callback is called once the MobileAds SDK is initialized.
            });

        });
    }
    public void ShowPrivacyOptionsForm()
    {
        Debug.Log("Showing privacy options form.");

        ConsentForm.ShowPrivacyOptionsForm((FormError showError) =>
        {
            if (showError != null)
            {
                Debug.LogError("Error showing privacy options form with error: " + showError.Message);
            }
            // Enable the privacy settings button.
            if (_privacyButton != null)
            {
                _privacyButton.interactable =
                    ConsentInformation.PrivacyOptionsRequirementStatus ==
                    PrivacyOptionsRequirementStatus.Required;
            }
        });
    }
}
