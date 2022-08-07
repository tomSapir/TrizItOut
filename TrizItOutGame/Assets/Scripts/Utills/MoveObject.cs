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

    [SerializeField]
    private float m_Steps;
    [SerializeField]
    private eMovementType m_MovmentType;

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
        switch(m_MovmentType)
        {
            case eMovementType.right:
                {
                    transform.position = new Vector3(transform.position.x + m_Steps, transform.position.y, transform.position.z);
                    m_MovmentType = eMovementType.left;
                    break;
                }
            case eMovementType.left:
                {
                    transform.position = new Vector3(transform.position.x - m_Steps, transform.position.y, transform.position.z);
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
