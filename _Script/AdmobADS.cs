using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api.Mediation.UnityAds;

public class AdmobADS : MonoBehaviour {
    
    //영상
    private RewardedAd rewardedAd, rewardedAdout;
    private string _rewardedAdUnitId;


    //보상형 전면 광고
    private string _GoOutADSid;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj; ////blackimg
    public Text adPop_txt;
    public Button cutTime_btn;

    System.DateTime now;
    System.DateTime lastDateTimenow;

    public GameObject GM;

    //  중요: 두 광고의 보상 획득 여부를 메인 스레드로 전달하기 위한 플래그 분리
    private bool isFirstAdRewardPending = false;
    private bool isSecondAdRewardPending = false;

    private void Awake()
    {
        GoogleMobileAds.Mediation.UnityAds.Api.UnityAds.SetConsentMetaData("gdpr.consent", true);
        GoogleMobileAds.Mediation.UnityAds.Api.UnityAds.SetConsentMetaData("privacy.consent", true);
    }


    // Use this for initialization 앱 ID
    void Start ()
    {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/9339319267";
        _GoOutADSid = "ca-app-pub-9179569099191885/7164867319";

        InitializeAds();

        if (PlayerPrefs.GetInt("outtimecut", 0) == 4 && PlayerPrefs.GetInt("scene", 0) == 0)
        {
            cutTime_btn.interactable = false;
        }

    }

    private void Update()
    {
        if (isFirstAdRewardPending)
        {
            isFirstAdRewardPending = false;
            ExecuteFirstAdReward(); // 메인 스레드에서 안전하게 실행!
        }

        if (isSecondAdRewardPending)
        {
            isSecondAdRewardPending = false;
            ExecuteSecondAdReward(); // 메인 스레드에서 안전하게 실행!
        }
    }

    public void InitializeAds()
    {

        if (Application.internetReachability != NetworkReachability.NotReachable) //인터넷연결된경우?
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                LoadRewardedAd();
                LoadRewardedAd2();
                // This callback is called once the MobileAds SDK is initialized.

                /*
                // initStatus 안에 어댑터 목록이 있어야 함
                Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
                foreach (var keyValuePair in map)
                {
                    string className = keyValuePair.Key;
                    AdapterStatus status = keyValuePair.Value;
                    Debug.Log($"어댑터: {className}, 상태: {status.InitializationState}");
                }
                */
            });

        }
        else
        {
            //Debug.Log("No Internet, skip init for now. 인터넷 연결 불가능");
        }
    }



    public void OnButtonClick()
    {
        MobileAds.OpenAdInspector((AdInspectorError error) =>
        {
            if (error != null)
                Debug.Log($"Ad Inspector 오류: {error.GetMessage()}");
            // Error will be set if there was an issue and the inspector was not displayed.
        });
    }

    public void LoadRewardedAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_rewardedAdUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(ad); //이벤트 등록
            });

    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log("광고");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {

            if (rewardedAd != null)
            {
                rewardedAd.Destroy();
                rewardedAd = null;
            }
            LoadRewardedAd();
            Debug.Log("광고 종료");

        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError(error);

            rewardedAd?.Destroy();
            rewardedAd = null;

            LoadRewardedAd();
        };
    }

    /*
    void giveMeReward()
    {
        GM.GetComponent<ShowAds>().AdReward();
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.SetInt("blad", 1);
        LoadRewardedAd();
    }
    */



    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "Number of Talk's already the max," + "\n" + "so you can't.";
        }
        else
        {
            if (rewardedAd != null && rewardedAd.CanShowAd())
            {
                //blackimg.SetActive(true);
                rewardedAd.Show((Reward reward) =>
                {
                    isFirstAdRewardPending = true; // Update()로 신호만 보냄
                });
            }
            else
            {
                Toast_obj.SetActive(true);
                adPop_txt.text = "Can't see it yet." + "\n" + "Try later.";
                //LoadRewardedAd();
            }
        }
    }

    // 메인 스레드에서 안전하게 실행될 첫 번째 보상 로직
    private void ExecuteFirstAdReward()
    {
        lastDateTimenow = System.DateTime.Now;
        int sceneIndex = PlayerPrefs.GetInt("scene", 0);

        if (sceneIndex == 2) PlayerPrefs.SetString("adtimespark", lastDateTimenow.ToString());
        else if (sceneIndex == 3) PlayerPrefs.SetString("adtimescity", lastDateTimenow.ToString());
        else PlayerPrefs.SetString("adtimes", lastDateTimenow.ToString());

        GM.GetComponent<ShowAds>().AdReward();
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.SetInt("blad", 1);
        PlayerPrefs.Save();
    }

    public void LoadRewardedAd2()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedAdout != null)
        {
            rewardedAdout.Destroy();
            rewardedAdout = null;
        }

        //Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedAd.Load(_GoOutADSid, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("Rewarded ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded ad loaded with response : " + ad.GetResponseInfo());

                rewardedAdout = ad;
                RegisterEventHandlers2(ad); //이벤트 등록
            });

    }







    /*
    public void LoadRewardedInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Destroy();
            rewardedInterstitialAd = null;
        }

        //Debug.Log("Loading the rewarded interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        RewardedInterstitialAd.Load(_GoOutADSid, adRequest,
            (RewardedInterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    //Debug.LogError("rewarded interstitial ad failed to load an ad " + "with error : " + error);
                    return;
                }

                //Debug.Log("Rewarded interstitial ad loaded with response : " + ad.GetResponseInfo());

                rewardedInterstitialAd = ad;
            });
    }
    */

    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedAdout != null && rewardedAdout.CanShowAd())
        {
            rewardedAdout.Show((Reward reward) =>
            {
                isSecondAdRewardPending = true; // Update()로 신호만 보냄
            });
        }
        
        else
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "Can't see it yet." + "\n" + "Try later.";
           // LoadRewardedAd2();
        }

    }
    // 메인 스레드에서 안전하게 실행될 두 번째 보상 로직 (UI 조작 포함)
    private void ExecuteSecondAdReward()
    {
        PlayerPrefs.SetInt("outtimecut", 4);
        PlayerPrefs.Save();

        cutTime_btn.interactable = false;
        Toast_obj.SetActive(true);
        adPop_txt.text = "Time needed to go out" + "\n" + "was reduced.";
    }

    private void RegisterEventHandlers2(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            //Debug.Log("광고");
        };

        ad.OnAdFullScreenContentClosed += () =>
        {
            if (rewardedAdout != null)
            {
                rewardedAdout.Destroy();
                rewardedAdout = null;
            }
            LoadRewardedAd2();
            Debug.Log("광고 종료");
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError(error);

            rewardedAdout?.Destroy();
            rewardedAdout = null;

            LoadRewardedAd2();
        };
    }

}
