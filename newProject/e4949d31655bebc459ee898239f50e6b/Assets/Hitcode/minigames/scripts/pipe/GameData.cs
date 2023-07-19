using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hitcode_RoomEscape;
namespace pipe{
	public class GameData : Singleton<GameData> {


		public int gridWidth;
		public int gridHeight;
		public float scale = 2;

		public bool isLock = false;

		public MyPathNode[,] grid;

		public void setGrid(int x,int y,GameObject pipe){
			string tpipename = pipe.name;
			if (tpipename == "")//there is no pipe
				return;
			int rotateTime = (int)(pipe.transform.localEulerAngles.z) / 90;
			if (rotateTime == -1) {//-90=270
				rotateTime = 3;
			}
			//			print (rotateTime +tpipename);
			string tnewpipename = Utils.leftshift(tpipename,rotateTime,tpipename.Length);

//			print (tpipename +"eu"+rotateTime+"  "+ tnewpipename);

			char[] char1 = tnewpipename.ToCharArray();
			for(int i = 0;i<char1.Length;i++){
				GameData.Instance.grid [x, y].passable [i] = char1 [i] == 't' ? 1 : 0;
			}
		}



        Actions[] actions;
		public void init(){
			isLock = false;
			originPipes = new Dictionary<string,GameObject> ();
            actions = instance.gameObject.transform.GetComponents<Actions>();

        }


		public void refreshPath(){
			List<MyPathNode> allPath = pipeSearch.getConnectPipes (0, 0);
			//clear
			foreach (MyPathNode tnode in GameData.Instance.grid) {
				GameObject tblock = GameObject.Find (tnode.X + "," + tnode.Y);
				tblock.GetComponent<SpriteRenderer> ().color = Color.white;
			}
			//draw path
			foreach (MyPathNode tpath in allPath) {
				GameObject tblock = GameObject.Find (tpath.X + "," + tpath.Y);
				tblock.GetComponent<SpriteRenderer> ().color = Color.red;
				if (tpath.X == GameData.instance.gridWidth - 1 && tpath.Y == GameData.instance.gridHeight - 1) {
					GameData.instance.isWin = true;
					print ("win!");

                    foreach (Actions taction in actions)
                    {
                        for (int i = 0; i < taction.actionSteps.Count; i++)
                        {
                            taction.playActionNow(i);
                        }

                    }
                }
			}
//			print (allPath.Count+"count");
		}

		[HideInInspector]
		public Dictionary<string,GameObject> originPipes;//loading is slow,for Instantiate
		[HideInInspector]
		public bool isWin = false;


	}
}


