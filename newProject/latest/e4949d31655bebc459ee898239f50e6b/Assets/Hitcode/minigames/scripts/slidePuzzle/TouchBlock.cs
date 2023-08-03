using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Hitcode_RoomEscape;
namespace slidePuzzle{
	public class TouchBlock : MonoBehaviour {

		// Use this for initialization
		public int gx,gy;
		public int originGx,originGy;
        Actions[] actions;
        void Start()
        {


            actions = transform.parent.parent.GetComponents<Actions>();

        }

		public void init(int x,int y){
			gx = x;
			gy = y;
			originGx = gx;
			originGy = gy;
		}

		// Update is called once per frame
		void Update () {

		}


		void OnMouseDown () {
			if (GameData.Instance.locked)
				return;



			if (gy +1 < GameData.Instance.row && GameData.Instance.gridState [gx, gy + 1] == 0) {
				GameData.Instance.locked = true;
				transform.DOLocalMoveY (GameData.Instance.gridPos [gx, gy + 1].y, .3f).OnComplete (() => {
					//set new gx	
					GameData.Instance.setData (gx, gy, 0);
					gy += 1;
					GameData.Instance.setData (gx, gy, 1);
					GameData.Instance.locked = false;
					checkWin();
				});
			}
			if (gy > 0 && GameData.Instance.gridState [gx, gy - 1] == 0) {
				GameData.Instance.locked = true;
				transform.DOLocalMoveY (GameData.Instance.gridPos[gx,gy-1].y, .3f).OnComplete(()=>{
					//set new gx	
					GameData.Instance.setData(gx,gy,0);
					gy -=1;
					GameData.Instance.setData(gx,gy,1);
					GameData.Instance.locked = false;
					checkWin();

				});
			}
			if (gx + 1 < GameData.Instance.col && GameData.Instance.gridState [gx+1, gy ] == 0) {
				GameData.Instance.locked = true;
				transform.DOLocalMoveX (GameData.Instance.gridPos [gx+1, gy].x, .3f).OnComplete (() => {
					//set new gx	
					GameData.Instance.setData (gx, gy, 0);
					gx += 1;
					GameData.Instance.setData (gx, gy, 1);
					GameData.Instance.locked = false;
					checkWin();
				});
			}
			if (gx > 0 && GameData.Instance.gridState [gx-1, gy ] == 0) {
				GameData.Instance.locked = true;
				transform.DOLocalMoveX (GameData.Instance.gridPos [gx-1, gy].x, .3f).OnComplete (() => {
					//set new gx	
					GameData.Instance.setData (gx, gy, 0);
					gx -= 1;
					GameData.Instance.setData (gx, gy, 1);
					GameData.Instance.locked = false;
					checkWin();
				});
			}



		}

		void checkWin(){
			bool iswin = true;
			GameObject hiddenBlock;
			foreach (Transform tblock in transform.parent) {
				

				if (tblock.GetComponent<TouchBlock> ().originGx != tblock.GetComponent<TouchBlock> ().gx || tblock.GetComponent<TouchBlock> ().originGy != tblock.GetComponent<TouchBlock> ().gy) {
						iswin = false;
						break;
				}
			}
			if (iswin) {
				print ("iswin");
				GameObject.Find ("0_0").GetComponent<SpriteRenderer> ().enabled = true;
				GameData.Instance.locked = true;
                //win
                foreach (Actions taction in actions)
                {
                    for (int i = 0; i < taction.actionSteps.Count; i++)
                    {
                        taction.playActionNow(i);
                    }

                }
            }
		}
	}
}
