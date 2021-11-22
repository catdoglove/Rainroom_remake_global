using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadEvt : MonoBehaviour
{
    AsyncOperation async;
    int i = 0;
    //카메라 태그로 찾고 적용
    public GameObject menu_obj;
    public Camera camera_c;

    // Use this for initialization
    void Start()
    {
        
            //카메라
            camera_c = Camera.main;
            menu_obj = GameObject.FindGameObjectWithTag("메뉴Canvas");
            menu_obj.GetComponent<Canvas>().worldCamera = camera_c;

    }
    IEnumerator Load()
    {
        if (PlayerPrefs.GetInt("scene", 0) == 2)
        {
            async = SceneManager.LoadSceneAsync("Park");
            PlayerPrefs.SetInt("outtimecut", 0);
        }
        else if (PlayerPrefs.GetInt("scene", 0) == 3)
        {
            async = SceneManager.LoadSceneAsync("City");
            PlayerPrefs.SetInt("outtimecut", 0);
        }
        else if (PlayerPrefs.GetInt("scene", 0) == 0)
        {
            async = SceneManager.LoadSceneAsync("Main");
        }
        else
        {
            async = SceneManager.LoadSceneAsync("Main");
        }
        while (!async.isDone)
        {
            yield return true;
        }
    }


    
    // Update is called once per frame
    void Update()
    {
        i++;
        if (i == 100)
        {
            StartCoroutine(Load());
        }
    }
    



}
