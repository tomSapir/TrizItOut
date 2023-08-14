using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace pipe{
	public class TouchPipe : MonoBehaviour {

		public int X,Y;
		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {

		}

		void OnMouseDown(){
			if (GameData.Instance.isLock || GameData.Instance.isWin)
				return;
			int newRotate = (int)transform.localEulerAngles.z+90;
			transform.DORotate(new Vector3(0,0,newRotate),.2f).OnComplete(resetPath);
			GameData.Instance.isLock = true;
		}

		void resetPath(){
			GameData.Instance.setGrid (X, Y, transform.gameObject);
			GameData.Instance.refreshPath ();
			GameData.Instance.isLock = false;
		}
	}
}