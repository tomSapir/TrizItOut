using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace Hitcode_RoomEscape
{
    [CustomEditor(typeof(Actions))]
    //	[CanEditMultipleObjects] 

    public class ActionsEditor : Editor
    {


        Actions self;
        Texture2D checkOn = null;
        Texture2D checkOff = null;
        public override void OnInspectorGUI()
        {

            self = target as Actions;

            Undo.RecordObject(self, "action");


            if (self.actionSteps == null)
            {
                self.actionSteps = new List<Action>();
               
            }
    



            //return;

            //tool bar for add states
            EditorGUILayout.BeginHorizontal(GUI.skin.box);

            //         ActionManager am = ScriptableObject.CreateInstance<ActionManager>();
            //int tActionId = am.generateId (self.gameObject);
            if (self.actionsId == 0 || self.actionsId == null) self.actionsId = GetInstanceID();
            EditorGUILayout.LabelField("ActionList ID:" + self.actionsId);

            //		self.cType = (StateTypes)EditorGUILayout.EnumPopup(self.cType);


            if (GUILayout.Button("Add", GUI.skin.button))
            {

                self.actionSteps.Add(null);
            }

            if (GUILayout.Button("clear", GUI.skin.button))
            {

                self.actionSteps = new List<Action>();
            }

            EditorGUILayout.EndHorizontal();



            GUIStyle gs = new GUIStyle();
            gs.normal.background = Texture2D.whiteTexture;
            gs.alignment = TextAnchor.MiddleCenter;


            for (int i = 0; i < self.actionSteps.Count; i++)
            {//steps
                EditorGUILayout.BeginVertical(GUI.skin.box);





                EditorGUILayout.BeginHorizontal();
                gs.normal.textColor = Color.red;



                EditorGUILayout.LabelField("Actions Index: " + i, gs);
                gs.normal.textColor = Color.blue;




                if (GUILayout.Button("Del", GUI.skin.button))
                {
                    self.actionSteps.RemoveAt(i);
                    return;
                }

                EditorGUILayout.EndHorizontal();
                //tool bar to add new gameobjects
                EditorGUILayout.BeginHorizontal();

                Action tAction = (Action)self.actionSteps[i];
                if (tAction == null)
                {
                    tAction = new Action();//init the just add action
                    //Debug.Log("taction" + tAction);
                }
                tAction.cType = (actionTypes)EditorGUILayout.EnumPopup(tAction.cType);//just for temp use,to tell which types of input ui created


                if (tAction == null) return;
                //define show_hide if not exist
                if (tAction.showHides == null)
                    tAction.showHides = new Show_hides();
                Show_hides tshowHides = tAction.showHides;
                if (tshowHides.show_hide == null) tshowHides.show_hide = new List<Show_hide>();
     


                //define transfroms if not exist
                if (tAction.transfroms == null)
                    tAction.transfroms = new Transforms();
                Transforms tTransforms = tAction.transfroms;
                if (tTransforms._transfrom == null) tTransforms._transfrom = new List<_Transform>();



                //define rotate if not exist
                if (tAction.rotates == null)
                    tAction.rotates = new _Rotates();
                _Rotates tRotates = tAction.rotates;
                if (tRotates._transfrom == null) tRotates._transfrom = new List<_Rotate>();



                //define pickup if not exist
                if (tAction.pickups == null)
                    tAction.pickups = new PickUps();
                PickUps tPickups = tAction.pickups;
                if (tPickups._pickups == null) tPickups._pickups = new List<PickUp>();


                //define playanim if not exist
                if (tAction.playanims == null)
                    tAction.playanims = new PlayAnims();
                PlayAnims tplayAnims = tAction.playanims;
                if (tplayAnims.playAnim == null) tplayAnims.playAnim = new List<PlayAnim>();


                //define switchCamera if not exist
                if (tAction.switchCameras == null)
                    tAction.switchCameras = new SwitchCameras();
                SwitchCameras tswitchCameras = tAction.switchCameras;
                if (tswitchCameras.swtichCamera == null) tswitchCameras.swtichCamera = new List<SwitchCamera>();


                //define switchScene if not exist
                if (tAction.switchScenes == null)
                    tAction.switchScenes = new SwitchScenes();
                SwitchScenes tswitchScenes = tAction.switchScenes;
                if (tswitchScenes.SwitchScene == null) tswitchScenes.SwitchScene = new List<SwitchScene>();

                //define play Text if not exist
                if (tAction.playTexts == null)
                    tAction.playTexts = new PlayTexts();
                PlayTexts tplayTexts = tAction.playTexts;
                if (tplayTexts.playTexts == null) tplayTexts.playTexts = new List<PlayText>();

                //define play sound if not exist
                if (tAction.playSounds == null)
                    tAction.playSounds = new PlaySounds();
                PlaySounds tPlaySounds = tAction.playSounds;
                if (tPlaySounds.playSounds == null) tPlaySounds.playSounds = new List<PlaySound>();

                //define setStates if not exist
                if (tAction.setStates == null)
                    tAction.setStates = new SetStates();
                SetStates tSetStates = tAction.setStates;
                if (tSetStates.stateList == null) tSetStates.stateList = new List<_State>();

                //define sendMsg if not exist
                if (tAction.sendMsgs == null)
                    tAction.sendMsgs = new SendMsgs();
                SendMsgs tSendMsgs = tAction.sendMsgs;
                if (tSendMsgs.sendMsgList == null) tSendMsgs.sendMsgList = new List<SendMsg>();

                //define comment if not exist
                if (tAction.comments == null)
                    tAction.comments = new Comments();
                Comments tComments = tAction.comments;
                if (tComments.comment == null)
                    tComments.comment = new List<string>();


                //define lockdelay if not exist
                if (tAction.lockDelays == null)
                    tAction.lockDelays = new LockDelays();
                LockDelays tLockDelays = tAction.lockDelays;
                if (tLockDelays.lockDelays == null)
                    tLockDelays.lockDelays = new List<LockDelay>();


                //define lockdelay if not exist
                if (tAction.journals == null)
                    tAction.journals = new Journals();
                Journals tJounals = tAction.journals;
                if (tJounals.journals == null)
                    tJounals.journals = new List<Journal>();



                //load data from each state
                if (GUILayout.Button("+", GUI.skin.button))
                {
                    //add each detail action to each actions

                    switch (tAction.cType)
                    {
                        case actionTypes.showhide:
                            //init one element for this actions
                            tshowHides.show_hide.Add(new Show_hide());
                            break;
                        case actionTypes.Move:
                            tTransforms._transfrom.Add(new _Transform());
                            break;
                        case actionTypes.Rotate:
                            tRotates._transfrom.Add(new _Rotate());
                            break;
                        case actionTypes.pickup:
                            tPickups._pickups.Add(new PickUp());
                            break;
                        case actionTypes.playAnim:
                            tplayAnims.playAnim.Add(new PlayAnim());
                            break;
                        case actionTypes.swtichCamera:
                            //only need one camera
                            if (tswitchCameras.swtichCamera.Count == 0)
                            {
                                tswitchCameras.swtichCamera.Add(new SwitchCamera());
                            }
                            break;
                        case actionTypes.switchScene:
                            //only need one
                            if (tswitchScenes.SwitchScene.Count == 0)
                            {
                                tswitchScenes.SwitchScene.Add(new SwitchScene());
                            }
                            break;
                        case actionTypes.playText:
                            //only need one
                            if (tplayTexts.playTexts.Count == 0)
                            {
                                tplayTexts.playTexts.Add(new PlayText());
                            }
                            break;
                        case actionTypes.playSound:
                            tPlaySounds.playSounds.Add(new PlaySound());
                            break;
                        case actionTypes.setState:
                            tSetStates.stateList.Add(new _State());
                            break;
                        case actionTypes.sendMessage:
                            tSendMsgs.sendMsgList.Add(new SendMsg());
                            break;
                        case actionTypes.comment:
                            if (tComments.comment.Count == 0)
                            {
                                tComments.comment.Add("");
                            }
                            break;
                        case actionTypes.lockDelay:
                            if (tLockDelays.lockDelays.Count == 0)
                            {
                                tLockDelays.lockDelays.Add(new LockDelay());
                            }
                            break;
                        case actionTypes.journal:
                            if (tJounals.journals.Count == 0)
                            {
                                tJounals.journals.Add(new Journal());
                            }
                            break;
                    }

                }//switch add






                EditorGUILayout.EndHorizontal();

                //refresh  lists in each actions


                for (int j = 0; j < tshowHides.show_hide.Count; j++)
                {//always one show,one hide(even not one of them not used)



                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("SHOW");
                    EditorGUI.BeginDisabledGroup(tshowHides.show_hide[j].ishowSelf);



                    //EditorGUIUtility.labelWidth = 50;
                    tshowHides.show_hide[j].object2Show = (GameObject)EditorGUILayout.ObjectField(tshowHides.show_hide[j].object2Show, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();


                    GUILayout.Label("self");
                    tshowHides.show_hide[j].ishowSelf = GUILayout.Toggle(tshowHides.show_hide[j].ishowSelf, tshowHides.show_hide[j].ishowSelf ? checkOn : checkOff);

                    if (tshowHides.show_hide[j].ishowSelf)
                    {
                        tshowHides.show_hide[j].object2Show = self.gameObject;
                    }

                    GUILayout.Label("HIDE");
                    EditorGUI.BeginDisabledGroup(tshowHides.show_hide[j].isHideSelf);
                    tshowHides.show_hide[j].object2Hide = (GameObject)EditorGUILayout.ObjectField(tshowHides.show_hide[j].object2Hide, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Label("self");
                    tshowHides.show_hide[j].isHideSelf = GUILayout.Toggle(tshowHides.show_hide[j].isHideSelf, tshowHides.show_hide[j].isHideSelf ? checkOn : checkOff);

                    if (tshowHides.show_hide[j].isHideSelf)
                    {
                        tshowHides.show_hide[j].object2Hide = self.gameObject;
                    }
                    

                    gs.stretchWidth = true;
                    if (GUILayout.Button("-", GUI.skin.button))
                    {

                        tshowHides.show_hide.RemoveAt(j);

                    }
                    EditorGUILayout.EndHorizontal();




                    EditorGUILayout.EndVertical();
                }//hide_show


                //iterate transfroms
                for (int j = 0; j < tTransforms._transfrom.Count; j++)
                {//
                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginDisabledGroup(tTransforms._transfrom[j].isSelf);

                    GUILayout.Label("move");
                    tTransforms._transfrom[j].transformGameObject = (GameObject)EditorGUILayout.ObjectField(tTransforms._transfrom[j].transformGameObject, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();


                    GUILayout.Label("self");
                    tTransforms._transfrom[j].isSelf = GUILayout.Toggle(tTransforms._transfrom[j].isSelf, tTransforms._transfrom[j].isSelf ? checkOn : checkOff);

                  
                    if (tTransforms._transfrom[j].isSelf)
                    {
                        tTransforms._transfrom[j].transformGameObject = self.gameObject;
                        
                    }
                  

         
                    GUIStyle tstyle1 = new GUIStyle(GUI.skin.textField);
                    tstyle1.fixedWidth = 25;

              

                    float x = tTransforms._transfrom[j].offsets.x, y = tTransforms._transfrom[j].offsets.y, z = tTransforms._transfrom[j].offsets.z;

                    float clabelwidth = EditorGUIUtility.labelWidth;
                    float cfieldwidth = EditorGUIUtility.fieldWidth;
                    EditorGUIUtility.labelWidth = 10;
                    EditorGUIUtility.fieldWidth = 25;
                    x = EditorGUILayout.FloatField("x",x);
                    y = EditorGUILayout.FloatField("y",y);
                    z = EditorGUILayout.FloatField("z",z);
                    
                    tTransforms._transfrom[j].offsets = new Vector3(x, y, z);



                    tTransforms._transfrom[j].tween = EditorGUILayout.FloatField("tween",tTransforms._transfrom[j].tween);



                    if (GUILayout.Button("-", GUI.skin.button))
                    {

                        tTransforms._transfrom.RemoveAt(j);
                    }

                    EditorGUIUtility.labelWidth = clabelwidth;
                    EditorGUIUtility.fieldWidth = cfieldwidth;

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();

                    
                }



                //iterate rotates
                for (int j = 0; j < tRotates._transfrom.Count; j++)
                {//
                    EditorGUILayout.BeginVertical(GUI.skin.box);

                    EditorGUILayout.BeginHorizontal();
                    EditorGUI.BeginDisabledGroup(tRotates._transfrom[j].isSelf);

                    GUILayout.Label("rotate");
                    tRotates._transfrom[j].transformGameObject = (GameObject)EditorGUILayout.ObjectField(tRotates._transfrom[j].transformGameObject, typeof(GameObject), true);
                    EditorGUI.EndDisabledGroup();


                    GUILayout.Label("self");
                    tRotates._transfrom[j].isSelf = GUILayout.Toggle(tRotates._transfrom[j].isSelf, tRotates._transfrom[j].isSelf ? checkOn : checkOff);


                    if (tRotates._transfrom[j].isSelf)
                    {
                        tRotates._transfrom[j].transformGameObject = self.gameObject;
                    }



                    float clabelwidth = EditorGUIUtility.labelWidth;
                    float cfieldwidth = EditorGUIUtility.fieldWidth;
                    EditorGUIUtility.labelWidth = 10;
                    EditorGUIUtility.fieldWidth = 25;
                    float x = tRotates._transfrom[j].offsets.x, y = tRotates._transfrom[j].offsets.y, z = tRotates._transfrom[j].offsets.z;
                    x = EditorGUILayout.FloatField("x", x);
                    y = EditorGUILayout.FloatField("y", y);
                    z = EditorGUILayout.FloatField("z", z);
                    tRotates._transfrom[j].offsets = new Vector3(x, y, z);


                    tRotates._transfrom[j].tween = EditorGUILayout.FloatField("tween",tRotates._transfrom[j].tween);



                    if (GUILayout.Button("-", GUI.skin.button))
                    {

                        tRotates._transfrom.RemoveAt(j);
                    }

                    EditorGUIUtility.labelWidth = clabelwidth;
                    EditorGUIUtility.fieldWidth = cfieldwidth;


                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }



                //literate pickups
                for (int j = 0; j < tPickups._pickups.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    GUILayout.Label("Pick Up");

                    EditorGUI.BeginDisabledGroup(tPickups._pickups[j].isSelf);
                    tPickups._pickups[j].pickupTarget = (GameObject)EditorGUILayout.ObjectField(tPickups._pickups[j].pickupTarget, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Label("self");
                    tPickups._pickups[j].isSelf = GUILayout.Toggle(tPickups._pickups[j].isSelf, tPickups._pickups[j].isSelf ? checkOn : checkOff);

                    if (tPickups._pickups[j].isSelf)
                    {
                        tPickups._pickups[j].pickupTarget = self.gameObject;
                    }
                    //infinitive
                    GUILayout.Label("not remove at once");
                    tPickups._pickups[j].infinitive = GUILayout.Toggle(tPickups._pickups[j].infinitive, tPickups._pickups[j].infinitive ? checkOn : checkOff);

                   

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tPickups._pickups.RemoveAt(j);
                    }

                    EditorGUILayout.EndHorizontal();
                }

                //literate playAnims
                for (int j = 0; j < tplayAnims.playAnim.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    GUILayout.Label("Anim Target");

                    EditorGUI.BeginDisabledGroup(tplayAnims.playAnim[j].isSelf);
                    tplayAnims.playAnim[j].animGameObject = (GameObject)EditorGUILayout.ObjectField(tplayAnims.playAnim[j].animGameObject, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Label("self");
                    tplayAnims.playAnim[j].isSelf = GUILayout.Toggle(tplayAnims.playAnim[j].isSelf, tplayAnims.playAnim[j].isSelf ? checkOn : checkOff);

                    if (tplayAnims.playAnim[j].isSelf)
                    {
                        tplayAnims.playAnim[j].animGameObject = self.gameObject;
                    }


                    GUILayout.Label("trigger name");
                    tplayAnims.playAnim[j].animName = EditorGUILayout.TextField(tplayAnims.playAnim[j].animName, GUI.skin.textArea);
                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tplayAnims.playAnim.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                //literate camera,but actually only one is neccessary
                for (int j = 0; j < tswitchCameras.swtichCamera.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    GUILayout.Label("Target Camera");
                    tswitchCameras.swtichCamera[j].TargetCamera = (Camera)EditorGUILayout.ObjectField(tswitchCameras.swtichCamera[j].TargetCamera, typeof(Camera),true);
                    GUILayout.Label("append");
                    tswitchCameras.swtichCamera[j].append = GUILayout.Toggle(tswitchCameras.swtichCamera[j].append, tswitchCameras.swtichCamera[j].append ? checkOn : checkOff);
                    GUILayout.Label("record");
                    tswitchCameras.swtichCamera[j].record = GUILayout.Toggle(tswitchCameras.swtichCamera[j].record, tswitchCameras.swtichCamera[j].record ? checkOn : checkOff);


                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tswitchCameras.swtichCamera.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                //literate switch scene,but actually only one is neccessary
                for (int j = 0; j < tswitchScenes.SwitchScene.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);
                    GUILayout.Label("Target Scene Name");

                    tswitchScenes.SwitchScene[j].sceneName = EditorGUILayout.TextField(tswitchScenes.SwitchScene[j].sceneName, GUI.skin.textArea);
                 

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tswitchScenes.SwitchScene.RemoveAt(j);
                    }
                    EditorGUILayout.EndHorizontal();
                }

                //literate playText
             
                for (int j = 0; j < tplayTexts.playTexts.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();



                    GUILayout.Label("Play Text", GUILayout.ExpandWidth(true));
                    GUIStyle textStyle = new GUIStyle(GUI.skin.textField);
                    textStyle.normal.textColor = Color.blue;
                    textStyle.stretchHeight = false;
                    tplayTexts.playTexts[j].text = EditorGUILayout.TextField(tplayTexts.playTexts[j].text, textStyle);

                    float clabelwidth = EditorGUIUtility.labelWidth;
                    float cfieldwidth = EditorGUIUtility.fieldWidth;
                    EditorGUIUtility.labelWidth = 60;
                    EditorGUIUtility.fieldWidth = 25;
                    tplayTexts.playTexts[j].typeWrite = EditorGUILayout.FloatField("typeWrite", tplayTexts.playTexts[j].typeWrite);

                    GUILayout.Label("localId:", GUILayout.Width(50));
                    tplayTexts.playTexts[j].localId = EditorGUILayout.TextField(tplayTexts.playTexts[j].localId, GUILayout.Width(50));

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tplayTexts.playTexts.RemoveAt(j);

                    }

                    EditorGUIUtility.labelWidth = clabelwidth;
                    EditorGUIUtility.fieldWidth = cfieldwidth;

                    EditorGUILayout.EndHorizontal();
                }


                //literate playSound

                for (int j = 0; j < tPlaySounds.playSounds.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal(GUI.skin.box);


                    GUILayout.Label("mute", GUILayout.Width(30));
                    tPlaySounds.playSounds[j].mute = GUILayout.Toggle(tPlaySounds.playSounds[j].mute, tPlaySounds.playSounds[j].mute ? checkOn : checkOff, GUILayout.Width(20));

                    //EditorGUI.BeginDisabledGroup(tPlaySounds.playSounds[j].mute);
                    GUILayout.Label("BG", GUILayout.Width(20));
                    tPlaySounds.playSounds[j].isBG = GUILayout.Toggle(tPlaySounds.playSounds[j].isBG, tPlaySounds.playSounds[j].isBG ? checkOn : checkOff, GUILayout.Width(20));

                    EditorGUI.BeginDisabledGroup(tPlaySounds.playSounds[j].isBG);
                    GUILayout.Label("Loop", GUILayout.Width(30));
                    if (tPlaySounds.playSounds[j].isBG) tPlaySounds.playSounds[j].isLoop = true;
                    tPlaySounds.playSounds[j].isLoop = GUILayout.Toggle(tPlaySounds.playSounds[j].isLoop, tPlaySounds.playSounds[j].isLoop ? checkOn : checkOff, GUILayout.Width(20));
                    EditorGUI.EndDisabledGroup();

                    GUILayout.Label("Sound Name:", GUILayout.Width(100));
                    tPlaySounds.playSounds[j].soundName = GUILayout.TextField(tPlaySounds.playSounds[j].soundName);
                    //EditorGUI.EndDisabledGroup();






                    if (GUILayout.Button("-", GUI.skin.button, GUILayout.Width(20)))
                    {
                        tPlaySounds.playSounds.RemoveAt(j);

                    }

                    EditorGUILayout.EndHorizontal();
                }


                //iterate setstates
                for (int j = 0; j < tSetStates.stateList.Count; j++)
                {
                    

                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.BeginHorizontal();
                  

                    GUILayout.Label("Set State");
                    tSetStates.stateList[j].stateName = EditorGUILayout.TextField(tSetStates.stateList[j].stateName, GUI.skin.textArea);
                    tSetStates.stateList[j].stateOp = (valueOp)EditorGUILayout.EnumPopup(tSetStates.stateList[j].stateOp);

                    GUILayout.Label("value");
                    tSetStates.stateList[j].stateValue = EditorGUILayout.IntField(tSetStates.stateList[j].stateValue, GUI.skin.textArea);


                    if (GUILayout.Button("Force Set", GUI.skin.button))
                    {

                        if (PlayerPrefs.HasKey(tSetStates.stateList[j].stateName + 0)) 
                            {
                                PlayerPrefs.SetInt(tSetStates.stateList[j].stateName + 0, tSetStates.stateList[j].stateValue);
                                break;
                            }
                    }

                  
                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tSetStates.stateList.RemoveAt(j);
                    }


                    
            

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.EndVertical();
                }

                //literate sendmsg
                for (int j = 0; j < tSendMsgs.sendMsgList.Count; j++)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Target Object");

                    EditorGUI.BeginDisabledGroup(tSendMsgs.sendMsgList[j].isSelf);
                    tSendMsgs.sendMsgList[j].msgTarget = (GameObject)EditorGUILayout.ObjectField(tSendMsgs.sendMsgList[j].msgTarget, typeof(GameObject),true);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Label("self");
                    tSendMsgs.sendMsgList[j].isSelf = GUILayout.Toggle(tSendMsgs.sendMsgList[j].isSelf, tSendMsgs.sendMsgList[j].isSelf ? checkOn : checkOff);

                    if (tSendMsgs.sendMsgList[j].isSelf)
                    {
                        tSendMsgs.sendMsgList[j].msgTarget = self.gameObject;
                    }

                    GUILayout.Label("Func");
                    tSendMsgs.sendMsgList[j].MsgName = GUILayout.TextField(tSendMsgs.sendMsgList[j].MsgName, GUILayout.Width(80));// EditorGUILayout.TextField("")


                    GUILayout.Label("param");
                    tSendMsgs.sendMsgList[j].param = GUILayout.TextField(tSendMsgs.sendMsgList[j].param,GUILayout.Width(50));// EditorGUILayout.TextField("")

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tSendMsgs.sendMsgList.RemoveAt(j);
                    }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();
            }


               





                //iterate lockDelay
                for (int j = 0; j < tLockDelays.lockDelays.Count; j++)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Action Target");

                    EditorGUI.BeginDisabledGroup(tLockDelays.lockDelays[j].isSelf);
                    tLockDelays.lockDelays[j].target = (GameObject)EditorGUILayout.ObjectField(tLockDelays.lockDelays[j].target, typeof(GameObject), true);
                    EditorGUI.EndDisabledGroup();
                    GUILayout.Label("self");
                    tLockDelays.lockDelays[j].isSelf = GUILayout.Toggle(tLockDelays.lockDelays[j].isSelf, tLockDelays.lockDelays[j].isSelf ? checkOn : checkOff);

                    if (tLockDelays.lockDelays[j].isSelf)
                    {
                        tLockDelays.lockDelays[j].target = self.gameObject;
                    }

                    float clabelwidth = EditorGUIUtility.labelWidth;
                    float cfieldwidth = EditorGUIUtility.fieldWidth;
                    EditorGUIUtility.labelWidth = 50;
                    EditorGUIUtility.fieldWidth = 25;
                    tLockDelays.lockDelays[j].actionId = EditorGUILayout.IntField("actionId", tLockDelays.lockDelays[j].actionId);

           
                    tLockDelays.lockDelays[j].delayTime = EditorGUILayout.FloatField("Delay", tLockDelays.lockDelays[j].delayTime);


                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tLockDelays.lockDelays.RemoveAt(j);
                    }


                    EditorGUIUtility.labelWidth = clabelwidth;
                    EditorGUIUtility.fieldWidth = cfieldwidth;

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }


                //iterate journal
                for (int j = 0; j < tJounals.journals.Count; j++)
                {
                    EditorGUILayout.BeginVertical(GUI.skin.box);
                    EditorGUILayout.BeginHorizontal();
                    //GUILayout.Label("Jounal Name: ");

                   

                    float clabelwidth = EditorGUIUtility.labelWidth;
                    float cfieldwidth = EditorGUIUtility.fieldWidth;
                    EditorGUIUtility.labelWidth = 50;
                    EditorGUIUtility.fieldWidth = 25;

                    //GUILayout.Label("Journal Name:", GUILayout.Width(100));
                    //tJounals.journals[j].journalname = EditorGUILayout.TextField(tJounals.journals[j].journalname, GUILayout.Width(50));

                    GUILayout.Label("taked_param", GUILayout.Width(80));
                    tJounals.journals[j].taked_param = EditorGUILayout.TextField(tJounals.journals[j].taked_param, GUILayout.Width(50));


                    if (GUILayout.Button("-", GUI.skin.button,GUILayout.Width(20)))
                    {
                        tJounals.journals.RemoveAt(j);
                    }


                    EditorGUIUtility.labelWidth = clabelwidth;
                    EditorGUIUtility.fieldWidth = cfieldwidth;

                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.EndVertical();
                }


                //iterate comments
                List<string> tCommmentList = tComments.comment;
                for (int j = 0; j < tCommmentList.Count; j++)
                {
                    EditorGUILayout.BeginHorizontal();



                    GUILayout.Label("Comments", GUILayout.ExpandWidth(true));
                    GUIStyle textStyle = new GUIStyle(GUI.skin.textField);
                    textStyle.normal.textColor = Color.magenta;
                    textStyle.stretchHeight = false;
                    tCommmentList[j] = EditorGUILayout.TextField(tCommmentList[j], textStyle);

                    if (GUILayout.Button("-", GUI.skin.button))
                    {
                        tCommmentList.RemoveAt(j);

                    }

                    EditorGUILayout.EndHorizontal();
                }



                EditorGUILayout.EndVertical();


            }//for actionSteps


           
            EditorUtility.SetDirty(target);
            if (GUI.changed)
            {

                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(self.gameObject.scene);
                
            }


        }//oninspectgui;
    }
}
