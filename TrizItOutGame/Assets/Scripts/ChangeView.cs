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

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        currDisplay.GetComponent<SpriteRenderer>().sprite = m_Sprite;
        currDisplay.CurrentState = DisplayManagerLevel1.State.zoom;
    }
}
