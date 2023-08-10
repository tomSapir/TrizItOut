using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System.IO;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

namespace Hitcode_RoomEscape
{
    public class PanelSaveLoad : MonoBehaviour
    {
        public GameObject panelConfrim;
        public GameObject mask;
        // Use this for initialization
        void Start()
        {
            transform.Find("btnClose").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnClose");
            
        }

        
        private void OnEnable()
        {
            if (mode == 0)
            {
                transform.Find("title").GetComponent<Text>().text = Localization.Instance.GetString("titleSave");
            }
            else
            {
                transform.Find("title").GetComponent<Text>().text = Localization.Instance.GetString("titleLoad");
            }

            Transform tcontinue = transform.Find("btnContinue");
            if (tcontinue != null)
            {
                tcontinue.GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnContinue");
            }

            //disable continue if no data
            //string filePath = Utils.GetPath + 0 + ".png";
            string tquitScene = GameData.getInstance().loadSavedData(0);
            //if (!File.Exists(filePath))
            if(tquitScene == null || tquitScene.Trim() == "")
            {
                tcontinue.GetComponent<Button>().interactable = false;
            }



            for (int i = 1; i <= 6; i++)
            {
                GameObject tobject = transform.Find("layout").Find("SaveSlot" + i).gameObject;

                //string filePath = Application.dataPath + "/Hitcode/src/Resources/ScreenShots/Save/" + i + ".png";


                string filePath = Utils.GetPath  + i + ".png";

                Texture2D texture = null;
                byte[] fileData;

                if (File.Exists(filePath))
                {
                    fileData = File.ReadAllBytes(filePath);
                    texture = new Texture2D(2, 2);
                    texture.LoadImage(fileData);

                    tobject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

                    tobject.GetComponentInChildren<Text>().text = PlayerPrefs.GetString("slotData" + i);
                }
                else
                {
                    continue;
                }



            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void close()
        {
            gameObject.SetActive(false);
        }

        Camera myCam;
        [HideInInspector]
        public bool force = false;
        int mode;

        public void init(int mode_)
        {
            mode = mode_;
            gameObject.SetActive(true);

           
        }

        public void continueLastGame(){
            mode = 2;
            saveOrLoad(0);   
        }

        public void saveOrLoad(int index)
        {
            string filePath = Utils.GetPath  + index + ".png";
            Texture2D texture = null;
            byte[] fileData;

            if (mode == 0)
            {
                
                if (File.Exists(filePath))
                {
                    print("old save");

                    panelConfrim.GetComponent<PanelConfirm>().init(0, index);
                    //we will call back
                    if (!force)
                        return;

                }
                else
                {
                    print("new save");
                    //just save it 
                }


                //GameObject.Find("Saveload").GetComponent<Saver>().Save();//save to serlize data
                //SaveLoadManager.Instance.SetCurrentProfile(index);
                //SaveLoadManager.Instance.Save(index);
                
                myCam = GameObject.Find("Main Camera").GetComponent<Camera>();
                capture(index);


                //load just saved picture
                fileData = File.ReadAllBytes(filePath);
                texture = new Texture2D(2, 2);
                texture.LoadImage(fileData);
                transform.Find("layout").Find("SaveSlot" + index).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(texture.width / 2, texture.height / 2));

                //record save data text
                DateTime dt = DateTime.Now;
                string tdata = dt.ToString("yyyy-MM-dd-hh-mm-ss");
                transform.Find("layout").Find("SaveSlot" + index).GetComponentInChildren<Text>().text = tdata;
                PlayerPrefs.SetString("slotData" + index, tdata);

                //save data, save on assigned slot
                GameData.Instance.SaveGame(index);

                force = false;
            }
            else//load mode
            {
                if (index == 0 || File.Exists(filePath))
                {
                   

                    if (index > 0) mode = 1;//fix mode if not using continue
                    panelConfrim.GetComponent<PanelConfirm>().init(mode, index);

                    //we will call back
                    if (!force) 
                        return; 


                    Time.timeScale = 1;
                   

                    //alternative loading way;
                    Image tmask = mask.GetComponent<Image>();
                    tmask.enabled = true;
                    mask.SetActive(true);

                    tmask.DOFade(1, .4f).OnComplete(() =>
                    {
                        GameData.Instance.locked = false;


                        string tquitScene = GameData.getInstance().loadSavedData(index);
                

                        SceneManager.LoadScene(tquitScene);

                    });

                    print("loadgame..");
                }
                
            }
        }

        public int resWidth = 200;
        public int resHeight = 150;

        private bool takeHiResShot = false;

        //public static string ScreenShotName(int width, int height)
        //{
        //    return string.Format("{0}/screenshots.png",
        //                         Application.dataPath,
        //                         width, height,
        //                         System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        //}

        public void capture(int profileIndex)
        {
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24);
            myCam.targetTexture = rt;
            Texture2D screenShot = new Texture2D(resWidth, resHeight, TextureFormat.RGB24, false);
            myCam.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            myCam.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = profileIndex.ToString();//ScreenShotName(resWidth, resHeight);

            //var path = System.IO.Path.Combine(Application.dataPath + "/Hitcode/src/Resources/ScreenShots/Save", filename + ".png");
            var path = Utils.GetPath + filename+".png";
            System.IO.File.WriteAllBytes(path, bytes);
           

            //System.IO.File.WriteAllBytes(filename, bytes);
            //Debug.Log(string.Format("Took screenshot to: {0}", filename));
            takeHiResShot = false;
        }
    }
}