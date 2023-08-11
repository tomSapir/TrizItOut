using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using System;

namespace Hitcode_RoomEscape
{
    public class ItemManager : EditorWindow
    {
        [MenuItem("Room Escape/Item Manager")]
        //Itemdatabase
        static void Initialize()
        {
            ItemManager window = (ItemManager)EditorWindow.GetWindow(typeof(ItemManager), false, "Item Manager");
        }


        static ItemsDataBase itemdatabase = null;

        static Texture skypeTexture;
        static Texture emailTexture;
        static Texture headTexture;
        static Texture folderIcon;

     
        
        
        List<bool> manageItem = new List<bool>();

        Vector2 scrollPosition;

        static KeyCode test;

    
  
        public int toolbarInt = 0;
        public string[] toolbarStrings = new string[] { "Create Items", "Manage Items" };


      
       
        Vector2 scrollPosition1;

        public int toolbarInt1 = 0;
        public string[] toolbarStrings1 = new string[] { "Create Blueprints", "Manage Blueprints" };
        string currentName;
        bool cFold = false;

        static string projectName = "";
        Vector2 scrollPos = Vector2.zero;
        void OnGUI()
        {
            Header();


            //Init();
            GameObject config = Resources.Load("Configure") as GameObject;
            if (config != null)
            {
                projectName = config.GetComponent<Configure>().currentProjectName;
            }


            itemdatabase = (ItemsDataBase)Resources.Load(projectName+ "/ItemDatabase");
            if (itemdatabase == null)
            {

                GUILayout.Label("Item database missing.Please change a project in game config!");
                return;
                //itemdatabase = CreateItemDatabase.createItemDatabase();
                //itemdatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");

            }



            EditorGUILayout.BeginHorizontal();

            GUIStyle tstyle = new GUIStyle(GUI.skin.label);
            //tstyle.fixedWidth = Screen.width - 200;
            tstyle.normal.textColor = Color.red;
            tstyle.alignment = TextAnchor.MiddleLeft;
            GUILayout.Label("Type a unique name to add a new Item", GUILayout.Width(230));



            tstyle = new GUIStyle(GUI.skin.button);
            tstyle.fixedWidth = 50;

            currentName = EditorGUILayout.TextField(currentName, GUI.skin.textArea);
            if (GUILayout.Button("Add", tstyle))
            {

                if (currentName == "" || currentName == null) return;
                bool isDup = false;
                for (int i = 0; i < itemdatabase.itemList.Count; i++)
                {
                    if (itemdatabase.itemList[i].itemName == currentName)
                    {
                        isDup = true;
                        break;

                    }
                }
                if (isDup) return;
                //State tstate = new State();
                //tstate.StateName = currentName;
                //PlayerPrefs.SetInt(currentName, 0);
                Item tItem = new Item();

                long ticks = DateTime.Now.Ticks;
                byte[] bytes = BitConverter.GetBytes(ticks);
                string id = Convert.ToBase64String(bytes)
                                        .Replace('+', '_')
                                        .Replace('/', '-')
                                        .TrimEnd('=');

                //tItem.itemID =  (int)(ticks);//itemdatabase.itemList.Count;
                tItem.itemID = itemdatabase.itemList.Count-1;
                //Debug.Log(tItem.itemID);
                //tItem.itemID = UnityEngine.Random.Range(0, 9999999);
                tItem.itemName = currentName;
                itemdatabase.itemList.Add(tItem);

            }

            if (GUILayout.Button("fold", tstyle))
            {
                cFold = !cFold;
                for (int i = 0; i < itemdatabase.itemList.Count; i++)                      //get through all items which are there in the itemdatabase
                {
                    
                    manageItem[i] = cFold;
                }
            }

                //start iterate list
                EditorGUILayout.EndHorizontal();
            //if (itemdatabase == null)
            //    itemdatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
            if (itemdatabase.itemList.Count == 0)                                  //if there is no item in the database you get this...yes it is right to have == 1
            {
                //GUILayout.Label("There is no Item in the Database!");                   //information that you do not have one
            }
            else
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                GUILayout.BeginVertical();
                for (int i = 0; i < itemdatabase.itemList.Count; i++)                      //get through all items which are there in the itemdatabase
                {
                   
                        manageItem.Add(false);                                                                                                  //foldout is closed at default
                        GUILayout.BeginVertical("Box");
                        EditorGUILayout.BeginHorizontal();
                        manageItem[i] = EditorGUILayout.Foldout(manageItem[i], "" + itemdatabase.itemList[i].itemName);                  
                        GUILayout.Label("Desc:",GUILayout.Width(50));
                        itemdatabase.itemList[i].itemDesc = EditorGUILayout.TextField(itemdatabase.itemList[i].itemDesc);
                        GUILayout.Label("localId:",GUILayout.Width(50));
                    itemdatabase.itemList[i].localId = EditorGUILayout.TextField(itemdatabase.itemList[i].localId,GUILayout.Width(100));

                    EditorGUILayout.EndHorizontal();
                        if (manageItem[i])                                                                                                      //if you press on it you get this
                        {

                            EditorUtility.SetDirty(itemdatabase);                                                                          //message the scriptableobject that you change something now                                                                                                
                            GUI.color = Color.red;                                                                                              //all upcoming GUIelements get changed to red
                            if (GUILayout.Button("Delete Item"))                                           //create button that deletes the item
                            {
                                itemdatabase.itemList.RemoveAt(i);                                                                         //remove the item out of the itemdatabase
                                EditorUtility.SetDirty(itemdatabase);                                                                      //and message the database again that you changed something
                            break;
                        }

                            EditorGUILayout.BeginHorizontal();
                            GUI.color = Color.white;                                                                                            //next GUIElements will be white
                            GUILayout.Label("Item Name");
                         ReadOnlyTextField("",itemdatabase.itemList[i].itemName);//EditorGUILayout.TextField(itemdatabase.itemList[i].itemName,GUI.skin.textArea);          //textfield for the itemname which you wanna create
                            itemdatabase.itemList[i].itemID = i;                                             //itemID getting set automaticly ...its unique...better do not change it :D  

                        tstyle = new GUIStyle(GUI.skin.label);
                            tstyle.stretchWidth = true;
                            ReadOnlyTextField("Item ID:", itemdatabase.itemList[i].itemID.ToString());
                            GUILayout.Label("Item Type");
                            itemdatabase.itemList[i].itemType = (ItemType)EditorGUILayout.EnumPopup(itemdatabase.itemList[i].itemType);                                      //the itemtype which you want to have can be selected with the enumpopup
                            GUILayout.EndHorizontal();
                            
                          

                            //GUILayout.EndHorizontal();
                            GUILayout.Label("Item Icon");
                            itemdatabase.itemList[i].itemIcon = (Sprite)EditorGUILayout.ObjectField(itemdatabase.itemList[i].itemIcon, typeof(Sprite), false, GUILayout.Width(50),GUILayout.Height(50));         //objectfield for the itemicon for your new item

                       

                            EditorUtility.SetDirty(itemdatabase);                                                                                              //message scriptable object that you have changed something
                        }
                        GUILayout.EndVertical();
                  

                }
                GUILayout.EndVertical();
                EditorGUILayout.EndScrollView();
            }



        }


