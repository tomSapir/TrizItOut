using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace hexfill{

	public class TouchHex : MonoBehaviour {
		public int myx,myy;

		// Use this for initialization
		public hexfill hexfill;
		void Start () {

		}

		// Update is called once per frame
		void Update () {

		}

		public int dir = 0;//direction,clockwise from top left: 0,1,2,3,4,5
		void OnMouseDown () {

		

			if (canTouch) {
				hexfill.resetTempBlocks();
				//			transform.parent.parent.GetComponent<hexfill> ().activeRound (myx,myy);

				GameObject nextBlock = null;

				//clear other not touched triggers
				hexfill.resetTempBlocks();
				//active self first

				hexfill.activeCBlock (gameObject);
				canTouch = false;


				nextBlock = hexfill.getNext (myx, myy, dir);
				if (nextBlock == null) {
					hexfill.activeRound (myx,myy);
				}
				while (nextBlock != null) {
					TouchHex ttouch = nextBlock.GetComponent<TouchHex> ();
					if (ttouch.state == 0 && !ttouch.isActive) {
						hexfill.activeCBlock (nextBlock);
						int newx = nextBlock.GetComponent<TouchHex> ().myx;
						int newy = nextBlock.GetComponent<TouchHex> ().myy;
						nextBlock = hexfill.getNext (newx, newy, dir);
						if (nextBlock == null) {
							hexfill.activeRound (newx,newy);
						}
					} else {
						int newx = nextBlock.GetComponent<TouchHex> ().myx;
						int newy = nextBlock.GetComponent<TouchHex> ().myy;
						hexfill.activeRound (newx,newy);
					}

				}


			}
		}

		public bool canTouch = false;
		public bool isActive = false;//whether actived
		public int state = 0;
	}
}
