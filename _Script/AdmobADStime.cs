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

    void Start()
    {
        color = new Color(1f, 1f, 1f);

        _rewardedAdUnitId = "ca-app-pub-9179569099191885/8513428768";

        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            LoadRewardedAd();
        }
        else
        {
            //Debug.Log("No Internet, skip init for now 인터넷 연결 X");
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
            });

    }


    public void showAdmobVideo()
    {
        //Debug.Log("상태보기 : " + rewardedAd);

        PlayerPrefs.SetInt("wait", 1);
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                closeTimeADS();
                Toast_obj.SetActive(true);
                Toast_txt.text = "잠자는 시간이 2시간 감소되었다.";
                StartCoroutine("ToastImgFadeOut");
                PlayerPrefs.SetInt("sleeptimeadsreward", 99);
                alarm_obj.SetActive(false);

                PlayerPrefs.SetInt("blad", 1);
                PlayerPrefs.SetInt("adrunout", 0);
            });
        }
        else
        {
            //GM.GetComponent<UnityADSMilk>().adYes();
            PlayerPrefs.SetInt("wait", 2);
            MilkToast();
            LoadRewardedAd();
        }

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
            Toast_txt.text = "아직 볼 수 없다." + "\n" + "나중에 시도해보자.";
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

}
