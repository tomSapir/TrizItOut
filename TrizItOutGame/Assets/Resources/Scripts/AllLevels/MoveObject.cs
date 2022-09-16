using UnityEngine;

public class MoveObject : MonoBehaviour, IInteractable
{
    public enum eMovementType { Left, Right, Up, Down }

    public delegate void ObjectMovedAction(eMovementType eMovementType);

    public float m_Steps;
    public eMovementType m_MovmentType;

    public event ObjectMovedAction OnObjectMoved;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        OnObjectMoved?.Invoke(m_MovmentType);
        switch(m_MovmentType)
        {
            case eMovementType.Right:
                {
                    m_MovmentType = eMovementType.Left;
                    break;
                }
            case eMovementType.Left:
                {
                    m_MovmentType = eMovementType.Right;
                    break;
                }
        }
    }
}
