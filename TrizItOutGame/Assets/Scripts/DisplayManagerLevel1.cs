using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManagerLevel1 : MonoBehaviour
{
    private int m_CurrentWall;
    private int m_PreviousWall;

    public int CurrentWall
    {
        get { return m_CurrentWall; }
        set
        {
            if (value == 3)
            {
                m_CurrentWall = 1; 
            }
            else if (value == 0)
            {
                m_CurrentWall = 2;
            }
            else
            {
                m_CurrentWall = value;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_PreviousWall = 0;
        m_CurrentWall = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_CurrentWall != m_PreviousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level1/Background" + CurrentWall.ToString());


          //  Debug.Log("Got to if");
        }

       // Debug.Log("Got to );
        m_PreviousWall = CurrentWall;
    }
}
