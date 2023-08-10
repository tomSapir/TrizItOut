using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace Hitcode_RoomEscape
{
    public class PanelUIText : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        bool lastClicked = false;
        public void OnClick()
        {
            if (GameData.Instance.Textlocked)
            {
                GameData.Instance.rm.showFull();
                return;
            }
            
            if (GameData.Instance.PlayTexts.Count == 0)
            {
                //if(!lastClicked)
                //{
                //    lastClicked = true;
                //    GameObject.Find("UItipText").GetComponent<Text>().text = "";

                //}
                //else
                //{
                    GameData.Instance.rm.delayUnlock();
                    gameObject.SetActive(false);
                    lastClicked = false;
                //}
               
            }
            else
            {

                GameData.Instance.rm.playText();
                //GameData.Instance.gameUI.panelText.GetComponentInChildren<Text>().text = GameData.Instance.PlayTexts[0];
               
                //GameData.Instance.PlayTexts.RemoveAt(0);
            }
           
           
            
        }


        //bool IsTouchedUI()
        //{
        //    bool touchedUI = false;
        //    if (Application.isMobilePlatform)
        //    {
        //        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
        //        {
        //            touchedUI = true;
        //        }
        //    }
        //    else if (EventSystem.current.IsPointerOverGameObject())
        //    {
        //        touchedUI = true;
        //    }
        //    return touchedUI;
        //}
    }

}
