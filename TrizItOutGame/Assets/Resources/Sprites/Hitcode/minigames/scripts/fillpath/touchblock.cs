using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace fillpath{
	public class touchblock : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public int x,y;
		public bool canPick = false;//whether can be a startpoint
		public bool isColored =false;//whether passed already
		public int num = 0;
		public bool canPass = false;
		void OnMouseDown(){
			if (!canPick) {//not a pick point
				return;
			} else {
				GameData.Instance.cPicked = gameObject;
				colorfy ();
			}
		

			canPick = false;
		}

		void OnMouseOver(){
			if (GameData.Instance.cPicked == null)
				return;
			if (num>0 && !canPass)//if got number
				return;
			if (!isColored) {
				touchblock tblock = GameData.Instance.lastBlock.GetComponent<touchblock> ();
				// check if is neigbour
//				print(tblock.x +"_"+tblock.y);
					if (Mathf.Abs (x - tblock.x)==1 && y == tblock.y) {
						colorfy ();
					}else if (Mathf.Abs (y - tblock.y)==1 && x == tblock.x) {
						colorfy ();
					}
			
			}
		}

		void OnMouseUp(){
			GameData.Instance.cPicked = null;
		}


		void colorfy(){
			GetComponent<SpriteRenderer> ().color = Color.green;
			GetComponent<SpriteRenderer> ().DOColor (Color.red, .5f);
			isColored = true;
			GameData.Instance.totalPassed++;//add count for check win;
			GameData.Instance.checkWin();
			GameData.Instance.lastBlock = gameObject;
			canPick = false;//also disable pick(even move to other not used start blocks)
			if (num > 0) {//just passing a number block,check new
				GameData.Instance.numberPassed++;
				GameData.Instance.retfreshNumBlock();

			}
		}
	}
}
