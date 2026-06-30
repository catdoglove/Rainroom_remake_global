using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class RoomTalk : MonoBehaviour
{

    List<Dictionary<string, object>> data_talk, data_item; //csv파일
   // int etcNum = 0;
    public Text Text_obj; //선언 및 보여질
    string[] testText_cut; //대사 끊기
    string text_str; //실질적 대사출력

    public GameObject talkbtn, itembtn, talkballoon, closeTB, talkCursor; //대화버튼 및 영역
    bool ihaveitem;

    int[] allArr = new int[6]; //총 호감단계
    int loveLv = 0; //호감도 단계라고 생각하면 됨
    int countTalkNum;//대화횟수

    float speedF = 0.05f;
    int nowArr = 0; //현재 줄
    int[] randArr;//난수 필

    //질문만들기
    string quesStr; //질문용대화
    public Text btnTxt1, btnTxt2; //질문버튼 텍스트
    public GameObject quesBtmArea, quesBack; //질문버튼, 뒤
    int choiceNum; //예스or노

    //아이템 관련- 0책, 1벽지, 2전등, 3창문, 4씨앗, 5그림, 6시계
    int[] itemLv = new int[7]; // 등급
    int itemAllArr; //총 줄수 
    int itemNowArr; //현재 줄

    public GameObject change_turtle, turtle_btn;
    int num_turtle;

    //캐릭터 변환
    public Animator charAni;

    public GameObject pop_obj;
    public Text txt_pop;

    //종료
    int exit_int, exitTalk;
    int cnt_exit;
    public string toastTxt;

    public GameObject seedRain_obj, seedTxt_obj,gmS;

    string str_Code;

    private string[] lineStr;
    private int cnt;

    private void toastFunction()
    {
        if (PlayerPrefs.GetInt("sleeping", 0) == 1) //잘때 멘트
        {
            toastTxt = "(Good night..)";
        }
        else
        {
            switch (PlayerPrefs.GetInt("likelv", 0))
            {
                case 0:
                    toastTxt = "..Bye";
                    break;

                case 1:
                    toastTxt = "Goodbye..";
                    break;

                case 2:
                    toastTxt = "Well, Good bye..";
                    break;

                case 3:
                    toastTxt = "It was nice to see you. Good bye.";
                    break;

                case 4:
                    toastTxt = "Good bye. See you later.";
                    break;

                default:
                    toastTxt = "See you later. Friend";
                    break;
            }
        }
        

            AndroidJavaClass toastClass =
                    new AndroidJavaClass("android.widget.Toast");

        object[] toastParams = new object[3];
        AndroidJavaClass unityActivity =
          new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        toastParams[0] =
                     unityActivity.GetStatic<AndroidJavaObject>
                               ("currentActivity");
        toastParams[1] = toastTxt;
        toastParams[2] = toastClass.GetStatic<int>
                               ("LENGTH_LONG");
        
        AndroidJavaObject toastObject =
                        toastClass.CallStatic<AndroidJavaObject>
                                      ("makeText", toastParams);
        
        toastObject.Call("show");

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        { 
           // closeTalkBoon();
            if (exit_int == 0)
            {
                exit_int = 1;
                talkballoon.SetActive(true);
                talkCursor.SetActive(true);
                closeTB.GetComponent<Button>().interactable = true;
                closeTB.SetActive(true);
                if (PlayerPrefs.GetInt("sleeping", 0) == 1)
                {
                    Text_obj.text = "(Now sleeping. Go out quietly?)\n(Touch back button twice.)";
                }
                else
                {
                    Text_obj.text = "Are you going?\n(Touch back button twice.)";
                }
            }
            else
            {
                toastFunction();
                StartCoroutine("applicationQuit");
            }
        }



        if (exit_int == 1)
        {
            cnt_exit++;

            if (!talkballoon.activeSelf)
            {
                exit_int = 0;
                cnt_exit = 0;
            }

        }


        if (cnt_exit == 150)
        {
            if (PlayerPrefs.GetInt("sleeping", 0) == 1)
            {
                Text_obj.text = "(..hmm)";
            }
            else
            {
                Text_obj.text = "..um";
            }
            exit_int = 0;
            cnt_exit = 0;

        }
        else if (cnt_exit == 250)
        {
            closeTalkBoon();
        }


    }

    IEnumerator applicationQuit()
    {
        yield return new WaitForSeconds(1f);
        Application.Quit();
    }

    async void csvvreader()
    {
        data_talk = await CSVReader.ReadAsync("Assets/CSV/talk_room.csv"); //대사 불러오기  
        data_item = await CSVReader.ReadAsync("Assets/CSV/talk_item.csv");
    }

    // Start is called before the first frame update
    void Start()
    {
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);

        csvvreader();

        allArr[0] = 50;
        allArr[1] = 50;
        allArr[2] = 85;
        allArr[3] = 85;
        allArr[4] = 85;
        allArr[5] = 200;

        setCharAni();

        str_Code = PlayerPrefs.GetString("code", "");

    }

    void cleantalk() //대화 초기화
    {
        Text_obj.text = "";
        text_str = "";
    }


    void lovetalk()
    { //호감도에 또는 사물에 따른 대화
        loveLv = PlayerPrefs.GetInt("likelv", 0);
        
        if (loveLv >= 5)
        {
            loveLv = 5;
        }

        lineReload();
        
        text_str = "" + data_talk[randArr[nowArr - 1]]["대화" + loveLv]; //문장넣기 0~9
    }

    void lineReload() // 대화 차례대로 보여주기 및 대화줄 초기화
    {
        if (nowArr == 0) // 난수 돌리기
        {
            GetRandomInt(allArr[0]); //테스트 0
            nowArr++;
        }
        else if (nowArr < allArr[0]) //대화 차례대로 보이기
        {
            nowArr++;
        }
        else if (nowArr >= allArr[0]) //대화 줄 초기화
        {
            GetRandomInt(allArr[0]);
            nowArr = 0;
            nowArr++;
        }

    }

    public int[] GetRandomInt(int length) //중복없는 난수생성기
    {
        randArr = new int[length];
        bool isSame;

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
    }

    public void talkA() //대사치기
    {
        //Debug.Log(countTalkNum);

        //if (countTalkNum == 0)
        // {
        //Debug.Log("대화횟수마감");
        // }
        // else
        // {
        //Audio_obj.GetComponent<SoundEvt>().talkSound();
        if(PlayerPrefs.GetInt("talk", 5) <= 0)
        {

        }
        else
        {
            cleantalk();
            TalkSound();
            int a = PlayerPrefs.GetInt("likepoint", 0);
            a = a + 1;
            PlayerPrefs.SetInt("likepoint", a);
            lovetalk();

            if (text_str.Contains("^"))
            { //질문이 있는경우
                lineStr = text_str.ToString().Split('|'); // 0:질문 1:대답버튼 2:1번의 대답 3:대답버튼 4:3번의 대답   
                StartCoroutine("questionTalkRun");
            }
            else
            {
                StartCoroutine(talkRun());
            }
            UpLike();
            HeartPlus();
        }
    }


    //대사 출력
    IEnumerator talkRun()
    {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        falseObject();
        cnt = 0;
        while (cnt != text_str.Length)
        {
            // 1. 현재 출력할 문자 확인
            char currentChar = text_str[cnt];
            Text_obj.text += currentChar.ToString();
            cnt++;

            // 2. 문자에 따라 대기 시간 다르게 설정
            if (currentChar == '.' || currentChar == ',' || currentChar == '!' || currentChar == '?')
            {
                yield return new WaitForSeconds(speedF * 9f);
            }
            else
            {
                // 일반 글자일 때는 원래 속도대로 출력
                yield return new WaitForSeconds(speedF);
            }
        }
        trueObject();
    }

    //질문 출력
    IEnumerator questionTalkRun()
    {
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        falseObject();
        closeTB.SetActive(false);
        quesBack.SetActive(true);
        //  quesStr = " ";
        btnTxt1.text = " ";
        btnTxt2.text = " ";
        cnt = 1;
        while (cnt != lineStr[0].Length)
        {
            // 1. 현재 출력할 문자 확인
            char currentChar = lineStr[0][cnt];
            Text_obj.text += currentChar.ToString();
            cnt++;

            // 2. 문자에 따라 대기 시간 다르게 설정
            if (currentChar == '.' || currentChar == ',' || currentChar == '!' || currentChar == '?')
            {
                // 쉼표나 마침표, 느낌표 등에서는 한 템포 더 길게 쉼 (예: 기본 속도의 3배)
                yield return new WaitForSeconds(speedF * 9f);
            }
            else
            {
                // 일반 글자일 때는 원래 속도대로 출력
                yield return new WaitForSeconds(speedF);
            }
        }

        btnTxt1.text += lineStr[1].ToString();
        btnTxt2.text += lineStr[3].ToString();

        quesBtmArea.SetActive(true);
    }


    //선택한 질문 출력
    IEnumerator choiceTextRun()
    {
        //대화속도
        speedF = PlayerPrefs.GetFloat("talkspeed", 0.05f);
        falseObject();

        //  quesStr = " ";
        cnt = 0;

        if (choiceNum == 1)
        {
            while (cnt != lineStr[2].Length)
            {
                // 1. 현재 출력할 문자 확인
                char currentChar = lineStr[2][cnt];
                Text_obj.text += currentChar.ToString();
                cnt++;

                // 2. 문자에 따라 대기 시간 다르게 설정
                if (currentChar == '.' || currentChar == ',' || currentChar == '!' || currentChar == '?')
                {
                    yield return new WaitForSeconds(speedF * 9f);
                }
                else
                {
                    // 일반 글자일 때는 원래 속도대로 대기
                    yield return new WaitForSeconds(speedF);
                }
            }
        }
        else if (choiceNum == 2)
        {
            while (cnt != lineStr[4].Length)
            {
                // 1. 현재 출력할 문자 확인
                char currentChar = lineStr[4][cnt];
                Text_obj.text += currentChar.ToString();
                cnt++;

                // 2. 문자에 따라 대기 시간 다르게 설정
                if (currentChar == '.' || currentChar == ',' || currentChar == '!' || currentChar == '?')
                {
                    yield return new WaitForSeconds(speedF * 9f);
                }
                else
                {
                    // 일반 글자일 때는 원래 속도대로 대기
                    yield return new WaitForSeconds(speedF);
                }
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

        charAni.Play("char_talk");
        
        talkballoon.SetActive(true);
        closeTB.GetComponent<Button>().interactable = false;
        closeTB.SetActive(true);

        turtle_btn.SetActive(false);
        talkbtn.SetActive(false);
        quesBtmArea.SetActive(false);
        quesBack.SetActive(false);
        talkCursor.SetActive(false);
        seedTxt_obj.SetActive(false);

    }


    void trueObject()
    {
        closeTB.GetComponent<Button>().interactable = true;
        talkbtn.SetActive(true);
        talkCursor.SetActive(true);
        turtle_btn.SetActive(true);
        seedTxt_obj.SetActive(true);
        setCharAni();

    }


    public void closeTalkBoon()
    {
        talkballoon.SetActive(false);
        closeTB.SetActive(false);
        closeTB.GetComponent<Button>().interactable = false;
        change_turtle.SetActive(false);

        //exit_int = 0;
        //cnt_exit = 0;

        seedRain_obj.SetActive(false);
        
    }


    void setCharAni()
    {
        switch (PlayerPrefs.GetInt("likelv", 0))
        {
            case 0:
                charAni.Play("char_ani");
                break;

            case 1:
                charAni.Play("char_ani1");
                break;

            case 2:
                charAni.Play("char_ani2");
                break;

            case 3:
                charAni.Play("char_ani3");
                break;

            case 4:
                charAni.Play("char_ani4");
                break;

            case 5:
                charAni.Play("char_ani5");
                break;

            case 6:
                charAni.Play("char_ani6");
                break;

            default:
                charAni.Play("char_ani6");
                break;

        }

    }

    void callTalkItem()
    {
       // itemLv[5] = PlayerPrefs.GetInt("그림", 0);
      //  itemLv[6] = PlayerPrefs.GetInt("시계", 0);
    }

    IEnumerator itemTalkRun()
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

    //물건 대사

    public void talkBook()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("booklv", 0)]["book"];
        StartCoroutine(talkRun());

    }

    public void talkWall()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("walllv", 0)]["wall"];
        StartCoroutine(talkRun());

    }

    public void talkLight()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("lightlv", 0)]["light"];
        StartCoroutine(talkRun());

    }

    public void talkWindow()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("windowlv", 0)]["window"];
        StartCoroutine(talkRun());

    }

    public void talkSeed()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("seedgrow", 1) - 1]["seed"];
        StartCoroutine(talkRun());

    }

    public void talkPicture()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("draw", 0) - 1]["picture"];
        StartCoroutine(talkRun());

    }

    public void talkClock()
    {
        cleantalk();
        text_str = "" + data_item[PlayerPrefs.GetInt("clock", 0) - 1]["clock"];
        StartCoroutine(talkRun());

    }


    public void talkTurtle()
    {
        cleantalk();
        change_turtle.SetActive(true);
        text_str = "" + data_item[num_turtle]["turtle"];
        StartCoroutine(talkRun());


        if (num_turtle < 9)
        {
            num_turtle++;
        }
        else
        {
            num_turtle = 0;
        }

    }

    public void talkDal()
    {        
        cleantalk();
        text_str = "The dalgona seller gave me this. Of course this is not really fish.";
        StartCoroutine(talkRun());
    }


    void UpLike()
    {
        int like = PlayerPrefs.GetInt("likepoint", 0);

        if (like >= 400 && PlayerPrefs.GetInt("likelv", 0) >= 5 && PlayerPrefs.GetInt("booklv", 0) >= 8 && PlayerPrefs.GetInt("windowlv", 0) >= 8)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", PlayerPrefs.GetInt("likelv", 0) + 1);
            SetMoreTxt();
        }
        else if (like >= 350 && PlayerPrefs.GetInt("likelv", 0) == 4 && PlayerPrefs.GetInt("booklv", 0) >= 8 && PlayerPrefs.GetInt("windowlv", 0) >= 8)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", 5);
            SetMoreTxt();
        }
        else if (like >= 310 && PlayerPrefs.GetInt("likelv", 0) == 3 && PlayerPrefs.GetInt("booklv", 0) >= 8 && PlayerPrefs.GetInt("windowlv", 0) >= 8)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", 4);
            SetMoreTxt();
        }
        else if (like >= 225 && PlayerPrefs.GetInt("likelv", 0) == 2 && PlayerPrefs.GetInt("booklv", 0) >= 6 && PlayerPrefs.GetInt("windowlv", 0) >= 6)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", 3);
            SetMoreTxt();
        }
        else if (like >= 110 && PlayerPrefs.GetInt("likelv", 0) == 1 && PlayerPrefs.GetInt("booklv", 0) >= 4 && PlayerPrefs.GetInt("windowlv", 0) >= 4)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", 2);
            SetMoreTxt();
        }
        else if (like >= 50 && PlayerPrefs.GetInt("likelv", 0) == 0 && PlayerPrefs.GetInt("booklv", 0) >= 2 && PlayerPrefs.GetInt("windowlv", 0) >= 2)
        {
            like = 0;
            PlayerPrefs.SetInt("likepoint", like);
            PlayerPrefs.SetInt("likelv", 1);
            SetMoreTxt();
        }
    }

    void SetMoreTxt()
    {
        txt_pop.text = "It seems a little more familiar.";
        pop_obj.SetActive(true);
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
