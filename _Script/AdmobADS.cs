using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

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



    // Use this for initialization 앱 ID
    void Start () {

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            // This callback is called once the MobileAds SDK is initialized.
        });

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/9339319267";
        _GoOutADSid = "ca-app-pub-9179569099191885/1077685867";

        if (PlayerPrefs.GetInt("outtimecut", 0) == 4 && PlayerPrefs.GetInt("scene", 0) == 0)
        {
            cutTime_btn.interactable = false;
        }
        color = new Color(1f, 1f, 1f);


        LoadRewardedAd();
        LoadRewardedInterstitialAd();

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
            });

        RegisterEventHandlers(rewardedAd); //이벤트 등록
    }


    private void RegisterReloadHandler(RewardedAd ad)
    {
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            //Debug.Log("Rewarded Ad full screen content closed.");

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Rewarded ad failed to open full screen content " + "with error : " + error);

            // Reload the ad so that we can show another as soon as possible.
            LoadRewardedAd();
        };
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
            //blackimg.SetActive(false);
            LoadRewardedAd();
            //Debug.Log("광고닫기");
        };
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
            if (rewardedAd != null)
            {
                //blackimg.SetActive(true);
                rewardedAd.Show((Reward reward) =>
                {
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
                    GM.GetComponent<ShowAds>().AdReward();
                    PlayerPrefs.SetInt("talk", 5);
                    PlayerPrefs.SetInt("blad", 1);
                    LoadRewardedAd();
                });
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
        var adRequest = new AdRequest.Builder().Build();

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
        RegisterEventHandlers(rewardedInterstitialAd); //이벤트 등록
    }


    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        //Debug.Log("상태보기 : " + rewardedInterstitialAd);
        if (rewardedInterstitialAd != null)
        {
            rewardedInterstitialAd.Show((Reward reward) =>
            {
                //blackimg.SetActive(true);
                // TODO: Reward the user.
                PlayerPrefs.SetInt("outtimecut", 4);
                cutTime_btn.interactable = false;

                Toast_obj.SetActive(true);
                adPop_txt.text = "Time needed to go out" + "\n" + "was reduced.";

            });
        }
        else
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "Can't see it yet." + "\n" + "Try later.";
            LoadRewardedInterstitialAd();
        }

    }


    private void RegisterEventHandlers(RewardedInterstitialAd ad)
    {
        ad.OnAdPaid += (AdValue adValue) =>
        {

        };
        ad.OnAdImpressionRecorded += () =>
        {
            //Debug.Log("Interstitial ad recorded an impression.");
        };
        ad.OnAdClicked += () =>
        {
            //Debug.Log("Interstitial ad was clicked.");
        };
        ad.OnAdFullScreenContentOpened += () =>
        {
            //Debug.Log("Interstitial ad full screen content opened.");
        };
        ad.OnAdFullScreenContentClosed += () =>
        {
            //blackimg.SetActive(false);
            LoadRewardedInterstitialAd();

            //Debug.Log("Interstitial ad full screen content closed.");
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);
        };
    }










    private void RegisterReloadHandler(RewardedInterstitialAd ad)
    {
        ad.OnAdFullScreenContentClosed += (null);
        {
            //Debug.Log("Interstitial Ad full screen content closed.");

            LoadRewardedInterstitialAd();
        };
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            //Debug.LogError("Interstitial ad failed to open full screen content " + "with error : " + error);

            LoadRewardedInterstitialAd();
        };
    }



    //방지
    public void closeBlackImg()
    {
        //blackimg.SetActive(false);
    }
}
