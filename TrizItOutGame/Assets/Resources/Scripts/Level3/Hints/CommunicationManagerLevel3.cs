using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CommunicationManagerLevel3 : MonoBehaviour
{
    public GameObject m_CommunicationWindow;
    public GameObject m_CommunicationText;
    public TextWriter m_TextWriter;

    void Start()
    {
        GameObject.Find("Door_Mission").GetComponent<DoorMissionHandler>().doorWasOpendEvent += onDoorWasOpen;
    }

    private void onDoorWasOpen()
    {
        ShowMsg("It seems like you can move to another room now...");
        print("Im here");
    }

    public void ShowMsg(string i_Msg)
    {
        StartCoroutine(ShowMsgEnumerator(i_Msg));
    }

    IEnumerator ShowMsgEnumerator(string i_Msg)
    {
        m_CommunicationWindow.SetActive(true);
        m_CommunicationText.SetActive(true);
        m_TextWriter.AddWriter(m_CommunicationText.GetComponent<Text>(), i_Msg, 0.05f);
        yield return new WaitForSeconds(8);
        m_CommunicationText.GetComponent<Text>().text = string.Empty;
        m_CommunicationWindow.SetActive(false);
        m_CommunicationText.SetActive(false);
    }
}
