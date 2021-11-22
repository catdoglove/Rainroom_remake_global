using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParkTime : MonoBehaviour
{
    public static int trashRnd2, leafRnd;
    public float moveX, moveY, moveLX, moveLY;
    public GameObject trash_obj, leaf_obj, first_help;

    string str;
    int talk;
    
    string str_Code;

    //쓰레기통
    public Sprite[] spr_trash;
    public GameObject trashB, memoTrash, trashButton;
    int item_num;
    int iTrash;
    public GameObject blackAd_obj;

    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetInt("outtimecut", 0);
        if (PlayerPrefs.GetInt("backHomeTrash", 0) == 999)
        {
            item_num = 5;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];
        }
        //도움말 최초 1회 실행
        if (PlayerPrefs.GetInt("firstHelpPark", 0) == 0)
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
        iTrash = PlayerPrefs.GetInt("trashnum", 0);
        str = PlayerPrefs.GetString("code", "");
        str_Code = PlayerPrefs.GetString("code", "");
        StartCoroutine("updateSecp");

        trashB.GetComponent<Image>().sprite = spr_trash[PlayerPrefs.GetInt("trashCanImage", 0)];
        ckTrash();

    }


    public void closeFirstHelp()
    {
        first_help.SetActive(false);
        PlayerPrefs.SetInt("firstHelpPark", 99);
    }

    public void getTrash()
    {
        iTrash++;

        ckTrash();

        PlayerPrefs.SetInt("trashnum", iTrash);
        PlayerPrefs.SetInt("trashCanImage", item_num);
    }

    void ckTrash()
    {
        if (iTrash < 20)
        {
            item_num = 0;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];

        }
        else if (iTrash >= 100)
        {
            PlayerPrefs.SetInt("allTrash", 99);
            if (PlayerPrefs.GetInt("backHomeTrash", 0) == 999)
            {
                item_num = 5;
                trashB.GetComponent<Image>().sprite = spr_trash[item_num];
            }
        }
        else if (iTrash >= 80)
        {
            item_num = 4;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];
        }
        else if (iTrash >= 60)
        {
            item_num = 3;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];
        }
        else if (iTrash >= 40)
        {
            item_num = 2;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];
        }
        else if (iTrash >= 20)
        {
            item_num = 1;
            trashB.GetComponent<Image>().sprite = spr_trash[item_num];
        }

    }

    public void memoTrashOpen()
    {
        if (PlayerPrefs.GetInt("backHomeTrash", 0) == 999)
        {
            if (iTrash >= 100)
            {
                int h;
                memoTrash.SetActive(true);
                PlayerPrefs.SetInt("trashnum", 0);
                iTrash = 0;
                trashB.GetComponent<Image>().sprite = spr_trash[0];
                h = PlayerPrefs.GetInt(str_Code + "h", 0);
                h = h + 20;
                PlayerPrefs.SetInt(str_Code + "h", h);
                PlayerPrefs.SetInt("backHomeTrash", 1);
                PlayerPrefs.SetInt("allTrash", 1);
            }
        }
    }

    public void memoTrashClose()
    {
        memoTrash.SetActive(false);
    }


    IEnumerator updateSecp()
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
            //최대량 제한 빗물 마음
            if (PlayerPrefs.GetInt(str + "r", 0) > 999999)
            {
                PlayerPrefs.SetInt(str + "r", 999999);
            }

            if (PlayerPrefs.GetInt(str + "h", 0) > 99999)
            {
                PlayerPrefs.SetInt(str + "h", 99999);
            }
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

    void Baquitrash()
    {
        //쓰레기
        if (trashRnd2 == 1)
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
            trashRnd2 = Random.Range(0, 20);
            moveX = Random.Range(-7.1f, 7.1f);
            trash_obj.SetActive(false);
        }

        //잎
        if (leafRnd == 1)
        {

            if (moveLX == 0 && moveLX == 0)
            {
                moveLX = Random.Range(-7.1f, 7.1f);
            }

            leaf_obj.SetActive(true);
            leaf_obj.transform.position = new Vector3(moveLX, leaf_obj.transform.position.y, leaf_obj.transform.position.z);

        }
        else
        {
            leafRnd = Random.Range(0, 20);
            moveLX = Random.Range(-7.1f, 7.1f);
            leaf_obj.SetActive(false);
        }


    }


}
