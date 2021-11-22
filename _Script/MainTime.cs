using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainTime : MonoBehaviour
{
    string str;
    int talk;

    public static int wepRnd, wepShow, baqueRnd, baqueShow;
    public float moveX, moveY, b_moveX, b_moveY;
    public GameObject wep_obj, baques_obj, talk_btn;

    //신문
    System.DateTime lastGudoc, nowGudog;
    int hG, mG;
    public Button btn_gudoc;
    public Text txt_gudoc;
    System.DateTime lastDateGudog;
    System.TimeSpan compareGudog;
    string lastGudog;
    int i_news;
    public GameObject news_obj;


    int h;
    int r,like;


    //전단지
    public int lastgd;
    System.DateTime nowPaper;
    int hGp, mGp;
    public Button btn_paper;
    public Text txt_paper;
    System.DateTime lastDatePaper;
    System.TimeSpan comparePaper;
    string lastPaper;
    public Sprite[] spr_paperFood;

    //물컵
    public GameObject glassBtn, btn_glassShow;
    public Sprite[] spr_glass;
    public int glassWater,w;
    public Text txt_glassShow, txt_rain;


    //전단지
    public GameObject heartpaperImg, btn_heartHelp,hHelp_obj, hPop_obj;
    public GameObject heartpaperEatBtn;
    public GameObject heartpaperChoice;
    public Text heartNotE;

    //거북이
    public GameObject gobok, btn_gobok, img_bless;
    int beuk;
    public float gmoveY, gmoveX;
    //씨앗
    public GameObject seed_obj, seedYN_obj;
    string seedlastTime;
    int now, grow, shours, sminute;
    public Text seedTime_txt;
    public Sprite[] spr_seed;
    public GameObject popUpTime_obj;
    public Text txt_popUpTime, txt_seedW;

    public GameObject talkballoon, closeTB;

    public GameObject popUp_obj;
    public Text txt_popUp;

    public GameObject g,gm;

    //티비
    public GameObject tv_obj;
    int tv=0;
    public Sprite spr_tv1, spr_tv2;

    //외출시간
    public GameObject umbrella_obj, timerPopClock_obj;
    public Sprite[] spr_umbrella;
    public Text txt_goOut;

    //카메라 태그로 찾고 적용
    public GameObject menu_obj;
    public Camera camera_c;

    public GameObject title_obj;
    public GameObject blackAd_obj;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("titlecheck", 0) == 1)
        {
            title_obj.SetActive(false);
        }

        PlayerPrefs.SetInt("titlecheck", 0);

        PlayerPrefs.SetInt("scene", 0);

        //구독전단버튼시간태그로불러오기

        if (PlayerPrefs.GetInt("scene", 0) == 0)
        {

            GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(true);

            g = GameObject.FindGameObjectWithTag("구독");
            btn_gudoc = g.GetComponent<Button>();
            g = GameObject.FindGameObjectWithTag("구독T");
            txt_gudoc = g.GetComponent<Text>();
            g = GameObject.FindGameObjectWithTag("전단");
            btn_paper = g.GetComponent<Button>();
            g = GameObject.FindGameObjectWithTag("전단T");
            txt_paper = g.GetComponent<Text>();

            GameObject.Find("메뉴펼치기").transform.Find("메뉴목록").gameObject.SetActive(false);
        }

        gmoveY = gobok.transform.position.y;
        gmoveX = gobok.transform.position.y;

        //업데이트대신쓴다
        str = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSec");
        StartCoroutine("MoveB");

        talk = PlayerPrefs.GetInt("talk", 5);



        //카메라
        camera_c = Camera.main;
        menu_obj = GameObject.FindGameObjectWithTag("메뉴Canvas");
        menu_obj.GetComponent<Canvas>().worldCamera = camera_c;


        if (PlayerPrefs.GetInt("allTrash", 0) == 99)
        {
            PlayerPrefs.SetInt("backHomeTrash", 999);
        }


    }


    public void closeTitle()
    {
        title_obj.SetActive(false);
    }

    //
    IEnumerator updateSec()
    {
        int a = 0;
        while (a == 0)
        {
            if (PlayerPrefs.GetInt("blad", 0) == 1)
            {
                blackAd_obj.SetActive(false);
                PlayerPrefs.SetInt("blad", 0);
            }
            //최대량 제한 빗물 마음
            if (PlayerPrefs.GetInt(str + "r", 0) > 999999)
            {
                PlayerPrefs.SetInt(str + "r", 999999);
            }

            if (PlayerPrefs.GetInt(str + "h", 0) > 99999)
            {
                PlayerPrefs.SetInt(str + "h", 99999);
            }
            glassWater++;
            if (glassWater >= 30)
            {
                cup();
                glassWater = 0;
            }
            BaquiWep();
            News();
            food();
            SeedTimeFlow();
            CheckOutTime();
            //TvMove();
            PlayerPrefs.Save();
            yield return new WaitForSeconds(1f);
        }
    }


    public void talkBtn()
    {
        talk = PlayerPrefs.GetInt("talk", 5);
        talk--;
        if (talk <= 0)
        {
            talk = 0;
        }
        if (talk >= 5)
        {
            talk = 4;
        }
        PlayerPrefs.SetInt("talk", talk);
    }

    void BaquiWep()
    {

        //거미줄
        if (wepRnd == 1)
        {
            if(moveX==0&& moveX == 0)
            {
                moveX = Random.Range(-7.1f, 7.1f);
                moveY = Random.Range(2.1f, 3.9f);
            }
            wep_obj.SetActive (true);
            wep_obj.transform.position = new Vector3(moveX, moveY, wep_obj.transform.position.z);

        }
        else
        {
            wepRnd = Random.Range(0, 20);
            moveX = Random.Range(-7.1f, 7.1f);
            moveY = Random.Range(2.1f, 3.9f);
            wep_obj.SetActive (false);
        }


        //바퀴벌레
        
        if (baqueShow == 0)
        {
            baqueRnd = 0;
            b_moveX = -14.5f;

            baques_obj.transform.position = new Vector3(b_moveX, baques_obj.transform.position.y, baques_obj.transform.position.z);

            baqueRnd = Random.Range(0, 21);
            if (baqueRnd == 1)
            {
                baqueShow = 1;
                baques_obj.SetActive(true);
            }
        }
        else
        {
        }
        
    }

    void News()
    {

        //신문시간
        nowGudog = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastGudog = PlayerPrefs.GetString("saveGudoc", nowGudog.ToString());
        lastDateGudog = System.DateTime.Parse(lastGudog);
        compareGudog = System.DateTime.Now - lastDateGudog;
        hG = (int)compareGudog.TotalHours;
        mG = (int)compareGudog.TotalMinutes;
        mG = mG - (mG / 60) * 60;
        mG = 59 - mG;
        hG = 11 - hG;
        if (hG < 0)
        {

            btn_gudoc.GetComponent<Button>().interactable = true;
            hG = 0;
            mG = 0;

        }
        else
        {
            btn_gudoc.GetComponent<Button>().interactable = false;
            string stru = string.Format(@"{00:00}" + ":", hG) + string.Format(@"{00:00}", mG);
            txt_gudoc.text = stru;
        }
    }


    public void gudocOpen()
    {
        news_obj.SetActive(true);
        PlayerPrefs.SetString("saveGudoc", System.DateTime.Now.ToString());
        h = PlayerPrefs.GetInt(str + "h", 0);
        r = PlayerPrefs.GetInt(str + "r", 0);
        h = h + 50;
        r = r + 500;
        PlayerPrefs.SetInt(str + "r", r);
        PlayerPrefs.SetInt(str + "h", h);
    }


    public void gudocClose()
    {
        news_obj.SetActive(false);
    }

    void food()
    {

        //시간
        nowPaper = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastPaper = PlayerPrefs.GetString("savePaper", nowPaper.ToString());
        lastDatePaper = System.DateTime.Parse(lastPaper);
        comparePaper = System.DateTime.Now - lastDatePaper;
        hGp = (int)comparePaper.TotalHours;
        mGp = (int)comparePaper.TotalMinutes;
        mGp = mGp - (mGp / 60) * 60;
        mGp = 59 - mGp;
        hGp = 0 - hGp;
        if (hGp < 0)
        {
            btn_paper.GetComponent<Button>().interactable = true;
            hGp = 0;
            mGp = 0;

        }
        else
        {
            btn_paper.GetComponent<Button>().interactable = false;
            string stru = string.Format(@"{00:00}" + ":", hGp) + string.Format(@"{00:00}", mGp);
            txt_paper.text = stru;
        }
    }

    
    public void heartpaperY()
    {//먹는다
        
        h = PlayerPrefs.GetInt(str + "h", 0);
        r = PlayerPrefs.GetInt(str + "r", 0);
        like =PlayerPrefs.GetInt("likepoint", 0);

        heartpaperEatBtn.SetActive(true);
        heartpaperEatBtn.GetComponent<Image>().sprite = spr_paperFood[PlayerPrefs.GetInt("heartpaper", 0)];
        switch (PlayerPrefs.GetInt("heartpaper", 0))
        {
            case 1: //짜
                if (h >= 50)
                {
                    gm.GetComponent<SoundEvt>().heartpaperSound();
                    h = h - 50;
                    like = like + 10;
                    PlayerPrefs.SetInt(str + "h", h);
                    PlayerPrefs.SetInt("likepoint", like);
                    PlayerPrefs.SetString("savePaper", System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                    heartpaperChoice.SetActive(false);
                    heartpaperImg.SetActive(false);
                }
                else
                {
                    heartNotE.text = "마음이 부족하다.";
                    hPop_obj.SetActive(true);
                    heartpaperEatBtn.SetActive(false);
                    gm.GetComponent<SoundEvt>().touchSound();
                }
                break;

            case 2: //볶
                if (h >= 50)
                {
                    gm.GetComponent<SoundEvt>().heartpaperSound();
                    h = h - 50;
                    like = like + 10;
                    PlayerPrefs.SetInt(str + "h", h);
                    PlayerPrefs.SetInt("likepoint", like);
                    PlayerPrefs.SetString("savePaper", System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                    heartpaperChoice.SetActive(false);
                    heartpaperImg.SetActive(false);
                }
                else
                {
                    heartNotE.text = "마음이 부족하다.";
                    hPop_obj.SetActive(true);
                    heartpaperEatBtn.SetActive(false);
                    gm.GetComponent<SoundEvt>().touchSound();
                }
                break;

            case 3: //우
                if (h >= 60)
                {
                    gm.GetComponent<SoundEvt>().heartpaperSound();
                    h = h - 60;
                    like = like + 15;
                    PlayerPrefs.SetInt(str + "h", h);
                    PlayerPrefs.SetInt("likepoint", like);
                    PlayerPrefs.SetString("savePaper", System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                    heartpaperChoice.SetActive(false);
                    heartpaperImg.SetActive(false);
                }
                else
                {
                    heartNotE.text = "마음이 부족하다.";
                    hPop_obj.SetActive(true);
                    heartpaperEatBtn.SetActive(false);
                    gm.GetComponent<SoundEvt>().touchSound();
                }
                break;

            case 4: //짬
                if (h >= 60)
                {
                    gm.GetComponent<SoundEvt>().heartpaperSound();
                    h = h - 60;
                    like = like + 15;
                    PlayerPrefs.SetInt(str + "h", h);
                    PlayerPrefs.SetInt("likepoint", like);
                    PlayerPrefs.SetString("savePaper", System.DateTime.Now.ToString());
                    PlayerPrefs.Save();
                    heartpaperChoice.SetActive(false);
                    heartpaperImg.SetActive(false);
                }
                else
                {
                    heartNotE.text = "마음이 부족하다.";
                    hPop_obj.SetActive(true);
                    heartpaperEatBtn.SetActive(false);
                    gm.GetComponent<SoundEvt>().touchSound();
                }
                break;
            default:
                break;
        }
    }
    
    public void heartpaperN()
    {//안
        heartpaperChoice.SetActive(false);
    }

    public void food1()
    {//짜장
        PlayerPrefs.SetInt("heartpaper", 1);
        heartpaperChoice.SetActive(true);
    }
    public void food2()
    {//볶음밥
        PlayerPrefs.SetInt("heartpaper", 2);
        heartpaperChoice.SetActive(true);
    }
    public void food3()
    {//짬뽕
        PlayerPrefs.SetInt("heartpaper", 3);
        heartpaperChoice.SetActive(true);
    }
    public void food4()
    {//우동
        PlayerPrefs.SetInt("heartpaper", 4);
        heartpaperChoice.SetActive(true);
    }

    public void CloseBedal()
    {
        heartpaperEatBtn.SetActive(false);
    }
    
    public void HeartpaperHelp()
    {
        if (hHelp_obj.activeSelf)
        {
            hHelp_obj.SetActive(false);
        }
        else
        {
            hHelp_obj.SetActive(true);
        }
    }

    void cup()
    {

        //if (PlayerPrefs.GetInt("glass", 0) == 1)
        //{
            w = PlayerPrefs.GetInt("water", 0);
            if (w < 1)
            { //0
                glassBtn.GetComponent<Image>().sprite = spr_glass[0];
            }
            else if (w < 49)
            { //1~249
                glassBtn.GetComponent<Image>().sprite = spr_glass[1];
            }
            else
            { //250
                glassBtn.GetComponent<Image>().sprite = spr_glass[2];
            }
            if(w > 49)
            {
                w = 49;
            }
            PlayerPrefs.SetInt("water", w + 1);
        //}
    }


    public void getGlassWater()
    {
        int k = 0;
        k = PlayerPrefs.GetInt("water", 0) * 5;
        txt_glassShow.text = "물을 " + k + "만큼 모았다.";
        k = PlayerPrefs.GetInt(str + "r", 0) + k;
        PlayerPrefs.SetInt(str + "r", k);
        PlayerPrefs.SetInt("water", 0);
        PlayerPrefs.Save();
        glassBtn.GetComponent<Image>().sprite = spr_glass[0];
        btn_glassShow.SetActive(true);
        txt_rain.text = "" + PlayerPrefs.GetInt(str + "r", 0);

    }

    void tutle()
    {
        if (PlayerPrefs.GetInt("sleepmotion", 0) == 1)
        {
            
        }

        if (gmoveX <= -5)
        {
            beuk = 1;
            gobok.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (gmoveX >= 6.5)
        {
            beuk = 0;
            gobok.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (beuk == 1)
        {
            gmoveX = gmoveX + 0.01f;
        }
        else
        {
            gmoveX = gmoveX - 0.01f;
        }
        gobok.transform.position = new Vector3(gmoveX, gmoveY, gobok.transform.position.z);
    }

    public void ActBeadal()
    {
        


        if (heartpaperImg.activeSelf)
        {
            heartpaperImg.SetActive(false);
        }
        else
        {
            heartpaperImg.SetActive(true);
        }
    }


    //씨앗 자라게 텍스트 출력 끝나고 나온다
    public void GrowSeed()
    {
        now = PlayerPrefs.GetInt("seedlv", 0);
        grow = PlayerPrefs.GetInt("seedgrow", 1);
        if (grow >= 9)
        {
            if (PlayerPrefs.GetInt("infoflower", 0)==0)
            {
                PlayerPrefs.SetInt("infoflower", 1);
            }
        }
        else
        {
            //만렙인가? 아니면
            //12시간이지났는가?
            if (now > grow)
            {
                //물을 줄수 없음

                popUpTime_obj.SetActive(true);
                txt_popUpTime.text = "아직 축축하다.";
            }
            else
            {
                //물을 줄수 있음
                seedYN_obj.SetActive(true);
                txt_seedW.text = "물을 " + PlayerPrefs.GetInt("seedlv", 0) * 1000 + " 줄까?";
            }
        }

    }

    public void SeedY()
    {

        int fl;
        fl = PlayerPrefs.GetInt("seedlv", 1);
        if (fl >= 9)
        {

        }
        else
        {

            talkballoon.SetActive(false);
            closeTB.SetActive(false);
            closeTB.GetComponent<Button>().interactable = false;
            r = PlayerPrefs.GetInt(str + "r", 0);
            fl = fl * 1000;
            if (r >= fl)
            {//돈이있니?
                r = r - fl;
                fl = PlayerPrefs.GetInt("seedlv", 0);
                PlayerPrefs.SetInt("seedlv", fl + 1);
                PlayerPrefs.SetInt(str + "r", r);

                PlayerPrefs.SetString("seedLastTime", System.DateTime.Now.ToString());

                PlayerPrefs.SetInt("like", PlayerPrefs.GetInt("like", 0) + 4);
            }
            else
            {
                popUp_obj.SetActive(true);
                txt_popUp.text = "물이 부족해..";
            }

        }//end of else

        seedYN_obj.SetActive(false);
    }
    public void SeedN()
    {
        talkballoon.SetActive(false);
        closeTB.SetActive(false);
        closeTB.GetComponent<Button>().interactable = false;

        seedYN_obj.SetActive(false);
    }


    void SeedTimeFlow()
    {
        now = PlayerPrefs.GetInt("seedlv", 0);
        grow = PlayerPrefs.GetInt("seedgrow", 1);
        System.DateTime d = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        seedlastTime = PlayerPrefs.GetString("seedLastTime", d.ToString());
        System.DateTime lastDateTime = System.DateTime.Parse(seedlastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        shours = (int)compareTime.TotalHours;
        sminute = (int)compareTime.TotalMinutes;
        sminute = sminute - (sminute / 60) * 60;
        sminute = 59 - sminute;
        shours = 11 - shours;


        string strb = string.Format(@"{00:00}" + ":", shours) + string.Format(@"{00:00}", sminute);
        seedTime_txt.text = strb;
        if (sminute <= 0 && shours == 0)
        {
            shours = -1;
        }
        if (shours < 0)
        {
            seedTime_txt.text = "00:00";
            if (now > grow)
            {
                grow = PlayerPrefs.GetInt("seedgrow", 1);
                grow++;
                PlayerPrefs.SetInt("seedgrow", grow);
                seed_obj.GetComponent<Image>().sprite = spr_seed[grow];
                PlayerPrefs.Save();
            }
        }
        //seed_obj.GetComponent<Image>().sprite = spr_seed[grow-1];
    }

    public void ClosePopUpTime()
    {
        popUpTime_obj.SetActive(false);
        timerPopClock_obj.SetActive(false);
    }

    //티비
    void TvMove()
    {
        if (tv == 0)
        {
            tv_obj.GetComponent<Image>().sprite = spr_tv2;
            tv = 1;
        }
        else
        {
            tv_obj.GetComponent<Image>().sprite = spr_tv1;
            tv = 0;
        }
    }

    /// <summary>
    /// 외출시간
    /// </summary>
    /// <returns></returns>
    void CheckOutTime()
    {

        System.DateTime now;
        string lastTime;
        int ac, acb;
        //외출시간
        now = new System.DateTime(1980, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);
        lastTime = PlayerPrefs.GetString("outtime", now.ToString());
        System.DateTime lastDateTime = System.DateTime.Parse(lastTime);
        System.TimeSpan compareTime = System.DateTime.Now - lastDateTime;
        ac = (int)compareTime.TotalMinutes;
        acb = (int)compareTime.TotalSeconds;
        acb = acb - (acb / 60) * 60;
        acb = 59 - acb;
        ac = 14 - ac;////////////////////////////////////////////////////////////
        if (PlayerPrefs.GetInt("outtimecut", 0)==4)
        {
            ac = ac - 10;
        }
        if (ac < 0)
        {
            umbrella_obj.GetComponent<Image>().sprite = spr_umbrella[0];
        }
        else
        {
            umbrella_obj.GetComponent<Image>().sprite = spr_umbrella[1];
        }

        //외출남은시간
        if (ac < 0)
        {
            txt_goOut.text = "00:00";
        }
        else
        {
            string stru = string.Format(@"{00:00}" + ":", ac) + string.Format(@"{00:00}", acb);
            txt_goOut.text = stru;
        }
    }


    IEnumerator MoveB()
    {
        int a = 0;
        while (a == 0)
        {
            tutle();
            if (baqueShow == 1)
            {
                b_moveX = baques_obj.transform.position.x + (0.1f);//(1.5f * Time.deltaTime);
                baques_obj.transform.position = new Vector3(b_moveX, baques_obj.transform.position.y, baques_obj.transform.position.z);
                if (b_moveX > 14)
                {
                    baqueShow = 0;
                }
                if (baqueShow == 0)
                {
                    baqueRnd = 0;
                    b_moveX = -14.5f;
                    baques_obj.transform.position = new Vector3(b_moveX, baques_obj.transform.position.y, baques_obj.transform.position.z);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
    
}
