using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;


public class AdmobADS : MonoBehaviour {
    
    //배너
    private BannerView bannerView;
    AdRequest request;

    //영상
    private RewardBasedVideoAd rewardBasedVideo;
    string adUnitIdvideo;

    //전면
    private InterstitialAd interstitial;

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


        if (PlayerPrefs.GetInt("outtimecut", 0) == 4 && PlayerPrefs.GetInt("scene", 0) == 0)
        {

            cutTime_btn.interactable = false;
        }
        color = new Color(1f, 1f, 1f);

#if UNITY_ANDROID
        string appId = "ca-app-pub-9179569099191885~8249233951"; //테스트용ca-app-pub-3940256099942544~3347511713
#elif UNITY_IPHONE
            string appId = "ca-app-pub-3940256099942544~1458002511";
#else
        string appId = "unexpected_platform";
#endif
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

        //this.RequestBanner();


        this.rewardBasedVideo = RewardBasedVideoAd.Instance;
        
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;

        this.RequestRewardedVideo();
        this.RequestInterstitial();

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        RewardedInterstitialAd.LoadAd("ca-app-pub-9179569099191885/6551741269", request, adLoadCallback);

    }

    //terminating with uncaught exception of type Il2CppExceptionWrapper ㅇㅔ러 없앰
    private void OnDisable()
    {
        rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed;
    }

    //배너
    private void RequestBanner()
    {
#if UNITY_ANDROID
            string adUnitId = "ca-app-pub-9179569099191885~8249233951";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);
    }


    
    //동영상
    private void RequestRewardedVideo()
    {

#if UNITY_ANDROID
            adUnitIdvideo = "ca-app-pub-9179569099191885/4627868936"; // 테스트 ca-app-pub-3940256099942544/5224354917
#elif UNITY_IPHONE
            adUnitIdvideo = "ca-app-pub-3940256099942544/1712485313";
#else
        adUnitIdvideo = "unexpected_platform";
#endif
        // Create an empty ad request.
        request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
        this.rewardBasedVideo.LoadAd(request, adUnitIdvideo);
    }

    //시청보상
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
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
        if (rewardBasedVideo.IsLoaded())
        {
            blackimg.SetActive(true);
            rewardBasedVideo.Show();
        }
        else
        {
            Toast_obj.SetActive(true);
            adPop_txt.text = "아직 볼 수 없다." + "\n" + " 나중에 시도해보자.";
        }
    }
    


    public void callBanner()
    {
        this.RequestBanner();
    }

    

    //전면광고
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9179569099191885/4721052486"; // 테스트ca-app-pub-3940256099942544/1033173712
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        
    }

    public void ShowAdInterstitial()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            PlayerPrefs.SetInt("outtimecut", 4);
            cutTime_btn.interactable = false;
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
    private void adLoadCallback(RewardedInterstitialAd ad, string error)
    {
        if (error == null)
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
