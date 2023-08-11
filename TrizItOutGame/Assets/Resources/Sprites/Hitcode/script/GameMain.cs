using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using System.Reflection;
using System;
namespace Hitcode_RoomEscape
{
    public class GameMain : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

            //GameData.isInGame = true;
            GameData.getInstance().resetData();
            //GameData.getInstance().maing = this;
            //GameData.getInstance().main = GameObject.Find("GameUI").GetComponent<MainScript>();
            initData();


            initView();


        }

        // Update is called once per frame
        void Update()
        {

        }
        public List<List<string>> levelData;

        void initData()
        {
            //GameData.getInstance().main.timeCount = 0;
            string tmusicName = "bgmusic";

            GameManager.getInstance().playMusic(tmusicName);
            //init controller
            string tlevelname = Application.loadedLevelName;
            string tclevel = tlevelname.Substring(5, tlevelname.Length - 5);
            //just activate the class


        }

        GameObject finger;
        public GameObject hero;
        void initView()
        {

            switch (Application.loadedLevelName)
            {
                case "level1":

                    break;
            }


        }


        void gameWin()
        {
            //GameData.getInstance().main.gameWin(true);
        }

    }
}