using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Counter : MonoBehaviour {

    // Use this for initialization
    GameObject numberbarOri;
    List<GameObject> numberbars;

    Vector3 startPos;
    public float scale = 1f;
   
    public static Counter self;
    void Start() {
        init();
    }
    void init()
    {
        foreach (Transform tchild in transform)
        {
            if (tchild.name == "numbers")
                Destroy(tchild.gameObject);
        }
        StartCoroutine("initGame");
    }
    IEnumerator initGame() {
        yield return new WaitForEndOfFrame();
        self = this;
        GameObject tpos = transform.Find("startPos").gameObject;
        startPos = tpos.transform.position;
        float numberGap = transform.Find("numberarea").GetComponent<SpriteRenderer>().bounds.size.x/4f;

        numberbarOri = Resources.Load("masknumber") as GameObject;
        numberbars = new List<GameObject>();
        for(int i = 0; i < 4; i++)
        {
            GameObject tnumber = Instantiate(numberbarOri,transform) as GameObject;
            tnumber.transform.localScale *= scale;
            tnumber.name = "numbers";
            numberbars.Add(tnumber);

            tnumber.transform.position = new Vector3(i * numberGap,0,0)+startPos;
            tnumber.transform.Find("numberbar").GetComponent<SpriteRenderer>().sortingOrder = transform.Find("counter").GetComponent<SpriteRenderer>().sortingOrder + 1;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public string correctNumber = "0000";
    public string getNumber()
    {
        string tstr = "";
        for(int i = 0; i < self.transform.childCount; i++)
        {
            GameObject tchild = self.transform.GetChild(i).gameObject;
            CounterNumber tcounterNumber = tchild.GetComponent<CounterNumber>();
            if (tcounterNumber != null)
            {
                string tnum = tcounterNumber.cnumber.ToString();
                tstr += tnum;
            }
        }
        return(tstr);
    }

    



}
