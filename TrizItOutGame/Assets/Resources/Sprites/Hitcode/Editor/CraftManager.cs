using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
namespace Hitcode_RoomEscape
{
    public class CraftManager : EditorWindow
    {
        [MenuItem("Room Escape/Craft Manager")]


        static void Initialize()
        {
            CraftManager window = (CraftManager)EditorWindow.GetWindow(typeof(CraftManager), false, "Craft Manager");

        }

        static void Init()
        {
            //EditorWindow.GetWindow(typeof(CraftManager));
            //headTexture = Resources.Load<Texture>("EditorWindowTextures/headTexture");
            //skypeTexture = Resources.Load<Texture>("EditorWindowTextures/skype-icon");
            //emailTexture = Resources.Load<Texture>("EditorWindowTextures/email-icon");
            //folderIcon = Resources.Load<Texture>("EditorWindowTextures/folder-icon");



            itemsDataBase = Resources.Load(projectName + "/ItemDatabase") as ItemsDataBase;
            if (itemsDataBase == null)
            {

            }




        }

        static Texture skypeTexture;
        static Texture emailTexture;
        static Texture headTexture;
        static Texture folderIcon;

        bool showInputManager = true;
        bool showItemDataBase = true;
        bool showBluePrintDataBase = true;

        //Itemdatabase
        static ItemsDataBase itemsDataBase = null;
        //static ItemsDataBase inventoryItemList = null;
        static ItemAttributeList itemAttributeList = null;
        //static InputManager inputManagerDatabase = null;


        Vector2 scrollPosition;

        static KeyCode test;

        bool showItemAttributes;
        string addAttributeName = "";
        int attributeAmount = 1;
        int[] attributeName;
        int[] attributeValue;

        int[] attributeNamesManage = new int[100];
        int[] attributeValueManage = new int[100];
        int attributeAmountManage;

        bool showItem;

        public int toolbarInt = 0;
        public string[] toolbarStrings = new string[] { "Create Items", "Manage Items" };

        //Blueprintdatabase
        static BlueprintDatabase bluePrintDatabase = null;
        List<bool> manageItem = new List<bool>();
        int amountOfFinalItem;
        //    float timeToCraft;
        int finalItemID;
        int amountofingredients;
        int[] ingredients;
        int[] amount;
        //ItemDataBaseList itemdatabase;
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

            GameObject config = Resources.Load("Configure") as GameObject;
            if (config != null)
            {
                projectName = config.GetComponent<Configure>().currentProjectName;
            }
           

            if (itemsDataBase == null) Init();


            //if (bluePrintDatabase == null)
            //{
                bluePrintDatabase = (BlueprintDatabase)Resources.Load(projectName + "/BlueprintDatabase");
                if (bluePrintDatabase == null)
                {
                    //bluePrintDatabase = CreateBlueprintDatabase.createBlueprintDatabase();
                    //bluePrintDatabase = (BlueprintDatabase)Resources.Load("BlueprintDatabase");
                    GUILayout.Label("Craft database missing.Please change a project in game config!");
                    return;
                }
            //}


            EditorGUILayout.BeginHorizontal();

            GUIStyle tstyle = new GUIStyle(GUI.skin.label);
            //tstyle.fixedWidth = Screen.width - 200;
            //tstyle.normal.textColor = Color.red;
            //tstyle.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.LabelField("Click add to add a bluepfrint", GUILayout.Width(230));

            //get itemlist in popup
            string[] items = new string[itemsDataBase.itemList.Count];
            for (int z = 0; z < items.Length; z++)
            {
                items[z] = itemsDataBase.itemList[z].itemName;
            }



            GUILayout.BeginHorizontal();

            GUI.color = Color.white;


            GUILayout.EndHorizontal();


            tstyle = new GUIStyle(GUI.skin.button);
            tstyle.fixedWidth = 50;


            if (GUILayout.Button("Add", tstyle))
            {

                Blueprint tBluePrint = new Blueprint();
                List<int> tIngredients = new List<int>();
                tBluePrint.ingredients = tIngredients;

                bluePrintDatabase.blueprints.Add(tBluePrint);

            }

            if (GUILayout.Button("fold", tstyle))
            {
                cFold = !cFold;
                for (int i = 0; i < bluePrintDatabase.blueprints.Count; i++)
                {

                    manageItem[i] = cFold;
                }
            }

            tstyle.fixedWidth = 80;
          

            EditorGUILayout.EndHorizontal();

            //check if have item to make blueprint
            if (itemsDataBase.itemList.Count == 0)
            {
                GUILayout.Label("no Item.Please add at least 2 items in item manager");
            }

