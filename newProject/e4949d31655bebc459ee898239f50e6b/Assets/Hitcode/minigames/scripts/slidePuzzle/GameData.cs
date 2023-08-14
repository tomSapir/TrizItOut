using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace slidePuzzle{
	public class GameData : Singleton<GameData> {
		public int row = 2;
		public int col = 2;

		public Vector2[,] gridPos;//save grid position
		public int[,] gridState;  
		
	[HideInInspector]
		public bool locked = false;
		public void init(){
			gridPos = new Vector2[col, row];
			gridState = new int[col, row];
			allBlocks = new List<GameObject> ();
		}

		public void setData(int tx,int ty,int state){
			gridState [tx, ty] = state;
		}
		public void setPos(int tx,int ty,Vector2 pos){
			gridPos [tx, ty] = pos;
		}

		public List<GameObject> allBlocks;
		public void disOrder(){
			List<GameObject> newAllBlocks = new List<GameObject>();
			allBlocks.RemoveAt (0);//ingore the firt block
			while (allBlocks.Count > 0) {
				
				int tindex = Mathf.FloorToInt (Random.Range (0, allBlocks.Count));
				GameObject tblock = allBlocks[tindex];
//				Destroy (tblock);
				newAllBlocks.Add (tblock);
				allBlocks.RemoveAt (tindex);
			}

			for (int i = 0; i < newAllBlocks.Count; i++) {
				newAllBlocks [i].transform.localPosition = gridPos [Mathf.FloorToInt((i+1)/row),(i+1)%row];

				newAllBlocks [i].GetComponent<TouchBlock> ().gx = Mathf.FloorToInt((i+1)/row);
				newAllBlocks [i].GetComponent<TouchBlock> ().gy = (i+1)%row;
//				newAllBlocks [i].name = Mathf.FloorToInt ((i+1) / row) + "_" + (i+1) % row;
//				print(newAllBlocks [i].GetComponent<TouchBlock> ().originGx);
			}
		}


	}


}


