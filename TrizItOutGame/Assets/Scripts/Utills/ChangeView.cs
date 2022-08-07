using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string SpriteName;
    [SerializeField]
    private Sprite m_Sprite;
    private const string ChangeViewSpritesPath = "Sprites/Level1/";
    [SerializeField]
    private GameObject m_CommunicationInterface;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        currDisplay.GetComponent<SpriteRenderer>().sprite = m_Sprite;
        currDisplay.CurrentState = DisplayManagerLevel1.State.zoom;
        ShowMsg();
    }

    private void ShowMsg()
    {
        switch(SpriteName)
        {
            case "PCSide_ZoomIn_Close":
                {
                    m_CommunicationInterface.GetComponent<CommunicationManager>().ShowMsg("Mmmm... The computer is closed.");
                    break;
                }
        }
    }
}
