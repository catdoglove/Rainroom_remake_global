using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackbtnEvt : MonoBehaviour
{
    public GameObject[] wnd;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            for(int i = 0; i < wnd.Length; i++)
            wnd[i].SetActive(false);
        }

    }
}
