using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IInteractable
{
    public enum eMovementType { left, right, up, down }

    public delegate void ObjectMovedAction(eMovementType eMovementType);
    public float m_Steps;
    public eMovementType MovmentType { get; set; }
    public event ObjectMovedAction OnObjectMoved;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(OnObjectMoved != null)
        {
            OnObjectMoved(MovmentType);
        }    

        switch(MovmentType)
        {
            case eMovementType.right:
                {
                    MovmentType = eMovementType.left;
                    break;
                }
            case eMovementType.left:
                {
                    MovmentType = eMovementType.right;
                    break;
                }
        }
    }
}
