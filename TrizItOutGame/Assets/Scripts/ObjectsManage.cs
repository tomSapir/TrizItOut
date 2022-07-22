using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManage : MonoBehaviour
{
    private DisplayManagerLevel1 currentDisplay;

    public GameObject[] objectsToMange;
    // Start is called before the first frame update
    void Start()
    {
        currentDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();
    }

    // Update is called once per frame
    void Update()
    {
        ManageObjects();
    }

    void ManageObjects()
    {
        for (int i = 0; i < objectsToMange.Length; i++)
        {
            if (objectsToMange[i].name == currentDisplay.GetComponent<SpriteRenderer>().sprite.name)
            {
                objectsToMange[i].SetActive(true);
            }
            else
            {
                objectsToMange[i].SetActive(false);
            }
        }
    }
}
