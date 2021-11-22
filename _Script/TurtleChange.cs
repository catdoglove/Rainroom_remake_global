using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurtleChange : MonoBehaviour
{
    public GameObject turtleWin_obj, turtleWinYN_obj,rainToHeart_obj, heartToRain_obj;
    int i_change, have_r, have_h;
    public Text txt_r,txt_h;

    public GameObject turtlePop_obj;
    public Text txt_pop;

    string str_Code;

    // Start is called before the first frame update
    void Start()
    {
        str_Code = PlayerPrefs.GetString("code", "");
    }

    public void ActTurtle()
    {
        if (turtleWin_obj.activeSelf)
        {
            turtleWin_obj.SetActive(false);
        }
        else
        {
            SetTextHave();

            turtleWin_obj.SetActive(true);
        }
    }

    public void ChangeRH()
    {
        turtleWinYN_obj.SetActive(true);
        rainToHeart_obj.SetActive(true);
        heartToRain_obj.SetActive(false);
        i_change = 1;
    }
    public void ChangeHR()
    {
        turtleWinYN_obj.SetActive(true);
        heartToRain_obj.SetActive(true);
        rainToHeart_obj.SetActive(false);
        i_change = 2;
    }

    public void ChangeY()
    {
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (i_change == 1)
        {//물>하트
            if (have_r >= 2000)
            {
                have_r = have_r - 2000;
                have_h = have_h + 10;
                PlayerPrefs.SetInt(str_Code + "r",have_r);
                PlayerPrefs.SetInt(str_Code + "h", have_h);
                txt_pop.text = "교환되었습니다.";
                turtlePop_obj.SetActive(true);
                SetTextHave();
            }
            else
            {
                txt_pop.text = "수량이 부족합니다.";
                turtlePop_obj.SetActive(true);
                turtleWinYN_obj.SetActive(false);
            }
        }
        else if (i_change == 2)
        {//하트>물
            if (have_h >= 20)
            {
                have_h = have_h - 20;
                have_r = have_r + 1000;
                PlayerPrefs.SetInt(str_Code + "r", have_r);
                PlayerPrefs.SetInt(str_Code + "h", have_h);
                txt_pop.text = "교환되었습니다.";
                turtlePop_obj.SetActive(true);
                SetTextHave();
            }
            else
            {
                txt_pop.text = "수량이 부족합니다.";
                turtlePop_obj.SetActive(true);
                turtleWinYN_obj.SetActive(false);
            }
        }
        PlayerPrefs.Save();
    }

    public void ChangeN()
    {
        turtleWinYN_obj.SetActive(false);
    }

    public void SetTextHave()
    {
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        txt_r.text = "" + have_r;
        txt_h.text = "" + have_h;
    }

}
