using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainShop : MonoBehaviour
{
    //방업그레이드
    public GameObject wall_obj, window_obj, turtle_obj, book_obj, light_obj, seed_obj, bed_obj, seedBtn_obj, cupBtn_obj, desk_obj;
    //외출
    public GameObject clock_obj,draw_obj,frame_obj,fish_obj;
    public Text txt;
    public Sprite[] spr_wall, spr_window, spr_book, spr_light, spr_seed, spr_sleep, spr_glass, spr_desk, spr_draw, spr_clock, spr_frame;
    public int[] cost_wall, cost_window, cost_book, cost_light;
    public int[] upCk;
    public int item_num;

    public Text[] txt_window, txt_wall, txt_book, txt_light, txt_turtle, txt_seed, txt_bed, txt_cup;
    public string[] window_name, wall_name, book_name, light_name;
    public Text txt_rain, txt_heart, txt_Popup;
    public GameObject shopWin_obj, shopPopup_obj,shopWinYN_obj;
    public GameObject bedBox_obj, frameBox_obj, deskBox_obj, dalgonaBox_obj, dalgonaBtn_obj;
    public Sprite spr_boxOpen, spr_boxClose;

    public GameObject shopHelp_obj;

    int cost_r, cost_h, level, sum, have_r, have_h;

    string str_Code;

    //MAX용
    public GameObject btn_memoBook, btn_memoWindow, btn_memoSeed, btn_memoLight, btn_memoWall, btn_colorWindow, btn_colorWall, btn_colorLight, btn_colorBed, btn_colorBook, btn_colorSeed;
    public GameObject btn_colorClock, btn_colorDraw, btn_colorFrame, btn_colorDesk, btn_memoClock, btn_memoDraw, btn_boxFrame, btn_boxDesk;
    public Sprite[] spr_windowColorImg, spr_wallColorImg, spr_sleepColorImg, spr_bookColorImg, spr_seedColorImg, spr_clockColorImg, spr_frameColorImg, spr_deskColorImg;
    int wincolNum, wallcolNum, lightcolNum, sleepcolNum, bookcolNum, seedcolNum, clockcolNum, drawcolNum, framecolNum, deskcolNum;
    public Text[] txt_memoName;
    [TextArea]
    public string[] str_memo;
    public GameObject memoImg, switchImg, switchImg2, boxWin_obj;

    public GameObject[] MaxX_obj, shopBtn_obj;
    public Sprite cup_spr;
    public GameObject notice_obj;
    //외출
    public GameObject outItem_obj;
    public Text txt_talk, txt_talkTime;
    private void Awake()
    {
        First();
    }

    // Start is called before the first frame update
    void Start()
    {
        str_Code = PlayerPrefs.GetString("code", "");
        

        if (PlayerPrefs.GetInt("lightlv", 0) >= 2)
        {
            switchImg.SetActive(true);
            switchImg2.SetActive(true);
        }
        //가격과 이름
        setPrice();
        Setf();
        Notice();
    }

    public void Notice()
    {
        if (PlayerPrefs.GetInt("notice1", 0) == 0)
        {
            notice_obj.SetActive(true);
        }
    }
    public void CloseNotice()
    {
        PlayerPrefs.SetInt(str_Code + "r", PlayerPrefs.GetInt(str_Code + "r", 0)+1000);
        PlayerPrefs.SetInt(str_Code + "h", PlayerPrefs.GetInt(str_Code + "h", 0) + 10);
        notice_obj.SetActive(false);
        PlayerPrefs.SetInt("notice1", 1);
    }

    void Setf()
    {

        if (PlayerPrefs.GetInt("turtlelv", 0) == 1)
        {
            shopBtn_obj[1].GetComponent<Button>().interactable = false;
            //레벨
            txt_turtle[0].text = "Lv.MAX";
            //이름
            txt_turtle[1].text = "거북이";
            //물
            txt_turtle[2].text = "x";
            //마음
            txt_turtle[3].text = "x";
            turtle_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("seedlv", 0) >= 1)
        {
            shopBtn_obj[2].GetComponent<Button>().interactable = false;
            //레벨
            txt_seed[0].text = "Lv.MAX";
            //이름
            txt_seed[1].text = "씨앗";
            //물
            txt_seed[2].text = "x";
            //마음
            txt_seed[3].text = "x";
            seed_obj.SetActive(true);
        }

        if (PlayerPrefs.GetInt("bedlv", 0) >= 1)
        {
            shopBtn_obj[7].GetComponent<Button>().interactable = false;
            //레벨
            txt_bed[0].text = "Lv.MAX";
            //이름
            txt_bed[1].text = "이불";
            //물
            txt_bed[2].text = "x";
            //마음
            txt_bed[3].text = "x";
            bed_obj.SetActive(true);
            //상자
            bedBox_obj.GetComponent<Button>().interactable = true;
            sleepcolNum = PlayerPrefs.GetInt("sleepColor", 0);
            bed_obj.GetComponent<Image>().sprite = spr_sleep[PlayerPrefs.GetInt("bedlv", 0) - 1];

            if (PlayerPrefs.GetInt("bedlv", 0) >= 4)
            {
                bed_obj.GetComponent<Image>().sprite = spr_sleepColorImg[sleepcolNum];
            }
            //btn_colorBed.SetActive(true);
        }

        if (PlayerPrefs.GetInt("cuplv", 0) == 1)
        {
            shopBtn_obj[0].GetComponent<Button>().interactable = false;
            //레벨
            txt_cup[0].text = "Lv.MAX";
            //이름
            txt_cup[1].text = "물컵";
            //물
            txt_cup[2].text = "x";
            //마음
            txt_cup[3].text = "x";
            cupBtn_obj.SetActive(true);
        }
        if (PlayerPrefs.GetInt("booklv", 0) >= 9)
        {
            shopBtn_obj[4].GetComponent<Button>().interactable = false;
            //레벨
            txt_book[0].text = "Lv.MAX";
            //이름
            txt_book[1].text = "책장";
            //물
            txt_book[2].text = "x";
            //마음
            txt_book[3].text = "x";
        }
        if (PlayerPrefs.GetInt("walllv", 0) >= 2)
        {
            shopBtn_obj[5].GetComponent<Button>().interactable = false;
            //레벨
            txt_wall[0].text = "Lv.MAX";
            //이름
            txt_wall[1].text = "깨끗한벽지";
            //물
            txt_wall[2].text = "x";
            //마음
            txt_wall[3].text = "x";
        }
        if (PlayerPrefs.GetInt("lightlv", 0) >= 2)
        {
            shopBtn_obj[6].GetComponent<Button>().interactable = false;
            //레벨
            txt_light[0].text = "Lv.MAX";
            //이름
            txt_light[1].text = "불켜진 전구";
            //물
            txt_light[2].text = "x";
            //마음
            txt_light[3].text = "x";
        }
        if (PlayerPrefs.GetInt("windowlv", 0) >= 8)
        {
            shopBtn_obj[3].GetComponent<Button>().interactable = false;
            //레벨
            txt_window[0].text = "Lv.MAX";
            //이름
            txt_window[1].text = "예쁜커튼";
            //물
            txt_window[2].text = "x";
            //마음
            txt_window[3].text = "x";
        }

        level = PlayerPrefs.GetInt("windowlv", 0);
        window_obj.GetComponent<Image>().sprite = spr_window[level];
        if (level >= 8)
        {

            wincolNum = PlayerPrefs.GetInt("windowColor", 0);
            window_obj.GetComponent<Image>().sprite = spr_windowColorImg[wincolNum];
        }

        level = PlayerPrefs.GetInt("walllv", 0);
        wall_obj.GetComponent<Image>().sprite = spr_wall[level];
        if (level >= 2)
        {

            wallcolNum = PlayerPrefs.GetInt("wallColor", 0);
            wall_obj.GetComponent<Image>().sprite = spr_wallColorImg[wallcolNum];
        }

        level = PlayerPrefs.GetInt("booklv", 0);
        book_obj.GetComponent<Image>().sprite = spr_book[level];
        if (level >= 9)
        {

            bookcolNum = PlayerPrefs.GetInt("bookColor", 0);
            book_obj.GetComponent<Image>().sprite = spr_bookColorImg[bookcolNum];
        }

        level = PlayerPrefs.GetInt("lightlv", 0);
        light_obj.GetComponent<Image>().sprite = spr_light[level];
        if (level >= 5)
        {
            lightcolNum = PlayerPrefs.GetInt("lightColor", 5);
            light_obj.GetComponent<Image>().sprite = spr_light[lightcolNum];
        }


        if (PlayerPrefs.GetInt("seedlv", 0) > 1)
        {
            int sd = PlayerPrefs.GetInt("seedgrow", 1) - 1;
            seed_obj.GetComponent<Image>().sprite = spr_seed[sd];
            if (PlayerPrefs.GetInt("seedColor", 0) > 0)
            {
                seed_obj.GetComponent<Image>().sprite = spr_seedColorImg[PlayerPrefs.GetInt("seedColor", 0)];
            }
        }
        //시계
        if (PlayerPrefs.GetInt("clock", 0) >= 1)
        {
            int sd = PlayerPrefs.GetInt("clock", 0);
            clock_obj.GetComponent<Image>().sprite = spr_clock[sd];
            clock_obj.SetActive(true);
            if (PlayerPrefs.GetInt("clockColors", 0) > 0)
            {
                clock_obj.GetComponent<Image>().sprite = spr_clockColorImg[PlayerPrefs.GetInt("clockColors", 0)];
            }
        }
        //그림
        if (PlayerPrefs.GetInt("draw", 0) >= 1)
        {
            int sd = PlayerPrefs.GetInt("draw", 0);
            draw_obj.GetComponent<Image>().sprite = spr_draw[sd];
            draw_obj.SetActive(true);
            if (PlayerPrefs.GetInt("drawColors", 0) > 0)
            {
                draw_obj.GetComponent<Image>().sprite = spr_draw[PlayerPrefs.GetInt("drawColors", 0)];
            }
        }
        //그림틀
        if (PlayerPrefs.GetInt("frame", 0) >= 1)
        {
            int sd = PlayerPrefs.GetInt("frame", 0);
            frame_obj.GetComponent<Image>().sprite = spr_frame[sd];
            frame_obj.SetActive(true);
            //상자
            frameBox_obj.GetComponent<Button>().interactable = true;


            if (PlayerPrefs.GetInt("frameColors", 0) > 0)
            {
                frame_obj.GetComponent<Image>().sprite = spr_frameColorImg[PlayerPrefs.GetInt("frameColors", 0)];
            }
        }
        
        //잉어
        if (PlayerPrefs.GetInt("dalgona", 0) >= 15)
        {

            if (PlayerPrefs.GetInt("dalgonabox", 0) == 0)
            {
                fish_obj.SetActive(true);
            }
        }
        //책상
        if (PlayerPrefs.GetInt("desklv", 0) >= 1)
        {
            int sd = PlayerPrefs.GetInt("desklv", 0);
            desk_obj.GetComponent<Image>().sprite = spr_desk[sd];
            desk_obj.SetActive(true);
            //상자
            deskBox_obj.GetComponent<Button>().interactable = true;

            if (PlayerPrefs.GetInt("deskColors", 0) > 0)
            {
                desk_obj.GetComponent<Image>().sprite = spr_deskColorImg[PlayerPrefs.GetInt("deskColors", 0)];
            }
        }

        //씨앗맥스
        // seed_obj.GetComponent<Image>().sprite = spr_seed[PlayerPrefs.GetInt("seedlv", 0)];
        //컬러



        if (PlayerPrefs.GetInt("bedbox", 0) == 1)
        {
            bed_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("framebox", 0) == 1)
        {
            frame_obj.SetActive(false);
        }
        if (PlayerPrefs.GetInt("deskbox", 0) == 1)
        {
            desk_obj.SetActive(false);
        }
    }
    

    public void shopAct()
    {
        if (shopWin_obj.activeSelf)
        {
            shopWin_obj.SetActive(false);
        }
        else
        {
            shopWin_obj.SetActive(true);
            boxWin_obj.SetActive(false);
            txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
            txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            set();

            cost_r = 0;
            cost_h = 0;
            Setf();
        }
    }

    //MAX 일때 못삼
    public void ShopBuyYN()
    {
        int ck = 0;

        if (PlayerPrefs.GetInt("cuplv", 0) >= 1&& item_num==1)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("turtlelv", 0) >= 1 && item_num == 2)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("seedlv", 0) >= 1 && item_num == 3)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("windowlv", 0) >= 8 && item_num == 4)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("booklv", 0) >= 9 && item_num == 5)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("walllv", 0) >= 2 && item_num == 6)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("lightlv", 0) >= 2 && item_num == 7)
        {
            ck++;
        }
        if (PlayerPrefs.GetInt("bedlv", 0) >= 1 && item_num == 8)
        {
            ck++;
        }
        
        shopWinYN_obj.SetActive(true);
        if (ck == 1)
        {
            shopWinYN_obj.SetActive(false);
        }
    }
    public void ShopBuyY()
    {
        shopWinYN_obj.SetActive(false);
        switch (item_num)
        {
            case 1:
                BuyCup();
                break;
            case 2:
                BuyTurtle();
                break;
            case 3:
                BuySeed();
                break;
            case 4:
                BuyWindow();
                break;
            case 5:
                BuyBook();
                break;
            case 6:
                BuyWall();
                break;
            case 7:
                BuyLight();
                break;
            case 8:
                Buybed();
                break;
        }
    }

    public void ShopBuyN()
    {
        shopWinYN_obj.SetActive(false);
    }

    public void SetCup1()
    {
        item_num = 1;
    }
    public void SetTurtle2()
    {
        item_num = 2;
    }
    public void SetSeed3()
    {
        item_num = 3;
    }
    public void SetWindow4()
    {
        item_num = 4;
    }

    public void SetBook5()
    {
        item_num = 5;
    }
    public void SetWall6()
    {
        item_num = 6;
    }
    public void SetLight7()
    {
        item_num = 7;
    }
    public void Setbed8()
    {
        item_num = 8;
    }

    void setPrice()
    {
        //물마음-창문
        window_name[0] = "깨진창문";
        cost_window[0] = 0;
        cost_window[1] = 15;

        window_name[1] = "보수된창문";
        cost_window[2] = 60;
        cost_window[3] = 30;

        window_name[2] = "보수된창문+";
        cost_window[4] = 500;
        cost_window[5] = 100;

        window_name[3] = "창문";
        cost_window[6] = 1000;
        cost_window[7] = 120;

        window_name[4] = "스티커붙이기";
        cost_window[8] = 2000;
        cost_window[9] = 150;

        window_name[5] = "스티커붙이기+";
        cost_window[10] = 5000;
        cost_window[11] = 190;

        window_name[6] = "커튼달린창문";
        cost_window[12] = 9000;
        cost_window[13] = 240;

        window_name[7] = "밝은커튼";
        cost_window[14] = 15000;
        cost_window[15] = 400;

        window_name[8] = "예쁜커튼";
        cost_window[16] = 0;
        cost_window[17] = 0;

        //물마음-벽
        wall_name[0] = "습기찬벽지";
        cost_wall[0] = 4000;
        cost_wall[1] = 300;

        wall_name[1] = "보수된벽지";
        cost_wall[2] = 15000;
        cost_wall[3] = 500;

        wall_name[2] = "깨끗한벽지";
        cost_wall[4] = 0;
        cost_wall[5] = 0;

        //물마음-책
        book_name[0] = "전단지";
        cost_book[0] = 0;
        cost_book[1] = 10;

        book_name[1] = "신문";
        cost_book[2] = 50;
        cost_book[3] = 20;

        book_name[2] = "찢어진 잡지";
        cost_book[4] = 300;
        cost_book[5] = 50;

        book_name[3] = "위인전";
        cost_book[6] = 1000;
        cost_book[7] = 70;

        book_name[4] = "요리책";
        cost_book[8] = 2000;
        cost_book[9] = 100;

        book_name[5] = "동화책";
        cost_book[10] = 5000;
        cost_book[11] = 130;

        book_name[6] = "유머책";
        cost_book[12] = 9000;
        cost_book[13] = 160;

        book_name[7] = "만화책";
        cost_book[14] = 15000;
        cost_book[15] = 200;

        book_name[8] = "소설책";
        cost_book[16] = 30000;
        cost_book[17] = 450;

        book_name[9] = "책장";
        cost_book[18] = 0;
        cost_book[19] = 0;

        //물마음-전등
        light_name[0] = "깨진 전구";
        cost_light[0] = 10000;
        cost_light[1] = 100;

        light_name[1] = "전구";
        cost_light[2] = 15000;
        cost_light[3] = 200;

        light_name[2] = "불켜진 전구";
        cost_light[4] = 0;
        cost_light[5] = 0;
    }

    void BuyWindow()
    {
        level = PlayerPrefs.GetInt("windowlv", 0);
        if (level >= 8) { }
        else
        {
            
            sum = level * 2;
            cost_r = cost_window[sum];
            cost_h = cost_window[sum + 1];
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("windowlv", level);
                    WindowRe();

                    window_obj.GetComponent<Image>().sprite = spr_window[level];
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는"+"\n"+"가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuyWall()
    {
        level = PlayerPrefs.GetInt("walllv", 0);
        if (level >= 2) { }
        else
        {
            sum = level * 2;
            cost_r = cost_wall[sum];
            cost_h = cost_wall[sum + 1];
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("walllv", level);
                    WallRe();

                    wall_obj.GetComponent<Image>().sprite = spr_wall[level];
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는"+"\n"+"가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는"+"\n"+"가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuyBook()
    {
        level = PlayerPrefs.GetInt("booklv", 0);
        if (level >= 9) { }
        else
        {
            sum = level * 2;
            cost_r = cost_book[sum];
            cost_h = cost_book[sum + 1];
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("booklv", level);
                    BookRe();

                    book_obj.GetComponent<Image>().sprite = spr_book[level];
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuyLight()
    {
        level = PlayerPrefs.GetInt("lightlv", 0);
        if (level >= 2) { }
        else
        {
            sum = level * 2;
            cost_r = cost_light[sum];
            cost_h = cost_light[sum + 1];
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("lightlv", level);
                    LightRe();

                    light_obj.GetComponent<Image>().sprite = spr_light[level];
                    PlayerPrefs.Save();

                    if (level >= 2)
                    {
                        shopBtn_obj[6].GetComponent<Button>().interactable = false;
                        txt_light[0].text = "Lv.MAX";
                        //이름
                        txt_light[1].text = light_name[level];
                        //물
                        txt_light[2].text = "x";
                        //마음
                        txt_light[3].text = "x";
                        switchImg.SetActive(true);
                        switchImg2.SetActive(true);
                    }

                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuyTurtle()
    {
        level = PlayerPrefs.GetInt("turtlelv", 0);
        if (level >= 1) { }
        else
        {
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_r = 5000;
            cost_h = 150;
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("turtlelv", level);
                    have_r = have_r - cost_r;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "r", have_r);
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
                    txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);

                    shopBtn_obj[1].GetComponent<Button>().interactable = false;
                    //레벨
                    txt_turtle[0].text = "Lv.MAX";
                    //이름
                    txt_turtle[1].text = "거북이";
                    //물
                    txt_turtle[2].text = "x";
                    //마음
                    txt_turtle[3].text = "x";

                    turtle_obj.SetActive(true);
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuySeed()
    {
        level = PlayerPrefs.GetInt("seedlv", 0);
        if (level >= 1) { }
        else
        {
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_r = 2000;
            cost_h = 100;
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("seedlv", level);
                    have_r = have_r - cost_r;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "r", have_r);
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
                    txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);
                    shopBtn_obj[2].GetComponent<Button>().interactable = false;
                    //레벨
                    txt_seed[0].text = "Lv.MAX";
                    //이름
                    txt_seed[1].text = "씨앗";
                    //물
                    txt_seed[2].text = "x";
                    //마음
                    txt_seed[3].text = "x";
                    seed_obj.SetActive(true);
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }
    void Buybed()
    {
        level = PlayerPrefs.GetInt("bedlv", 0);
        if (level >= 1) { }
        else
        {
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_r = 19000;
            cost_h = 290;
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("bedlv", level);
                    have_r = have_r - cost_r;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "r", have_r);
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
                    txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);
                    shopBtn_obj[7].GetComponent<Button>().interactable = false;
                    //레벨
                    txt_bed[0].text = "Lv.MAX";
                    //이름
                    txt_bed[1].text = "이불";
                    //물
                    txt_bed[2].text = "x";
                    //마음
                    txt_bed[3].text = "x";
                    bed_obj.SetActive(true);
                    PlayerPrefs.Save();
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }

    void BuyCup()
    {
        level = PlayerPrefs.GetInt("cuplv", 0);
        if (level >= 1) { }
        else
        {
            have_r = PlayerPrefs.GetInt(str_Code + "r", 0);
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_r = 0;
            cost_h = 15;
            if (have_r >= cost_r)
            {
                if (have_h >= cost_h)
                {
                    level++;
                    PlayerPrefs.SetInt("cuplv", level);
                    have_r = have_r - cost_r;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "r", have_r);
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
                    txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);
                    shopBtn_obj[0].GetComponent<Button>().interactable = false;
                    //레벨
                    txt_cup[0].text = "Lv.MAX";
                    //이름
                    txt_cup[1].text = "물컵";
                    //물
                    txt_cup[2].text = "x";
                    //마음
                    txt_cup[3].text = "x";
                    cupBtn_obj.SetActive(true);
                    PlayerPrefs.Save();
                    PlayerPrefs.SetInt("water", 0);
                    cupBtn_obj.GetComponent<Image>().sprite = cup_spr;
                }
                else
                {
                    shopPopup_obj.SetActive(true);
                    txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
                }
            }
            else
            {
                shopPopup_obj.SetActive(true);
                txt_Popup.text = "구입하기에는" + "\n" + "가지고 있는 것이 부족하다.";
            }
        }
    }

    void WindowRe()
    {
        level = PlayerPrefs.GetInt("windowlv", 0);
        level--;
        sum = level * 2;
        cost_r = cost_window[sum];
        cost_h = cost_window[sum + 1];
        calc();
        level++;
        sum = level * 2;
        cost_r = cost_window[sum];
        cost_h = cost_window[sum + 1];
        //레벨
        txt_window[0].text = "Lv." + level;
        //이름
        txt_window[1].text = window_name[level];
        //물
        txt_window[2].text = "" + cost_r;
        //마음
        txt_window[3].text = "" + cost_h;

        if (level >= 8)
        {
            shopBtn_obj[3].GetComponent<Button>().interactable = false;
            txt_window[0].text = "Lv.MAX";
            //이름
            txt_window[1].text = "예쁜커튼";
            //물
            txt_window[2].text = "x";
            //마음
            txt_window[3].text = "x";

        }
    }

    void WallRe()
    {
        level = PlayerPrefs.GetInt("walllv", 0);
        level--;
        sum = level * 2;
        cost_r = cost_wall[sum];
        cost_h = cost_wall[sum + 1];
        calc();
        level++;
        sum = level * 2;
        cost_r = cost_wall[sum];
        cost_h = cost_wall[sum + 1];
        //레벨
        txt_wall[0].text = "Lv." + level;
        //이름
        txt_wall[1].text = wall_name[level];
        //물
        txt_wall[2].text = "" + cost_r;
        //마음
        txt_wall[3].text = "" + cost_h;
        if (level >= 2)
        {
            shopBtn_obj[5].GetComponent<Button>().interactable = false;
            txt_wall[0].text = "Lv.MAX";
            //이름
            txt_wall[1].text = "깨끗한벽지";
            //물
            txt_wall[2].text = "x";
            //마음
            txt_wall[3].text = "x";
        }
    }
    void BookRe()
    {
        if (level >= 9)
        {
            shopBtn_obj[4].GetComponent<Button>().interactable = false;
            txt_book[0].text = "Lv.MAX";
            txt_book[1].text = book_name[level];
            txt_book[2].text = "x";
            txt_book[3].text = "x";
            //btn_memoBook.SetActive(true);
            //btn_colorBook.SetActive(true);
        }
        else
        {
            level = PlayerPrefs.GetInt("booklv", 0);
            level--;
            sum = level * 2;
            cost_r = cost_book[sum];
            cost_h = cost_book[sum + 1];
            calc();
            level++;
            sum = level * 2;
            cost_r = cost_book[sum];
            cost_h = cost_book[sum + 1];
            //레벨
            txt_book[0].text = "Lv." + level;
            //이름
            txt_book[1].text = book_name[level];
            //물
            txt_book[2].text = "" + cost_r;
            //마음
            txt_book[3].text = "" + cost_h;
        }
    }

    void LightRe()
    {
        level = PlayerPrefs.GetInt("lightlv", 0);
        level--;
        sum = level * 2;
        cost_r = cost_light[sum];
        cost_h = cost_light[sum + 1];
        calc();
        level++;
        sum = level * 2;
        cost_r = cost_light[sum];
        cost_h = cost_light[sum + 1];
        //레벨
        txt_light[0].text = "Lv." + level;
        //이름
        txt_light[1].text = light_name[level];
        //물
        txt_light[2].text = "" + cost_r;
        //마음
        txt_light[3].text = "" + cost_h;
        if (level >= 2)
        {
            shopBtn_obj[6].GetComponent<Button>().interactable = false;
            txt_light[0].text = "Lv.MAX";
            //btn_memoLight.SetActive(true);
            //btn_colorLight.SetActive(true);
        }
    }

    void calc()
    {
        have_r = have_r - cost_r;
        have_h = have_h - cost_h;
        PlayerPrefs.SetInt(str_Code + "r", have_r);
        PlayerPrefs.SetInt(str_Code + "h", have_h);
        txt_rain.text = "" + PlayerPrefs.GetInt(str_Code + "r", 0);
        txt_heart.text = "" + PlayerPrefs.GetInt(str_Code + "h", 0);
    }

    void set()
    {
        int ll;

        ll = PlayerPrefs.GetInt("booklv", 0);
        sum = ll * 2;
        cost_r = cost_book[sum];
        cost_h = cost_book[sum + 1];
        //레벨
        txt_book[0].text = "Lv." + ll;
        //이름
        txt_book[1].text = book_name[ll];
        //물
        txt_book[2].text = "" + cost_r;
        //마음
        txt_book[3].text = "" + cost_h;

        ll = PlayerPrefs.GetInt("lightlv", 0);

        if (ll >= 2)
        {
            shopBtn_obj[6].GetComponent<Button>().interactable = false;
            //레벨
            txt_light[0].text = "Lv.MAX";
            //이름
            txt_light[1].text = "";
            //물
            txt_light[2].text = "x";
            //마음
            txt_light[3].text = "x";
        }
        else
        {
            sum = ll * 2;
            cost_r = cost_light[sum];
            cost_h = cost_light[sum + 1];
            //레벨
            txt_light[0].text = "Lv." + ll;
            //이름
            txt_light[1].text = light_name[ll];
            //물
            txt_light[2].text = "" + cost_r;
            //마음
            txt_light[3].text = "" + cost_h;
        }

        ll = PlayerPrefs.GetInt("walllv", 0);
        sum = ll * 2;
        cost_r = cost_wall[sum];
        cost_h = cost_wall[sum + 1];
        //레벨
        txt_wall[0].text = "Lv." + ll;
        //이름
        txt_wall[1].text = wall_name[ll];
        //물
        txt_wall[2].text = "" + cost_r;
        //마음
        txt_wall[3].text = "" + cost_h;

        ll = PlayerPrefs.GetInt("windowlv", 0);

        if (ll >= 8)
        {

            shopBtn_obj[3].GetComponent<Button>().interactable = false;
            //레벨
            txt_window[0].text = "Lv.MAX";
            //이름
            txt_window[1].text = "예쁜커튼";
            //물
            txt_window[2].text = "x";
            //마음
            txt_window[3].text = "x";
        }
        else
        {
            sum = ll * 2;
            cost_r = cost_window[sum];
            cost_h = cost_window[sum + 1];
            //레벨
            txt_window[0].text = "Lv." + ll;
            //이름
            txt_window[1].text = window_name[ll];
            //물
            txt_window[2].text = "" + cost_r;
            //마음
            txt_window[3].text = "" + cost_h;
        }
    }

    public void closePop()
    {
        shopPopup_obj.SetActive(false);
    }


    //상점외출물건
    public void ActoutItem()
    {
        if (outItem_obj.activeSelf)
        {
            outItem_obj.SetActive(false);
        }
        else
        {
            outItem_obj.SetActive(true);
            SetOutItem();
        }
    }

    /// <summary>
    /// 시계 그림 책상 액자
    /// </summary>
    void SetOutItem()
    {
        if (PlayerPrefs.GetInt("windowlv", 0)>=8)
        {
            btn_colorWindow.GetComponent<Button>().interactable = true;
            btn_memoWindow.GetComponent<Button>().interactable = true;
            MaxX_obj[0].SetActive(false);
        }

        if (PlayerPrefs.GetInt("booklv", 0) >= 9)
        {
            btn_colorBook.GetComponent<Button>().interactable = true;
            btn_memoBook.GetComponent<Button>().interactable = true;
            MaxX_obj[1].SetActive(false);
        }

        if (PlayerPrefs.GetInt("walllv", 0) >= 2)
        {
            btn_colorWall.GetComponent<Button>().interactable = true;
            btn_memoWall.GetComponent<Button>().interactable = true;
            MaxX_obj[2].SetActive(false);
        }

        if (PlayerPrefs.GetInt("lightlv", 0) >= 5)
        {
            btn_colorLight.GetComponent<Button>().interactable = true;
            btn_memoLight.GetComponent<Button>().interactable = true;
            MaxX_obj[3].SetActive(false);
        }

        if (PlayerPrefs.GetInt("seedlv", 0) >= 9)
        {
            btn_colorSeed.GetComponent<Button>().interactable = true;
            btn_memoSeed.GetComponent<Button>().interactable = true;
            MaxX_obj[4].SetActive(false);
        }

        if (PlayerPrefs.GetInt("draw", 0) >= 9)
        {
            btn_colorDraw.GetComponent<Button>().interactable = true;
            btn_memoDraw.GetComponent<Button>().interactable = true;
            MaxX_obj[5].SetActive(false);
        }
        if (PlayerPrefs.GetInt("clock", 0) >= 5)
        {
            btn_colorClock.GetComponent<Button>().interactable = true;
            btn_memoClock.GetComponent<Button>().interactable = true;
            MaxX_obj[6].SetActive(false);
        }
        if (PlayerPrefs.GetInt("bedlv", 0) >= 4)
        {
            btn_colorBed.GetComponent<Button>().interactable = true;
            MaxX_obj[7].SetActive(false);
        }
        if (PlayerPrefs.GetInt("frame", 0) >= 5)
        {
            btn_colorFrame.GetComponent<Button>().interactable = true;
            MaxX_obj[8].SetActive(false);
        }
        if (PlayerPrefs.GetInt("desklv", 0) >= 3)
        {
            btn_colorDesk.GetComponent<Button>().interactable = true;
            MaxX_obj[9].SetActive(false);
        }
    }


    void Allfalse()
    {
        btn_boxFrame.GetComponent<Button>().interactable = false;
        btn_colorFrame.GetComponent<Button>().interactable = false;
        btn_colorDesk.GetComponent<Button>().interactable = false;
        btn_colorClock.GetComponent<Button>().interactable = false;
        btn_colorDraw.GetComponent<Button>().interactable = false;
        btn_memoClock.GetComponent<Button>().interactable = false;
        btn_memoDraw.GetComponent<Button>().interactable = false;
    }

    public void showBookAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "책에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[0];
    }

    public void showWindowAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "창문에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[1];
    }

    public void showSeedAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "씨앗에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[2];
    }

    public void showLightAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "전등에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[3];
        memoImg.SetActive(true);

    }

    public void showWallAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "벽지에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[4];

    }
    public void showClockAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "시계에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[5];
    }

    public void showPictureAllTxt()
    {
        memoImg.SetActive(true);
        txt_memoName[0].text = "그림에 대하여";
        memoFalse();
        txt_memoName[1].text = str_memo[6];
    }


    public void closeMemo()
    {
        memoImg.SetActive(false);
    }


    void memoFalse()
    {
        memoImg.SetActive(true);
    }

    public void SwichPop()
    {
        shopPopup_obj.SetActive(true);
        txt_Popup.text = "잘 때 사용할 수 있다.";
    }


    public void changeWindowColor()
    {
        if (wincolNum < 9)
        { //10개
            wincolNum = PlayerPrefs.GetInt("windowColor", 0) + 1;
            window_obj.GetComponent<Image>().sprite = spr_windowColorImg[wincolNum];
            PlayerPrefs.SetInt("windowColor", wincolNum);
        }
        else
        {
            wincolNum = 0;
            window_obj.GetComponent<Image>().sprite = spr_windowColorImg[wincolNum];
            PlayerPrefs.SetInt("windowColor", 0);
        }
        PlayerPrefs.Save();
    }


    public void changeRoomColor()
    {
        if (wallcolNum < 3)
        { //4개
            wallcolNum = PlayerPrefs.GetInt("wallColor", 0) + 1;
            wall_obj.GetComponent<Image>().sprite = spr_wallColorImg[wallcolNum];
            PlayerPrefs.SetInt("wallColor", wallcolNum);
        }
        else
        {
            wallcolNum = 0;
            wall_obj.GetComponent<Image>().sprite = spr_wallColorImg[wallcolNum];
            PlayerPrefs.SetInt("wallColor", 0);
        }
        PlayerPrefs.Save();
    }


    public void changeLightColor()
    {
        if (lightcolNum < 5)
        { //3~5까지
            lightcolNum = PlayerPrefs.GetInt("lightColor", 3) + 1;
            light_obj.GetComponent<Image>().sprite = spr_light[lightcolNum];
            PlayerPrefs.SetInt("lightColor", lightcolNum);
        }
        else
        {
            lightcolNum = 3;
            light_obj.GetComponent<Image>().sprite = spr_light[lightcolNum];
            PlayerPrefs.SetInt("lightColor", 3);
        }
        PlayerPrefs.Save();
    }


    public void changeBedColor()
    {
        if (sleepcolNum < 4)
        { //5까지
            sleepcolNum = PlayerPrefs.GetInt("sleepColor", 0) + 1;
            bed_obj.GetComponent<Image>().sprite = spr_sleepColorImg[sleepcolNum];
            PlayerPrefs.SetInt("sleepColor", sleepcolNum);
        }
        else
        {
            sleepcolNum = 0;
            bed_obj.GetComponent<Image>().sprite = spr_sleepColorImg[sleepcolNum];
            PlayerPrefs.SetInt("sleepColor", 0);
        }
        PlayerPrefs.Save();
    }

    public void changeBookColor()
    {
        if (bookcolNum < 4)
        { //5개
            bookcolNum = PlayerPrefs.GetInt("bookColor", 0) + 1;
            book_obj.GetComponent<Image>().sprite = spr_bookColorImg[bookcolNum];
            PlayerPrefs.SetInt("bookColor", bookcolNum);
        }
        else
        {
            bookcolNum = 0;
            book_obj.GetComponent<Image>().sprite = spr_bookColorImg[bookcolNum];
            PlayerPrefs.SetInt("bookColor", 0);
        }
        PlayerPrefs.Save();
    }


    public void changeSeedColor()
    {
        if (seedcolNum < 4)
        { //5개
            seedcolNum = PlayerPrefs.GetInt("seedColor", 0) + 1;
            seed_obj.GetComponent<Image>().sprite = spr_seedColorImg[seedcolNum];
            PlayerPrefs.SetInt("seedColor", seedcolNum);
        }
        else
        {
            seedcolNum = 0;
            seed_obj.GetComponent<Image>().sprite = spr_seedColorImg[seedcolNum];
            PlayerPrefs.SetInt("seedColor", 0);
        }
        PlayerPrefs.Save();
    }

    public void changeClockColor()
    {
        if (clockcolNum < 7)
        { 
            clockcolNum = PlayerPrefs.GetInt("clockColors", 0) + 1;
            clock_obj.GetComponent<Image>().sprite = spr_clockColorImg[clockcolNum];
            PlayerPrefs.SetInt("clockColors", clockcolNum);
        }
        else
        {
            clockcolNum = 0;
            clock_obj.GetComponent<Image>().sprite = spr_clockColorImg[clockcolNum];
            PlayerPrefs.SetInt("clockColors", 0);
        }
        PlayerPrefs.Save();
    }

    public void changeDrawColor()
    {
        if (drawcolNum < 9)
        { 
            drawcolNum = PlayerPrefs.GetInt("drawColors", 0) + 1;
            draw_obj.GetComponent<Image>().sprite = spr_draw[drawcolNum];
            PlayerPrefs.SetInt("drawColors", drawcolNum);
        }
        else
        {
            drawcolNum = 1;
            draw_obj.GetComponent<Image>().sprite = spr_draw[drawcolNum];
            PlayerPrefs.SetInt("drawColors", 1);
        }
        PlayerPrefs.Save();
    }

    public void changeFrameColor()
    {
        if (framecolNum < 5)
        { 
            framecolNum = PlayerPrefs.GetInt("frameColors", 0) + 1;
            frame_obj.GetComponent<Image>().sprite = spr_frameColorImg[framecolNum];
            PlayerPrefs.SetInt("frameColors", framecolNum);
        }
        else
        {
            framecolNum = 0;
            frame_obj.GetComponent<Image>().sprite = spr_frameColorImg[framecolNum];
            PlayerPrefs.SetInt("frameColors", 0);
        }
        PlayerPrefs.Save();
    }
    
    
    public void changeDeskColor()
    {
        if (deskcolNum < 3)
        { 
            deskcolNum = PlayerPrefs.GetInt("deskColors", 0) + 1;
            desk_obj.GetComponent<Image>().sprite = spr_deskColorImg[deskcolNum];
            PlayerPrefs.SetInt("deskColors", deskcolNum);
        }
        else
        {
            deskcolNum = 0;
            desk_obj.GetComponent<Image>().sprite = spr_deskColorImg[deskcolNum];
            PlayerPrefs.SetInt("deskColors", 0);
        }
        PlayerPrefs.Save();
    }

    void First()
    {

        #region
        int c = 0;
        string str = "";
        if (c == PlayerPrefs.GetInt("first", 0))
        {
            for (int i = 0; i < 16; i++)
            {
                int a = Random.Range(0, 16);//0~15
                switch (a)
                {
                    case 0:
                        str = str + "0";
                        break;
                    case 1:
                        str = str + "1";
                        break;
                    case 2:
                        str = str + "2";
                        break;
                    case 3:
                        str = str + "3";
                        break;
                    case 4:
                        str = str + "4";
                        break;
                    case 5:
                        str = str + "5";
                        break;
                    case 6:
                        str = str + "6";
                        break;
                    case 7:
                        str = str + "7";
                        break;
                    case 8:
                        str = str + "8";
                        break;
                    case 9:
                        str = str + "9";
                        break;
                    case 10:
                        str = str + "a";
                        break;
                    case 11:
                        str = str + "b";
                        break;
                    case 12:
                        str = str + "c";
                        break;
                    case 13:
                        str = str + "d";
                        break;
                    case 14:
                        str = str + "e";
                        break;
                    case 15:
                        str = str + "f";
                        break;
                    default:
                        break;
                }
            }
            PlayerPrefs.SetString("code", str);
            PlayerPrefs.SetInt("first", 1);
            PlayerPrefs.Save();

            PlayerPrefs.SetString("adtimes", System.DateTime.Now.ToString());
        }//endOfIf

        #endregion
    }


    //상자창
    public void ActBoxWin()
    {
        if (boxWin_obj.activeSelf)
        {
            boxWin_obj.SetActive(false);
        }
        else
        {
            boxWin_obj.SetActive(true);

            if (PlayerPrefs.GetInt("bedbox", 0) == 0)
            {
                bedBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            }
            else
            {
                bedBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            }
            if (PlayerPrefs.GetInt("framebox", 0) == 0)
            {
                frameBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            }
            else
            {
                frameBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            }
            if (PlayerPrefs.GetInt("deskbox", 0) == 0)
            {
                deskBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            }
            else
            {
                deskBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            }
            if (PlayerPrefs.GetInt("dalgona", 0) >= 15)
            {
                dalgonaBtn_obj.SetActive(true);
            }

            if (PlayerPrefs.GetInt("dalgonabox", 0) == 0)
            {
                dalgonaBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            }
            else
            {
                dalgonaBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            }
        }
    }

    //상자버튼
    public void BedBox()
    {
        if (PlayerPrefs.GetInt("bedbox", 0) == 0)
        {
            bedBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            bed_obj.SetActive(false);
            PlayerPrefs.SetInt("bedbox", 1);
        }
        else
        {
            bedBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            bed_obj.SetActive(true);
            PlayerPrefs.SetInt("bedbox", 0);
        }
    }

    public void FrameBox()
    {
        if (PlayerPrefs.GetInt("framebox", 0) == 0)
        {
            frameBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            frame_obj.SetActive(false);
            PlayerPrefs.SetInt("framebox", 1);
        }
        else
        {
            frameBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            frame_obj.SetActive(true);
            PlayerPrefs.SetInt("framebox", 0);
        }
    }

    public void DeskBox()
    {
        if (PlayerPrefs.GetInt("deskbox", 0) == 0)
        {
            deskBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            desk_obj.SetActive(false);
            PlayerPrefs.SetInt("deskbox", 1);
        }
        else
        {
            deskBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            desk_obj.SetActive(true);
            PlayerPrefs.SetInt("deskbox", 0);
        }
    }

    //잉어
    public void FishBox()
    {
        if (PlayerPrefs.GetInt("dalgonabox", 0) == 0)
        {
            dalgonaBox_obj.GetComponent<Image>().sprite = spr_boxClose;
            fish_obj.SetActive(false);
            PlayerPrefs.SetInt("dalgonabox", 1);
        }
        else
        {
            dalgonaBox_obj.GetComponent<Image>().sprite = spr_boxOpen;
            fish_obj.SetActive(true);
            PlayerPrefs.SetInt("dalgonabox", 0);
        }
    }

    public void ActHelp()
    {
        if (shopHelp_obj.activeSelf)
        {
            shopHelp_obj.SetActive(false);
        }
        else
        {
            shopHelp_obj.SetActive(true);
        }
    }



    /*
    void first()
    {
        if (PlayerPrefs.GetInt("scene", 0)==1)
        {
            txt_talk.color ="";
        }
        
    } 
    */

}