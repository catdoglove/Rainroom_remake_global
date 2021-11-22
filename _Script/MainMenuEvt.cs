using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuEvt : MonoBehaviour
{
    public GameObject menu_obj,option_obj, menuOut_obj, menuIn_obj, backWnd;
    public GameObject muteImg_obj, muteBGImg_obj;
    public Sprite[] mute_spr;

    public GameObject GM,GMD;

    public GameObject help_obj, helpImg_obj, helpOut_obj, helpCity_obj, helpR_obj, helpO_obj, helpL_obj;
    public Sprite[] spr_help, spr_helpOut;
    int help_i;

    //대화속도
    public Text speed_txt;
    

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("firstroom", 0) == 0)
        {
            OpenHelp();
            PlayerPrefs.SetInt("firstroom", 2);
        }
        
        PlayerPrefs.SetInt("scene", 0);

        //대화속도
        switch (PlayerPrefs.GetFloat("talkspeed", 0.05f))
        {
            case 0.05f:
                speed_txt.text = "대화속도 보통";
                break;
            case 0.03f:
                speed_txt.text = "대화속도 빠름";
                break;
            case 0.07f:
                speed_txt.text = "대화속도 느림";
                break;
        }

        //음소거
        if (PlayerPrefs.GetInt("soundBGmute", 0) == 1)
        {
            muteBGImg_obj.GetComponent<Image>().sprite = mute_spr[1];
        }
        else
        {
            muteBGImg_obj.GetComponent<Image>().sprite = mute_spr[0];
        }


        if (PlayerPrefs.GetInt("soundmute", 0) == 1)
        {
            muteImg_obj.GetComponent<Image>().sprite = mute_spr[1];
        }
        else
        {
            muteImg_obj.GetComponent<Image>().sprite = mute_spr[0];
        }

    }
    



    //메뉴창 닫기 열기
    public void ActMenu()
    {
        if(PlayerPrefs.GetInt("scene", 0) == 0)
        {
            if (GM == null)
            {
                GM = GameObject.FindGameObjectWithTag("GameObject");
            }

            menu_obj = menuIn_obj;
        }
        else
        {
            menu_obj = menuOut_obj;
        }

        if (menu_obj.activeSelf)
        {
            menu_obj.SetActive(false);
        }
        else
        {

            if (PlayerPrefs.GetInt("scene", 0) == 0)
            {
                AllClose();
            }
            menu_obj.SetActive(true);
            option_obj.SetActive(false);
        }
    }


    public void CloseOption()
    {
        option_obj.SetActive(false);
    }
    //옵션창 닫기 열기
    public void ActOption()
    {
        
        if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            if (GM == null)
            {
                GM = GameObject.FindGameObjectWithTag("GameObject");
            }
        }

        if (option_obj.activeSelf)
        {
            option_obj.SetActive(false);
        }
        else
        {
            if (PlayerPrefs.GetInt("scene", 0) == 0)
            {
                AllClose();
            }
            option_obj.SetActive(true);
            backWnd.SetActive(false);
        }
    }

    //상점창
    public void GetActShop()
    {
        if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            AllClose();
        }
        GM.GetComponent<MainShop>().shopAct();
    }

    //구독
    public void GetActNews()
    {
        if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            AllClose();
        }
        GM.GetComponent<MainTime>().gudocOpen();
    }

    //전단창
    public void GetActBeadal()
    {
        if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            AllClose();
        }
        GM.GetComponent<MainTime>().ActBeadal();
    }


    //돌아가창
    public void backWndOpen()
    {
        backWnd.SetActive(true);
        option_obj.SetActive(false);
    }

    public void backWndClose()
    {
        backWnd.SetActive(false);
    }

    public void GoBack()
    {
        //SceneManager.LoadSceneAsync("Main");
        SceneManager.LoadSceneAsync("Load");
        PlayerPrefs.SetInt("scene", 0);
        menu_obj.SetActive(false);
        backWnd.SetActive(false);
    }

    public void MuteBG()
    {
        if (GM == null)
        {
            GM = GameObject.Find("GameObject");
        }
        
            if (PlayerPrefs.GetInt("soundBGmute", 0) == 1)
            {
                PlayerPrefs.SetInt("soundBGmute", 0);
                GM.GetComponent<SoundEvt>().BGMute();
                muteBGImg_obj.GetComponent<Image>().sprite = mute_spr[0];
            }
            else
            {
                PlayerPrefs.SetInt("soundBGmute", 1);
                GM.GetComponent<SoundEvt>().BGMute();
                muteBGImg_obj.GetComponent<Image>().sprite = mute_spr[1];
            }
        PlayerPrefs.Save();
    }
    public void MuteSE()
    {
        if (GM == null)
        {
            GM = GameObject.Find("GameObject");
        }
        
            if (PlayerPrefs.GetInt("soundmute", 0) == 1)
            {
                GM.GetComponent<SoundEvt>().soundMute();
                muteImg_obj.GetComponent<Image>().sprite = mute_spr[0];
            }
            else
            {
                GM.GetComponent<SoundEvt>().soundMute();
                muteImg_obj.GetComponent<Image>().sprite = mute_spr[1];
            }
        PlayerPrefs.Save();
    }


    public void AllClose()
    {
        GMD.GetComponent<MainInfo>().infoWin_obj.SetActive(false);
        option_obj.SetActive(false);
        GM.GetComponent<MainShop>().shopWin_obj.SetActive(false);
        GM.GetComponent<MainShop>().outItem_obj.SetActive(false);
        GM.GetComponent<MainShop>().shopWinYN_obj.SetActive(false);
    }

    /*
    public void changeToastTxt()
    {
        if (endck == 1)
        {
            endTxt.text = "종료메세지A";
            AndroidDialogAndToastBinding.instance.toastShort("기본인사로 변경되었습니다.");
            endck = 2;
            Movebtn.opOp = 0;
        }
        else if (endck == 2)
        {
            endTxt.text = "종료메세지B";
            AndroidDialogAndToastBinding.instance.toastShort("호감도에 따른 인사로 변경되었습니다.");
            endck = 1;
            Movebtn.opOp = 0;
        }
        PlayerPrefs.SetInt("endck", endck);
    }
    */

    //도움말
    public void OpenHelp()
    {
        if (PlayerPrefs.GetInt("scene", 0)==2)
        {
            helpOut_obj.SetActive(true);
            //helpOut_obj.GetComponent<Image>().sprite = spr_helpOut[0];
        }
        else if (PlayerPrefs.GetInt("scene", 2) == 3)
        {
            helpCity_obj.SetActive(true);
            //helpOut_obj.GetComponent<Image>().sprite = spr_helpOut[1];
        }
        else
        {
            help_obj.SetActive(true);
            help_i = 0;
            helpImg_obj.GetComponent<Image>().sprite = spr_help[help_i];
        }
    }

    public void HelpR()
    {
        if (help_i == 3)
        {
            help_obj.SetActive(false);
            helpR_obj.SetActive(true);
            helpO_obj.SetActive(false);
        }
        else if (help_i == 2)
        {
            help_i++;
            helpR_obj.SetActive(false);
            helpO_obj.SetActive(true);
            helpImg_obj.GetComponent<Image>().sprite = spr_help[help_i];
        }
        else
        {
            helpL_obj.SetActive(true);
            helpR_obj.SetActive(true);
            helpO_obj.SetActive(false);
            help_i++;
            helpImg_obj.GetComponent<Image>().sprite = spr_help[help_i];
        }
    }

    public void HelpL()
    {
        if (help_i == 0)
        {
        }
        else
        {
            helpR_obj.SetActive(true);
            helpO_obj.SetActive(false);
            help_i--;
            helpImg_obj.GetComponent<Image>().sprite = spr_help[help_i];
            if (help_i == 0)
            {
                helpL_obj.SetActive(false);
            }
        }
    }

    public void CloseOutHelp()
    {
        helpOut_obj.SetActive(false);
        helpCity_obj.SetActive(false);
    }

    public void talkSpeed()
    {
        //1,2,3단계 조절해서 속도 조절하기
        float f =PlayerPrefs.GetFloat("talkspeed", 0.05f);
        if (f == 0.07f)
        {
            speed_txt.text = "대화속도 보통";
            PlayerPrefs.SetFloat("talkspeed", 0.05f);
        }
        else if (f == 0.05f)
        {
            speed_txt.text = "대화속도 빠름";
            PlayerPrefs.SetFloat("talkspeed", 0.03f);
        }
        else if (f == 0.03f)
        {
            speed_txt.text = "대화속도 느림";
            PlayerPrefs.SetFloat("talkspeed", 0.07f);
        }
    }


    public void showInfoLink()
    {
        Application.OpenURL("https://docs.google.com/document/d/1JFdyCym-5Kxns2xcA-w8W5ir5YiL4J-6JrJeMF8zcuk/edit?usp=sharing");
    }

}
