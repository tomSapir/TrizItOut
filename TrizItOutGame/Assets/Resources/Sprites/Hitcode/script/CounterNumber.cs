using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class CounterNumber : MonoBehaviour {
    public int cnumber = 0;
    GameObject numberbar;
    Vector3 startpos;
    float moveGap;
    // Use this for initialization
    void Start () {
        numberbar = transform.Find("numberbar").gameObject;
        startpos = numberbar.transform.position;
        moveGap = numberbar.GetComponent<SpriteRenderer>().bounds.size.y/11f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    bool locked = false;
    private void OnMouseDown()
    {
        if (locked) return;
            
        if (cnumber <= 9)
        {
            if (cnumber == 0)
            {
                numberbar.transform.position = startpos;
            }
            numberbar.transform.DOMoveY(numberbar.transform.position.y+moveGap, .2f).OnComplete(() =>
            {
                locked = false;
                
            });

            cnumber++;
            if (cnumber == 10)
            {
                cnumber = 0;
            }
        }

        locked = true;

    }
}
