using DialogueEditor;
using fillpath;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Hitcode_RoomEscape;
public class ClickAreaTouch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public SpeechNode m_action;
    public bool isOption;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void clickContinue()
    {
        //print("aaaaa");
        if (Hitcode_RoomEscape.GameData.Instance.Textlocked1)
        {
            ConversationManager.Instance.showFull();
        }
        else
        {
            if (isOption)
            {
                return;
            }
            if (m_action != null)
            {
                ConversationManager.Instance.DoSpeech(m_action);
            }
            else
            {
                ConversationManager.Instance.OptionSelected(null);
                m_action = null;
            }
            
        }
    }
}
