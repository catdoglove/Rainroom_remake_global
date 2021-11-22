using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutItem : MonoBehaviour
{
    public GameObject outGoodsWin_obj,onRoom_obj;
    public GameObject[] goods_obj;
    public GameObject[] check_obj;
    public Sprite[] spr_goodsImg;

    public GameObject txt_obj,ending_obj, endingImg_obj,endBtnL_obj, endBtnR_obj;
    public Sprite[] spr_end;
    public Sprite spr_endBtn, spr_endRBtn;
    public int page_i;

    int item_num;
    // Start is called before the first frame update
    void Start()
    {
        int sum = 0;
        for (int i = 0; i < 9; i++)
        {
            if (PlayerPrefs.GetInt("outgoods"+i, 0) == 1)
            {
                goods_obj[i].SetActive(true);
                sum++;
            }
        }
        if (sum >= 1)
        {
            onRoom_obj.SetActive(true);
        }
        int s=PlayerPrefs.GetInt("setoutgoods");
        onRoom_obj.GetComponent<Image>().sprite = spr_goodsImg[s];
        

        if(sum>=8&& PlayerPrefs.GetInt("setending", 0) == 1)
        {
            ending_obj.SetActive(true);
        }

        if (sum == 8)
        {
            txt_obj.SetActive(true);
        }else if(sum == 9)
        {
            goods_obj[4].SetActive(true);
            txt_obj.SetActive(false);
        }
    }
    

    public void Actoutgoods()
    {
        if (outGoodsWin_obj.activeSelf)
        {
            outGoodsWin_obj.SetActive(false);
        }
        else
        {
            for (int i = 0; i < 9; i++)
            {
                    check_obj[i].SetActive(false);
            }
            int s = PlayerPrefs.GetInt("setoutgoods");
            check_obj[s].SetActive(true);
            outGoodsWin_obj.SetActive(true);
        }
    }


    public void Set0()
    {
        item_num = 0;
        SetGoods();
    }
    public void Set1()
    {
        item_num = 1;
        SetGoods();
    }
    public void Set2()
    {
        item_num = 2;
        SetGoods();
    }
    public void Set3()
    {
        item_num = 3;
        SetGoods();
    }
    public void Set4()
    {
        item_num = 4;
        SetGoods();
    }
    public void Set5()
    {
        item_num = 5;
        SetGoods();
    }
    public void Set6()
    {
        item_num = 6;
        SetGoods();
    }
    public void Set7()
    {
        item_num = 7;
        SetGoods();
    }

    public void Set8()
    {
        item_num = 8;
        SetGoods();
    }
    void SetGoods()
    {
        //고양이 미니어쳐 곰인형 거미 엔딩 디퓨저 우산 도트 컵
        PlayerPrefs.SetInt("setoutgoods", item_num);
        for (int i = 0; i < 9; i++)
        {
            check_obj[i].SetActive(false);
        }
        int s = PlayerPrefs.GetInt("setoutgoods");
        check_obj[s].SetActive(true);
        onRoom_obj.GetComponent<Image>().sprite = spr_goodsImg[s];
    }

    public void EndR()
    {
        if (page_i == 5)
        {

            PlayerPrefs.SetInt("outgoods4", 1);
            PlayerPrefs.SetInt("setending", 2);
            PlayerPrefs.SetInt("setoutgoods",4);
            ending_obj.SetActive(false);
            onRoom_obj.GetComponent<Image>().sprite = spr_goodsImg[4];
            goods_obj[4].SetActive(true);
            txt_obj.SetActive(false);
        }
        else
        {
            page_i++;
            endingImg_obj.GetComponent<Image>().sprite = spr_end[page_i];
            endBtnL_obj.SetActive(true);

            if (page_i == 5)
            {
                endBtnR_obj.GetComponent<Image>().sprite = spr_endBtn;
            }
        }
    }
    public void EndL()
    {
        if (page_i == 0)
        {

        }
        else
        {
            endBtnR_obj.GetComponent<Image>().sprite = spr_endRBtn;
            page_i--;
            endingImg_obj.GetComponent<Image>().sprite = spr_end[page_i];
            if (page_i == 0)
            {
                endBtnL_obj.SetActive(false);
            }
        }
    }
}
