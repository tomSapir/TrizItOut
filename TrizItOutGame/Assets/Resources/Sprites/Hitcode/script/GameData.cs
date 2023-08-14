using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
using System.Linq;

namespace Hitcode_RoomEscape
{
    public class GameData :ScriptableObject
    {

        //ItemDataBaseList itemList = null;
        //public List<Item> myInventory;
        public string projectName;
        ItemsDataBase itemsDataBase;
        BlueprintDatabase bluePrintDatabase;
        JournalDataBase journalDataBase;
        public List<JournalData> journalDatas = new List<JournalData>();
        public bool locked = false;//all locked
        public bool Textlocked = false;//text locked
        public bool Textlocked1 = false;//text locked in dialogue
        //public bool isTexting = false;//is texting,disable inventory and 

        public int isSfxOn, isSoundOn;
        public int bestscore;
        public int currentScene;
        public bool isWin, isFail;

        public List<Item> items = new List<Item>();
        public List<Blueprint> bluePrints = new List<Blueprint>();
        public RoomManager rm;
        public GameUI gameUI;
        public Dictionary<string,Vector3> CameraDic;//the each scenes last information before switched to others
        public List<string> PlayTexts;//use for talk

      
        public float typeGap;
        //public int currentProfile;//the current save slot index
        public bool inited = false;
     
        public int cLanguage = 0;

        public void init()
        {

            GameObject config = Resources.Load("Configure") as GameObject;
            if (config != null)
            {
                projectName = config.GetComponent<Configure>().currentProjectName;
            }

            //all items
            itemsDataBase = (ItemsDataBase)Resources.Load(projectName + "/ItemDatabase");
            bluePrintDatabase = (BlueprintDatabase)Resources.Load(projectName + "/BlueprintDatabase");
            journalDataBase = (JournalDataBase)Resources.Load(projectName + "/JournalDataBase");
      

            //string[] tmyItems = PlayerPrefsX.GetStringArray("myItems"+currentProfile);
            //if(tmyItems!=null && tmyItems.Length > 0)
            //{
            //    for (int i = 0; i < tmyItems.Length; i++) {
            //        Item tItem = getItemByName(tmyItems[i]);
            //        items.Add(tItem);
            //    }
            //}

            bluePrints = bluePrintDatabase.blueprints;

            if(CameraDic == null)
            CameraDic = new Dictionary<string, Vector3>();


            setLanguage();

            //only for your testing game. You start the game not from main menu,but wanna load the game from your last play
            if (!inited)
            {
                loadSavedData();
            }
        }

        public void setLanguage()
        {
            Localization.Instance.SetLanguage(cLanguage);
        }



        public void AddItemByName(string itemName)
        {
            Item tItem = itemsDataBase.itemList.Find(i => i.itemName == itemName);
            items.Add(tItem);
        }
        public void AddJournalByName(string itemName)
        {

            JournalData tItem = getJournalByName(itemName);


            bool isExisted = false;
            foreach (JournalData tjournal in journalDatas)
            {
                //not add duplicated journal
                if (tjournal.journalName == itemName)
                {

                    isExisted = true;
                    break;
                }

            }

            if (!isExisted)
            {
                tItem = journalDataBase.itemList.Find(i => i.journalName == itemName);
                journalDatas.Add(tItem);
            }

        }


