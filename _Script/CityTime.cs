using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityTime : MonoBehaviour
{
    public static int trashRnd, trashShow, peopleRnd, peopleRnd2, peopleShow, peopleShow2, peopleImgRnd;
    public float moveX, moveY, b_moveX, b_moveY, b2_moveX, b2_moveY;
    public GameObject trash_obj, peoples_obj, peoples_obj2, first_help;

    public Sprite[] spr_people;
    public GameObject blackAd_obj;

    int talk;
    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("outtimecut", 0);
        //도움말 최초 1회 실행
        if (PlayerPrefs.GetInt("firstHelpCity", 0) == 0)
        {
            first_help.SetActive(true);
        }
        
        if (PlayerPrefs.GetInt("setending", 0) == 0)
        {
            if (PlayerPrefs.GetInt("likelv", 0) >= 12)
            {
                int sum = 0;
                for (int i = 0; i < 9; i++)
                {
                    if (PlayerPrefs.GetInt("outgoods" + i, 0) == 1)
                    {
                        sum++;
                    }
                }
                if (sum >= 8)
                {
                    PlayerPrefs.SetInt("setending", 1);
                }
            }
        }


        StartCoroutine("updateSec");
        StartCoroutine("MoveP");
        StartCoroutine("MoveP2");
    }

    public void closeFirstHelp()
    {
        first_help.SetActive(false);
        PlayerPrefs.SetInt("firstHelpCity", 99);
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
            Baquitrash();

            PlayerPrefs.SetString("outtime", System.DateTime.Now.ToString());
            yield return new WaitForSeconds(1f);
        }
    }

    void Baquitrash()
    {
        //봉투
        if (trashRnd == 1)
        {

            if (moveX == 0 && moveX == 0)
            {
                moveX = Random.Range(-7.1f, 7.1f);
            }

            trash_obj.SetActive(true);
            trash_obj.transform.position = new Vector3(moveX, trash_obj.transform.position.y, trash_obj.transform.position.z);

        }
        else
        {
            trashRnd = Random.Range(0, 12);
            moveX = Random.Range(-7.1f, 7.1f);
            trash_obj.SetActive(false);
        }


        //사람

        if (peopleShow == 0)
        {
            peopleRnd = 0;
            peopleImgRnd = 0;
            b_moveX = -14.5f;

            peoples_obj.transform.position = new Vector3(b_moveX, peoples_obj.transform.position.y, peoples_obj.transform.position.z);

            peopleRnd = Random.Range(0, 4);
            peopleImgRnd = Random.Range(0, 6);
            if (peopleRnd == 1)
            {
                peopleShow = 1;
                peoples_obj.SetActive(true);
                peoples_obj.GetComponent<SpriteRenderer>().sprite = spr_people[peopleImgRnd];

            }
        }
        else if(peopleShow2 == 0)
        {
            peopleRnd2 = 0;
            peopleImgRnd = 0;
            b2_moveX = 14.5f;

            peoples_obj2.transform.position = new Vector3(b2_moveX, peoples_obj2.transform.position.y, peoples_obj2.transform.position.z);

            peopleRnd2 = Random.Range(0, 5);
            peopleImgRnd = Random.Range(0, 6);
            if (peopleRnd2 == 1)
            {
                peopleShow2 = 1;
                peoples_obj2.SetActive(true);
                peoples_obj2.GetComponent<SpriteRenderer>().sprite = spr_people[peopleImgRnd];

            }
        }

    }

    IEnumerator MoveP()
    {
        int a = 0;
        while (a == 0)
        {
            if (peopleShow == 1)
            {
                b_moveX = peoples_obj.transform.position.x + (0.1f);//(1.5f * Time.deltaTime);

                b_moveY = peoples_obj.transform.position.y + (0.1f);//(1.5f * Time.deltaTime);

                
                if(b_moveY > -4.39)
                {
                    b_moveY = peoples_obj.transform.position.y - (0.02f);//(1.5f * Time.deltaTime);

                }
                else if( b_moveY <-4.6)
                {
                    b_moveY = peoples_obj.transform.position.y + (0.01f);//(1.5f * Time.deltaTime);
                }

                peoples_obj.transform.position = new Vector3(b_moveX, b_moveY, peoples_obj.transform.position.z);
                if (b_moveX > 14)
                {
                    peopleShow = 0;
                }
                if (peopleShow == 0)
                {
                    peopleRnd = 0;
                    b_moveX = -14.5f;
                    peoples_obj.transform.position = new Vector3(b_moveX, b_moveY, peoples_obj.transform.position.z);
                }
            }
            yield return new WaitForSeconds(0.13f);
        }
    }

    IEnumerator MoveP2()
    {
        int a = 0;
        while (a == 0)
        {
            if (peopleShow2 == 1)
            {
                b2_moveX = peoples_obj2.transform.position.x - (0.1f);//(1.5f * Time.deltaTime);


                b2_moveY = peoples_obj2.transform.position.y + (0.1f);//(1.5f * Time.deltaTime);

                
                if (b2_moveY > -4.39)
                {
                    b2_moveY = peoples_obj2.transform.position.y - (0.02f);//(1.5f * Time.deltaTime);

                }
                else if (b2_moveY < -4.6)
                {
                    b2_moveY = peoples_obj2.transform.position.y + (0.01f);//(1.5f * Time.deltaTime);
                }

                peoples_obj2.transform.position = new Vector3(b2_moveX, b2_moveY, peoples_obj2.transform.position.z);
                if (b2_moveX < -14)
                {
                    peopleShow2 = 0;
                }
                if (peopleShow2 == 0)
                {
                    peopleRnd = 0;
                    b2_moveX = 14.5f;
                    peoples_obj2.transform.position = new Vector3(b2_moveX, b2_moveY, peoples_obj2.transform.position.z);
                }
            }
            yield return new WaitForSeconds(0.14f);
        }
    }



}
