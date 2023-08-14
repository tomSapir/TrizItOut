using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
namespace Hitcode_tangram
{
    public class PolyPuzzle : MonoBehaviour
    {



       public void subSceneClosed()
        {
            transform.Find("gameCam").GetComponent<Camera>().enabled = false;
        }

        // Use this for initialization
        void init(string textNo = "")
        {
            print("initPuzzle");
            //GameManager.getInstance().init();
            if(textNo ==null || textNo.Trim() == "")
            {
                GameData.instance.texNo = -1;
            }
            else {
                GameData.instance.texNo = int.Parse(textNo);
            }
            


            //init game
            Tangram tg = GameObject.Find("Tangram").GetComponent<Tangram>();
            tg.onlyClear();

            //set difficulty and level
            GameData.difficulty = 0;

            GameData.instance.cLevel = Random.Range(0, GameData.totalLevel[GameData.difficulty]);

            //start game;
            transform.Find("gameCam").GetComponent<Camera>().enabled = true;
            tg.initSingleMode();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void checkWin()
        {

       
              
           
        }


    }
}
