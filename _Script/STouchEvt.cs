using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STouchEvt : MonoBehaviour
{
    string str_Code;
    int h, r;
    public GameObject GM;

    public GameObject wep_obj;
    // Start is called before the first frame update
    void Start()
    {

        str_Code = PlayerPrefs.GetString("code", "");
    }
    

    private void OnMouseDown()
    {
        GM.GetComponent<SoundEvt>().trashSound();
        wep_obj.SetActive(false);
        MainTime.wepRnd = 0;
        h = PlayerPrefs.GetInt(str_Code + "h", 0);
        r = PlayerPrefs.GetInt(str_Code + "r", 0);
        h = h + 3;
        r = r + 25;
        PlayerPrefs.SetInt(str_Code + "r", r);
        PlayerPrefs.SetInt(str_Code + "h", h);
        PlayerPrefs.Save();
    }

}
