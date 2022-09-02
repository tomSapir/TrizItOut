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

    IEnumerator ShowMsgEnumerator(string i_Msg)
    {
        m_CommunicationWindow.SetActive(true);
        m_CommunicationText.SetActive(true);
        m_TextWriter.AddWriter(m_CommunicationText.GetComponent<Text>(), i_Msg, 0.05f);
        yield return new WaitForSeconds(8);
        m_CommunicationWindow.SetActive(false);
        m_CommunicationText.SetActive(false);
        m_CommunicationText.GetComponent<Text>().text = string.Empty;
    }
}