        public bool isConsumable(string itemName)
        {
            Item tItem = itemsDataBase.itemList.Find(i => i.itemName == itemName);
            if (tItem.itemType == ItemType.Consumable)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void removeItemByName(string itemName)
        {
            Item tItem = itemsDataBase.itemList.Find(i => i.itemName == itemName);
            items.Remove(tItem);
        }

        public Item getItemByName(string itemName)
        {
            Item tItem = itemsDataBase.itemList.Find(i => i.itemName == itemName);
            return tItem;
        }

        public Item GetItemById(int id)
        {
            Item tItem = itemsDataBase.itemList.Find(i => i.itemID == id);
            return tItem;
        }

        public JournalData getJournalByName(string journalName)
        {
            JournalData tJournal = journalDataBase.itemList.Find(i => i.journalName == journalName);
            return tJournal;
        }

        StatesDataBase stateDataList;
        /// <summary>
        /// the game keep save automatically on slot 0.
        /// slot 0 is always be the lastest data
        /// </summary>
        public void SaveGame(int index = 0)
        {

            //use player prefs to save item
            string[] itemNames= items.Select(i => i.itemName).ToArray();
            PlayerPrefsX.SetStringArray("myItems"+ index, itemNames);

            //use player prefs to save journal
            string[] journalNames = journalDatas.Select(i => i.journalName).ToArray();
            PlayerPrefsX.SetStringArray("myJournals" + index, journalNames);

            //save all other auto saved data on slot 0 to assigned slot;
            if (stateDataList == null)
            {
                stateDataList = (StatesDataBase)Resources.Load(projectName + "/StateDataBase");
            }
   
            for (int i = 0; i < stateDataList.StateList.Count; i++)
            {
                //copy auto save data(from slot 0)to your assigned slot
                int tValue = PlayerPrefs.GetInt(stateDataList.StateList[i].StateName + 0);
                PlayerPrefs.SetInt(stateDataList.StateList[i].StateName + index, tValue);
            }
            
            //save other system data
            string currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            PlayerPrefs.SetString("quitSceneName" + index, currentScene);//record current scene on assigned slot;
            PlayerPrefs.SetString("currentItem" + index, GameData.Instance.currentItem);//save current item info on assigned slot
           


            return;

            //if use file save
            #if UNITY_WEBGL
               return;
            #endif


            SaveGameData data = new SaveGameData();
            data.itemsName = items.Select(i => i.itemName).ToArray();


            SaveLoadController.SaveGame(data);
        }

        public void clearSlot(int index = 0)
        {
            string projectName = GameData.Instance.projectName;
            if (stateDataList == null)
            {
                stateDataList = (StatesDataBase)Resources.Load(projectName + "/StateDataBase");
            }

            for (int i = 0; i < stateDataList.StateList.Count; i++)
            {
                PlayerPrefs.DeleteKey(stateDataList.StateList[i].StateName + index);
            }

        
            PlayerPrefs.DeleteKey("quitSceneName" + index);
            PlayerPrefs.DeleteKey("currentItem" + index );


            PlayerPrefsX.SetStringArray("myItems" + index, new string[] { });
            PlayerPrefsX.SetStringArray("myJournals" + index, new string[] { });
            if (index == 0)
            {
                GameData.Instance.items = new List<Item>();
                GameData.Instance.journalDatas = new List<JournalData>();
            }
            else//also delete file
            {
                //var path = System.IO.Path.Combine(Application.dataPath + "/Hitcode/src/Resources/ScreenShots/Save", index + ".png");
                var path = Utils.GetPath + index + ".png";
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                //and clear the save data time
                PlayerPrefs.DeleteKey("slotData" + index);
            }

        }


        public void deleteData()
        {
            
            SaveLoadController.DeleteSaveGame();
        }


        public string loadSavedData(int index = 0)
        {
            string projectName = GameData.Instance.projectName;
            StatesDataBase stateDataList = (StatesDataBase)Resources.Load(projectName + "/StateDataBase");

            for (int i = 0; i < stateDataList.StateList.Count; i++)
            {
                //save selected slot value to default slot(game using slot.(slot 0))
                int tValue = PlayerPrefs.GetInt(stateDataList.StateList[i].StateName + index);
                PlayerPrefs.SetInt(stateDataList.StateList[i].StateName + 0, tValue);
            }

            //save other system data to default slot(slot 0),ready for load
            string tquitScene = PlayerPrefs.GetString("quitSceneName" + index);
            string currentItem = PlayerPrefs.GetString("currentItem" + index);
            PlayerPrefs.SetString("quitSceneName" + 0, tquitScene);//record selected slot scene on default slot;
            PlayerPrefs.SetString("currentItem" + 0, currentItem);//save selected slot item info on default slot
            GameData.Instance.currentItem = currentItem;
            //load items
            List<Item> items = new List<Item>();
            string[] tmyItems = PlayerPrefsX.GetStringArray("myItems" + index);
            if (tmyItems != null && tmyItems.Length > 0)
            {
                for (int i = 0; i < tmyItems.Length; i++)
                {
                    Item tItem = GameData.Instance.getItemByName(tmyItems[i]);
                    items.Add(tItem);
                }
            }
            GameData.Instance.items = items;

            //load journals
            List<JournalData> journals = new List<JournalData>();
            string[] tMyJournals = PlayerPrefsX.GetStringArray("myJournals" + index);
            if (tMyJournals != null && tMyJournals.Length > 0)
            {
                for (int i = 0; i < tMyJournals.Length; i++)
                {
                    JournalData tJounal = GameData.Instance.getJournalByName(tMyJournals[i]);
                    journals.Add(tJounal);
                }
            }

            GameData.Instance.journalDatas = journals;

            //record item and journal info to slot 0
            PlayerPrefsX.SetStringArray("myItems" + 0, tmyItems);
            PlayerPrefsX.SetStringArray("myJournals" + 0, tMyJournals);

            return tquitScene;
        }
        
        public List<Camera> cameraList;//when change camera,add to the list.This can be use for undo to the previous camera
        public Camera currentSubCam;//sub scene camera
        public bool areaGame;//a sub scene,mini game is acitvated
        public string currentItem="";
        public void resetData()
        {
            cameraList = new List<Camera>();
            currentSubCam = null;
            areaGame = false;
           
            
        }


        public static GameData Instance;
        public static GameData getInstance()
        {
            if (Instance == null)
            {
                Instance = ScriptableObject.CreateInstance<GameData>();
                Instance.init();
            }
            return Instance;
        }

        /// <summary>
        /// Gets the system laguage.
        /// </summary>
        /// <returns>The system laguage.</returns>
        public int GetSystemLaguage()
        {
            int returnValue = 0;
            switch (Application.systemLanguage)
            {
                case SystemLanguage.Chinese:
                    returnValue = 1;
                    break;
                case SystemLanguage.ChineseSimplified:
                    returnValue = 1;
                    break;
                case SystemLanguage.ChineseTraditional:
                    returnValue = 1;
                    break;
                default:
                    returnValue = 0;
                    break;

            }
            returnValue = 0;//test
            return returnValue;
        }

    }
}
