using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommunicationManagerLevel3 : MonoBehaviour
{
    public GameObject m_CommunicationWindow;
    public GameObject m_CommunicationText;
    public TextWriter m_TextWriter;

    IEnumerator ShowMsgEnumerator(string i_Msg)
    {
        m_CommunicationWindow.SetActive(true);
        m_CommunicationText.SetActive(true);
        m_TextWriter.AddWriter(m_CommunicationText.GetComponent<TextMeshProUGUI>(), i_Msg, 0.05f);
        yield return new WaitForSeconds(4);
        m_CommunicationWindow.SetActive(false);
        m_CommunicationText.SetActive(false);
        m_CommunicationText.GetComponent<TextMeshProUGUI>().text = string.Empty;
    }
}
