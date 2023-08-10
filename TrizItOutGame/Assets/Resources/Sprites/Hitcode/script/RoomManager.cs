using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
namespace Hitcode_RoomEscape
{
    public class RoomManager : MonoBehaviour
    {


        // Use this for initialization
        void Start()
        {

            StartCoroutine("waitaframe");
            string currentScene = SceneManager.GetActiveScene().name;
            if(currentScene != "MainMenu")
            {
                PlayerPrefs.SetString("quitSceneName" + 0, currentScene);//record current scene on slot 0(autosave slot);
            }
           
            GameManager.getInstance().init();
            GameManager.getInstance().stopAllSFX();
            GameData.Instance.resetData();
            GameData.Instance.rm = this;


        }


        IEnumerator waitaframe()
        {
            yield return new WaitForEndOfFrame();
            GameData.getInstance().cameraList.Add(Camera.main.GetComponent<Camera>());
            GameObject UICam = GameObject.Find("UICam");
            if (UICam != null) {
                UICam.transform.Find("Canvas").GetComponent<GameUI>().initView();
            }


            //reset Camera to its lastPositon
            //record last camera;
            string cName = SceneManager.GetActiveScene().name;
            if (GameData.Instance.CameraDic.ContainsKey(cName))
            {
                //reset last camera postion
                GameData.Instance.cameraList[0].transform.position = GameData.Instance.CameraDic[cName];
            }

            GameData.Instance.cLanguage = PlayerPrefs.GetInt("clanguage", 0);
            GameData.Instance.setLanguage();



        }

        //any operation would clear text first
        public void clearText()
        {
            GameObject tUIText = GameObject.Find("UItipText");
            if (tUIText != null)
            {
                tUIText.GetComponent<Text>().text = "";
                tUIText.transform.parent.gameObject.SetActive(false);
                GameData.Instance.locked = false;
            }
        }


        // Update is called once per frame
        void Update()
        {

        }

        public void playsound()
        {
            GameManager.getInstance().playMusic("bgmusic");

        }


        int cTextIndex = 1;
        [HideInInspector]
        public string tempShowText;
        public void playText()
        {
            cTextIndex = 1;
            GameObject.Find("UItipText").GetComponent<Text>().text = "";


            GameData.Instance.locked = true;
            if (GameData.Instance.typeGap > 0f)
            {
                GameData.Instance.Textlocked = true;
                GameData.Instance.locked = true;
                tempShowText = GameData.Instance.PlayTexts[0];
                cTextIndex = 1;
                GameManager.getInstance().stopSfx("typewriter");
                GameManager.getInstance().playSfx("typewriter");

                StopCoroutine("typeWrite");
                StartCoroutine("typeWrite");
            }
            else
            {
                allShowed();
            }
        }



        IEnumerator typeWrite()
        {

            while (cTextIndex < tempShowText.Length)
            {
                yield return new WaitForSeconds(GameData.Instance.typeGap);

                string tstr = tempShowText.Substring(0, cTextIndex);
                for (int i = 0; i < (tempShowText.Length - cTextIndex); i++)
                {
                    tstr += " ";
                }
                GameObject.Find("UItipText").GetComponent<Text>().text = tstr;
                cTextIndex++;
                if (cTextIndex == tempShowText.Length)
                {
                    allShowed();
                    StopCoroutine("typeWrite");
                }
            }
        }

        public void showFull()
        {
            cTextIndex = tempShowText.Length;
            GameObject.Find("UItipText").GetComponent<Text>().text = tempShowText.Substring(0, cTextIndex);
            GameData.Instance.Textlocked = true;
            StopCoroutine("typeWrite");
            GameManager.getInstance().stopSfx("typewriter");
            GameData.Instance.PlayTexts.RemoveAt(0);
            cTextIndex = 0;
            StartCoroutine("unLock");
        }

        public void allShowed()
        {
            GameManager.getInstance().stopSfx("typewriter");
            if (GameData.Instance.PlayTexts.Count > 0)
            {
                GameObject.Find("UItipText").GetComponent<Text>().text = GameData.Instance.PlayTexts[0];
                GameData.Instance.PlayTexts.RemoveAt(0);
            }
            else
            {
                GameObject.Find("UItipText").GetComponent<Text>().text = "";
            }
            GameData.Instance.Textlocked = true;
            StartCoroutine("unLock");
            cTextIndex = 0;
        }


        public void playGetItemTip(string itemName)
        {
            //visual effect
            GameData.Instance.gameUI.itemTip.SetActive(true);
            Image img1 = GameData.Instance.gameUI.itemTip.GetComponent<Image>();
            Image img2 = GameData.Instance.gameUI.itemTip.transform.Find("item").GetComponent<Image>();

            Item tItem = GameData.Instance.getItemByName(itemName);
            img2.sprite = tItem.itemIcon;

            //fit size
            img2.transform.localScale = Vector3.one;
            img2.SetNativeSize();

            float tgridW = img2.transform.parent.GetComponent<Image>().rectTransform.rect.width;
            float tw = img2.rectTransform.rect.width;
            float th = img2.rectTransform.rect.height;

            float tsize = Mathf.Max(tw, th);
            img2.transform.localScale *= tgridW / tsize;

            img1.color = new Color(1, 1, 1, 0);
            img2.color = new Color(1, 1, 1, 0);

            img1.DOFade(1, .3f).OnComplete(() => {
                img1.DOFade(0, .3f).SetDelay(.3f);
            });
            img2.DOFade(1, .3f).OnComplete(() => {
                img2.DOFade(0, .3f).SetDelay(.3f);
            });

            Image tipLightImg = GameData.Instance.gameUI.transform.Find("btnInventory").transform.Find("tipLight").GetComponent<Image>();
            tipLightImg.color = new Color(1, 1, 1, 0);
            tipLightImg.DOFade(1, .3f).SetDelay(.6f).OnComplete(() => {
                tipLightImg.DOFade(0, 1).SetDelay(.3f).OnComplete(() => {
                    tipLightImg.DOFade(0, .3f);
                });
            });

            GameManager.getInstance().playSfx("getitem");
        }

        IEnumerator unLock()
        {
            yield return new WaitForEndOfFrame();
            GameData.Instance.Textlocked = false;
        }

        float delayTime;
        GameObject delayTarget;
        int actionId;
        public void lockDelay(float _delayTime, GameObject _target, int _actionId)
        {
            delayTarget = _target;
            actionId = _actionId;
            delayTime = _delayTime;
            StopCoroutine("delayGame");
            StartCoroutine("delayGame");
            GameData.Instance.locked = true;

        }

        IEnumerator delayGame()
        {
            yield return new WaitForSeconds(delayTime);
            GameData.Instance.locked = false;
            if (delayTarget != null)
            {
                Actions _actions = delayTarget.GetComponent<Actions>();
                _actions.playActionNow(actionId);
            }
        }

     

        public void delayUnlock()
        {
           
              StartCoroutine("waitUnlock");
            
        }

        IEnumerator waitUnlock()
        {
            yield return new WaitForSeconds(.3f);
            GameData.Instance.locked = false;
        }
        

        void OnApplicationQuit() 
        {
            Debug.Log("Application ending after " + Time.time + " seconds");
            string currentScene = SceneManager.GetActiveScene().name;
            if (currentScene != "MainMenu")
            {
                PlayerPrefs.SetString("quitSceneName" + 0, currentScene);//record current scene on slot 0(autosave slot);
            }
        }
    }
}
