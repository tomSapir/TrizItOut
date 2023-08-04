using System.IO;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace Hitcode_RoomEscape
{
    public class State_ManagerEditor : EditorWindow
    {
        [SerializeField]
        static StatesDataBase stateDataList;
       


        [MenuItem("Room Escape/State Manager")]

        static void Initialize()
        {
            State_ManagerEditor window = (State_ManagerEditor)EditorWindow.GetWindow(typeof(State_ManagerEditor), false, "State Manager");
        }
     
        int rate;
        string currentName;
        Vector2 scrollPos = Vector2.zero;

        static string projectName = "";
        void OnGUI()
        {

            Header();
            if (rate <= 200)
            {
                rate++;
            }
            else {
                rate = 0;
            }

            
           

            //if (stateDataList == null)
            //{
                

                GameObject config = Resources.Load("Configure") as GameObject;
                if (config != null)
                {
                    projectName = config.GetComponent<Configure>().currentProjectName;
                }

                stateDataList = (StatesDataBase)Resources.Load(projectName + "/StateDataBase");
                //if (stateDataList == null)
                //    stateDataList = CreateStateDatabase.createStateDatabase();
                //else
                //    stateDataList = (StatesDataBase)Resources.Load("StateDataBase");

            //}


            EditorGUILayout.BeginHorizontal();

            GUIStyle tstyle = new GUIStyle(GUI.skin.label);
            //tstyle.fixedWidth = Screen.width - 200;
            //tstyle.normal.textColor = Color.red;
            //tstyle.alignment = TextAnchor.MiddleLeft;
            EditorGUILayout.LabelField("Type a unique name to create a new state", GUILayout.Width(250));



            tstyle = new GUIStyle(GUI.skin.button);
            tstyle.fixedWidth = 50;
            
            currentName = EditorGUILayout.TextField(currentName, GUI.skin.textArea);
            
            if (GUILayout.Button("Add", tstyle))
            {
               
                if (currentName == "" || currentName == null) return;
                bool isDup = false;
                for (int i = 0; i < stateDataList.StateList.Count; i++)
                {
                    if (stateDataList.StateList[i].StateName == currentName)
                    {
                        isDup = true;
                        break;
                        
                    }
                }
                if (isDup) return;
                    State tstate = new State();
                tstate.StateName = currentName;
                PlayerPrefs.SetInt(currentName + 0, 0);
                stateDataList.StateList.Add(tstate);
                
            }

            if (GUILayout.Button("save", tstyle))
            {
                
                AssetDatabase.SaveAssets();
            }

            EditorGUILayout.EndHorizontal();
            

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            if (stateDataList == null || stateDataList.StateList == null) return;
            for (int i = 0; i < stateDataList.StateList.Count; i++)
            {


             
                EditorGUILayout.BeginHorizontal();

                


                //GUILayout.Label("name", tstyle);


                //stateDataList.StateList[i].StateName = EditorGUILayout.TextField(stateDataList.StateList[i].StateName,GUI.skin.textArea);
                ReadOnlyTextField("name", stateDataList.StateList[i].StateName);

                int tvalue = stateDataList.StateList[i].StateValue;
                if (PlayerPrefs.HasKey(stateDataList.StateList[i].StateName + 0) == false)
                {
                    PlayerPrefs.SetInt(stateDataList.StateList[i].StateName + 0, 0);
                }
                tvalue = PlayerPrefs.GetInt(stateDataList.StateList[i].StateName + 0);

                //int tvalue = stateDataList.StateList[i].StateValue;
                //stateDataList.StateList[i].StateValue = EditorGUILayout.IntField(tvalue, GUI.skin.textArea);
                ReadOnlyTextField("value", tvalue.ToString());

                GUILayout.Label("discription");
                stateDataList.StateList[i].stateDescription = EditorGUILayout.TextField(stateDataList.StateList[i].stateDescription, GUI.skin.textArea);
                


                if (GUILayout.Button("Remove", GUI.skin.button))
                {
                    PlayerPrefs.DeleteKey(stateDataList.StateList[i].StateName);
                    stateDataList.StateList.RemoveAt(i);
                   

                }






                EditorGUILayout.EndHorizontal();

               
            }

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Reset all Saved Data"))
            {
  

                for (int i = 0; i <= 6; i++)
                {
                    GameData.getInstance().clearSlot(i);
                }


            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndScrollView();



            if (GUI.changed) {
                EditorUtility.SetDirty(stateDataList);
            }

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



        public void OnInspectorUpdate()
        {
           
            // This will only get called 10 times per second.
            Repaint();
        }


        static Texture skypeTexture;
        static Texture emailTexture;
        static Texture headTexture;
        static Texture folderIcon;

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
            if(GUILayout.Button("Contact E-mail(Invoice required)", GUIStyle.none))
            {
                Application.OpenURL(Const.email);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();

            GUI.DrawTexture(new Rect(85, 50, 12, 12), skypeTexture);
            GUILayout.Space(25);
            if(GUILayout.Button("Discuss & Support Forum", GUIStyle.none))
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

    }






}