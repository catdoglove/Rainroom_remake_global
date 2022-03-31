using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityShop : MonoBehaviour
{
    public GameObject shop_obj, todayShop_obj, shopHelp_obj, doorOpen_obj, doorClose_obj, blackimg;
    public GameObject shopYN_obj, shopTodayYN_obj;
    public int num_i;
    public Text[] txt_bed, txt_desk, txt_light;
    public string[] desk_name, bed_name, light_name;
    public int[] cost_desk, cost_bed, cost_light;
    int sum, level, cost_r, cost_h, have_r, have_h;
    string str_Code;

    public GameObject shopPopup_obj;
    public Text txt_Popup;

    public Text txt_rain, txt_heart;
    public Text[] txt_today;
    public GameObject[] todayBtn_obj, shopBtn_obj;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("titlecheck", 1);
        str_Code = PlayerPrefs.GetString("code", "");
        setPrice();
    }

    // Update is called once per frame
    public void openShop()
    {
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        SetOpen();
        SetText();
        shop_obj.SetActive(true);
        doorOpen_obj.SetActive(true);
        doorClose_obj.SetActive(false);
        shopYN_obj.SetActive(false);
        shopTodayYN_obj.SetActive(false);
    }

    public void closeShop()
    {
        shop_obj.SetActive(false);
        todayShop_obj.SetActive(false);
        doorClose_obj.SetActive(true);
        doorOpen_obj.SetActive(false);
    }

    public void openTodayShop()
    {
        SetToday();
        todayShop_obj.SetActive(true);
    }

    public void openHelp()
    {
        shopHelp_obj.SetActive(true);
    }

    public void closeHelp()
    {
        shopHelp_obj.SetActive(false);
    }

    public void Num0()
    {
        num_i = 0;
    }
    public void Num1()
    {
        num_i = 1;
    }
    public void Num2()
    {
        num_i = 2;
    }
    public void Num3()
    {
        num_i = 3;
    }
    public void Num4()
    {
        num_i = 4;
    }
    public void Num5()
    {
        num_i = 5;
    }
    public void Num6()
    {
        num_i = 6;
    }
    public void Num7()
    {
        num_i = 7;
    }
    public void Num8()
    {
        num_i = 8;
    }

    public void ClosePop()
    {
        shopPopup_obj.SetActive(false);
    }

    public void ShopBuyYN()
    {
        if (num_i==0)
        {
            level = PlayerPrefs.GetInt("bedlv", 0);
            if (level < 1)
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "아직 Max가 되지 않아 살 수 없다.";
            }
            else
            {

                if (level < 4)
                {
                    shopYN_obj.SetActive(true);
                }
            }
        }
        else if(num_i == 2)
        {
            level = PlayerPrefs.GetInt("lightlv", 0);
            if (level < 2)
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "아직 Max가 되지 않아 살 수 없다.";
            }
            else
            {
                if (level < 5)
                {
                    shopYN_obj.SetActive(true);
                }
            }
        }
        else
        {
            level = PlayerPrefs.GetInt("desklv", 0);
            if (level < 3)
            {
                shopYN_obj.SetActive(true);
            }
        }
    }
    public void ShopBuyY()
    {

        switch (num_i)
        {
            case 0:
                BuyBed();
                break;
            case 1:
                BuyDesk();
                break;
            case 2:
                BuyLight();
                break;
            default:
                break;
        }
        shopYN_obj.SetActive(false);
    }
    public void ShopBuyN()
    {
        shopYN_obj.SetActive(false);
    }

    public void ShopTodayBuyYN()
    {
        if(PlayerPrefs.GetInt("outgoods" + num_i, 0) == 1)
        {

        }
        else
        {
            shopTodayYN_obj.SetActive(true);
        }
    }
    public void ShopTodayBuyY()
    {
        switch (num_i)
        {
            case 0:
                cost_r = 19000;
                cost_h = 240;
                break;
            case 1:
                cost_r = 20000;
                cost_h = 250;
                break;
            case 2:
                cost_r = 17000;
                cost_h = 240;
                break;
            case 3:
                cost_r = 15000;
                cost_h = 220;
                break;
            case 4:
                break;
            case 5:
                cost_r = 17000;
                cost_h = 230;
                break;
            case 6:
                cost_r = 20000;
                cost_h = 250;
                break;
            case 7:
                cost_r = 16000;
                cost_h = 230;
                break;
            case 8:
                cost_r = 16000;
                cost_h = 230;
                break;
            default:
                break;
        }
        BuyToday();
        shopTodayYN_obj.SetActive(false);
    }
    public void ShopTodayBuyN()
    {
        shopTodayYN_obj.SetActive(false);
    }

    void BuyBed()
    {
        level = PlayerPrefs.GetInt("bedlv", 0);
        level = level - 1;
        sum = level * 2;
        cost_h = cost_bed[sum];
        cost_r = cost_bed[sum + 1];
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (have_r >= cost_r)
        {
            if (have_h >= cost_h)
            {
                GetSum();
                //마음
                txt_bed[0].text = "" + cost_bed[sum + 2];
                //빗물
                txt_bed[1].text = "" + cost_bed[sum + 3];
                //이름
                txt_bed[2].text = bed_name[level+1];

                level = level + 1;
                //레벨
                txt_bed[3].text = "Lv." + level;
                level = level + 1;
                if (level >= 4)
                {
                    //마음
                    txt_bed[0].text = "x";
                    //빗물
                    txt_bed[1].text = "x";
                    shopBtn_obj[0].GetComponent<Button>().interactable = false;
                    txt_bed[3].text = "Lv.Max";
                }
                PlayerPrefs.SetInt("bedlv", level);
                SetText();
            }
            else
            {
                PopShop();
            }
        }
        else
        {
            PopShop();
        }

    }
    void BuyDesk()
    {

        level = PlayerPrefs.GetInt("desklv", 0);
        sum = level * 2;
        cost_h = cost_desk[sum];
        cost_r = cost_desk[sum + 1];
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (have_r >= cost_r)
        {
            if (have_h >= cost_h)
            {
                GetSum();
                //마음
                txt_desk[0].text = "" + cost_desk[sum + 2];
                //빗물
                txt_desk[1].text = "" + cost_desk[sum + 3];
                level++;
                //이름
                txt_desk[2].text = desk_name[level];
                //레벨
                txt_desk[3].text = "Lv." + level;
                if (level >= 3)
                {
                    //마음
                    txt_desk[0].text = "x";
                    //빗물
                    txt_desk[1].text = "x";
                    txt_desk[3].text = "Lv.Max";
                    shopBtn_obj[1].GetComponent<Button>().interactable = false;
                }
                PlayerPrefs.SetInt("desklv", level);
                SetText();
            }
            else
            {
                PopShop();
            }
        }
        else
        {
            PopShop();
        }
    }
    void BuyLight()
    {
        level = PlayerPrefs.GetInt("lightlv", 0);
        level = level - 2;
        sum = level * 2;
        cost_h = cost_light[sum];
        cost_r = cost_light[sum + 1];
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (have_r >= cost_r)
        {
            if (have_h >= cost_h)
            {
                GetSum();
                //마음
                txt_light[0].text = "" + cost_light[sum + 2];
                //빗물
                txt_light[1].text = "" + cost_light[sum + 3];
                //이름
                txt_light[2].text = light_name[level+1];

                level = level + 1;
                //레벨
                txt_light[3].text = "Lv." + level;
                level = level + 2;
                if (level >= 5)
                {
                    //마음
                    txt_light[0].text = "x";
                    //빗물
                    txt_light[1].text = "x";
                    shopBtn_obj[2].GetComponent<Button>().interactable = false;
                    txt_light[3].text = "Lv.Max";
                }
                PlayerPrefs.SetInt("lightlv", level);
                SetText();
            }
            else
            {
                PopShop();
            }
        }
        else
        {
            PopShop();
        }
    }

    /// <summary>
    /// 계산 및 저장
    /// </summary>
    void GetSum()
    {
        have_r = have_r - cost_r;
        have_h = have_h - cost_h;
        PlayerPrefs.SetInt(str_Code + "r", have_r);
        PlayerPrefs.SetInt(str_Code + "h", have_h);
    }

    void SetText()
    {
        txt_rain.text = ""+have_r;
        txt_heart.text = ""+have_h;
    }


    //상점을 열때
    void SetOpen()
    {

        level = PlayerPrefs.GetInt("bedlv", 0);
        if (level < 1)
        {
            //마음
            txt_bed[0].text = "x";
            //빗물
            txt_bed[1].text = "x";
            //이름
            txt_bed[2].text = "이불";
            txt_bed[3].text = "Lv.0";
        }
        else
        {
            level--;
            sum = level * 2;
            //마음
            txt_bed[0].text = "" + cost_bed[sum];
            //빗물
            txt_bed[1].text = "" + cost_bed[sum + 1];
            //이름
            txt_bed[2].text = bed_name[level];
            //레벨
            txt_bed[3].text = "Lv." + level;
        }
        level = level + 1;
        if (level >= 4)
        {
            //마음
            txt_bed[0].text = "x";
            //빗물
            txt_bed[1].text = "x";
            //이름
            txt_bed[2].text = "오동나무침대";
            shopBtn_obj[0].GetComponent<Button>().interactable = false;
            txt_bed[3].text = "Lv.Max";
        }

        level = PlayerPrefs.GetInt("desklv", 0);
        sum = level * 2;
        //마음
        txt_desk[0].text = "" + cost_desk[sum];
        //빗물
        txt_desk[1].text = "" + cost_desk[sum + 1];
        //이름
        txt_desk[2].text = desk_name[level];
        //레벨
        txt_desk[3].text = "Lv." + level;
        if (level >= 3)
        {
            shopBtn_obj[1].GetComponent<Button>().interactable = false;
            //마음
            txt_desk[0].text = "x";
            //빗물
            txt_desk[1].text = "x";
            //이름
            txt_desk[2].text = "원목책상";
            txt_desk[3].text = "Lv.Max";
        }



        level = PlayerPrefs.GetInt("lightlv", 0);
        if (level < 2)
        {
            //마음
            txt_light[0].text = "x";
            //빗물
            txt_light[1].text = "x";
            //이름
            txt_light[2].text = "전등";
            txt_light[3].text = "Lv.0";
        }
        else
        {
            level = level - 2;
            sum = level * 2;
            //마음
            txt_light[0].text = "" + cost_light[sum];
            //빗물
            txt_light[1].text = "" + cost_light[sum + 1];
            //이름
            txt_light[2].text = light_name[level];
            //레벨
            txt_light[3].text = "Lv." + level;
        }
        
        level = level + 2;
        if (level >= 5)
        {
            shopBtn_obj[2].GetComponent<Button>().interactable = false;
            //마음
            txt_light[0].text = "x";
            //빗물
            txt_light[1].text = "x";
            //이름
            txt_light[2].text = "고급전등";

            txt_light[3].text = "Lv.Max";
        }
    }

    void setPrice()
    {
        //책상
        desk_name[0] = "책상";
        cost_desk[0] = 220;
        cost_desk[1] = 15000;

        desk_name[1] = "나무책상";
        cost_desk[2] = 260;
        cost_desk[3] = 17000;

        desk_name[2] = "원목책상";
        cost_desk[4] = 300;
        cost_desk[5] = 19000;

        desk_name[3] = "원목책상";
        cost_desk[6] = 0;
        cost_desk[7] = 0;

        //침대
        bed_name[0] = "침대";
        cost_bed[0] = 320;
        cost_bed[1] = 20000;

        bed_name[1] = "나무침대";
        cost_bed[2] = 340;
        cost_bed[3] = 22000;

        bed_name[2] = "오동나무침대";
        cost_bed[4] = 380;
        cost_bed[5] = 25000;

        bed_name[3] = "오동나무침대";
        cost_bed[6] = 0;
        cost_bed[7] = 0;

        //전등
        light_name[0] = "하얀전등";
        cost_light[0] = 250;
        cost_light[1] = 17000;

        light_name[1] = "까만전등";
        cost_light[2] = 280;
        cost_light[3] = 19000;

        light_name[2] = "고급전등";
        cost_light[4] = 330;
        cost_light[5] = 24000;

        light_name[3] = "고급전등";
        cost_light[6] = 0;
        cost_light[7] = 0;
    }

    void SetToday()
    {

        //고양이 미니어쳐 곰인형 거미 엔딩 디퓨저 우산 도트 컵
        for (int i = 0; i <= 8; i++)
        {
            if (PlayerPrefs.GetInt("outgoods" + i, 0) == 1)
            {
                if (i == 4)
                {

                }
                else
                {

                    todayBtn_obj[i].GetComponent<Button>().interactable = false;
                    int ci = i * 4;
                    //마음
                    txt_today[ci].text = "x";
                    //빗물
                    txt_today[ci + 1].text = "x";
                    //이름
                    txt_today[ci + 2].text = "품절";
                    //레벨
                    txt_today[ci + 3].text = "Lv.Max";
                }
            }
        }
    }

    void BuyToday()
    {
        have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        if (have_r >= cost_r)
        {
            if (have_h >= cost_h)
            {
                GetSum();

                PlayerPrefs.SetInt("outgoods" + num_i, 1);
                int i = 0;
                i = num_i * 4;
                todayBtn_obj[num_i].GetComponent<Button>().interactable = false;
                //마음
                txt_today[i].text = "x";
                //빗물
                txt_today[i + 1].text = "x";
                //이름
                txt_today[i + 2].text = "품절";
                //레벨
                txt_today[i + 3].text = "Lv.Max";
                PlayerPrefs.SetInt("setoutgoods", num_i);
                SetText();
            }
            else
            {
                PopShop();
            }
        }
        else
        {
            PopShop();
        }
    }

    /// <summary>
    /// 부족토스트
    /// </summary>
    void PopShop()
    {
        shopPopup_obj.SetActive(true);
        txt_Popup.text = "구입하기에 가지고 있는 것이 부족하다.";
    }
}
