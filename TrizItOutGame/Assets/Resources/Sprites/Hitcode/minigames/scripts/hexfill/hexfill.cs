using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
namespace hexfill{
	public class hexfill : MonoBehaviour {
		GameObject container;
		// Use this for initialization

		SimpleJSON.JSONNode myMap;
		public int clevel;
        Actions[] actions;
        void Start()
        {
            actions = transform.GetComponents<Actions>();
        }

		// Update is called once per frame
		void Update () {

		}

        void init()
        {
            container = transform.Find("container").gameObject;
            GameData.Instance.init();
            int tlevel = Random.Range(0, 15);
            myMap = GameData.Instance.getLevel(tlevel);


            Transform tcontainer = transform.Find("container");
            if (tcontainer.transform.childCount > 0)
            {
                foreach (Transform child in tcontainer.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }


            }

            StartCoroutine("initView");
        }


		int nBlocks = 0;
		IEnumerator initView(){
            yield return new WaitForEndOfFrame();

            

            nBlocks = 0;

			GameObject hexblock = Resources.Load ("hexfill/hex") as GameObject;
			int i = 0;
			int lastW = 0;
			float tileW = hexblock.GetComponent<SpriteRenderer>().bounds.size.x;

			float startX = transform.position.x;

			int maxW = 0;//get the max bound of the map
			for(int kk = 0;kk<myMap.Count;kk++){
				if(maxW < myMap[kk].Count){
					maxW =  myMap[kk].Count;
				}
			}



			while (i < myMap.Count) {
				int mapW = myMap [i].Count;
				if (mapW > lastW) {
					startX = startX - 1 * (tileW / 2);
				} else {
					startX = startX + 1 * (tileW / 2);
				}

				int j = 0;
				while (j < mapW) {
					GameObject thexBlock = Instantiate (hexblock, container.transform);
					//the ordinate system requires the full rect arrangment
					int realHexX = (j + Mathf.FloorToInt ((maxW - mapW) / 2));
					int realHexY = i;
					thexBlock.name = "hex" +realHexX  + "_" +  i;
					thexBlock.AddComponent<PolygonCollider2D> ();//dectect on box would be unaccurate as others block may cover its area
					thexBlock.AddComponent<TouchHex> ();
					int tstate = myMap [i] [j];
					thexBlock.GetComponent<TouchHex> ().state = tstate;
					thexBlock.GetComponent<TouchHex> ().myx = realHexX;
					thexBlock.GetComponent<TouchHex> ().myy = realHexY;
					thexBlock.GetComponent<TouchHex> ().hexfill = this; 
					thexBlock.transform.position = new Vector3 (startX + j * tileW ,- i * (tileW / 4 * 3) ,0);
					if (tstate == 1) {
						thexBlock.GetComponent<SpriteRenderer> ().color = Color.black;
						nBlocks--;//exclude dead blocks
					}
					j++;
					nBlocks++;//count total blocks;
				}


				lastW = mapW;
				i++;
			}
				

			container.transform.Translate (-myMap[0].Count*tileW/3.4f,myMap[0].Count*tileW/2,0);

			//init first item
			int middlex = myMap[myMap.Count/2].Count;
			GameObject tblock = GameObject.Find("hex"+middlex/2+"_"+middlex/2);




			activeCBlock (tblock);//count the active blocks


			activeRound (middlex/2,middlex/2);


		}

