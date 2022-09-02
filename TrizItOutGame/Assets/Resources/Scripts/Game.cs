using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game m_Instance;
    public int ReachedLevel { get; set; } = 1;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (m_Instance == null)
        {
            m_Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnLevelLoading(int i_Level)
    {

    }

 

}
