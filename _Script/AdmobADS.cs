using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;

public class AdmobADS : MonoBehaviour {
    
    AdRequest request;

    //영상
    private RewardedAd rewardedAd;
    string adUnitIdvideo;


    //보상형 전면 광고
    private RewardedInterstitialAd rewardedInterstitialAd;

    int rewardCoin;
    Color color;
    public GameObject Toast_obj, blackimg;
    public Text adPop_txt;
    public Button cutTime_btn;

    System.DateTime now;
    System.DateTime lastDateTimenow;

    public GameObject GM;



    // Use this for initialization 앱 ID
    void Start () {

        MobileAds.Initialize(initStatus => { });
#if UNITY_ANDROID
        string appId = "ca-app-pub-9179569099191885~8249233951"; //테스트용ca-app-pub-3940256099942544~3347511713
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif


        if (PlayerPrefs.GetInt("outtimecut", 0) == 4 && PlayerPrefs.GetInt("scene", 0) == 0)
        {
            cutTime_btn.interactable = false;
        }
        color = new Color(1f, 1f, 1f);


        //보상형광고
#if UNITY_ANDROID
        adUnitIdvideo = "ca-app-pub-9179569099191885/4627868936"; // 테스트 ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
            adUnitIdvideo = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitIdvideo = "unexpected_platform";
#endif


        this.rewardedAd = new RewardedAd(adUnitIdvideo);

        // Called when the user should be rewarded for watching a video.
        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.rewardedAd.OnAdClosed += HandleRewardBasedVideoClosed;

        RequestRewardedVideo();

        // 보상형 전면
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/6551741269", request, adLoadCallback);

    }

    //terminating with uncaught exception of type Il2CppExceptionWrapper ㅇㅔ러 없앰
    private void OnDisable()
    {
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdClosed -= HandleRewardBasedVideoClosed;
    }

    
    //동영상
    private void RequestRewardedVideo()
    {
        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardedAd.LoadAd(request);
    }

    //시청보상
    public void HandleUserEarnedReward(object sender, Reward args)
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
    }

    //동영상닫음
    private void HandleRewardBasedVideoClosed(object sender, System.EventArgs args)
    {
        blackimg.SetActive(false);
        RequestRewardedVideo();
    }

    public void showAdmobVideo()
    {
        if (PlayerPrefs.GetInt("talk", 5) >= 5)
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "대화 횟수가 이미 최대값이라" + "\n" + "시청할 수 없다.";
        }
        else
        {
            if (this.rewardedAd.IsLoaded())
            {
                blackimg.SetActive(true);
                this.rewardedAd.Show();
            }
            else
            {
                Toast_obj.SetActive(true);
                adPop_txt.text = "아직 볼 수 없다." + "\n" + "나중에 시도해보자.";
            }
        }
    }
    

    /*
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
        interstitial.Destroy();
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }
    */

        //보상형 전면 광고
    private void adLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs arg2)
    {
        if (arg2 == null)
        {
            rewardedInterstitialAd = ad;
            rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresent;

        }
    }

    //보상형 전면 광고 보여주기
    public void ShowRewardedInterstitialAd()
    {
        if (rewardedInterstitialAd != null)
        {
            blackimg.SetActive(true);
            rewardedInterstitialAd.Show(userEarnedRewardCallback);
        }
    }

    private void userEarnedRewardCallback(Reward reward)
    {
        // TODO: Reward the user.
        PlayerPrefs.SetInt("outtimecut", 4);
        cutTime_btn.interactable = false;
        blackimg.SetActive(false);



        Toast_obj.SetActive(true);
        adPop_txt.text = "외출 대기 시간이" + "\n" + "줄어들었다.";
    }
    private void HandleAdFailedToPresent(object sender, AdErrorEventArgs args)
    {
        //MonoBehavior.print("Rewarded interstitial ad has failed to present.");
    }


    //방지
    public void closeBlackImg()
    {
        blackimg.SetActive(false);
    }
}
