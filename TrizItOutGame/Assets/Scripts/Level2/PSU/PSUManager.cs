using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSUManager : MonoBehaviour
{
    public delegate void PsuMissionSolvedDelegate();

    private HashSet<int> m_CurrentPapersClip = new HashSet<int>();
    private readonly HashSet<int> m_SolutionPapersClip = new HashSet<int>() { 0, 3, 5, 8, 10, 19, 27, 28, 30};

    public event PsuMissionSolvedDelegate PsuMissionSolved;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<PaperClipPlaceholderManager>().PaperClipPressed += PaperClip_Pressed;
            transform.GetChild(i).gameObject.GetComponent<PaperClipPlaceholderManager>().m_Id = i;
        }
    }

    public void PaperClip_Pressed(int i_Id)
    {
        addOrRemovePaperClip(i_Id);
        if (m_CurrentPapersClip.Count == m_SolutionPapersClip.Count)
        {
            if(m_CurrentPapersClip.SetEquals(m_SolutionPapersClip))
            {
                turnOffChilds();
                if (PsuMissionSolved != null)
                {
                    PsuMissionSolved();
                }
            }
        }
    }

    private void turnOffChilds()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.layer = 2;
        }
    }

    private void addOrRemovePaperClip(int i_Id)
    {
        if (m_CurrentPapersClip.Contains(i_Id))
        {
            m_CurrentPapersClip.Remove(i_Id);
        }
        else
        {
            m_CurrentPapersClip.Add(i_Id);
        }
    }
}
