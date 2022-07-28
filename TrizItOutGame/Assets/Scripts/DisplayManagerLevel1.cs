using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayManagerLevel1 : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Missions;

    // index for the current background displayed (from 1 to 2 in this case)
    [SerializeField]
    private int m_CurrentWall;

    private int m_PreviousWall;

    [SerializeField]
    private GameObject m_furniture1;
    [SerializeField]
    private GameObject m_interactables1;

    [SerializeField]
    private GameObject m_furniture2;
    [SerializeField]
    private GameObject m_interactables2;

    public GameObject[] UiRenderObject;


    public enum State
    {
        normal, zoom
    };

    public State CurrentState { get; set; }

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

    void Start()
    {
        m_PreviousWall = 0;
        m_CurrentWall = 1;
        CurrentState = State.normal;
        RenderUI();
    }

    void Update()
    {
        if (m_CurrentWall != m_PreviousWall)
        {
            GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Level1/Background" + CurrentWall.ToString());
        }

        m_PreviousWall = CurrentWall;

        showRelevantPickUpItems();
        showRelevantInteractableItems();

        if(CurrentState == State.normal)
        {
            m_Missions.SetActive(false);
        }
        else
        {
            m_Missions.SetActive(true);
            string missionName = GetComponent<SpriteRenderer>().sprite.name;
            m_Missions.GetComponent<MissionsManager>().ActiveRelevantMission(missionName);
        }
    }

    private void showRelevantInteractableItems()
    {
        if (m_CurrentWall == 1)
        {
            m_interactables2.SetActive(false);

            if (CurrentState == State.normal)
            {
                m_interactables1.SetActive(true);
            }
            else
            {
                m_interactables1.SetActive(false);
            }
        }

        else if (m_CurrentWall == 2)
        {
            m_interactables1.SetActive(false);

            if (CurrentState == State.normal)
            {
                m_interactables2.SetActive(true);
            }
            else
            {
                m_interactables2.SetActive(false);
            }
        }

    }

    private void showRelevantPickUpItems()
    {

        if (m_CurrentWall == 1)
        {
            m_furniture2.SetActive(false);

            if (CurrentState == State.normal)
            {
                m_furniture1.SetActive(true);
            }
            else
            {
                m_furniture1.SetActive(false);
            }
        }

        else if (m_CurrentWall == 2)
        {
            m_furniture1.SetActive(false);

            if (CurrentState == State.normal)
            {
                m_furniture2.SetActive(true);
            }
            else
            {
                m_furniture2.SetActive(false);
            }
        }

    }

    void RenderUI()
    {
        for (int i = 0; i < UiRenderObject.Length; i++)
        {
            UiRenderObject[i].SetActive(false);
        }
    }
}
