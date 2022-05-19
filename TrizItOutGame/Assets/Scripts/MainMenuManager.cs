using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnClickStartBtn()
    {
        Debug.Log("onClick");
        SceneManager.LoadScene(1);
    }
}
