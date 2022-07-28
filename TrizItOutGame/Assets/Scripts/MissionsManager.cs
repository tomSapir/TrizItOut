using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_ComputerMission;

    [SerializeField]
    private GameObject m_SafeBoxMission;


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
        switch (i_Mission)

        {
            case "PCSide_ZoomIN_Close":
            case "PCSide_ZoomIn_Open":
                {
                    m_ComputerMission.SetActive(true);
                    break;
                }

            case "SafeBox_Code_Zoom":
            case "SafeBox_Open_ZoomIn":
                {
                    m_SafeBoxMission.SetActive(true);
                    break;
                }



        }
    }
}