	  	/// <summary>
		/// Make 6 blocks round it can touch(if exist)
	  	/// </summary>
	  	/// <param name="tx">Tx.</param>
	  	/// <param name="ty">Ty.</param>
		public void activeRound(int _tx,int _ty){

			GameData.Instance.tempTouchable = new List<GameObject> ();


			int tx = 0;int ty = 0;

			if (_ty % 2 == 0) {
				tx = _tx - 1;
				ty = _ty - 1;
			} else {
				tx = _tx;
				ty = _ty - 1;
			}



			GameObject tblock = GameObject.Find("hex"+tx+"_"+ty);
			TouchHex ttouchhex = null;
			if (tblock != null) {
				ttouchhex =	tblock.GetComponent<TouchHex> ();
				if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {//these conditions make sure whether the blocks can be touch
					tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
					tblock.GetComponent<TouchHex> ().canTouch = true;
					tblock.GetComponent<TouchHex> ().dir = 0;
					GameData.Instance.tempTouchable.Add (tblock);
				}
			}

			if (_ty % 2 == 0) {
				tx = _tx;
				ty = _ty - 1;
			} else {
				tx = _tx+1;
				ty = _ty - 1;
			}
			tblock = GameObject.Find("hex"+tx+"_"+ty);
			if (tblock != null) {
				ttouchhex = tblock.GetComponent<TouchHex> ();
				if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {
					tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
					tblock.GetComponent<TouchHex> ().canTouch = true;
					tblock.GetComponent<TouchHex> ().dir = 1;
					GameData.Instance.tempTouchable.Add (tblock);
				}
			}
			if (_ty % 2 == 0) {
				tx = _tx + 1;
				ty = _ty;
			} else {
				tx = _tx+1;
				ty = _ty;
			}
			tblock = GameObject.Find("hex"+tx+"_"+ty);
			if (tblock != null) {
				ttouchhex = tblock.GetComponent<TouchHex> ();
				if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {
					tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
					tblock.GetComponent<TouchHex> ().canTouch = true;
					tblock.GetComponent<TouchHex> ().dir = 2;
					GameData.Instance.tempTouchable.Add (tblock);
				}
			}


				if (_ty % 2 == 0) {
					tx = _tx;
					ty = _ty + 1;
				} else {
					tx = _tx+1;
					ty = _ty + 1;
				}
				tblock = GameObject.Find("hex"+tx+"_"+ty);
				if (tblock != null) {
					ttouchhex = tblock.GetComponent<TouchHex> ();
					if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {
						tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
						tblock.GetComponent<TouchHex> ().canTouch = true;
						tblock.GetComponent<TouchHex> ().dir = 3;
						GameData.Instance.tempTouchable.Add (tblock);
					}
				}



					
				if (_ty % 2 == 0) {
					tx = _tx - 1;
					ty = _ty + 1;
				} else {
					tx = _tx;
					ty = _ty + 1;
				}
				tblock = GameObject.Find("hex"+tx+"_"+ty);
				if (tblock != null) {
					ttouchhex = tblock.GetComponent<TouchHex> ();
					if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {
						tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
						tblock.GetComponent<TouchHex> ().canTouch = true;
						tblock.GetComponent<TouchHex> ().dir = 4;
						GameData.Instance.tempTouchable.Add (tblock);
					}
				}


				if (_ty % 2 == 0) {
					tx = _tx - 1;
					ty = _ty ;
				} else {
					tx = _tx - 1;
					ty = _ty;
				}
				tblock = GameObject.Find("hex"+tx+"_"+ty);
				if (tblock != null) {
					ttouchhex = tblock.GetComponent<TouchHex> ();
					if (tblock != null && ttouchhex.state == 0 && ttouchhex.isActive == false) {
						tblock.GetComponent<SpriteRenderer> ().color = Color.cyan;
						tblock.GetComponent<TouchHex> ().canTouch = true;
						tblock.GetComponent<TouchHex> ().dir = 5;
						GameData.Instance.tempTouchable.Add (tblock);
					}
				}



		}


		public void resetTempBlocks(){
			foreach (GameObject tblock in GameData.Instance.tempTouchable) {
				TouchHex ttouchhex = tblock.GetComponent<TouchHex> ();
				ttouchhex.canTouch = false;
				ttouchhex.GetComponent<SpriteRenderer> ().color = Color.white;
			}

			GameData.Instance.tempTouchable = new List<GameObject> ();
		}

		public GameObject getNext(int _tx,int _ty,int dir){
			GameObject nextBlock = null;
			int tx = 0;int ty = 0;


			switch (dir) {
			case 0:
				if (_ty % 2 == 0) {
					tx = _tx - 1;
					ty = _ty - 1;
				} else {
					tx = _tx;
					ty = _ty - 1;
				}
				break;
			case 1:
				if (_ty % 2 == 0) {
					tx = _tx;
					ty = _ty - 1;
				} else {
					tx = _tx+1;
					ty = _ty - 1;
				}
				break;
			case 2:
				if (_ty % 2 == 0) {
					tx = _tx + 1;
					ty = _ty;
				} else {
					tx = _tx+1;
					ty = _ty;
				}
				break;
			case 3:
				if (_ty % 2 == 0) {
					tx = _tx;
					ty = _ty + 1;
				} else {
					tx = _tx+1;
					ty = _ty + 1;
				}
				break;
			case 4:
				if (_ty % 2 == 0) {
					tx = _tx - 1;
					ty = _ty + 1;
				} else {
					tx = _tx;
					ty = _ty + 1;
				}
				break;
			case 5:
				if (_ty % 2 == 0) {
					tx = _tx - 1;
					ty = _ty ;
				} else {
					tx = _tx - 1;
					ty = _ty;
				}
				break;
			}


			GameObject tblock = GameObject.Find ("hex" + tx + "_" + ty);
			TouchHex ttouchhex = null;
			if (tblock != null) {
				ttouchhex =	tblock.GetComponent<TouchHex> ();
				if (ttouchhex.state == 0 && ttouchhex.isActive == false) {
					nextBlock = tblock;

				}
			}
			return nextBlock;



		}

		public void activeCBlock(GameObject tblock){
			tblock.GetComponent<TouchHex> ().isActive = true;
			tblock.GetComponent<SpriteRenderer> ().color = Color.red;
			GameData.Instance.nActiveBlock++;
			if (GameData.Instance.nActiveBlock == nBlocks) {//all block activated;
                print("win");

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
