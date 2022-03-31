using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //랜덤필

public class RoomText : MonoBehaviour
{
    List<Dictionary<string, object>> data_diray, data_news; //csv파일
    public Text diary_text;
    string text_str;

    public Text newsBigTxt, newsMidTxt, newsSmlTxt, newsWhTxt, newsWhAndTxt; //신문내용

    string[] newsTxttArr = new string[3];
    string[] newsWhTxttArr = new string[2];

    int[] allArr = new int[2]; //전체 줄
    int nowArr = 1; //현재 줄
    int nowArr2 = 1; //현재 줄
    int[] randArr;//난수 필
    int[] randArr2;//난수 필


    int[] testArr = new int[60];

    // Start is called before the first frame update
    void Start()
    {
        data_diray = CSVReader.Read("CSV/deardiary"); //대사 불러오기   
        data_news = CSVReader.Read("CSV/news"); //대사 불러오기   

        allArr[0] = 60; //일기
        allArr[1] = 100; //신문
    }

    public int[] GetRandomInt(int length) //중복없는 난수생성기 length에 줄수 넣어호출
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

            PlayerPrefs.SetInt("newsrandom"+i, randArr[i]);
        }
        return randArr;
    }

    public int[] GetRandomInt2(int length) //중복없는 난수생성기 length에 줄수 넣어호출
    {
        randArr2 = new int[length];
        bool isSame;

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

            PlayerPrefs.SetInt("diaryrandom" + i, randArr2[i]);
        }
        return randArr2;
    }

    public void showNews()
    {
        if (PlayerPrefs.GetInt("gudocgetonce", 1) == 1)
        {
            lineReload(1);

            newsTxttArr[0] = "" + data_news[PlayerPrefs.GetInt("newsrandom" + nowArr, 0)]["big"];
            newsTxttArr[1] = "" + data_news[PlayerPrefs.GetInt("newsrandom" + nowArr, 0)]["middle"];
            newsTxttArr[2] = "" + data_news[PlayerPrefs.GetInt("newsrandom" + nowArr, 0)]["small"];


            newsBigTxt.text = newsTxttArr[0];
            newsMidTxt.text = newsTxttArr[1];
            newsSmlTxt.text = newsTxttArr[2];

            showNewsmall();
        }
        PlayerPrefs.SetInt("gudocgetonce", 0);

    }

    void showNewsmall()
    {
        newsWhTxttArr[0] = "" + data_news[PlayerPrefs.GetInt("newsrandom" + nowArr, 0)]["today"];
        newsWhTxttArr[1] = "" + data_news[PlayerPrefs.GetInt("newsrandom" + nowArr, 0)]["mean"];

        newsWhTxt.text = newsWhTxttArr[0];
        newsWhAndTxt.text = newsWhTxttArr[1];
    }

    public void showDiary()
    {
        //초기화 후 보여주기
        text_str = "";
        diary_text.text = "";        
    
        lineReload(0);
        
        text_str = "" + data_diray[PlayerPrefs.GetInt("diaryrandom" + nowArr2, 0)]["diary"]; //1줄씩 나옴
        diary_text.text = text_str;
        
        
    }



    void lineReload(int code) // 줄 초기화
    {
        if (code == 0)//일기
        {

            nowArr2 = PlayerPrefs.GetInt("diaryline", 0);

            if (nowArr2 <= 1) // 난수 돌리기
            {
                GetRandomInt2(allArr[code]);
                nowArr2++;
            }
            else if (nowArr2 < allArr[code]) //대화 차례대로 보이기
            {
                nowArr2++;
            }
            else if (nowArr2 >= allArr[code]) //대화 줄 초기화
            {
                GetRandomInt2(allArr[code]);
                nowArr2 = 0;
                nowArr2++;
                
            }
            PlayerPrefs.SetInt("diaryline", nowArr2);

        }
        else if(code == 1)//신문
        {


            nowArr = PlayerPrefs.GetInt("newsline", 0);

            if (nowArr <= 1) // 난수 돌리기
            {
                GetRandomInt(allArr[code]);
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
            
            PlayerPrefs.SetInt("newsline", nowArr);

        }
   

        

    }

}