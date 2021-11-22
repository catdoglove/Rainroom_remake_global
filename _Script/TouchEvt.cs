using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchEvt : MonoBehaviour
{
    string str_Code;
    int h, r;
    public GameObject GM;

    public GameObject wep_obj, baques_obj, trash_obj, leaf_obj, park_trash_obj;
    // Start is called before the first frame update
    void Start()
    {

        str_Code = PlayerPrefs.GetString("code", "");
    }
    
    public void sTouch()
    {
        MainTime.wepRnd = 0;
        h = PlayerPrefs.GetInt(str_Code + "h", 0);
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        h = h + 3;
        r = r + 25;
        PlayerPrefs.SetInt(str_Code + "r", r);
        PlayerPrefs.SetInt(str_Code + "h", h);
    }

    private void OnMouseDown()
    {
        GM.GetComponent<SoundEvt>().touchSound();
        baques_obj.SetActive(false);
        MainTime.baqueRnd = 0;
        MainTime.baqueShow = 0;
        h = PlayerPrefs.GetInt(str_Code + "h", 0);
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        h = h + 3;
        r = r + 25;
        PlayerPrefs.SetInt(str_Code + "r", r);
        PlayerPrefs.SetInt(str_Code + "h", h);
    }

    public void tTouch()
    {

        trash_obj.SetActive(false);
        CityTime.trashRnd = 0;
        h = PlayerPrefs.GetInt(str_Code + "h", 0);
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        h = h + 5;
        r = r + 30;
        PlayerPrefs.SetInt(str_Code + "r", r);
        PlayerPrefs.SetInt(str_Code + "h", h);
    }

    public void parkLeafTouch()
    {
        leaf_obj.SetActive(false);
        ParkTime.leafRnd = 0;
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        r = r + 50;
        PlayerPrefs.SetInt(str_Code + "r", r);
    }

    public void parkTrashTouch()
    {
        park_trash_obj.SetActive(false);
        ParkTime.trashRnd2 = 0;
        h = PlayerPrefs.GetInt(str_Code + "h", 0);
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        h = h + 3;
        r = r + 25;
        PlayerPrefs.SetInt(str_Code + "r", r);
        PlayerPrefs.SetInt(str_Code + "h", h);
    }

}
