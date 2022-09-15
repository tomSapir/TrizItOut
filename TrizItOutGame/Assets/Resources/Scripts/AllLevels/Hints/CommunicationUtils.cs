using UnityEngine;
using UnityEngine.SceneManagement;

public class CommunicationUtils : MonoBehaviour
{
    public static void FindCommunicationManagerAndShowMsg(string i_Msg)
    {
        if (SceneManager.GetActiveScene().name == "Level1_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel1>().ShowMsg(i_Msg);
        }
        else if (SceneManager.GetActiveScene().name == "Level2_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel2>().ShowMsg(i_Msg);
        }
        else if (SceneManager.GetActiveScene().name == "Level3_Scene")
        {
            GameObject.Find("Communication_Iterface").GetComponent<CommunicationManagerLevel3>().ShowMsg(i_Msg);
        }
    }
}
