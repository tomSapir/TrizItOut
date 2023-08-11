using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
namespace Hitcode_RoomEscape
{
    [CustomEditor(typeof(TriggerMark))]


    public class TriggerMarkEditor : Editor
    {

        Texture2D checkOn = null;
        Texture2D checkOff = null;
        TriggerMark self;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            self = target as TriggerMark;
            //Undo.RecordObject(self, "trigger");

            Vector3 newpos = self.myCam.WorldToScreenPoint(self.transform.position);// - self.myCam.transform.position;

            Vector3 screenPos = self.myCam.transform.position;

            Vector3 screenSize = (new Vector3(GetMainGameViewSize().x, GetMainGameViewSize().y, 0));
        
            //(0,0)is bottom left
            switch (self.anchorType)
            {
                case TriggerMark.myEnum.topleft:
                    self.offset = new Vector3(newpos.x, newpos.y - screenSize.y , 0);
                    //Debug.Log("offset:  " + self.offset);
                    break;
                case TriggerMark.myEnum.top:
                    self.offset = new Vector3(newpos.x - screenSize.x/2  , newpos.y - screenSize.y , 0);
                    
                    break;
                case TriggerMark.myEnum.topright:
                    self.offset = new Vector3(newpos.x - screenSize.x, newpos.y - screenSize.y );
                    
                    break;
                case TriggerMark.myEnum.right:
                    self.offset = new Vector3(newpos.x - screenSize.x, newpos.y - screenSize.y/2, 0);
                    
                    break;
                case TriggerMark.myEnum.bottomright:
                    self.offset = new Vector3(newpos.x - screenSize.x, newpos.y, 0);
                    
                    break;
                case TriggerMark.myEnum.bottom:
                    self.offset = new Vector3(newpos.x - screenSize.x /2 , newpos.y, 0);
                    
                    break;
                case TriggerMark.myEnum.bottomleft:
                    self.offset = new Vector3(newpos.x, newpos.y, 0);
                    
                    break;
                case TriggerMark.myEnum.left:
                    self.offset = new Vector3(newpos.x, newpos.y - screenSize.y / 2, 0);
                    
                    break;
            }
            self.offsetXRatio = self.offset.x / screenSize.x;
            self.offsetYRatio = self.offset.y/ screenSize.y  ;

            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("use Design Offset Position");
            self.useDesignOffsetPosition = GUILayout.Toggle(self.useDesignOffsetPosition ,self.useDesignOffsetPosition ? checkOn : checkOff);

            
            EditorGUILayout.EndHorizontal();

           
            //GUILayout.Label("x");
            //offsetX = EditorGUILayout.IntField(tplay.actionIndex, GUI.skin.textArea, GUILayout.ExpandWidth(true));


        }



        public static Vector2 GetMainGameViewSize()
        {
            System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
            System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
            return (Vector2)Res;
        }

    }

    
}
