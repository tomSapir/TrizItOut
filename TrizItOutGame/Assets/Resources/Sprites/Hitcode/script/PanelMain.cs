using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using DG.Tweening;
namespace Hitcode_RoomEscape
{
    public class PanelMain : MonoBehaviour
    {

        // game UI elements
        public Text btnStart,btnLoad,btnYes,btnNo,txtWarn,tipbg;//, btnMore, btnReview;
        [HideInInspector]
        public GameObject title;
        public Toggle toggleMusic, toggleSFX;
        public Image mask;
        public Sprite[] localIcon;
        public GameObject panelSaveLoad;
 
        Scene levelC;
        // Use this for initialization
        void Start()
        {
            GameManager.getInstance().init();
            //		GameManager.getInstance ().hideBanner (true);
            //GameData.getInstance().cLevel = -1;
            fadeOut();

            //GameObject.Find("maintitle").GetComponent<Text>().text = Localization.Instance.GetString("mainTitle");




            toggleMusic.isOn = GameData.getInstance().isSoundOn == 1 ? true : false;//0 is on
            toggleSFX.isOn = GameData.getInstance().isSfxOn == 1 ? true : false;

           

            txtWarn.transform.parent.gameObject.SetActive(false);
            btnYes.transform.parent.gameObject.SetActive(false);
            btnNo.transform.parent.gameObject.SetActive(false);


            localizeView();


        }

        void localizeView()
        {


            GameData.Instance.cLanguage = PlayerPrefs.GetInt("clanguage");
            
            transform.Find("btnLocal").GetComponent<Image>().sprite = localIcon[GameData.Instance.cLanguage];
            GameData.Instance.setLanguage();

            btnStart.text = Localization.Instance.GetString("btnStart");
            //btnContinue.text = Localization.Instance.GetString("btnContinue");
#if UNITY_WEBGL
            btnLoad.text = Localization.Instance.GetString("btnContinue");

#else
            btnLoad.text = Localization.Instance.GetString("btnLoad");
#endif
            btnYes.text = Localization.Instance.GetString("btnYes");
            btnNo.text = Localization.Instance.GetString("btnNo");

            tipbg.text = Localization.Instance.GetString("newGameTip");

            //disable load on first play
            int isfirstplay = PlayerPrefs.GetInt("firstplay", 0);
            if(isfirstplay == 0)
            {
                btnLoad.transform.parent.GetComponent<Button>().interactable = false;
                btnLoad.color = new Color(1, 1, 1, .3f);
               
            }

            //web gl localization requires font to be installed on the system,so here hide the button.
#if UNITY_WEBGL
            transform.Find("btnLocal").gameObject.SetActive(false);
#endif
        }

        GameObject all_level;//levelmenu container
        GameObject all_mainMenu;
        void OnEnable()
        {
            all_mainMenu = GameObject.Find("all_mainMenu");

        }




        // Update is called once per frame
        void Update()
        {
#if UNITY_IOS
//		GameManager.getInstance().hideBanner(true);
#endif
        }

