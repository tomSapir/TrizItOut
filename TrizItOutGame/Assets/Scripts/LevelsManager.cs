using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public void OnClickLevel1Btn()
    {
        SceneManager.LoadScene("Level1_Scene");
    }
}
