using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SoundEvt : MonoBehaviour {

	public AudioSource se_touch,se_back;
	public AudioClip sp_touch;
	public AudioSource se_touch1;
	public AudioClip sp_touch1;
	public GameObject muteImg,muteBGImg;
	public Sprite [] spr_mute;

	//아이템소리
	public AudioClip sp_book,sp_light,sp_window,sp_tv,sp_cat,sp_clock,sp_sleep,sp_eatGold,sp_eatCity,sp_heartpaper, sp_trash;
	public AudioSource se_book,se_light,se_window,se_tv,se_cat,se_clock,se_sleep,se_eatGold,se_eatCity,se_heartpaper, se_trash;

	// Use this for initialization
	void Start () {
		

		se_touch = gameObject.GetComponent<AudioSource> ();
		se_touch.clip=sp_touch1;
		se_touch1 = gameObject.GetComponent<AudioSource> ();
		se_touch1.clip=sp_touch1;
		se_book = gameObject.GetComponent<AudioSource> ();
		se_book.clip=sp_book;
		se_window = gameObject.GetComponent<AudioSource> ();
		se_window.clip=sp_window;
		se_light = gameObject.GetComponent<AudioSource> ();
		se_light.clip=sp_light;
		se_tv = gameObject.GetComponent<AudioSource> ();
		se_tv.clip=sp_tv;
		se_cat = gameObject.GetComponent<AudioSource> ();
		se_cat.clip=sp_cat;
		se_clock = gameObject.GetComponent<AudioSource> ();
		se_clock.clip=sp_clock;
		se_sleep = gameObject.GetComponent<AudioSource> ();
		se_sleep.clip=sp_sleep;

		se_eatGold = gameObject.GetComponent<AudioSource> ();
		se_eatGold.clip=sp_eatGold;
        se_trash = gameObject.GetComponent<AudioSource>();
        se_trash.clip = sp_trash;

        se_eatCity = gameObject.GetComponent<AudioSource> ();
		se_eatCity.clip=sp_eatCity;

		se_heartpaper = gameObject.GetComponent<AudioSource> ();
		se_heartpaper.clip=sp_heartpaper;

		if (PlayerPrefs.GetInt ("soundmute", 0)==1) {
			se_touch.mute = true;
			se_book.mute = true;
			se_window.mute = true;
			se_light.mute = true;
			se_touch1.mute = true;
			se_cat.mute = true;
			se_clock.mute = true;
			se_sleep.mute = true;
			se_eatGold.mute = true;
			se_eatCity.mute = true;
			se_heartpaper.mute = true;
            se_trash.mute = true;
        }

        if (PlayerPrefs.GetInt("soundBGmute", 0) == 1)
        {
            se_back.mute = true;
        }
        /*
		if (PlayerPrefs.GetInt ("soundBGmute", 0)==1) {			
			if (PlayerPrefs.GetInt ("scene", 0) == 0) {		
				//upgrade.bgm_rain.mute = true;
			} else if (PlayerPrefs.GetInt ("scene", 0) == 1) {	
				//upgrade_park.bgm_rain.mute = true;			
			} else {
				//upgrade_city.bgm_rain.mute = true;	
			}
			muteBGImg.GetComponent<Image>().sprite=spr_mute[1];//소리음소거
		}
        */
    }
	

	public void XtouchSound(){
		
		se_touch.Play ();
	}

	public void touchSound()
    {
        if (PlayerPrefs.GetInt("soundmute", 0) == 1)
        {
            se_touch.mute = true;
        }
        else
        {
            se_touch.mute = false;
        }
        se_touch1 = gameObject.GetComponent<AudioSource> ();
		se_touch1.clip=sp_touch1;
		se_touch1.loop = false;
		se_touch1.Play ();
	}

	public void bookSound()
    {
        if (PlayerPrefs.GetInt("soundmute", 0) == 1)
        {
            se_book.mute = true;
        }
        else
        {
            se_book.mute = false;
        }
        se_book = gameObject.GetComponent<AudioSource> ();
		se_book.clip=sp_book;
		se_book.loop = false;
		se_book.Play ();
	}

	public void windowSound()
    {
        if (PlayerPrefs.GetInt("soundmute", 0) == 1)
        {
            se_window.mute = true;
        }
        else
        {
            se_window.mute = false;
        }
        se_window = gameObject.GetComponent<AudioSource> ();
		se_window.clip=sp_window;
		se_window.loop = false;
		se_window.Play ();
	}

	public void lightSound(){
		se_light = gameObject.GetComponent<AudioSource> ();
		se_light.clip=sp_light;
		se_light.loop = false;
		se_light.Play ();
	}

	public void tvSound(){
		se_tv = gameObject.GetComponent<AudioSource> ();
		se_tv.clip=sp_tv;
		se_tv.loop = false;
		se_tv.Play ();
	}

	public void catSound(){
		se_cat = gameObject.GetComponent<AudioSource> ();
		se_cat.clip=sp_cat;
		se_cat.loop = false;
		se_cat.Play ();
	}


	public void clockSound(){
		se_clock = gameObject.GetComponent<AudioSource> ();
		se_clock.clip=sp_clock;
		se_clock.loop = false;
		se_clock.Play ();
	}

	public void sleepSound(){
		se_sleep = gameObject.GetComponent<AudioSource> ();
		se_sleep.clip=sp_sleep;
		se_sleep.loop = false;
		se_sleep.Play ();
	}


	public void eatGoldSound(){
		se_eatGold = gameObject.GetComponent<AudioSource> ();
		se_eatGold.clip=sp_eatGold;
		se_eatGold.loop = false;
		se_eatGold.Play ();
	}


    public void trashSound()
    {
        se_trash = gameObject.GetComponent<AudioSource>();
        se_trash.clip = sp_trash;
        se_trash.loop = false;
        se_trash.Play();
    }

    public void eatCitySound(){
		se_eatCity = gameObject.GetComponent<AudioSource> ();
		se_eatCity.clip=sp_eatCity;
		se_eatCity.loop = false;
        se_eatCity.Play ();
	}


	public void heartpaperSound(){
		se_heartpaper = gameObject.GetComponent<AudioSource> ();
		se_heartpaper.clip=sp_heartpaper;
		se_heartpaper.loop = false;
        se_heartpaper.Play ();
	}

	//효과음
	public void soundMute(){
		if (PlayerPrefs.GetInt("soundmute", 0) == 0) {
			se_touch.mute = true;
			se_book.mute = true;
			se_window.mute = true;
			se_light.mute = true;
			se_touch1.mute = true;
			se_cat.mute = true;
			se_clock.mute = true;
			se_sleep.mute = true;
			se_eatGold.mute = true;
			se_eatCity.mute = true;
			se_heartpaper.mute = true;
            se_trash.mute = true;
            //muteImg.GetComponent<Image>().sprite=spr_mute[1];//소리음소거
            PlayerPrefs.SetInt("soundmute",1);
		} else {
			se_touch.mute = false;
			se_book.mute = false;
			se_window.mute = false;
			se_light.mute = false;
			se_touch1.mute = false;
			se_cat.mute = false;
			se_clock.mute = false;
			se_sleep.mute = false;
			se_eatGold.mute = false;
			se_eatCity.mute = false;
			se_heartpaper.mute = false;
            se_trash.mute = false;
            //muteImg.GetComponent<Image>().sprite=spr_mute[0];//소리재생
            PlayerPrefs.SetInt("soundmute",0);
		}
	}


    //배경음 뮤트
    public void BGMute()
    {
        if (PlayerPrefs.GetInt("soundBGmute", 0)==1)
        {
                se_back.mute = true;
        }
        else
        {
            se_back.mute = false;
        }


    }




    //배경음 방안
    public void soundBGMute(){
		/*
			if (upgrade.bgm_rain.mute == false) {				
				upgrade.bgm_rain.mute = true;
				muteBGImg.GetComponent<Image>().sprite=spr_mute[1];//소리음소거
				PlayerPrefs.SetInt("soundBGmute",1);
			} else {
				upgrade.bgm_rain.mute = false;		
				muteBGImg.GetComponent<Image>().sprite=spr_mute[0];//소리음소거
				PlayerPrefs.SetInt("soundBGmute",0);
			}

    */


	}



	//배경음 외출
	public void soundBGOutMute(){
		/*
			if (upgrade_park.bgm_rain.mute == false) {				
				upgrade_park.bgm_rain.mute = true;
				muteBGImg.GetComponent<Image>().sprite=spr_mute[1];//소리음소거
				PlayerPrefs.SetInt("soundBGmute",1);
			} else {
				upgrade_park.bgm_rain.mute = false;		
				muteBGImg.GetComponent<Image>().sprite=spr_mute[0];//소리음소거
				PlayerPrefs.SetInt("soundBGmute",0);
			}
            */
		}

	public void soundBGCOutMute(){
        /*
		if (upgrade_city.bgm_rain.mute == false) {				
			upgrade_city.bgm_rain.mute = true;
			muteBGImg.GetComponent<Image>().sprite=spr_mute[1];//소리음소거
			PlayerPrefs.SetInt("soundBGmute",1);
		} else {
			upgrade_city.bgm_rain.mute = false;		
			muteBGImg.GetComponent<Image>().sprite=spr_mute[0];//소리음소거
			PlayerPrefs.SetInt("soundBGmute",0);
		}
        */
	}

}
