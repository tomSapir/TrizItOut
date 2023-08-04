using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using fillpath;
using DialogueEditor;

namespace Hitcode_RoomEscape {
    public class DialogMsgManager : MonoBehaviour
    {
        // Start is called before the first frame update

        GameObject dialogues;
        void Start()
        {

            dialogues = GameObject.Find("Dialogues");

        }

        private void OnEnable()
        {
            DialogueEditor.ConversationManager.OnConversationStarted += started;
            DialogueEditor.ConversationManager.OnConversationEnded += ended;
        }

        private void ended()
        {
            GameData.Instance.locked = false;
        }

        private void started()
        {
            GameData.Instance.locked = true;   
        }

        private void OnDisable()
        {
            DialogueEditor.ConversationManager.OnConversationStarted -= started;
            DialogueEditor.ConversationManager.OnConversationEnded -= ended;
        }


        // Update is called once per frame
        void Update()
        {

        }

        void playConverstion(string DialogName)
        {
            //if (GameData.Instance.locked) return;
            GameData.Instance.locked = true;

            //print("play conversation");

            Transform cTalk = dialogues.transform.Find(DialogName + "_" + GameData.Instance.cLanguage);
            if (cTalk != null)
            {
                NPCConversation tc = cTalk.GetComponent<NPCConversation>();
                ConversationManager.Instance.StartConversation(tc);
                
            }
            else
            {
                cTalk = dialogues.transform.Find(DialogName);
                NPCConversation tc = cTalk.GetComponent<NPCConversation>();
                ConversationManager.Instance.StartConversation(tc);
               
            }

        }

        void unload()
        {
            print("unload dialogue");
            GameData.Instance.locked = false;
        }
    }
}
