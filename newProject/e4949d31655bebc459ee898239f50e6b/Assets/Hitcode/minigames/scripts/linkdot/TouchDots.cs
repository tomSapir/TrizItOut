using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
namespace linkDot{
	public class TouchDots : MonoBehaviour {
		public int tx,ty;
		// Use this for initialization
		int id;
        Actions[] actions;
        void Start()
        {
            actions = transform.parent.parent.GetComponents<Actions>();
            id = ty * GameData.bsize + tx;
		}

		// Update is called once per frame
		void Update () {

		}


		void OnMouseDown () {
			if (GameData.Instance.isWin)
				return;
			if (GameData.Instance.isHolding)
				return;
			Transform dotChild = transform.Find ("dot");

			int tdotColor = GameData.Instance.DotColorData [id];
			if (tdotColor != 0) {//got a dot here
				clearOldPath(tdotColor);

				GameData.Instance.pickColor = tdotColor;
//				print ("firstpick:dot:" + tdotColor);

				Color tcolor = GameData.Instance.colors [tdotColor];
				tcolor.a = .5f;
				transform.GetComponent<SpriteRenderer> ().color = tcolor;
				GameData.Instance.ColorData [id] = tdotColor;

				GameData.Instance.paths[tdotColor] = new List<int> ();//overwrite the old path
//				GameData.Instance.paths[tdotColor].Add (id);
				addPath(tdotColor,id);
			} else {
				int cBlockColor = GameData.Instance.ColorData [id];
//				print("cblockcolor"+cBlockColor);
				if (cBlockColor == 0) {//tap on a empty block,nothing should happen
					return;
				} else {
					//continue drawing on an already path block
					GameData.Instance.pickColor = cBlockColor;
					//reopen the linkage
					GameData.Instance.linkedLines[cBlockColor] = 0;
					for(int i = GameData.Instance.paths[cBlockColor].Count - 1;i> 0;i--){
						int oldid = GameData.Instance.paths[cBlockColor] [i];
						if (oldid != id) {//remove all paths until find the start one
							//and clear the useless path
							int oldx = Mathf.FloorToInt (oldid % GameData.bsize) ;
							int oldy = Mathf.FloorToInt (oldid / GameData.bsize);

							GameObject oldBg = GameObject.Find ("bg" + oldx + "_" + oldy);

							Color tcolor = GameData.Instance.colors [0];
							tcolor.a = 1f;
							oldBg.transform.GetComponent<SpriteRenderer> ().color = tcolor;
							GameData.Instance.ColorData [oldid] = 0;


//							GameData.Instance.paths[cBlockColor].RemoveAt (i);
							removePath(cBlockColor,i);
//							GameData.Instance.paths[cBlockColor].Add (id);
							addPath(cBlockColor,id);


						} else {
							break;
						}

					}

				}
			}




			GameData.Instance.isHolding = true;
			GameData.Instance.startId = id;
			GameData.Instance.lasttx = tx;
			GameData.Instance.lastty = ty;



		}


		void clearOldPath(int tcolorid){

			int tlen = GameData.Instance.paths[tcolorid].Count;



			while(GameData.Instance.paths[tcolorid].Count > 0){
				int oldid = GameData.Instance.paths[tcolorid] [GameData.Instance.paths[tcolorid].Count - 1];


					//and clear the old path
				int oldx = Mathf.FloorToInt (oldid % GameData.bsize) ;
				int oldy = Mathf.FloorToInt (oldid / GameData.bsize);

					GameObject oldBg = GameObject.Find ("bg" + oldx + "_" + oldy);

					Color tcolor = GameData.Instance.colors [0];
					oldBg.transform.GetComponent<SpriteRenderer> ().color = tcolor;
					GameData.Instance.ColorData [oldid] = 0;


//				GameData.Instance.paths[tcolorid].RemoveAt(GameData.Instance.paths[tcolorid].Count-1);
				removePath(tcolorid,GameData.Instance.paths[tcolorid].Count-1);
				//reopen the linkage
				GameData.Instance.linkedLines[tcolorid] = 0;


			}
		}



