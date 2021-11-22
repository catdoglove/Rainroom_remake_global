using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class CityTalk : MonoBehaviour
{
    List<Dictionary<string, object>> data_talk, data_eat; //csv파일
    int etcNum = 0;
    public Text Text_obj; //선언 및 보여질
    string[] testText_cut; //대사 끊기
    string text_str; //실질적 대사출력

    public GameObject talkbtn, talkballoon, closeTB, talkCursor, eatballon; //대화버튼 및 영역
    int ckFood;

    int[] allArr = new int[2];
    int loveLv = 0; //호감도 단계라고 생각하면 됨
    int countTalkNum;//대화횟수

    float speedF = 0.05f;
    int nowArr = 0; //현재 줄
    int[] randArr, randArr1, randArr2;//난수 필

    //질문만들기
    string quesStr; //질문용대화
    public Text btnTxt1, btnTxt2; //질문버튼 텍스트
    public GameObject quesBtmArea, quesBack; //질문버튼, 뒤
    int choiceNum; //예스or노

    //캐릭터 변환
    public Animator charAni;
    public Sprite[] chareat;

    public GameObject obj_cafe, obj_bunsik,charEat, obj_cafe_YN, obj_bunsik_YN, obj_bunsik_price,cafe_open_img, cafe_btn,toast_obj,bunsik_txt_heart;
    public Text txt_cafe_YN, txt_bunsik_YN, toast_txt;
    public Sprite[] spr_cafe;

    public GameObject GM, gmS;

    string str_Code;
    int have_h, cost_h;    
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toast_obj.SetActive(true);
            toast_txt.text = "여기서는 헤어질 수 없다.";
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("likelv", 0) >= 8) //호감도이상일경우
        {
            cafe_open_img.GetComponent<Image>().sprite = spr_cafe[0];
            cafe_btn.SetActive(true);
        }

        data_talk = CSVReader.Read("CSV/talk_out");
        data_eat = CSVReader.Read("CSV/city_eat");

        allArr[0] = 100;//대사
        allArr[1] = 10; //음식

        str_Code = PlayerPrefs.GetString("code", "");

    }

    void cleantalk() //대화 초기화
    {
        Text_obj.text = "";
        text_str = "";
    }


    void lineReload(int code) // 대화 차례대로 보여주기 및 대화줄 초기화
    {
        if (nowArr == 0) // 난수 돌리기
        {
            GetRandomInt(allArr[code]); //테스트 0
            nowArr++;
        }
        else if (nowArr < allArr[code]) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= allArr[code]) //대화 줄 초기화
        {
            GetRandomInt(allArr[code]);
            nowArr = 0;
            nowArr++;
        }

    }

    public int[] GetRandomInt(int length) //중복없는 난수생성기
    {
        bool isSame;

        switch (length)
        {
            case 100://대사

                randArr = new int[length];

                for (int i = 0; i < length; ++i)
                {
                    while (true)
                    {
                        randArr[i] = Random.Range(0, length); //0~(line_txt-1)
                        isSame = false;

                        for (int j = 0; j < i; ++j)
                        {
                            if (randArr[j] == randArr[i])
                            {
                                isSame = true;
                                break;
                            }
                        }
                        if (!isSame) break;
                    }
                }
                return randArr;


                break;

            case 10://음식

                randArr1 = new int[length];

                for (int i = 0; i < length; ++i)
                {
                    while (true)
                    {
                        randArr1[i] = Random.Range(0, length); //0~(line_txt-1)
                        isSame = false;

                        for (int j = 0; j < i; ++j)
                        {
                            if (randArr1[j] == randArr1[i])
                            {
                                isSame = true;
                                break;
                            }
                        }
                        if (!isSame) break;
                    }
                }
                return randArr1;

                break;
        }

        return null;
    }


    public void talkCity() //대사치기
    {
        if (PlayerPrefs.GetInt("talkcityCK", 0) == 88)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkcityCK", 99);
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        //Debug.Log(countTalkNum);

        //if (countTalkNum == 0)
        // {
        //Debug.Log("대화횟수마감");
        // }
        // else
        // {

        //소리
        //Audio_obj.GetComponent<SoundEvt>().talkSound();

        if (PlayerPrefs.GetInt("talk", 5) <= 0)
        {

        }
        else
        {
            TalkSound();
            int a = PlayerPrefs.GetInt("likepoint", 0);
            a = a + 1;
            PlayerPrefs.SetInt("likepoint", a);
            lineReload(0);

            text_str = "" + data_talk[randArr[nowArr - 1]]["park"];

            testText_cut = text_str.Split('/'); //끊기
            cleantalk();
            HeartPlus();
            if (testText_cut[0] == "q")
            { //질문이 있는경우
                StartCoroutine("questionTalkRun");
            }
            else
            {
                StartCoroutine("talkRun");
            }
        }
    }


    void talkBunsik(string str)
    {
        eatFalseObject();
        if (PlayerPrefs.GetInt("talkcityCK", 0) == 99)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkcityCK", 88);
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);

        //소리
        //Audio_obj.GetComponent<SoundEvt>().talkSound();
        lineReload(1);
        text_str = "" + data_eat[randArr1[nowArr - 1]][str];
        testText_cut = text_str.Split('/'); //끊기
        cleantalk();
        StartCoroutine("talkRun");
        StartCoroutine("eatFood");
    }

    public void talkEat0() //순대
    {
        eatYN();
        txt_bunsik_YN.text = "순대의 가격은 80";
        ckFood = 0;

    }

    public void talkEat1() //떡볶이
    {
        eatYN();
        txt_bunsik_YN.text = "떡볶이 가격은 70";
        ckFood = 1;
    }

    public void talkEat2() //어묵
    {
        eatYN();
        txt_bunsik_YN.text = "어묵의 가격은 50";
        ckFood = 2;
    }

    public void talkEat3() //튀김
    {
        eatYN();
        txt_bunsik_YN.text = "튀김의 가격은 70";
        ckFood = 3;
    }


    void talkCafe(string str)
    {
        eatFalseObject();
        if (PlayerPrefs.GetInt("talkcityCK", 0) == 99)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkcityCK", 88);
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);

        //소리
        //Audio_obj.GetComponent<SoundEvt>().talkSound();

        lineReload(1);
        text_str = "" + data_eat[randArr1[nowArr - 1]][str];
        testText_cut = text_str.Split('/'); //끊기
        cleantalk();
        StartCoroutine("talkRun");
        StartCoroutine("eatFood");
    }





    public void talkEat4() //커피
    {
        txt_cafe_YN.text = "커피";
        ckFood = 4;
        eatYN();
    }

    public void talkEat4_2() //차
    {
        txt_cafe_YN.text = "차";
        ckFood = 44;
        eatYN();
    }

    public void talkEat5() //과일주스
    {
        txt_cafe_YN.text = "과일주스";
        ckFood = 5;
        eatYN();
    }

    public void talkEat6() //아이스크림
    {
        txt_cafe_YN.text = "아이스크림";
        ckFood = 6;
        eatYN();
    }

    public void talkEat7() //빵
    {
        txt_cafe_YN.text = "빵";
        ckFood = 7;
        eatYN();
    }

    public void talkEat8() //쿠키
    {
        txt_cafe_YN.text = "쿠키";
        ckFood = 8;
        eatYN();
    }

    public void talkEat9() //샌드위치
    {
        eatYN();
        txt_cafe_YN.text = "샌드위치";
        ckFood = 9;
    }

    public void talkEat9_10() //마카롱
    {
        txt_cafe_YN.text = "마카롱";
        ckFood = 10;
        eatYN();
    }

    //대사 출력
    IEnumerator talkRun()
    {
        falseObject();
        for (int i = 0; i < testText_cut.Length; i++)
        {
            text_str = text_str.Insert(text_str.Length, testText_cut[i]);
            Text_obj.text = text_str;
            yield return new WaitForSeconds(speedF);
        }
        trueObject();
    }

    //질문 출력
    IEnumerator questionTalkRun()
    {
        falseObject();
        closeTB.SetActive(false);
        quesBack.SetActive(true);
        quesStr = " ";
        for (int i = 0; i < testText_cut.Length; i++)
        {
            quesStr = quesStr.Insert(quesStr.Length, testText_cut[i]);
        }

        for (int i = 1; i < testText_cut.Length; i++)
        {
            text_str = text_str.Insert(text_str.Length, testText_cut[i]);

            if (text_str.Contains("y"))
            {
                string str, str2;
                str = quesStr.Substring(quesStr.IndexOf("y") + 1, 4);
                btnTxt1.text = str;
                str2 = quesStr.Substring(quesStr.IndexOf("n") + 1, 4);
                btnTxt2.text = str2;
            }
            else
            {
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        quesBtmArea.SetActive(true);
    }

    //선택한 질문 출력
    IEnumerator choiceTextRun()
    {
        falseObject();

        quesStr = " ";
        for (int i = 0; i < testText_cut.Length; i++)
        {
            quesStr = quesStr.Insert(quesStr.Length, testText_cut[i]);
        }

        if (choiceNum == 1)
        {
            for (int i = quesStr.IndexOf("+"); i < quesStr.IndexOf("-") - 1; i++)
            {
                text_str = text_str.Insert(text_str.Length, testText_cut[i]);
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        else if (choiceNum == 2)
        {
            for (int i = quesStr.IndexOf("*"); i < quesStr.IndexOf("=") - 1; i++)
            {
                text_str = text_str.Insert(text_str.Length, testText_cut[i]);
                Text_obj.text = text_str;
                yield return new WaitForSeconds(speedF);
            }
        }
        trueObject();
    }


    //질문버튼
    public void choiceBtn1()
    {
        cleantalk();
        choiceNum = 1;
        StartCoroutine("choiceTextRun");

    }

    public void choiceBtn2()
    {
        cleantalk();
        choiceNum = 2;
        StartCoroutine("choiceTextRun");

    }



    void falseObject()
    {
        charAni.Play("city_char_talk");
        talkballoon.SetActive(true);
        closeTB.GetComponent<Button>().interactable = false;
        closeTB.SetActive(true);

        talkbtn.SetActive(false);
        quesBtmArea.SetActive(false);
        quesBack.SetActive(false);
        talkCursor.SetActive(false);
        
    }


    void trueObject()
    {

        talkCursor.SetActive(true);
        closeTB.GetComponent<Button>().interactable = true;
        talkbtn.SetActive(true);

        if((PlayerPrefs.GetInt("talkcityCK", 0) == 88)){

        }else{
            charAni.Play("city_char");
        }

    }


    public void closeTalkBoon()
    {
        talkballoon.SetActive(false);
        closeTB.SetActive(false);
        closeTB.GetComponent<Button>().interactable = false;
    }



    IEnumerator eatFood()
    {
        charEat.SetActive(true);
        charAni.Play("city_char_eat");

        switch (ckFood)
        {
            case 0: //순대
                charEat.GetComponent<Image>().sprite = chareat[0];
                break;

            case 1: //떡볶이
                charEat.GetComponent<Image>().sprite = chareat[1];
                break;

            case 2: //어묵
                charEat.GetComponent<Image>().sprite = chareat[2];
                break;

            case 3: //튀김
                charEat.GetComponent<Image>().sprite = chareat[3];
                break;

            case 4: //커피와 차
                charEat.GetComponent<Image>().sprite = chareat[4];
                break;

            case 44: //커피와 차
                charEat.GetComponent<Image>().sprite = chareat[4];
                break;

            case 5: //과일주스
                charEat.GetComponent<Image>().sprite = chareat[5];
                break;

            case 6: //아이스크림
                charEat.GetComponent<Image>().sprite = chareat[6];
                break;

            case 7: //빵
                charEat.GetComponent<Image>().sprite = chareat[7];
                break;

            case 8: //쿠키
                charEat.GetComponent<Image>().sprite = chareat[8];
                break;

            case 9: //샌드위치
                charEat.GetComponent<Image>().sprite = chareat[9];
                break;

            case 10: //마카롱
                charEat.GetComponent<Image>().sprite = chareat[10];
                break;


        } 

        yield return new WaitForSeconds(2f);

        charAni.Play("city_char");
        charEat.SetActive(false);
    }

    public void openCafe()
    {
        obj_cafe.SetActive(true);
    }
        
    public void closeYN()
    {
        obj_bunsik_price.SetActive(false);
        obj_bunsik_YN.SetActive(false);
        obj_cafe_YN.SetActive(false);
    }

    public void openBunsik()
    {
        obj_bunsik.SetActive(true);
    }

    public void eatFalseObject()
    {
        obj_cafe.SetActive(false);
        obj_cafe_YN.SetActive(false);
        obj_bunsik.SetActive(false);
        obj_bunsik_YN.SetActive(false);
        obj_bunsik_price.SetActive(false);
    }

    void eatYN() //이걸로 할까창
    {
        obj_cafe_YN.SetActive(true);
        obj_bunsik_price.SetActive(true);
        obj_bunsik_YN.SetActive(true);

        bunsik_txt_heart.SetActive(true);
    }


    public void eatNo()
    {
        obj_cafe_YN.SetActive(false);
        obj_bunsik_price.SetActive(false);
        obj_bunsik_YN.SetActive(false);
    }

    public void eatYES() 
    {


        have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
        int lp = PlayerPrefs.GetInt("likepoint", 0);

        switch (ckFood)
        {
            case 0: //순대

                cost_h = 80;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 13;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkBunsik("순대");
                    PlayerPrefs.Save();
                }
                else
                {
                    txt_bunsik_YN.text = "음.. 마음이 부족하구나";
                    bunsik_txt_heart.SetActive(false);
                }

                break;

            case 1: //떡볶이

                cost_h = 70;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 11;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkBunsik("떡볶이");
                    PlayerPrefs.Save();
                }
                else
                {
                    txt_bunsik_YN.text = "음.. 마음이 부족하구나";
                    bunsik_txt_heart.SetActive(false);
                }

                break;

            case 2: //어묵

                cost_h = 50;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 9;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkBunsik("어묵");
                    PlayerPrefs.Save();
                }
                else
                {
                    txt_bunsik_YN.text = "음.. 마음이 부족하구나";
                    bunsik_txt_heart.SetActive(false);
                }
                break;

            case 3: //튀김

                cost_h = 70;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 15;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkBunsik("튀김");
                    PlayerPrefs.Save();
                }
                else
                {
                    txt_bunsik_YN.text = "음.. 마음이 부족하구나";
                    bunsik_txt_heart.SetActive(false);
                }
                break;

            case 4: //커피

                cost_h = 60;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 11;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("커피");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }

                break;

            case 44: //차

                cost_h = 60;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 11;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("차차");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;

            case 5: //과일주스

                cost_h = 80;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 15;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("과일");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }

                break;

            case 6: //아이스크림

                cost_h = 100;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 17;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("아이스");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;

            case 7: //빵
                cost_h = 80;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 15;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("빠앙");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;

            case 8: //쿠키
                cost_h = 80;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 15;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("쿠키");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;

            case 9: //샌드위치
                cost_h = 120;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 20;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("샌드위치");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;

            case 10: //마카롱
                cost_h = 150;

                if (have_h >= cost_h)
                {
                    GM.GetComponent<SoundEvt>().eatCitySound();
                    lp = lp + 25;
                    have_h = have_h - cost_h;
                    PlayerPrefs.SetInt(str_Code + "h", have_h);
                    talkCafe("마카롱");
                    PlayerPrefs.Save();
                }
                else
                {
                    toast_obj.SetActive(true);
                    toast_txt.text = "마음이 부족하다.";
                }
                break;
        }


        PlayerPrefs.SetInt("likepoint", lp);
    }
    
    /// <summary>
    /// 레벨에따라 마음을 더해준다
    /// </summary>
    void HeartPlus()
    {
        int hp = PlayerPrefs.GetInt(str_Code + "h", 0);
        switch (PlayerPrefs.GetInt("likelv", 0))
        {
            case 0:
                hp = hp + 2;
                break;

            case 1:
                hp = hp + 3;
                break;

            case 2:
                hp = hp + 4;
                break;

            case 3:
                hp = hp + 5;
                break;

            case 4:
                hp = hp + 6;
                break;

            default:
                hp = hp + 7;
                break;
        }
        if (PlayerPrefs.GetInt("infoflower", 0) == 1)
        {
            hp++;
        }
        PlayerPrefs.SetInt(str_Code + "h", hp);
        PlayerPrefs.Save();
    }

    public void TalkSound()
    {
        gmS.GetComponent<SoundEvt>().touchSound();
    }
}
