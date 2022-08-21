using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CommunicationManager : MonoBehaviour
{
    public GameObject m_CommunicationWindow;
    public GameObject m_CommunicationText;

    [SerializeField]
    private TextWriter m_TextWriter;

    void Start()
    {
        ComputerMission computerMission = GameObject.Find("/Missions/Computer_Mission").GetComponent<ComputerMission>();
        computerMission.OnComputer += onComputerChanged;
    }

    public void ShowMsg(string i_Msg)
    {
        StartCoroutine(ShowMsgEnumerator(i_Msg));
    }

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

    public void onComputerChanged()
    {
        ShowMsg("well,it's seems like it is open now, nice one!");
    }
}
