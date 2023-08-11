using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
namespace Hitcode_RoomEscape
{
    public class StartWizard : EditorWindow
    {
        [MenuItem("Room Escape/Create New Game",false,0)]
        //Itemdatabase
        static void Initialize()
        {
            StartWizard window = (StartWizard)EditorWindow.GetWindow(typeof(StartWizard), false, "Start Winzard");
        }

        static void Init()
        {
            EditorWindow.GetWindow(typeof(StartWizard));
 
                

            

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

       
   
        static GameObject config;
        string projectName = "";
        string warnText;
        void OnGUI()
        {
            Header();

            ItemsDataBase tItemDatabase;
            BlueprintDatabase tBlueprintDatabase;
            StatesDataBase tStateDatabase;
            JournalDataBase tJournalDatabase;

            if (config == null)
            {
                config = Resources.Load("Configure") as GameObject;

            }

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("enter a new name", GUILayout.Width(120));

            projectName = GUILayout.TextField(projectName);
            projectName = System.Text.RegularExpressions.Regex.Replace(projectName, @"[^a-zA-Z0-9]","");

            if (GUI.changed)
            {
                warnText = "";
            }

                EditorGUI.BeginDisabledGroup(projectName.Length <= 0);
                if (GUILayout.Button("Create New Project")) {

                if (!System.IO.Directory.Exists("Assets/Hitcode/src/Resources/"+projectName))
                {
                    string guid = AssetDatabase.CreateFolder("Assets/Hitcode/src/Resources", projectName);
                    string newFolderPath = AssetDatabase.GUIDToAssetPath(guid);


                    //check if assetbases already exist
                    //item
                    tItemDatabase = (ItemsDataBase)Resources.Load(projectName + "/ItemDatabase") as ItemsDataBase;
                    if (tItemDatabase == null)
                    {

                        tItemDatabase = CreateItemDatabase.createItemDatabase(projectName);

                    }

                    //craft
                    tBlueprintDatabase = (BlueprintDatabase)Resources.Load(projectName + "/BlueprintDatabase") as BlueprintDatabase;
                    if (tBlueprintDatabase == null)
                    {
                        tBlueprintDatabase = CreateBlueprintDatabase.createBlueprintDatabase(projectName);
                        //tBlueprintDatabase = (BlueprintDatabase)Resources.Load("BlueprintDatabase");
                    }

                    //states
                    tStateDatabase = (StatesDataBase)Resources.Load(projectName + "/StateDataBase") as StatesDataBase;
                    if (tStateDatabase == null)
                    {

                        tStateDatabase = CreateStateDatabase.createStateDatabase(projectName);

                    }

                    //journal
                    tJournalDatabase = (JournalDataBase)Resources.Load(projectName + "/JounalDataBase") as JournalDataBase;
                    if (tJournalDatabase == null)
                    {

                        tJournalDatabase = CreateJournalDatabase.createJounalDatabase(projectName);

                    }


                    //set config current project name immediatly

                    var newConfig = Instantiate(config);
                    newConfig.GetComponent<Configure>().currentProjectName = projectName;

                    PrefabUtility.ReplacePrefab(
                            newConfig,
                            config,
                            ReplacePrefabOptions.ConnectToPrefab
                            );

                    warnText = "project create succeeded!";

                    //clear old project saved data(just game setting)
                    for(int i = 0; i <= 6; i++)
                    {
                        GameData.getInstance().clearSlot(i);
                    }

                }
                else
                {
                    warnText = "Already exitst.Please change a name";
                }
            }
            EditorGUI.EndDisabledGroup();
           
            EditorGUILayout.EndHorizontal();
            //warning text2
            GUIStyle tstyle = new GUIStyle(GUI.skin.label);
            //tstyle.fixedWidth = Screen.width - 200;
            tstyle.normal.textColor = Color.red;
            if (warnText != null && warnText != "")
            {
                EditorGUILayout.LabelField(warnText, tstyle);
            }


            EditorGUILayout.BeginHorizontal();

            

            GUILayout.Label("Current Project",GUILayout.Width(120));
            EditorGUI.BeginDisabledGroup(true);
            string currentProjectName = "";
            try
            {
                currentProjectName = config.GetComponent<Configure>().currentProjectName;
                currentProjectName = GUILayout.TextField(currentProjectName);
                EditorGUI.EndDisabledGroup();

                config = (GameObject)EditorGUILayout.ObjectField(config, typeof(GameObject), false);// GUILayout.Width(50), GUILayout.Height(50));
            }catch(System.Exception e)
            {
                return;
            }
            EditorGUILayout.EndHorizontal();


            

            EditorGUI.BeginDisabledGroup(true);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("itemAsset",GUILayout.Width(120));
            tItemDatabase = Resources.Load(currentProjectName + "/ItemDatabase") as ItemsDataBase;
            EditorGUILayout.ObjectField(tItemDatabase, typeof(ItemsDataBase), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("CraftDatabase", GUILayout.Width(120));
            tBlueprintDatabase = Resources.Load(currentProjectName + "/BlueprintDatabase") as BlueprintDatabase;
            EditorGUILayout.ObjectField(tBlueprintDatabase, typeof(BlueprintDatabase), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("StateDatabase", GUILayout.Width(120));
            tStateDatabase = Resources.Load(currentProjectName + "/StateDataBase") as StatesDataBase;
            EditorGUILayout.ObjectField(tStateDatabase, typeof(StatesDataBase), false);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("JournalDatabase", GUILayout.Width(120));
            tJournalDatabase = Resources.Load(currentProjectName + "/JournalDatabase") as JournalDataBase;
            EditorGUILayout.ObjectField(tJournalDatabase, typeof(JournalDataBase), false);
            EditorGUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();

            //warning text1
            string isReadyWarnTxt = "";
            if(tItemDatabase !=null  && tBlueprintDatabase != null && tStateDatabase != null && tJournalDatabase !=null)
            {
                isReadyWarnTxt = "Project is working fine now!!";
            }
            else
            {
                isReadyWarnTxt = "File missing.Please change project name on configure prefab.";
            }
            tstyle = new GUIStyle(GUI.skin.label);
            tstyle.normal.textColor = Color.red;
            EditorGUILayout.LabelField(isReadyWarnTxt, tstyle);





            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Click here to reset all Saved Data(just game progress) after change a project."))
            {
  
                for (int i = 0; i <= 6; i++)
                {
                    GameData.getInstance().clearSlot(i);
                }

                PlayerPrefs.DeleteAll();
            }
            GUI.backgroundColor = Color.white;



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