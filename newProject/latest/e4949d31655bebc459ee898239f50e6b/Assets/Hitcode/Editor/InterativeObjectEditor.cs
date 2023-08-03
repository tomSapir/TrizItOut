using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Hitcode_RoomEscape
{
    [CustomEditor(typeof(InteractiveObject))]
    //[CanEditMultipleObjects] 
    public class InterativeObjectEditor : Editor
    {

        InteractiveObject self;
        List<GameObject> targetShow;
        List<GameObject> targetHide;

        Texture2D checkOn = null;
        Texture2D checkOff = null;
        public override void OnInspectorGUI()
        {
            self = target as InteractiveObject;

            Undo.RecordObject(self, "inter");

            EditorGUILayout.BeginHorizontal(GUI.skin.box);



            if (self.interactives == null)
            {
                self.interactives = new List<Interactive>();
            }


            if (GUILayout.Button("Add", GUI.skin.button))
            {

                self.interactives.Add(new Interactive());
            }

            if (GUILayout.Button("clear", GUI.skin.button))
            {

                self.interactives = new List<Interactive>();
            }

            EditorGUILayout.EndHorizontal();


            //if (self.interactives.Count > 0)
            //    EditorGUILayout.BeginHorizontal(GUI.skin.box);




            GUIStyle gs = new GUIStyle();
            gs.normal.background = Texture2D.whiteTexture;
            gs.alignment = TextAnchor.MiddleCenter;

            EditorGUI.BeginChangeCheck();
            List<Interactive> oldInteractives = self.interactives;
            for (int i = 0; i < self.interactives.Count; i++)
            {
                EditorGUILayout.BeginVertical(GUI.skin.box);

                EditorGUILayout.BeginHorizontal();
                gs.normal.textColor = Color.red;



                EditorGUILayout.LabelField("Interactive Index: " + i, gs);
                gs.normal.textColor = Color.blue;


                GUILayout.Label("autoCheck");
                bool tautoCheck = self.interactives[i].autoCheck;
                self.interactives[i].autoCheck = GUILayout.Toggle(tautoCheck, tautoCheck ? checkOn : checkOff);



                if (GUILayout.Button("Del", GUI.skin.button))
                {
                    self.interactives.RemoveAt(i);
                    return;
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                self.interactives[i].myType = (interactiveTypes)EditorGUILayout.EnumPopup(self.interactives[i].myType);//just for temp use,to tell which types of input ui created


                //init conditions in each interactive section
                List<Condition> tConditions = self.interactives[i].conditions;
                if (tConditions == null) tConditions = new List<Condition>();
                //success aciton in each interactive section
                List<playSuccessAction> tSuccessAction = self.interactives[i].playSuccessActions;
                if (tSuccessAction == null) tSuccessAction = new List<playSuccessAction>();
                //fail action in each interactive section
                List<playFailAction> tFailAction = self.interactives[i].playFailActions;
                if (tFailAction == null) tFailAction = new List<playFailAction>();
                //comment in each interactive section
                List<ConditionComment> tConditionComment = self.interactives[i].conditionComments;
                if (tConditionComment == null) tConditionComment = new List<ConditionComment>();

                if (GUILayout.Button("+", GUI.skin.button))
                {
                    switch (self.interactives[i].myType)
                    {
                        case interactiveTypes.condition:
                            tConditions.Add(new Condition());
                            break;
                        case interactiveTypes.playSuccessAction:
                            tSuccessAction.Add(new playSuccessAction());
                            break;
                        case interactiveTypes.playFailAction:
                            tFailAction.Add(new playFailAction());
                            break;
                        case interactiveTypes.comment:
                            tConditionComment.Add(new ConditionComment());
                            break;
                    }

                }

                EditorGUILayout.EndHorizontal();
                //set to data
                self.interactives[i].conditions = tConditions;
                self.interactives[i].playSuccessActions = tSuccessAction;
                self.interactives[i].playFailActions = tFailAction;



                //iterate all conditions.
                for (int j = 0; j < self.interactives[i].conditions.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();
                    Condition tcondition = self.interactives[i].conditions[j];

                    //GUILayout.Label("Need Item:");
                    tcondition.usingItem = (itemOP)EditorGUILayout.EnumPopup(tcondition.usingItem);

                    tcondition.currentItem = EditorGUILayout.TextField(tcondition.currentItem, GUI.skin.textArea);
                   
                    GUILayout.Label("Param");
                    tcondition.stateName = EditorGUILayout.TextField(tcondition.stateName, GUI.skin.textArea);
                    tcondition.op = (operators)EditorGUILayout.EnumPopup(tcondition.op);
                    //GUILayout.Label("Value");
                    tcondition.stateValue = EditorGUILayout.IntField(tcondition.stateValue, GUI.skin.textArea);

                    //set to data
                    self.interactives[i].conditions[j] = tcondition;

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        self.interactives[i].conditions.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();




                }

                //iterate all fail play actions.
                for (int j = 0; j < self.interactives[i].playFailActions.Count; j++)
                {

                    //list all play
                    EditorGUILayout.BeginHorizontal();
                    playFailAction tplay = self.interactives[i].playFailActions[j];

                    GUILayout.Label("Fail Action:");
                    EditorGUI.BeginDisabledGroup(tplay.isSelf);
                   
                    //tplay.actionName = EditorGUILayout.TextField(tplay.actionName, GUI.skin.textArea, GUILayout.ExpandWidth(true));
                    tplay.actionTarget = (GameObject)EditorGUILayout.ObjectField(tplay.actionTarget, typeof(GameObject), true);

                    EditorGUI.EndDisabledGroup();


                    GUILayout.Label("self");
                    tplay.isSelf = GUILayout.Toggle(tplay.isSelf, tplay.isSelf ? checkOn : checkOff);
                    if (tplay.isSelf)
                    {
                        tplay.actionTarget = self.gameObject;

                    }

                    GUILayout.Label("Index");
                    tplay.actionIndex = EditorGUILayout.IntField(tplay.actionIndex, GUI.skin.textArea, GUILayout.ExpandWidth(true));

                    //set to data
                    self.interactives[i].playFailActions[j] = tplay;

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        self.interactives[i].playFailActions.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                //iterate all success play actions.
                for (int j = 0; j < self.interactives[i].playSuccessActions.Count; j++)
                {

                    //list all play
                    EditorGUILayout.BeginHorizontal();
                    playSuccessAction tplay = self.interactives[i].playSuccessActions[j];



                    GUILayout.Label("Success Action:");
                    EditorGUI.BeginDisabledGroup(tplay.isSelf);
                   
                    //tplay.actionName = EditorGUILayout.TextField(tplay.actionName, GUI.skin.textArea, GUILayout.ExpandWidth(true));
                    tplay.actionTarget = (GameObject)EditorGUILayout.ObjectField(tplay.actionTarget,typeof(GameObject),true);

                    EditorGUI.EndDisabledGroup();


                    GUILayout.Label("self");
                    tplay.isSelf = GUILayout.Toggle(tplay.isSelf, tplay.isSelf ? checkOn : checkOff);
                    if (tplay.isSelf)
                    {
                        tplay.actionTarget = self.gameObject;
              
                    }

                    GUILayout.Label("Index");
                    tplay.actionIndex = EditorGUILayout.IntField(tplay.actionIndex, GUI.skin.textArea, GUILayout.ExpandWidth(true));






                    //set to data
                    self.interactives[i].playSuccessActions[j] = tplay;

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        self.interactives[i].playSuccessActions.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }





                //iterate all comment  actions.
                for (int j = 0; j < tConditionComment.Count; j++)
                {

                    EditorGUILayout.BeginHorizontal();



                    GUILayout.Label("Comments", GUILayout.ExpandWidth(true));
                    GUIStyle textStyle = new GUIStyle(GUI.skin.textField);
                    textStyle.normal.textColor = Color.magenta;
                    textStyle.stretchHeight = false;
                    tConditionComment[j].comment = EditorGUILayout.TextField(tConditionComment[j].comment, textStyle);

            

                    //set to data
                    self.interactives[i].conditionComments = tConditionComment;

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        self.interactives[i].conditionComments.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }




                EditorGUILayout.EndVertical();//for each interactive group

            }
   


            EditorUtility.SetDirty(target);
            if (GUI.changed)
            {

               
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(self.gameObject.scene);

            }

            



            //EditorGUILayout.EndVertical();//for each interactive group

        }




    }
}
