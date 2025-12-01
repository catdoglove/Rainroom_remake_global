using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api.Mediation.IronSource;

public class AdmobADS : MonoBehaviour {
    
    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;


    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;
    private string _GoOutADSid;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj; ////blackimg
    public Text adPop_txt;
    public Button cutTime_btn;

    System.DateTime now;
    System.DateTime lastDateTimenow;

    public GameObject GM;
    bool rewardEarned = false;

    private void Awake()
    {
        GoogleMobileAds.Mediation.IronSource.Api.IronSource.SetMetaData("do_not_sell", "true");
    }


    // Use this for initialization 앱 ID
    void Start ()
    {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/9339319267";
        _GoOutADSid = "ca-app-pub-9179569099191885/1077685867";


        if (Application.internetReachability != NetworkReachability.NotReachable) //인터넷연결된경우?
        {
            // Initialize the Google Mobile Ads SDK.
            MobileAds.Initialize((InitializationStatus initStatus) =>
            {
                LoadRewardedAd();
                LoadRewardedInterstitialAd();
                // This callback is called once the MobileAds SDK is initialized.
            });

        }
        else
        {
            //Debug.Log("No Internet, skip init for now. 인터넷 연결 불가능");
        }

        if (PlayerPrefs.GetInt("outtimecut", 0) == 4 && PlayerPrefs.GetInt("scene", 0) == 0)
        {
            cutTime_btn.interactable = false;
        }


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
            if (rewardEarned)
            {
               // Debug.Log("광고닫기");
                giveMeReward();
                rewardEarned = false;
            }


        };
    }


    void giveMeReward()
    {
        GM.GetComponent<ShowAds>().AdReward();
        PlayerPrefs.SetInt("talk", 5);
        PlayerPrefs.SetInt("blad", 1);
        LoadRewardedAd();
    }




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
                    rewardEarned = true;
                    lastDateTimenow = System.DateTime.Now;
                    if (PlayerPrefs.GetInt("scene", 0) == 2)
                    {
                        PlayerPrefs.SetString("adtimespark", lastDateTimenow.ToString());
                    }
                    else if (PlayerPrefs.GetInt("scene", 0) == 3)
                    {
                        PlayerPrefs.SetString("adtimescity", lastDateTimenow.ToString());
                    }
                    else if (PlayerPrefs.GetInt("scene", 0) == 0)
                    {
                        PlayerPrefs.SetString("adtimes", lastDateTimenow.ToString());
                    }
                    else
                    {
                        PlayerPrefs.SetString("adtimes", lastDateTimenow.ToString());
                    }
                });
                PlayerPrefs.Save();
            }
            else
            {
                Toast_obj.SetActive(true);
                adPop_txt.text = "Can't see it yet." + "\n" + "Try later.";
                LoadRewardedAd();
            }
        }
    }











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


    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedInterstitialAd != null && rewardedInterstitialAd.CanShowAd())
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                //blackimg.SetActive(true);
                // TODO: Reward the user.
                PlayerPrefs.SetInt("outtimecut", 4);
                cutTime_btn.interactable = false;

                Toast_obj.SetActive(true);
                adPop_txt.text = "Time needed to go out" + "\n" + "was reduced.";
                LoadRewardedInterstitialAd();
            });
        }
        else
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "Can't see it yet." + "\n" + "Try later.";
            LoadRewardedInterstitialAd();
        }

    }

}
