using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdmobADStime : MonoBehaviour
{

    //영상
    private RewardedAd rewardedAd;
    private string _rewardedAdUnitId;



    Color color;
    public GameObject Toast_obj;
    public Text Toast_txt;

    public GameObject GM, timeWnd_obj, alarm_obj;


    private bool isRewardPending = false;

    void Start()
    {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8677080299";

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            LoadRewardedAd();
        }
        else
        {
            //Debug.Log("No Internet, skip init for now 인터넷 연결 X");
        }
    }
    private void Update()
    {
        if (isRewardPending)
        {
            isRewardPending = false;
            ExecuteReward();
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
                RegisterEventHandlers(ad);
            });

    }
    private void RegisterEventHandlers(RewardedAd ad)
    {
        ad.OnAdFullScreenContentClosed += () =>
        {
            LoadRewardedAd();
        };

        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            LoadRewardedAd();
        };
    }

    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                isRewardPending = true;
            });
        }
        else
        {
            PlayerPrefs.SetInt("wait", 2);
            MilkToast();
           // LoadRewardedAd();
        }

    }
    private void ExecuteReward()
    {
        closeTimeADS();
        Toast_obj.SetActive(true);
        Toast_txt.text = "Time has been reduced by 2 hours.";
        StartCoroutine("ToastImgFadeOut");
        PlayerPrefs.SetInt("sleeptimeadsreward", 99);
        alarm_obj.SetActive(false);

        PlayerPrefs.SetInt("blad", 1);
        PlayerPrefs.SetInt("adrunout", 0);
        PlayerPrefs.Save(); // Save() 추가 권장
    }


    public void openTimeADS()
    {
        timeWnd_obj.SetActive(true);
    }
    public void closeTimeADS()
    {
        timeWnd_obj.SetActive(false);
    }


    public void MilkToast()
    {
        if (PlayerPrefs.GetInt("wait", 0) == 2)
        {
            Toast_obj.SetActive(true);
            Toast_txt.text = "Can't see it yet." + "\n" + "Try later.";
            StartCoroutine("ToastImgFadeOut");
        }
    }



    IEnumerator ToastImgFadeOut()
    {
        if (PlayerPrefs.GetInt("setmilkadc", 0) == 1)
        {
            PlayerPrefs.SetInt("setmilkadc", 0);
        }

        color.a = Mathf.Lerp(0f, 1f, 1f);
        Toast_obj.GetComponent<Image>().color = color;
        Toast_obj.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        for (float i = 1f; i > 0f; i -= 0.05f)
        {
            color.a = Mathf.Lerp(0f, 1f, i);
            Toast_obj.GetComponent<Image>().color = color;
            yield return null;
        }
        Toast_obj.SetActive(false);

    }

    public void touchToastEvt()
    {
        Toast_obj.SetActive(false);
    }
    private void OnDestroy()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }
    }

}
