using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
using UnityEngine.UI;
public class MinigameManager : MonoBehaviour {

    public GameObject[] minigames;
    public GameObject gameButton;
    Actions myActions;
    // Use this for initialization
    string[] tipText =
    {
        "spin the lock to give the correct number",
        "touch on white an try to eliminate them all",
        "try to move the red block out",
        "try to link all the dots with same color",
        "try to fill the whole hex map.",
        "try to puzzle a complete picture",
        "try to connect the bottom left pipe to top right pipe",
        "Drag the pieces below to make a complete picture."

    };
    Text Tip;
    void Start () {

        myActions =  gameButton.GetComponent<Actions>();
        myActions.actionSteps[0].sendMsgs.sendMsgList[0].msgTarget = minigames[0];
        Tip = transform.Find("gameTip").GetComponent<Text>();
        
        Tip.text = tipText[cGame]+"\n Touch the green button on bottom to refresh/start each game";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    int cGame = 0;
    
    public void OnLeft()
    {
        for(int i = 0; i < minigames.Length; i++)
        {
            minigames[i].SetActive(false);
        }
        if(cGame > 0)
        {
            cGame--;
            myActions.actionSteps[0].sendMsgs.sendMsgList[0].msgTarget = minigames[cGame];
            
        }
        else
        {
            cGame = minigames.Length-1 ;
           
            myActions.actionSteps[0].sendMsgs.sendMsgList[0].msgTarget = minigames[cGame];
            
        }
        minigames[cGame].SetActive(true);
        Tip.text = tipText[cGame] + "\n Touch the green button on bottom to refresh/start each game";

    }

    public void OnRight()
    {
        for (int i = 0; i < minigames.Length; i++)
        {
            minigames[i].SetActive(false);
        }
        if (cGame < minigames.Length-1)
        {
            cGame++;
            myActions.actionSteps[0].sendMsgs.sendMsgList[0].msgTarget = minigames[cGame];
        }
        else
        {
            cGame = 0;
            myActions.actionSteps[0].sendMsgs.sendMsgList[0].msgTarget = minigames[cGame];

        }
        minigames[cGame].SetActive(true);
        Tip.text = tipText[cGame] + "\n Touch the green button on bottom to refresh/start each game";

    }


}
