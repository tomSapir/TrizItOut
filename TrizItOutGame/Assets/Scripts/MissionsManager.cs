using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ComputerMission;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ActiveRelevantMission(string i_Mission)
    {
        switch(i_Mission)
        {
            case "PCSide_ZoomIN_Close":
            case "PCSide_ZoomIn_Open":
                {
                    m_ComputerMission.SetActive(true);
                    break;
                }

        }
    }
}
