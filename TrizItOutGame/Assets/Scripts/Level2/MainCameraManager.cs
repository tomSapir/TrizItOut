using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraManager : MonoBehaviour
{

    private int m_CurrentWallIndex = 1;
    private int m_PreviousWallIndex = 1;
    private float m_DistanceToMoveXOfCamera = 17.8f;

    [SerializeField]
    private GameObject m_LeftBtn, m_RightBtn;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPosition = gameObject.transform.position;

        if (m_CurrentWallIndex != m_PreviousWallIndex)
        {
            if(m_PreviousWallIndex == m_CurrentWallIndex - 1)
            {
                gameObject.transform.position = new Vector3(currentPosition.x + m_DistanceToMoveXOfCamera, currentPosition.y, currentPosition.z);
            }
            else 
            {
                gameObject.transform.position = new Vector3(currentPosition.x - m_DistanceToMoveXOfCamera, currentPosition.y, currentPosition.z);
            }

            m_PreviousWallIndex = m_CurrentWallIndex;
        }

        ManageLeftAndRightsBtnsActivation();
    }

    private void ManageLeftAndRightsBtnsActivation()
    {
        if(m_CurrentWallIndex > 1 && m_CurrentWallIndex < sr_MostRightWallIndex)
        {
            m_LeftBtn.SetActive(true);
            m_RightBtn.SetActive(true);
        }
        else if(m_CurrentWallIndex == sr_MostRightWallIndex)
        {
            m_LeftBtn.SetActive(true);
            m_RightBtn.SetActive(false);
        }
        else
        {
            m_LeftBtn.SetActive(false);
            m_RightBtn.SetActive(true);
        }
    }

    public void OnClickRightChangeBackgroundBtn()
    {
        m_CurrentWallIndex++;
    }

    public void OnClickLeftChangeBackgroundBtn()
    {
        m_CurrentWallIndex--;
    }
}
