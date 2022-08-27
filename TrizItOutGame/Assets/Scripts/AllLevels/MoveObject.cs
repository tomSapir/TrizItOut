using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IInteractable
{
    public enum eMovementType
    {
        left,
        right,
        up,
        down
    }

    public delegate void ObjectMovedAction(eMovementType eMovementType);
    public float m_Steps;
    public eMovementType m_MovmentType;
    public event ObjectMovedAction OnObjectMoved;

    public eMovementType MovementType
    {
        get
        {
            return m_MovmentType;
        }
        set
        {
            m_MovmentType = value;
        }
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        if(OnObjectMoved != null)
        {
            OnObjectMoved(m_MovmentType);
        }    

        switch(m_MovmentType)
        {
            case eMovementType.right:
                {
                    m_MovmentType = eMovementType.left;
                    break;
                }
            case eMovementType.left:
                {
                    m_MovmentType = eMovementType.right;
                    break;
                }
            case eMovementType.up:
                {
                    // TODO: implement
                    break;
                }
            case eMovementType.down:
                {
                    // TODO: implement
                    break;
                }
        }
    }
}
