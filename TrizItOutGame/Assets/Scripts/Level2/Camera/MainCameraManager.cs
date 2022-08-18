using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCameraManager : MonoBehaviour
{
    private int m_CurrentWallIndex = 1;
    private int m_PreviousWallIndex = 1;
    private int m_WallBeforeMission = 0;
    private float m_DistanceToMoveXOfCamera = 17.8f;

    [SerializeField]
    private GameObject m_LeftBtn, m_RightBtn, m_GoBackBtn;

    private static readonly int sr_MostRightWallIndex = 3;

    public int CurrentWallIndex
    {
        get { return m_CurrentWallIndex; }
        set
        {
            if (value == sr_MostRightWallIndex + 1)
            {
                m_CurrentWallIndex = 1;
            }
            else if (value == 0)
            {
                m_CurrentWallIndex = sr_MostRightWallIndex;
            }
            else
            {
                m_CurrentWallIndex = value;
            }
        }
    }

    void Update()
    {
        manageCameraPosition();
        manageLeftAndRightsBtnsActivation();
    }

    private void Start()
    {
        ChangeToMission fanMission = GameObject.Find("Static_Fan").GetComponent<ChangeToMission>();
        fanMission.MissionWasChosen += Mission_Interact;

        GameObject.Find("Peep_Hole1").GetComponent<ChangeToMission>().MissionWasChosen += Mission_Interact;
        GameObject.Find("Peep_Hole2").GetComponent<ChangeToMission>().MissionWasChosen += Mission_Interact;
        GameObject.Find("Peep_Hole3").GetComponent<ChangeToMission>().MissionWasChosen += Mission_Interact;
        GameObject.Find("Peep_Hole4").GetComponent<ChangeToMission>().MissionWasChosen += Mission_Interact;
    }

    private void manageCameraPosition()
    {
        Vector3 currentPosition = gameObject.transform.position;

        if (m_CurrentWallIndex != m_PreviousWallIndex)
        {
            gameObject.transform.position = new Vector3((m_CurrentWallIndex - 1) * m_DistanceToMoveXOfCamera, currentPosition.y, currentPosition.z);
            m_PreviousWallIndex = m_CurrentWallIndex;
        }
    }

    private void manageLeftAndRightsBtnsActivation()
    {
        bool leftBtnShouldAppear = m_CurrentWallIndex > 1;
        bool rightBtnShouldApper = m_CurrentWallIndex >= 1 && m_CurrentWallIndex < sr_MostRightWallIndex;
        bool backBtnShouldAppear = m_CurrentWallIndex < 1;

        m_LeftBtn.SetActive(leftBtnShouldAppear);
        m_RightBtn.SetActive(rightBtnShouldApper);
        m_GoBackBtn.SetActive(backBtnShouldAppear);
    }

    public void OnClickRightChangeBackgroundBtn()
    {
        m_CurrentWallIndex++;
    }

    public void OnClickLeftChangeBackgroundBtn()
    {
        m_CurrentWallIndex--;
    }

    public void OnClickBackBtn()
    {
        m_CurrentWallIndex = m_WallBeforeMission;
    }

    public void Mission_Interact(int i_MissionWall)
    {
        m_WallBeforeMission = m_CurrentWallIndex;
        while (m_CurrentWallIndex != i_MissionWall)
        {
            m_LeftBtn.GetComponent<Button>().onClick.Invoke();
        }
    }
}