        public void OnInspectorUpdate()
        {

            // This will only get called 10 times per second.
            Repaint();
        }

        void Header()
        {

            GUILayout.BeginHorizontal();

            if (headTexture == null || emailTexture == null || skypeTexture == null || folderIcon == null)
            {
                headTexture = Resources.Load<Texture>("EditorWindowTextures/headTexture");
                skypeTexture = Resources.Load<Texture>("EditorWindowTextures/skype-icon");
                emailTexture = Resources.Load<Texture>("EditorWindowTextures/email-icon");
                folderIcon = Resources.Load<Texture>("EditorWindowTextures/folder-icon");
            }

            GUI.DrawTexture(new Rect(10, 10, 68, 68), headTexture);
            GUILayout.Space(80);

            GUILayout.BeginVertical();
            GUILayout.Space(10);

            GUILayout.BeginVertical("Box");
            GUILayout.Label("┌─Informations", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();

            GUI.DrawTexture(new Rect(85, 35, 12, 12), emailTexture);
            GUILayout.Space(25);
            if (GUILayout.Button("Contact E-mail(Invoice required)", GUIStyle.none))
            {
                Application.OpenURL(Const.email);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUI.DrawTexture(new Rect(85, 50, 12, 12), skypeTexture);
            GUILayout.Space(25);
            if (GUILayout.Button("Discuss & Support Forum", GUIStyle.none))
            {
                Application.OpenURL(Const.forumUrl);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUI.DrawTexture(new Rect(85, 62, 12, 12), folderIcon);
            GUILayout.Space(25);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Documentation", GUIStyle.none))
            {
                Application.OpenURL(Const.documentUrl);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

            GUILayout.EndVertical();

            GUILayout.EndHorizontal();


            EditorGUI.BeginChangeCheck();


        }





        void ReadOnlyTextField(string label, string text)
        {
            GUIStyle tstyle = new GUIStyle(GUI.skin.textField);
            tstyle.normal.textColor = Color.gray;
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label(label);//selectablelabel
                EditorGUILayout.SelectableLabel(text, tstyle, GUILayout.Height(EditorGUIUtility.singleLineHeight));
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}