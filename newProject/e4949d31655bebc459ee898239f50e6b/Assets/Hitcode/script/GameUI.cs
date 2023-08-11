using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Hitcode_RoomEscape
{
    public class GameUI : MonoBehaviour
    {

        // Use this for initialization
        Image mask;
        Image currentUseImg;
        Image currentImg;
        float inventoryGridW;
        Image noneImg;
        void Start()
        {
            //fade out
            GameObject tmaskob = transform.Find("Mask").gameObject;
            if (tmaskob != null)
            {
                mask = tmaskob.GetComponent<Image>();
                mask.enabled = true;
                mask.DOFade(0, 1).OnComplete(() => { mask.enabled = false; });
            }
            currentImg = transform.Find("btnInventory").Find("currentItemImg").GetComponent<Image>();
            noneImg = transform.Find("btnInventory").Find("none").GetComponent<Image>();
            inventoryGridW = transform.Find("btnInventory").GetComponent<Image>().rectTransform.rect.width;

            StartCoroutine("waitaframe");


        }

        IEnumerator waitaframe()
        {
            yield return new WaitForEndOfFrame();
            GameData.Instance.gameUI = this;
        }

        public void initView()
        {

            //verfiry the currentItem's exist
            bool currentItemExist = false;
            foreach (Item tItem in GameData.getInstance().items)
            {
                if (tItem == null) return;
                if (tItem.itemName == GameData.Instance.currentItem)
                {
                    currentItemExist = true;
                }

            }
            if (currentItemExist && GameData.Instance.currentItem != null && GameData.Instance.currentItem != "")
            {

                currentImg.sprite = GameData.Instance.getItemByName(GameData.Instance.currentItem).itemIcon;

            }
            else
            {
                currentImg.sprite = noneImg.sprite;
                currentImg.SetNativeSize();



            }
            if (currentImg != null)
            {
                currentImg.SetNativeSize();
                currentImg.transform.localScale = Vector3.one;
                float tw = currentImg.rectTransform.rect.width;
                float th = currentImg.rectTransform.rect.height;

                float tsize = Mathf.Max(tw, th);
                float tradio = inventoryGridW / tsize;
                tradio *= 0.9f;//let it a bit smaller than the container

                currentImg.transform.localScale *= tradio;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        public GameObject panelInventory;
        public GameObject panelText;
        public GameObject itemTip;
        public GameObject panelPause;
        public GameObject panelReadJournal;
        //public GameObject btnExitSubCam;

        public void OnClick(GameObject g)
        {
            //if (GameData.Instance.rm != null)
            //{
            //    GameData.Instance.rm.clearText();
            //}
            switch (g.name)
            {
                case "btnInventory":
                    if (GameData.Instance.locked) return;
                    panelInventory.SetActive(!panelInventory.activeSelf);
                    GameManager.getInstance().playSfx("flip");
                    break;
                case "btnExitPreviousScene":
                    if (GameData.Instance.cameraList != null)
                    {
                        if (GameData.Instance.locked) return;
                        print(GameData.Instance.cameraList.Count);
                        //if not at start scene
                        if (GameData.Instance.cameraList.Count > 1)
                        {
                            Camera tPrevCam = GameData.Instance.cameraList[GameData.Instance.cameraList.Count - 2];
                            Camera tCurrentCam = GameData.Instance.cameraList[GameData.Instance.cameraList.Count - 1];
                            if (tCurrentCam != null && tPrevCam != null)
                            {
                                tCurrentCam.enabled = false;
                                GameData.Instance.cameraList.RemoveAt(GameData.Instance.cameraList.Count - 1);

                                tPrevCam.enabled = true;

                            }

                        }
                        //only remain one or no camera,hide the back button
                        if (GameData.Instance.cameraList.Count <= 1)
                        {
                            g.GetComponent<Image>().enabled = false;

                        }
                        //clear text
                        GameData.Instance.rm.clearText();
                        GameManager.getInstance().playSfx("flip");

                    }
                    break;
                case "btnExitSubScene":
                    if (GameData.Instance.currentSubCam != null)
                    {
                        if (GameData.Instance.locked) return;
                        GameData.Instance.currentSubCam.enabled = false;
                        GameData.Instance.currentSubCam = null;
                        //only remain one or no camera,hide the back button
                        if (GameData.Instance.cameraList.Count <= 1)
                        {

                            GameObject.Find("btnExitPreviousScene").GetComponent<Image>().enabled = false;

                        }
                        else//got more than more cam in list,can undo
                        {
                            GameObject.Find("btnExitPreviousScene").GetComponent<Image>().enabled = true;
                        }
                    }
                    GameObject.Find("SubSceneMask").GetComponent<Image>().enabled = false;
                    GameObject.Find("btnExitSubScene").GetComponent<Image>().enabled = false;
                    GameData.Instance.areaGame = false;
                    //clear text
                    GameData.Instance.rm.clearText();
                    //lock game to forbid too fast next command
                    GameData.Instance.locked = true;
                    GameData.Instance.rm.delayUnlock();
                    GameManager.getInstance().playSfx("flip");
                    GameObject tg =  GameObject.Find("gameContainer");
                    if (tg != null)
                    {
                        tg.BroadcastMessage("subSceneClosed",SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case "btnPause":
                    if (GameData.Instance.locked) return;
                    GameData.Instance.locked = true;
                    Time.timeScale = 0;

                    panelPause.SetActive(true);
                    GameManager.getInstance().playSfx("flip");
                    break;
                //case "btnExitSubCam":
                //    CameraMotor tPerviousCam = GameObject.Find(VariablesManager.GetGlobal("perviousCam") as string).GetComponent<CameraMotor>();
                //    GameObject.Find("MainCamera").GetComponent<CameraController>().currentCameraMotor = tPerviousCam;
                //    VariablesManager.SetGlobal("isSubCam", false);
                //    g.SetActive(false);
                //    GameObject.Find("exitSubCam(controlPart)").GetComponent<GameCreator.Core.Actions>().Execute();
                //    break;
            }
        }

    }
}
