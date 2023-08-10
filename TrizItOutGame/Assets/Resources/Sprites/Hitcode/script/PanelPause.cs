using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace Hitcode_RoomEscape
{
    public class PanelPause : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnEnable()
        {
            transform.Find("bg").Find("btnResume").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnResume");
            transform.Find("bg").Find("btnExit").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnExit");
            transform.Find("bg").Find("pauseTitle").GetComponent<Text>().text = Localization.Instance.GetString("gamePaused");
            transform.Find("bg").Find("btnSave").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnSave");
            transform.Find("bg").Find("btnLoad").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnLoad");
            transform.Find("bg").Find("btnJournal").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnJournal");

            //disable save load when webgl.
#if UNITY_WEBGL
            transform.Find("bg").Find("btnSave").GetComponent<Button>().interactable = false;
            transform.Find("bg").Find("btnLoad").GetComponent<Button>().interactable = false;
            transform.Find("bg").Find("btnSave").GetComponentInChildren<Text>().color = new Color(1, 1, 1, .3f);
            transform.Find("bg").Find("btnLoad").GetComponentInChildren<Text>().color = new Color(1, 1, 1, .3f);
#endif
        }


        public GameObject panelSaveLoad;
        public GameObject panelJournal;
        public void OnClick(GameObject g)
        {
            switch (g.name)
            {
                case "btnSave":
                    GameManager.getInstance().playSfx("flip");
                    panelSaveLoad.GetComponent<PanelSaveLoad>().init(0);
                    break;
                case "btnLoad":
                    GameManager.getInstance().playSfx("flip");
                    panelSaveLoad.GetComponent<PanelSaveLoad>().init(1);
                    break;
                case "btnResume":
                    GameManager.getInstance().playSfx("flip");
                    GameData.Instance.locked = true;
                    Time.timeScale = 1;
                    gameObject.SetActive(false);

                    GameData.Instance.rm.delayUnlock();
                    break;
                case "btnExit":
                    GameManager.getInstance().playSfx("flip");
                    string tsceneName = "MainMenu";
                    Image tmask = GameObject.Find("Mask").GetComponent<Image>();
                    tmask.enabled = true;
                    Time.timeScale = 1;

                    string currentScene = SceneManager.GetActiveScene().name;
                    if (currentScene != "MainMenu")
                    {
                        PlayerPrefs.SetString("quitSceneName" + 0, currentScene);//record current scene;
                        PlayerPrefs.Save();
                    }

                    tmask.DOFade(1, .4f).OnComplete(() => {
                        SceneManager.LoadScene(tsceneName);
                        GameData.Instance.locked = false;
                    });
                    break;
                case "btnJournal":
                    GameManager.getInstance().playSfx("flip");
                    panelJournal.SetActive(true);
                    break;
            }
        }

    }
}