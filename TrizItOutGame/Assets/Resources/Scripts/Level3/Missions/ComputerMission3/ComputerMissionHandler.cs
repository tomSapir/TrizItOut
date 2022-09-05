using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerMissionHandler : MonoBehaviour
{
    public bool ComputerIsLocked { get; private set; } = true;
    private GameObject m_Communication;

    // Start is called before the first frame update
    void Start()
    {
        m_Communication = GameObject.Find("Canvas/Hints_And_Communication/Communication_Iterface");
        GameObject.Find("Screen_ZoomOut").GetComponent<ChangeToMission>().MissionWasChosen += onComputerMissionPicked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onComputerMissionPicked(int i_Chosen)
    {
        //if computer is locked -> msg the user.
    }
}
