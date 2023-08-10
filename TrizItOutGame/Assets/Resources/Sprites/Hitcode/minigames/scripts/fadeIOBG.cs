using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class fadeIOBG : MonoBehaviour {
	public SpriteRenderer mask1, mask2;
	// Use this for initialization
	void Start () {
		fadeMask1 ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void fadeMask1(){
		float trnd = 1;//Random.Range (.5f, 1f);
		mask2.DOFade (0, trnd).OnComplete (() => {
			mask2.DOFade (1, trnd).OnComplete(fadeMask1);
		});

	}


}
