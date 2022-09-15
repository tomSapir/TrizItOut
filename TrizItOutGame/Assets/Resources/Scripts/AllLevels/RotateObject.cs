using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour, IInteractable
{
    public delegate void RotateObjectAction();

    public float m_SpeedRotation;
    public float m_EndZ;

    public event RotateObjectAction OnRotateObject;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        RotateSprite();
    }

    private void RotateSprite()
    {
        if (OnRotateObject != null)
        {
            OnRotateObject();
        }
    }
}
