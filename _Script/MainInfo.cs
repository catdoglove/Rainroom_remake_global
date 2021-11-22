using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour
{
    public GameObject infoWin_obj,infoHelp_obj;
    string str_Code;
    public Text txt_heart,txt_lv;

    //호감도
    public static int like;
    public Slider sld_like;
    public GameObject face,flower_obj, cat_obj;
    public Sprite[] spr_face;
    public Text txt_likeLv, txt_faceLv;
    public GameObject GM;

    // Start is called before the first frame update
    void Start()
    {
        str_Code = PlayerPrefs.GetString("code", "");
        SetIofo();
    }

    public void CloseInfo()
    {
        infoWin_obj.SetActive(false);
    }

    public void infoAct()
    {
        if (infoWin_obj.activeSelf)
        {
            infoWin_obj.SetActive(false);
        }
        else
        {

            if (PlayerPrefs.GetInt("scene", 0) == 0)
            {
                GM.GetComponent<MainMenuEvt>().AllClose();
            }
            SetIofo();
            infoWin_obj.SetActive(true);
            //txt_lv.text = "" + PlayerPrefs.GetInt("likelv", 0);
            txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);

            if (PlayerPrefs.GetInt("infoflower", 0) == 1)
            {
                flower_obj.SetActive(true);
            }
            if (PlayerPrefs.GetInt("catlove", 0) == 1)
            {
                cat_obj.SetActive(true);
            }
        }
    }


    //창을열때 초기화
    void SetIofo()
    {
        //sld_like.maxValue = PlayerPrefs.GetFloat("maxlike", 50);
        sld_like.value = PlayerPrefs.GetInt("likepoint", 0);
        if (PlayerPrefs.GetInt("likelv", 0) == 1)
        {
            sld_like.maxValue = 112;
            face.GetComponent<Image>().sprite = spr_face[1];
            txt_likeLv.text = "[다음 호감도 조건]"+"\n"+"꾸준한 대화 및 창문,책 Lv.4이상";
        }
        if (PlayerPrefs.GetInt("likelv", 0) == 2)
        {
            sld_like.maxValue = 224;
            face.GetComponent<Image>().sprite = spr_face[2];
            txt_likeLv.text = "[다음 호감도 조건]" + "\n" + "꾸준한 대화 및 창문,책 Lv.6이상";
        }
        if (PlayerPrefs.GetInt("likelv", 0) == 3)
        {
            sld_like.maxValue = 316;
            face.GetComponent<Image>().sprite = spr_face[3];
            txt_likeLv.text = "[다음 호감도 조건]" + "\n" + "꾸준한 대화 및 창문,책 Lv.8이상";
        }
        if (PlayerPrefs.GetInt("likelv", 0) == 4)
        {
            sld_like.maxValue = 360;
            face.GetComponent<Image>().sprite = spr_face[4];
            txt_likeLv.text = "..할 말이 있어";
        }
        if (PlayerPrefs.GetInt("likelv", 0) == 5)
        {
            sld_like.maxValue = 415;
            face.GetComponent<Image>().sprite = spr_face[5];
            txt_likeLv.text = "고마워 :)";
        }
        if (PlayerPrefs.GetInt("likelv", 0) >= 6)
        {
            sld_like.maxValue = 415;
            face.GetComponent<Image>().sprite = spr_face[5];
            txt_likeLv.text = "고마워 :)";
            txt_faceLv.text = "" + (PlayerPrefs.GetInt("likelv", 0) - 5);
        }

    }


    public void ActInfoHelp()
    {
        if (infoHelp_obj.activeSelf)
        {
            infoHelp_obj.SetActive(false);
        }
        else
        {
            infoHelp_obj.SetActive(true);
        }
    }

}
