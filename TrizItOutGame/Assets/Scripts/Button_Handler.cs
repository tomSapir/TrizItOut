using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button_Handler : MonoBehaviour
{
    private DisplayManagerLevel1 currentDisplay;

    [SerializeField]
    private Button rightButton;
    [SerializeField]
    private Button leftButton;


    // Start is called before the first frame update
    void Start()
    {
        currentDisplay = GameObject.Find("DisplayImage").GetComponent<DisplayManagerLevel1>();

        if(currentDisplay == null)
        {
            Debug.LogError("could not found DisplayManagerLevel1");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickRightArrow()
    {
        currentDisplay.CurrentWall++;
        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(false);
    }  

    public void OnClickLeftArrow()
    {
        currentDisplay.CurrentWall--;
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(true);
    }
}