            if (bluePrintDatabase.blueprints != null && bluePrintDatabase.blueprints.Count == 0)
            {
                //GUILayout.Label("There is no Blueprint in the Database!");
            }
            else
            {
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
                for (int i = 0; i < bluePrintDatabase.blueprints.Count; i++)
                {

                    manageItem.Add(false);
                    GUILayout.BeginVertical("Box");

                    GUI.color = Color.red;
                    if (GUILayout.Button("Delete Blueprint"))
                    {
                        bluePrintDatabase.blueprints.RemoveAt(i);
                        EditorUtility.SetDirty(bluePrintDatabase);
                        break;
                    }

                    GUI.color = Color.white;
                    EditorGUILayout.BeginHorizontal();
                    manageItem[i] = EditorGUILayout.Foldout(manageItem[i], "");
                    GUILayout.Label("Final Item");


                    ////make index correct
                    //Item tlastItem = itemsDataBase.itemList.Find(j => j.itemName == bluePrintDatabase.blueprints[i].finalItemName);
                    //if (tlastItem != null)
                    //{
                    //    bluePrintDatabase.blueprints[i].finalItemID = tlastItem.itemID;
                    //}

                    GUIContent tc = new GUIContent("select a result item_" + i);//to save and get the index


                    EdDelayedPopup.Popup(tc, () => {
                        Rect trect = EdDelayedPopup.ButtonRect;

                        int tmyIndex = EdDelayedPopup.myIndex1;
                        GenericMenu menu = new GenericMenu();
                        
                        //menu.AddItem(new GUIContent("MenuItem1"), false, Callback, "item 1");
                        //menu.AddItem(new GUIContent("MenuItem2"), false, Callback, "item 2");
                        //menu.AddSeparator("");
                        //menu.AddItem(new GUIContent("SubMenu/MenuItem3"), false, Callback, "item 3");
                        //menu.AddItem(new GUIContent("SubMenu/MenuItem4"), false, Callback, "item 4");
                        //menu.AddItem(new GUIContent("SubMenu/MenuItem5"), false, Callback, "item 5");
                        //menu.AddSeparator("SubMenu/");
                        //menu.AddItem(new GUIContent("SubMenu/MenuItem6"), false, Callback, "item 6");
                        for (int k = 0; k < items.Length; k++)
                        {
                            string tItemName = items[k];
                            menu.AddItem(new GUIContent(tItemName), false, () => {


                                bluePrintDatabase.blueprints[tmyIndex].finalItemName = tItemName;
                                //Debug.Log(tmyIndex);
                                return;


                            });

                        }

                        //menu.ShowAsContext();
                        menu.DropDown(trect);

                    });


                    ReadOnlyTextField("", bluePrintDatabase.blueprints[i].finalItemName);

                    EditorGUILayout.EndHorizontal();


                    if (manageItem[i])
                    {

                        GUI.color = Color.white;

                        EditorUtility.SetDirty(bluePrintDatabase);

                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Label("Ingredients");
                        GUI.color = Color.green;

                        EditorGUI.BeginDisabledGroup(bluePrintDatabase.blueprints[i].ingredients.Count >= 2);
                        if (GUILayout.Button("+", GUILayout.Width(60)))
                        {

                            bluePrintDatabase.blueprints[i].ingredients.Add(0);
                            bluePrintDatabase.blueprints[i].ingredientsName.Add("");
                            bluePrintDatabase.blueprints[i].amount.Add(0);
                        }
                        EditorGUI.EndDisabledGroup();

                        EditorGUILayout.EndHorizontal();
                        GUI.color = Color.white;
                        for (int k = 0; k < bluePrintDatabase.blueprints[i].ingredients.Count; k++)
                        {
                            GUILayout.BeginHorizontal();


                            GUI.color = Color.white;
                            GUILayout.Label("Ingredient " + (k + 1));



                            tc = new GUIContent("select item as Ingredient_" + i+"_"+k);//to save and get the index
                            EdDelayedPopup.Popup(tc, () => {
                                Rect trect = EdDelayedPopup.ButtonRect;

                                int tmyIndex1 = EdDelayedPopup.myIndex1;
                                int tmyIndex2 = EdDelayedPopup.myIndex2;
                                GenericMenu menu = new GenericMenu();

                     
                                for (int kk = 0; kk < items.Length; kk++)
                                {
                                    string tItemName = items[kk];
                                    menu.AddItem(new GUIContent(tItemName), false, () => {

                                        
                                        bluePrintDatabase.blueprints[tmyIndex1].ingredientsName[tmyIndex2] = tItemName;
                                        return;


                                    });

                                }

                                //menu.ShowAsContext();
                                menu.DropDown(trect);

                            });

                            if(bluePrintDatabase.blueprints[i]!=null && bluePrintDatabase.blueprints[i].ingredientsName.Count > k)
                            ReadOnlyTextField("", bluePrintDatabase.blueprints[i].ingredientsName[k]);

                            //bluePrintDatabase.blueprints[i].ingredients[k] = EditorGUILayout.Popup("", bluePrintDatabase.blueprints[i].ingredients[k], items, EditorStyles.popup);//danger,no use,dont uncomment
                            
                            //GUILayout.Label("value");
                            //bluePrintDatabase.blueprints[i].amount[k] = EditorGUILayout.IntField("", bluePrintDatabase.blueprints[i].amount[k]);
                            GUI.color = Color.red;
                            if (GUILayout.Button("-"))
                                bluePrintDatabase.blueprints[i].ingredients.RemoveAt(k);
                            GUILayout.EndHorizontal();
                        }
                        GUI.color = Color.white;


                      


                        EditorUtility.SetDirty(bluePrintDatabase);                                                                                              //message scriptable object that you have changed something
                    }
                    GUILayout.EndVertical();
            
                }
                EditorGUILayout.EndScrollView();
            }

        }



        public void Callback(object obj)
        {
            Debug.Log("Selected: " + obj);
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