        [HideInInspector]
        public GameObject panelShop, panelFade;
        /// <summary>
        /// process kind of click events
        /// </summary>
        /// <param name="g">The green component.</param>
        public void OnClick(GameObject g)
        {
            if (GameData.getInstance().locked) return;
            switch (g.name)
            {
                case "btnStart":
                    GameManager.getInstance().playSfx("click");

                    txtWarn.transform.parent.gameObject.SetActive(true);
                    btnYes.transform.parent.gameObject.SetActive(true);
                    btnNo.transform.parent.gameObject.SetActive(true);

                    btnStart.transform.parent.gameObject.SetActive(false);
                    btnLoad.transform.parent.gameObject.SetActive(false);


                    //GameData.Instance.locked = true;

                    
                    break;
                case "btnLoad":
                    //web gl not support file save
#if UNITY_WEBGL
                    GameManager.getInstance().playSfx("click");
                    //from last startoom when quit
                    startroom = PlayerPrefs.GetString("quitSceneName" + 0, "startroom");
                    StartCoroutine("waitaframe");
#else
                    panelSaveLoad.GetComponent<PanelSaveLoad>().init(1);
#endif
                    break;
                case "btnContinue":
                    GameManager.getInstance().playSfx("click");
                    //from last startoom when quit
                    startroom = PlayerPrefs.GetString("quitSceneName" + 0, "startroom");
                    StartCoroutine("waitaframe");
                   
                    break;
                case "btnYes":
                    GameManager.getInstance().playSfx("click");
                    //GameData.Instance.deleteData();

                    //clear item data on slot 0(auto save slot)
                    
                   

                    GameData.Instance.clearSlot();


                    //reset game setting
                   PlayerPrefs.SetInt("sound", GameData.Instance.isSoundOn);
                   PlayerPrefs.SetInt("sfx", GameData.Instance.isSfxOn);
                   PlayerPrefs.SetInt("clanguage", GameData.Instance.cLanguage);

                    fadeIn("startroom");
                    break;
                case "btnNo":
                    GameManager.getInstance().playSfx("click");
                    txtWarn.transform.parent.gameObject.SetActive(false);
                    btnYes.transform.parent.gameObject.SetActive(false);
                    btnNo.transform.parent.gameObject.SetActive(false);

                    btnStart.transform.parent.gameObject.SetActive(true);
                    btnLoad.transform.parent.gameObject.SetActive(true);

                    break;
                case "btnMore":

                    GameManager.getInstance().playSfx("click");
             

#if (UNITY_IPHONE || UNITY_ANDROID)
                        Application.OpenURL("http://itunes.apple.com/WebObjects/MZSearch.woa/wa/search?submit=seeAllLockups&media=software&entity=software&term=xxxxxx");
#endif


              
                    break;
                case "btnReview":
                    GameManager.getInstance().playSfx("click");
                    //			UniRate.Instance.RateIfNetworkAvailable();
                    Application.OpenURL("itms-apps://ax.itunes.apple.com/WebObjects/MZStore.woa/wa/viewContentsUserReviews?type=Purple+Software&id = " + Const.appid);
                    break;
                case "btnShop":
                    GameManager.getInstance().playSfx("click");
                    panelShop.SetActive(true);
                    break;
                case "btnGC":
                    GameManager.getInstance().playSfx("click");
                    GameManager.getInstance().ShowLeaderboard();
                    break;
                case "btnLocal":
                    if(GameData.Instance.cLanguage < localIcon.Length-1)
                    {
                        GameData.Instance.cLanguage++;
                        GameData.Instance.setLanguage();
                    }
                    else
                    {
                        GameData.Instance.cLanguage = 0;
                        GameData.Instance.setLanguage();
                    }
                    PlayerPrefs.SetInt("clanguage", GameData.Instance.cLanguage);
                    transform.Find("btnLocal").GetComponent<Image>().sprite = localIcon[GameData.Instance.cLanguage];
                    localizeView();
                    break;
            }
        }

        bool musicInited = false;
        bool toggleSfxInited = false;
        /// <summary>
        /// process toggle button(music and sound effect buttons)
        /// </summary>
        /// <param name="toggle">Toggle.</param>
        public void OnToggle(Toggle toggle)
        {
            switch (toggle.gameObject.name)
            {
                case "ToggleMusic":
                    if (!musicInited)//forbid play unnecessary click sfx on start;
                    {
                        
                        musicInited = true;
                    }
                    else
                    {
                        GameManager.getInstance().playSfx("click");
                    }
                    GameData.getInstance().isSoundOn = toggle.isOn ? 1 : 0;

                    if (toggle.isOn)
                    {
                        GameManager.getInstance().stopBGMusic();
                    }
                    else
                    {
                        GameManager.getInstance().playMusic("bgmusic",true);
                    }
                    PlayerPrefs.SetInt("sound", GameData.getInstance().isSoundOn);

                    break;
                case "ToggleSfx":
                    GameData.getInstance().isSfxOn = toggle.isOn ? 1 : 0;
                    if (!toggleSfxInited)
                    {
                        toggleSfxInited = true;//forbid play unnecessary click sfx on start;
                    }
                    else
                    {
                        GameManager.getInstance().playSfx("click");
                    }
                    
                    if (toggle.isOn)
                    {
                        //GameManager.getInstance().stopAllSFX();
                    }

                    PlayerPrefs.SetInt("sfx", GameData.getInstance().isSfxOn);
                    break;
            }
        }


        void fadeOut()
        {
            mask.gameObject.SetActive(true);
            mask.GetComponent<Image>().color = Color.black;
            mask.GetComponent<Image>().DOFade(0, 1).OnComplete(fadeOutOver);
        }

        void fadeIn(string sceneName)
        {
            if (mask.IsActive())
                return;
            mask.gameObject.SetActive(true);
            mask.color = new Color(0, 0, 0, 0);
         
            mask.GetComponent<Image>().DOFade(1, 1).OnComplete(() => { fadeInOver(sceneName); });
        }


        void fadeInOver(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            PlayerPrefs.SetInt("firstplay", 1);
        }

        void fadeOutOver()
        {
            mask.gameObject.SetActive(false);
        }

        /// <summary>
        /// tween update event
        /// </summary>
        /// <param name="value">Value.</param>
        void OnUpdateTween(float value)

        {

            mask.color = new Color(0, 0, 0, value);
        }
        string startroom;
        IEnumerator waitaframe()
        {
            yield return new WaitForEndOfFrame();
            fadeIn(startroom);
        }
    }
}

