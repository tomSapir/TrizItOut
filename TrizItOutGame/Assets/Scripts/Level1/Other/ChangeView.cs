using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour, IInteractable
{
    [SerializeField]
    public string SpriteName;
    [SerializeField]
    public Sprite m_Sprite;
    private const string ChangeViewSpritesPath = "Sprites/Level1/";
    [SerializeField]
    private GameObject m_CommunicationInterface;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        currDisplay.GetComponent<SpriteRenderer>().sprite = m_Sprite;
        currDisplay.CurrentState = DisplayManagerLevel1.State.zoom;
        ShowMsg(m_Sprite.name);
    }

    private void ShowMsg(string i_NameSprite)
    {
        switch (i_NameSprite)
        {
            case "PCSide_ZoomIN_Close":
                {
                    m_CommunicationInterface.GetComponent<CommunicationManagerLevel1>().ShowMsg("Mmmm... The computer is closed.");
                    break;
                }
        }
    }
}
