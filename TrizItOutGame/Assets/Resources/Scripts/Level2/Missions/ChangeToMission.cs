using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMission : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int m_MissionWallNumber;

    public delegate void MissionPickedDelegate(int i_MissionWallNumber);
    public event MissionPickedDelegate MissionWasChosen;

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        MissionWasChosen?.Invoke(m_MissionWallNumber);

        Debug.Log("Changing to mission in display number: " + m_MissionWallNumber);
    }
}
