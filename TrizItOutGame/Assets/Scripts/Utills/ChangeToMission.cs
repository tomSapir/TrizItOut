using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeToMission : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int m_MissionWallNumber;

    public delegate void MissionPickedDelegate(int i_MissionWallNumber);
    public event MissionPickedDelegate MissionWasChosen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        MissionWasChosen?.Invoke(m_MissionWallNumber);
    }
}
