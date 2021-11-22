using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour {


    private void Awake()
    {
        //이름을 가져온다
        string str = gameObject.name;
        //태그를 가져온다
        GameObject[] des= GameObject.FindGameObjectsWithTag(str);
        //두개인가?
        int len = des.Length;
        if (len == 2)
        {
            //삭제
            DestroyImmediate(gameObject);
        }
        else
        {
            //유지
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        
    }

    /*
    //정적변수
    private static Singleton instance = null;
    //인스턴스 접근 프로퍼티
    public static Singleton Instances
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    */
}
