using UnityEngine;

public class ChangeToMission : MonoBehaviour, IInteractable
{
    public int m_MissionWallNumber;

    public delegate void MissionPickedDelegate(int i_MissionWallNumber);
    public event MissionPickedDelegate MissionWasChosen;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        MissionWasChosen?.Invoke(m_MissionWallNumber);
    }
}
