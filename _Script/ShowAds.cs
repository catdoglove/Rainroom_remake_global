using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowAds : MonoBehaviour
{
    public GameObject adWin_obj;
    

    public GameObject tvImg;
    public Sprite[] spr_adTV;


    System.DateTime now;
    System.DateTime lastDateTimenow;
    string lastTimem;

    public int ag, agb;

    public GameObject delBtn;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("updateSec1");
    }

    public void ActAd()
    {
        if (adWin_obj.activeSelf)
        {
            adWin_obj.SetActive(false);
        }
        else
        {
            adWin_obj.SetActive(true);
        }

    }


    void Adtime()
    {
        
        now = new System.DateTime(1980, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastTimem = PlayerPrefs.GetString("adtimes", now.ToString());
        Timechecker();
        System.DateTime lastDateTimem = System.DateTime.Parse(lastTimem);
        System.TimeSpan compareTimem = System.DateTime.Now - lastDateTimem;
        ag = (int)compareTimem.TotalMinutes;
        agb = (int)compareTimem.TotalSeconds;
        agb = agb - (agb / 60) * 60;
        agb = 59 - agb;
        ag = 4 - ag;
        if (ag < 0)
        {
            if (PlayerPrefs.GetInt("roomads",0)==0)
            {
                tvImg.GetComponent<Image>().sprite = spr_adTV[1];
                PlayerPrefs.SetInt("roomads", 1);
            }
            else
            {
                tvImg.GetComponent<Image>().sprite = spr_adTV[0];
                PlayerPrefs.SetInt("roomads", 0);
            }
            tvImg.GetComponent<Button>().interactable = true;
        }
        else
        {
            tvImg.GetComponent<Button>().interactable = false;
        }
    }

    public void AdReward()
    {
        adWin_obj.SetActive(false);
        tvImg.GetComponent<Image>().sprite = spr_adTV[0];
        tvImg.GetComponent<Button>().interactable = false;
        if (PlayerPrefs.GetInt("scene", 0) >= 2)
        {
            tvImg.GetComponent<Image>().sprite = spr_adTV[2];
        }
    }



    //
    IEnumerator updateSec1()
    {
        int a = 0;
        while (a == 0)
        {
            Adtime();
            
            yield return new WaitForSeconds(0.8f);
        }
    }

    public void ShowADOut()
    {
        PlayerPrefs.SetInt("outtimecut", 4);
    }


    void Timechecker()
    {
        if (PlayerPrefs.GetInt("scene", 0) == 2)
        {
            lastTimem = PlayerPrefs.GetString("adtimespark", now.ToString());
        }
        else if (PlayerPrefs.GetInt("scene", 0) == 3)
        {
            lastTimem = PlayerPrefs.GetString("adtimescity", now.ToString());
        }
        else if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            lastTimem = PlayerPrefs.GetString("adtimes", now.ToString());
        }
        else
        {
            lastTimem = PlayerPrefs.GetString("adtimes", now.ToString());
        }
    }
}
