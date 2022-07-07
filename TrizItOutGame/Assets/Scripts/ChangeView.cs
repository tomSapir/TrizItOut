using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour, IInteractable
{
    [SerializeField]
    private string SpriteName;

    private const string ChangeViewSpritesPath = "Sprites/Level1/";

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        currDisplay.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(ChangeViewSpritesPath + SpriteName);
    }
}