		void OnMouseOver(){
			if (GameData.Instance.isWin)
				return;
			if (GameData.Instance.isHolding) {
				if (GameData.Instance.pickColor != 0) {
//					if (GameData.Instance.linkedLines [GameData.Instance.pickColor] == 1)//this linkage is closed,unless the linkage being breaked or restart,wont be able to continue drawing
//						return;
					//if dot here,get dot color
					Transform dotChild = transform.Find ("dot");
					int tdotColor = GameData.Instance.DotColorData [id];
					//current block color;
					int tColorid = GameData.Instance.ColorData [id];
					//GameData.Instance.paths[tdotColor][0] != id meanes not draw on the start point agasin
					if ((tdotColor == 0 && tColorid == 0) || GameData.Instance.pickColor == tdotColor && GameData.Instance.paths[tdotColor][0] != id) {//all places which can be draw and have not draw on something
						
						//exclude not nearby blocks
						if ((Mathf.Abs (tx - GameData.Instance.lasttx) == 1 && ty == GameData.Instance.lastty) ||
							(Mathf.Abs (ty - GameData.Instance.lastty) == 1 && tx == GameData.Instance.lasttx)) {

							addColor ();
						}
					} else {//draw on an already exist self color path
						if (tColorid != 0) {
							int tlen = GameData.Instance.paths[tColorid].Count;


							//exclude not nearby blocks
							if ((Mathf.Abs (tx - GameData.Instance.lasttx) == 1 && ty == GameData.Instance.lastty) ||
							    (Mathf.Abs (ty - GameData.Instance.lastty) == 1 && tx == GameData.Instance.lasttx)) {
								if (tColorid == GameData.Instance.pickColor) {//draw on self old blocks

									//and clear the old path


									int oldId = GameData.Instance.paths [tColorid] [GameData.Instance.paths [tColorid].Count-1];
									while (oldId!= id) {
										int oldx = Mathf.FloorToInt (oldId % GameData.bsize) ;
										int oldy = Mathf.FloorToInt (oldId / GameData.bsize);
										GameObject oldBg = GameObject.Find ("bg" + oldx + "_" + oldy);

										Color tcolor = GameData.Instance.colors [0];
										oldBg.transform.GetComponent<SpriteRenderer> ().color = tcolor;
//										GameData.Instance.paths [tColorid].RemoveAt(GameData.Instance.paths [tColorid].Count-1);
										removePath(tColorid,GameData.Instance.paths[tColorid].Count-1);
											GameData.Instance.ColorData [oldId] = 0;

											//next prev id
											oldId = GameData.Instance.paths [tColorid] [GameData.Instance.paths [tColorid].Count - 1];


											GameData.Instance.lasttx = tx;
											GameData.Instance.lastty = ty;

								
									}

								} else {//draw on other block lines,cut them
									//clear the being cutted other color old paths
									int tOtherColorId = GameData.Instance.ColorData[id];
									int oldId = GameData.Instance.paths [tOtherColorId] [GameData.Instance.paths [tOtherColorId].Count-1];
									GameData.Instance.linkedLines [tOtherColorId] = 0;//reopen the linkage
									if (GameData.Instance.DotColorData[oldId] == 0 || tOtherColorId != GameData.Instance.pickColor) {//if this place doesnt have a dot or this place is a different color
										if (GameData.Instance.DotColorData [id] == 0) {	//make wont draw on other color dots
											while (oldId != id) {
									
												int oldx = Mathf.FloorToInt (oldId % GameData.bsize);
												int oldy = Mathf.FloorToInt (oldId / GameData.bsize);
												GameObject oldBg = GameObject.Find ("bg" + oldx + "_" + oldy);

												Color tcolor = GameData.Instance.colors [0];
												oldBg.transform.GetComponent<SpriteRenderer> ().color = tcolor;
//												GameData.Instance.paths [tOtherColorId].RemoveAt (GameData.Instance.paths [tOtherColorId].Count - 1);
												removePath(tOtherColorId,GameData.Instance.paths[tOtherColorId].Count-1);
												GameData.Instance.ColorData [oldId] = 0;

												//next prev id
												oldId = GameData.Instance.paths [tOtherColorId] [GameData.Instance.paths [tOtherColorId].Count - 1];

											}

											if (oldId == id) {
												int oldx = Mathf.FloorToInt (oldId % GameData.bsize);
												int oldy = Mathf.FloorToInt (oldId / GameData.bsize);
												GameObject oldBg = GameObject.Find ("bg" + oldx + "_" + oldy);

												Color tcolor = GameData.Instance.colors [0];
												oldBg.transform.GetComponent<SpriteRenderer> ().color = tcolor;
//												GameData.Instance.paths [tOtherColorId].RemoveAt (GameData.Instance.paths [tOtherColorId].Count - 1);
												removePath(tOtherColorId,GameData.Instance.paths[tOtherColorId].Count-1);
												GameData.Instance.ColorData [oldId] = 0;

									
											}
										}
									}

					


								}
							}
						}
					}

				}
				checkWin ();
			}//if holding
		}



