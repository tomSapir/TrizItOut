using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Hitcode_RoomEscape
{
    public class PanelConfirm : MonoBehaviour
    {
        [HideInInspector]
        public int mode = 0;//0,save 1,load;
        // Use this for initialization
        void Start()
        {

        }
        private void OnEnable()
        {
            if(mode == 0)
            {
                transform.Find("bg").Find("title").GetComponent<Text>().text = Localization.Instance.GetString("overwrite");
            }
            else if(mode == 1){
                transform.Find("bg").Find("title").GetComponent<Text>().text = Localization.Instance.GetString("loadthis");
            }else if(mode == 2)
            {
                transform.Find("bg").Find("title").GetComponent<Text>().text = Localization.Instance.GetString("continueGame");
            }

            transform.Find("bg").Find("btnYes").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnYes");
            transform.Find("bg").Find("btnNo").GetComponentInChildren<Text>().text = Localization.Instance.GetString("btnNo");
        }
        // Update is called once per frame
        void Update()
        {

        }
        public void click(GameObject g)
        {
            switch (g.name)
            {
                case "btnYes":
                    GameObject.Find("PanelSaveLoad").GetComponent<PanelSaveLoad>().force = true;
                    if (mode == 0)
                    {
                        
                       
                    }
                    else
                    {
                        //SaveLoadManager.Instance.SetCurrentProfile(cIndex);
                       
                    }
                    GameObject.Find("PanelSaveLoad").GetComponent<PanelSaveLoad>().saveOrLoad(cIndex);
                    gameObject.SetActive(false);
                    //tell the system you have loaded once. 
                    GameData.getInstance().inited = true;
                    break;
                case "btnNo":
                    gameObject.SetActive(false);
                    break;
            }
        }

        int cIndex;
        public void init(int mode_,int slotIndex_)
        {
            mode = mode_;
            cIndex = slotIndex_;
            gameObject.SetActive(true);
        }
    }
}