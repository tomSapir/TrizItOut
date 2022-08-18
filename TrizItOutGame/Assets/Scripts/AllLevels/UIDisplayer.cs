using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDisplayer : MonoBehaviour , IInteractable
{
    public GameObject DisplayObject;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        DisplayObject.SetActive(true);
    }
}
