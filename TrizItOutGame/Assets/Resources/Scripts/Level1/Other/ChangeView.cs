using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour, IInteractable
{
    public string m_SpriteName;
    public Sprite m_Sprite;
    private const string k_ChangeViewSpritesPath = "Sprites/Level1/";

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
                    CommunicationUtils.FindCommunicationManagerAndShowMsg("Mmmm... The computer is closed.");
                    break;
                }
            case "MiddleDrawer_Open":
                {
                    CommunicationUtils.FindCommunicationManagerAndShowMsg("Looks like a regular drawer, nothing you need here...");
                    break;
                }
        }
    }
}
