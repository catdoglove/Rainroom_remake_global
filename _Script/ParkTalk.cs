using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class ParkTalk : MonoBehaviour
{
    List<Dictionary<string, object>> data_talk, data_etc; //csv파일
   // int etcNum = 0;
    public Text Text_obj, dal_Text_obj; //선언 및 보여질
    string[] testText_cut; //대사 끊기
    string text_str; //실질적 대사출력
    public GameObject gmS;

    public GameObject talkbtn, talkballoon, talkcatballoon, closeTB, talkCursor, dalgonaballon; //대화버튼 및 영역
    int ckk, ck_cat, ck_dal;
    public GameObject catheart, cat_eat_txt, cat_enough_txt, cat_already_txt, dal_eat_txt, dal_enough_txt, dal_already_txt;

    int[] allArr = new int[3]; 
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

    string str_Code;
    int have_h, cost_h;
    
    public GameObject GM;

    public GameObject catPop_obj;
    public Text txt_pop;

    private string[] lineStr;
    private int cnt;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            catPop_obj.SetActive(true);
            txt_pop.text = "(Exit the game is available at home)";
        }

    }

    async void csvvreader()
    {
        data_talk = await CSVReader.ReadAsync("Assets/CSV/talk_out.csv");
        data_etc = await CSVReader.ReadAsync("Assets/CSV/etc_park.csv");
    }


    // Start is called before the first frame update
    void Start()
    {
        csvvreader();

        allArr[0] = 100;//대사
        allArr[1] = 15; //쓰레기
        allArr[2] = 20; //고양이

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


               // break;

            case 15://쓰레기
                
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

             //   break;

            case 20://고양이


                randArr2 = new int[length];

                for (int i = 0; i < length; ++i)
                {
                    while (true)
                    {
                        randArr2[i] = Random.Range(0, length); //0~(line_txt-1)
                        isSame = false;

                        for (int j = 0; j < i; ++j)
                        {
                            if (randArr2[j] == randArr2[i])
                            {
                                isSame = true;
                                break;
                            }
                        }
                        if (!isSame) break;
                    }
                }
                return randArr2;

            //    break;
        }

        return null;
    }


    public void talkPark() //대사치기
    {
        if (PlayerPrefs.GetInt("talkparkCK", 0) == 88 || PlayerPrefs.GetInt("talkparkCK", 0) == 77)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkparkCK", 99);
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
            cleantalk();
            TalkSound();
            int a = PlayerPrefs.GetInt("likepoint", 0);
            a = a + 1;
            PlayerPrefs.SetInt("likepoint", a);
            lineReload(0);

            text_str = "" + data_talk[randArr[nowArr - 1]]["park"];

            Text_obj.text = "";
            if (text_str.Contains("^"))
            { //질문이 있는경우
                lineStr = text_str.ToString().Split('|'); // 0:질문 1:대답버튼 2:1번의 대답 3:대답버튼 4:3번의 대답   
                StartCoroutine("questionTalkRun");
            }
            else
            {
                StartCoroutine(talkRun(speedF));
            }

            HeartPlus();
        }
    }

    public void talkTrash() //쓰레기대사
    {
        cleantalk();
        if (PlayerPrefs.GetInt("talkparkCK", 0) == 99 || PlayerPrefs.GetInt("talkparkCK", 0) == 77)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkparkCK", 88);
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);

        //소리
        //Audio_obj.GetComponent<SoundEvt>().talkSound();

        lineReload(1);

        text_str = "" + data_etc[randArr1[nowArr - 1]]["trash"];

        StartCoroutine(talkRun(speedF));

    }

    public void talkCat() //고양이대사
    {
        cleantalk();
        if (PlayerPrefs.GetInt("talkparkCK", 0) == 99 || PlayerPrefs.GetInt("talkparkCK", 0) == 88)
        {
            nowArr = 0;
        }
        PlayerPrefs.SetInt("talkparkCK", 77);
        ckk = 99;

        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);

        lineReload(2);

        text_str = "" + data_etc[randArr2[nowArr - 1]]["cat"];

        StartCoroutine(talkRun(speedF));
    }

    //고양이 먹이줌
    public void catYes()
    {

        

        if (ck_cat == 1)
        {
            cat_already_txt.SetActive(true);
            cat_eat_txt.SetActive(false);
            cat_enough_txt.SetActive(false);
            GM.GetComponent<SoundEvt>().touchSound();
        }
        else
        {
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_h = 50;


            if (have_h >= cost_h)
            {
                talkcatballoon.SetActive(false);
                talkballoon.SetActive(false);
                quesBack.SetActive(false);
                talkbtn.SetActive(true);

                ck_cat = 1;
                have_h = have_h - cost_h;
                PlayerPrefs.SetInt(str_Code + "h", have_h);
                catheart.SetActive(true);
                PlayerPrefs.Save();
                GM.GetComponent<SoundEvt>().catSound();

                //발도장
                int ct= PlayerPrefs.GetInt("catlike", 0);
                ct++;
                PlayerPrefs.SetInt("catlike", ct);
                if (PlayerPrefs.GetInt("catlike", 0) >= 15)
                {//성장완료
                    if (PlayerPrefs.GetInt("catlove", 0) == 0)
                    {
                        catPop_obj.SetActive(true);
                        txt_pop.text= "I got close to the cat." + "\n"+ "There's something on the Info window.";
                        PlayerPrefs.SetInt("catlove", 1);
                    }
                }
            }
            else
            {
                cat_enough_txt.SetActive(true);
                cat_already_txt.SetActive(false);
                cat_eat_txt.SetActive(false);
                GM.GetComponent<SoundEvt>().touchSound();
            }
        }


    }
    public void catNo()
    {
        talkcatballoon.SetActive(false);
        talkballoon.SetActive(false);
        quesBack.SetActive(false);
        talkbtn.SetActive(true);
    }

    //대사 출력
    IEnumerator talkRun(float f)
    {
        falseObject();
        cnt = 0;
        while (cnt != text_str.Length)
        {
            if (cnt < text_str.Length)
            {
                Text_obj.text += text_str[cnt].ToString();
                cnt++;
            }
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
        //   quesStr = " ";
        btnTxt1.text = "";
        btnTxt2.text = "";
        cnt = 1;
        while (cnt != lineStr[0].Length)
        {
            if (cnt < lineStr[0].Length)
            {
                Text_obj.text += lineStr[0][cnt].ToString();
                cnt++;
            }
            yield return new WaitForSeconds(speedF);
        }

        btnTxt1.text += lineStr[1].ToString();
        btnTxt2.text += lineStr[3].ToString();

        quesBtmArea.SetActive(true);
    }

    //선택한 질문 출력
    IEnumerator choiceTextRun()
    {
        falseObject();

        //  quesStr = " ";
        cnt = 0;

        if (choiceNum == 1)
        {
            while (cnt != lineStr[2].Length)
            {
                if (cnt < lineStr[2].Length)
                {
                    Text_obj.text += lineStr[2][cnt].ToString();
                    cnt++;
                }
                yield return new WaitForSeconds(speedF);
            }
        }
        else if (choiceNum == 2)
        {
            while (cnt != lineStr[4].Length)
            {
                if (cnt < lineStr[4].Length)
                {
                    Text_obj.text += lineStr[4][cnt].ToString();
                    cnt++;
                }
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
        charAni.Play("park_char_talk");
        talkballoon.SetActive(true);
        closeTB.GetComponent<Button>().interactable = false;
        closeTB.SetActive(true);

        talkbtn.SetActive(false);
        quesBtmArea.SetActive(false);
        quesBack.SetActive(false);
        talkCursor.SetActive(false);

        dalgonaClose();

    }


    void trueObject()
    {

        if (ckk == 99) //고양이대사
        {
            talkcatballoon.SetActive(true);
            quesBack.SetActive(true);
            closeTB.SetActive(false);
            ckk = 0;

            cat_eat_txt.SetActive(true);
            cat_enough_txt.SetActive(false);
            cat_already_txt.SetActive(false);
        }
        else
        {
            talkCursor.SetActive(true);
            closeTB.GetComponent<Button>().interactable = true;
            talkbtn.SetActive(true);
        }

        charAni.Play("park_char");

    }


    public void closeTalkBoon()
    {
        talkballoon.SetActive(false);
        closeTB.SetActive(false);
        closeTB.GetComponent<Button>().interactable = false;
    }


    public void dalgonaOpen()
    {
        dalgonaballon.SetActive(true);

        dal_already_txt.SetActive(false);
        dal_eat_txt.SetActive(true);
        dal_enough_txt.SetActive(false);
    }

    public void dalgonaClose()
    {
        dalgonaballon.SetActive(false);
    }
    
    public void dalgonaEat()
    {

        if (ck_dal == 1) //먹은경우
        {
            dal_already_txt.SetActive(true);
            dal_eat_txt.SetActive(false);
            dal_enough_txt.SetActive(false);
            GM.GetComponent<SoundEvt>().touchSound();
        }
        else
        {
            have_h = PlayerPrefs.GetInt(str_Code + "h", 0);
            cost_h = 50;

            if (have_h >= cost_h)
            {
                int a = PlayerPrefs.GetInt("likepoint", 0);
                a = a + 1;
                PlayerPrefs.SetInt("likepoint", a);

                have_h = have_h - cost_h;
                PlayerPrefs.SetInt(str_Code + "h", have_h);
                dalgonaballon.SetActive(false);
                StartCoroutine("eatDalgona");
                PlayerPrefs.Save();
                ck_dal = 1;

                GM.GetComponent<SoundEvt>().eatGoldSound();


                PlayerPrefs.SetInt("dalgona", PlayerPrefs.GetInt("dalgona", 0) + 1);
                //잉어
                if (PlayerPrefs.GetInt("dalgona", 0) == 15)
                {
                    catPop_obj.SetActive(true);
                    txt_pop.text = "I smelled something sweet." + "\n" + "Let's check the room.";
                }
            }
            else
            {
                dal_already_txt.SetActive(false);
                dal_eat_txt.SetActive(false);
                dal_enough_txt.SetActive(true);
                GM.GetComponent<SoundEvt>().touchSound();

            }
        }
    }

    IEnumerator eatDalgona()
    {
        charAni.Play("park_char_eat");
        yield return new WaitForSeconds(3f);
        charAni.Play("park_char");
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
