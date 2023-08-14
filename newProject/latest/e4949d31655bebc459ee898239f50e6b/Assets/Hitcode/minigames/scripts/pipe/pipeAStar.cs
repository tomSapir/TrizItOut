using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace pipe{
	public class pipeSearch
	{
		public static List<MyPathNode> getConnectPipes(int tx,int ty){
			
			//clear first
			checkList = new List<MyPathNode> ();
			List<MyPathNode> tpaths = new List<MyPathNode>();
			foreach (MyPathNode tnode in GameData.Instance.grid) {
				tnode.isChecked = false;
			}

			if (tx == 0 && ty == 0) {//isfirstnode,check whether connect to left first(the water source)
				if (GameData.Instance.grid [tx, ty].passable [0] == 0) {
					return checkList;//no need to do anything
				}
			}
			checkList.Add (GameData.Instance.grid [tx, ty]);
			tpaths.Add (GameData.Instance.grid [tx, ty]);


			while (checkList.Count > 0) {
				MyPathNode tnode = checkList [0];
				if (tnode.passable [0] == 1) {//check left neigbour
					if(tnode.X - 1 >= 0){
						if (GameData.Instance.grid [tnode.X - 1, tnode.Y].isChecked == false) {//if checked,ignore
							if (GameData.Instance.grid [tnode.X - 1, tnode.Y].passable[2] == 1) {
								tpaths.Add (GameData.Instance.grid [tnode.X - 1, tnode.Y]);//add to last
								checkList.Add (GameData.Instance.grid [tnode.X - 1, tnode.Y]);//passable,add to the checklist
							}////whether this neigbours' right is passable 
						}
					}//if left is exist
				}
				if(tnode.passable [1] == 1) {//check up
					if(tnode.Y + 1 < GameData.Instance.gridHeight){
						if (GameData.Instance.grid [tnode.X , tnode.Y+1].isChecked == false) {//if checked,ignore
							if (GameData.Instance.grid [tnode.X , tnode.Y+1].passable[3] == 1) {
								tpaths.Add (GameData.Instance.grid [tnode.X , tnode.Y+1]);//add to last
								checkList.Add (GameData.Instance.grid [tnode.X , tnode.Y+1]);//passable,add to the checklist
							}////whether this neigbour's down is passable 
						}
					}//if up is exist
				}
				if(tnode.passable [2] == 1) {//check right
					if(tnode.X + 1 < GameData.Instance.gridWidth){
						if (GameData.Instance.grid [tnode.X + 1, tnode.Y].isChecked == false) {//if checked,ignore
							if (GameData.Instance.grid [tnode.X + 1, tnode.Y].passable[0] == 1) {
								tpaths.Add (GameData.Instance.grid [tnode.X + 1, tnode.Y]);//add to last
								checkList.Add (GameData.Instance.grid [tnode.X + 1, tnode.Y]);//passable,add to the checklist
							}////whether this neigbours' left is passable 
						}
					}//if right is exist
				}
				if(tnode.passable [3] == 1) {//check down neigbour
					if(tnode.Y - 1 >= 0){
						if (GameData.Instance.grid [tnode.X , tnode.Y-1].isChecked == false) {//if checked,ignore
							if (GameData.Instance.grid [tnode.X , tnode.Y-1].passable[1] == 1) {
								tpaths.Add (GameData.Instance.grid [tnode.X, tnode.Y - 1]);//add to last
								checkList.Add (GameData.Instance.grid [tnode.X , tnode.Y-1]);//passable,add to the checklist
							}////whether this neigbour's up is passable 
						}
					}//if down is exist
				}

				//all checked,remove and mark checked (wont be check again)
				tnode.isChecked = true;
				checkList.RemoveAt (0);
			}//while




			return tpaths;
		}

		public static List<MyPathNode> checkList;


	}
}


