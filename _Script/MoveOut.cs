using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MoveOut : MonoBehaviour
{
    string str_Code;
    public GameObject toast;
    public Text toastTxt;


    public Text txt_Popup, txt_timePopup ;
    public GameObject shopPopup_obj,goOutYN_obj, timerPop_obj, timerPopClock_obj;
    // Start is called before the first frame update
    void Start()
    {

        str_Code = PlayerPrefs.GetString("code", "");
        
    }
    

    public void GoOutCity()
    {
        int h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (h >= 100)
        {
            GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(false);
            h = h - 100;
            PlayerPrefs.SetInt(str_Code + "h", h);
            PlayerPrefs.SetInt("gocitysuccess", 1);
            PlayerPrefs.SetString("outtime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("scene", 3);
            SceneManager.LoadSceneAsync("Load");
        }
        else
        {
            toast.SetActive(true);
            toastTxt.text = "There is a shortage of heart.";
        }
    }

    public void GoOutPark()
    {
        int h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (h >= 100)
        {
            GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(false);
            h = h - 100;
            PlayerPrefs.SetInt(str_Code + "h", h);
            PlayerPrefs.SetString("outtime", System.DateTime.Now.ToString());
            PlayerPrefs.SetInt("scene", 2);
            SceneManager.LoadSceneAsync("Load");
        }
        else
        {
            toast.SetActive(true);
            toastTxt.text = "There is a shortage of heart.";
        }
    }

    public void GoBack()
    {
        SceneManager.LoadSceneAsync("Load");
        PlayerPrefs.SetInt("scene", 0);
    }
    public void GoOutYN()
    {
        
        System.DateTime now;
        string lastTime;
        int ac, acb;
        //외출시간
        now = System.DateTime.Now.AddHours(-1);
        lastTime = PlayerPrefs.GetString("outtime", now.ToString());
        try
        {
            System.DateTime lastDateTimem2 = System.DateTime.Parse(lastTime);
        }
        catch (System.Exception)
        {
            lastTime = System.DateTime.Now.AddHours(-1).ToString();
        }
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        ac = (int)compareTime.TotalMinutes;
        acb = (int)compareTime.TotalSeconds;
        acb = acb - (acb / 60) * 60;
        acb = 59 - acb;
        ac = 14 - ac;
        
        if (PlayerPrefs.GetInt("outtimecut", 0) == 4)
        {
            ac = ac - 10;
        }
        if (ac<0)
        {
            if (PlayerPrefs.GetInt("likelv", 0) >= 5)
            {
                goOutYN_obj.SetActive(true);
                GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(false);

            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "They don't seem to want to go out." + "\n" + "Maybe can go out a little more familiar.";
                GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(false);
            }
        }
        else
        {
            timerPop_obj.SetActive(true);
            timerPopClock_obj.SetActive(true);
            txt_timePopup.text = "It hasn't been long since it came back." + "\n" + "They can go out when the umbrella dries.";
        }
    }

    public void CloseGoOutYN()
    {
        goOutYN_obj.SetActive(false);
    }

}