		void addColor(){
			int tdotColor = GameData.Instance.DotColorData [id];

//			if (GameData.Instance.linkedLines [GameData.Instance.pickColor] == 1 && GameData.Instance.pickColor == GameData.Instance.ColorData[id])//this linkage is closed,unless the linkage being breaked or restart,wont be able to continue drawing
//				return;

			if ((tdotColor!= 0 && tdotColor != GameData.Instance.pickColor)//draw on other color dots(not block)
				
			) {
				GameData.Instance.isHolding = false;
				GameData.Instance.pickColor = 0;
				return;
			} 




			Color tcolor = GameData.Instance.colors [GameData.Instance.pickColor];
			tcolor.a = .5f;
			transform.GetComponent<SpriteRenderer> ().color = tcolor;
//			GameData.Instance.paths[GameData.Instance.pickColor].Add (id);
			addPath(GameData.Instance.pickColor,id);
//			print ("added"+GameData.Instance.pickColor);

			GameData.Instance.ColorData [id] = GameData.Instance.pickColor;//write color to data


			if (tdotColor != 0 && tdotColor == GameData.Instance.pickColor && GameData.Instance.paths [tdotColor].Count > 1) {
				GameData.Instance.linkedLines [tdotColor] = 1;
				GameData.Instance.pickColor = 0;
			}


			GameData.Instance.lasttx = tx;
			GameData.Instance.lastty = ty;
		}


		void OnMouseUp(){
			if (GameData.Instance.isWin)
				return;
			GameData.Instance.isHolding = false;


		}



		void addPath(int colorId,int placeId){
			GameData.Instance.paths[colorId].Add (placeId);
			int tlen = GameData.Instance.paths [colorId].Count;
			if (tlen > 1) {
				int tlastId1 = GameData.Instance.paths [colorId] [tlen - 2];
				int tlastId2 = GameData.Instance.paths [colorId] [tlen - 1];


				int tx = Mathf.FloorToInt (tlastId1 % GameData.bsize);
				int ty = Mathf.FloorToInt (tlastId1 / GameData.bsize);

				int placeOffset = tlastId2 - tlastId1;
//				print ("placeOffset" + placeOffset);

				int tRight = 1;
				int tLeft = -1;
				int tUp = GameData.bsize;//paths go up
				int tDown = -GameData.bsize;
				GameObject tlink = null;
				if (placeOffset == 1) {//right
					tlink = GameObject.Find("linkr"+tx+"_"+ty);
				} else if (placeOffset == -1) {
					tlink = GameObject.Find("linkl"+tx+"_"+ty);
				} else if (placeOffset == tUp) {
					tlink = GameObject.Find("linku"+tx+"_"+ty);
				} else if (placeOffset == tDown) {
					tlink = GameObject.Find("linkd"+tx+"_"+ty);
				}
				if (tlink != null) {
					tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [colorId];
				}
			}

		}

		void removePath(int colorId,int index){
			


			//clear all linkage on this node
			int tlastId = GameData.Instance.paths [colorId] [index];
							int tx = Mathf.FloorToInt (tlastId % GameData.bsize);
							int ty = Mathf.FloorToInt (tlastId / GameData.bsize);
							GameObject tlink = null;
							tlink = GameObject.Find ("linkr" + tx + "_" + ty);
							tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
							tlink = GameObject.Find ("linkl" + tx + "_" + ty);
							tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
							tlink = GameObject.Find ("linku" + tx + "_" + ty);
							tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
							tlink = GameObject.Find ("linkd" + tx + "_" + ty);
							tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];


			GameData.Instance.paths[colorId].RemoveAt (index);

			if (index > 0) {
				tlastId = GameData.Instance.paths [colorId] [index - 1];
				tx = Mathf.FloorToInt (tlastId % GameData.bsize);
				ty = Mathf.FloorToInt (tlastId / GameData.bsize);
	
				tlink = GameObject.Find ("linkr" + tx + "_" + ty);
				tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
				tlink = GameObject.Find ("linkl" + tx + "_" + ty);
				tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
				tlink = GameObject.Find ("linku" + tx + "_" + ty);
				tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
				tlink = GameObject.Find ("linkd" + tx + "_" + ty);
				tlink.GetComponent<SpriteRenderer> ().color = GameData.Instance.colors [0];
			}



		}

		void checkWin(){
			int nwin = 0;
			for (int k = 0; k < GameData.Instance.linkedLines.Length; k++) {
				if (GameData.Instance.linkedLines [k] == 1) {
					nwin++;
				}

			}
			print("currentlink=" + nwin + "__"+GameData.Instance.dotPoses.Length);
			if(nwin == GameData.Instance.dotPoses.Length)
			{
				GameData.Instance.isHolding = false;
				GameData.Instance.isWin = true;
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
