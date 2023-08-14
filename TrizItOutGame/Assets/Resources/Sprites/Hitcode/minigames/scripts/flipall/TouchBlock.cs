using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Hitcode_RoomEscape;
namespace flipall{
	public class TouchBlock : MonoBehaviour {

		// Use this for initialization
		public int gx,gy;
//		public int originGx,originGy;
		public int id;
		SpriteRenderer sp;
        Actions[] actions;
        void Start()
        {


            actions = transform.parent.parent.GetComponents<Actions>();

        
            sp = GetComponent<SpriteRenderer> ();

		}

		public void init(int x,int y){

		}

		// Update is called once per frame
		void Update () {

		}


		void OnMouseDown () {
			if (GameData.Instance.isWin)
				return;
			if (!isIn)//wont be able to touch on invisible blocks
				return;
			fadeOut ();
			GameData.Instance.nActiveBlock--;
			GameObject rblock = GameObject.Find ("block" + (gx + 1) + "_" + gy);
			if (rblock != null) {
				int tid = rblock.GetComponent<TouchBlock> ().id;

				if (GameData.Instance.wallData [tid] == 0) {//no wall here
					if (!rblock.GetComponent<TouchBlock> ().isIn) {
						GameData.Instance.nActiveBlock++;
						rblock.GetComponent<TouchBlock>().fadeIn ();
					} else {
						GameData.Instance.nActiveBlock--;
						rblock.GetComponent<TouchBlock>().fadeOut ();
					}
				} else {//hit wall,clear
					rblock.GetComponent<TouchBlock> ().alert ();
				}
			}

			GameObject ublock = GameObject.Find ("block" + (gx) + "_" + (gy+1));
			if (ublock != null) {
				int tid = ublock.GetComponent<TouchBlock> ().id;

				if (GameData.Instance.wallData [tid] == 0) {//no wall here

					if (!ublock.GetComponent<TouchBlock> ().isIn) {
						GameData.Instance.nActiveBlock++;
						ublock.GetComponent<TouchBlock>().fadeIn ();
					} else {
						GameData.Instance.nActiveBlock--;
						ublock.GetComponent<TouchBlock>().fadeOut ();
					}
				} else {//hit wall,clear
					ublock.GetComponent<TouchBlock> ().alert ();
				}
			}

			GameObject lblock = GameObject.Find ("block" + (gx - 1) + "_" + gy);
			if (lblock != null) {
				int tid = lblock.GetComponent<TouchBlock> ().id;

				if (GameData.Instance.wallData [tid] == 0) {//no wall here

					if (!lblock.GetComponent<TouchBlock> ().isIn) {
						GameData.Instance.nActiveBlock++;
						lblock.GetComponent<TouchBlock>().fadeIn ();
					} else {
						GameData.Instance.nActiveBlock--;
						lblock.GetComponent<TouchBlock>().fadeOut ();
					}
				} else {//hit wall,clear
					lblock.GetComponent<TouchBlock> ().alert ();
				}
			}


			GameObject dblock = GameObject.Find ("block" + (gx) + "_" + (gy-1));
			if (dblock != null) {
				int tid = dblock.GetComponent<TouchBlock> ().id;

				if (GameData.Instance.wallData [tid] == 0) {//no wall here

					if (!dblock.GetComponent<TouchBlock> ().isIn) {
						GameData.Instance.nActiveBlock++;
						dblock.GetComponent<TouchBlock>().fadeIn ();
					} else {
						GameData.Instance.nActiveBlock--;
						dblock.GetComponent<TouchBlock>().fadeOut ();
					}
				} else {//hit wall,clear
					dblock.GetComponent<TouchBlock> ().alert ();
				}
			}

			checkWin ();
		}


		public bool isIn = false;
		//if is block the isIn is on for default
		public void isBlock(){
			isIn = true;
		}


		void fadeIn(){
			isIn = true;
			sp.DOColor (new Color(1,1,1,1), .2f);
	
		}


		void fadeOut(){
			isIn = false;
			sp.DOColor (new Color(1,1,1,0), .2f);

		}

		public void alert(){
			Sequence tseq = DOTween.Sequence ();
			tseq.Append(sp.DOColor (new Color(1,0,0,1), .2f));
			tseq.Append(sp.DOColor (new Color(1,1,1,0), .2f));
			tseq.Play ();
		}

		void checkWin(){
           
			if (GameData.Instance.nActiveBlock == 0) {
				GameData.Instance.isWin = true;
				print ("win!!!");
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
