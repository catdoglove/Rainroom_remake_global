using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkFunction : MonoBehaviour
{

    //카메라 태그로 찾고 적용
    public GameObject menu_obj;
    public Camera camera_c;

    // Start is called before the first frame update
    void Start()
    {
        //카메라
        camera_c = Camera.main;
        menu_obj = GameObject.FindGameObjectWithTag("메뉴Canvas");
        menu_obj.GetComponent<Canvas>().worldCamera = camera_c;

    }
    


}
