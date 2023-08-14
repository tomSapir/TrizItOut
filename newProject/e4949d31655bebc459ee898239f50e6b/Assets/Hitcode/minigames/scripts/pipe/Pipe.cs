using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum pipType
{

	ftft=1,
	fttf=2,
	fttt=3,
	tfft = 4,
	tftf = 5,
	tftt = 6,
	ttff = 7,
	ttft = 8,
	tttf = 9,
	tttt = 10,
	fftt = 11

}

namespace pipe{
	public class Pipe : MonoBehaviour {


	


		GameObject gridBox;
		GameObject gridBlock;
		 int gridWidth;
		 int gridHeight;
		 float scale = 2;
		public float gridSize;



		public static string distanceType;


		//This is what you need to show in the inspector.
		public static int distance = 2;
		GameObject container;
		int[,] maze;
        void Start() {
        }
        void init()
        {
            Transform container = transform.Find("container");

            foreach (Transform tchild in container.transform)
            {
                Destroy(tchild.gameObject);
            }
            StartCoroutine("initGame");
        }
        IEnumerator initGame() {
            yield return new WaitForEndOfFrame();
			GameData.Instance.init ();

			gridWidth = GameData.Instance.gridWidth;
			gridHeight = GameData.Instance.gridHeight;
			scale = GameData.Instance.scale;


			container = GameObject.Find ("container");
			Sprite gridBoxSp = Resources.Load <Sprite>("pipes/bg");
			Sprite gridBlockSp = Resources.Load <Sprite>("pipes/block") ;

			gridBox = new GameObject ();
			gridBox.AddComponent<SpriteRenderer>() ;
			gridBox.GetComponent<SpriteRenderer> ().sprite = gridBoxSp;


			gridBlock = new GameObject ();
			gridBlock.AddComponent<SpriteRenderer>() ;
			gridBlock.GetComponent<SpriteRenderer> ().sprite = gridBlockSp;


			gridSize = gridBox.GetComponent<SpriteRenderer> ().bounds.size.x*scale;
			//Generate a grid - nodes according to the specified size
			GameData.Instance.grid = new MyPathNode[gridWidth, gridHeight];


//			maze = Prim.Generate (0, 0, gridWidth, gridHeight,false);

			for (int x = 0; x < gridWidth; x++) {
				for (int y = 0; y < gridHeight; y++) {
					int trnd = Random.Range(0,100);
					bool isWall =trnd < 20 ?true:false;// maze [x, y] ==1 ? true : false;
					GameData.Instance.grid [x, y] = new MyPathNode ()
					{
						IsWall =  isWall,
						X = x,
						Y = y,
					};


				}
			}


		



			Hitcode_RoomEscape.Path tpath = Hitcode_RoomEscape.PathGenerator.Instance.GenerateRandomPath (0, 0,gridWidth-1,gridHeight-1, .5f);
			GameData.Instance.grid [0, 0].IsWall = false;
			int tx = 0;int ty = 0;
			Direction lastDirection = Direction.Right;//the first node alway from bottom left of the map,so the next grid(0,0)relative postion of it is right
			for (int i = 0; i < tpath.Count; i++) {

				if (tpath [i] == Direction.Right) {

					switch (lastDirection) {
					case Direction.Right://last is on left
						GameData.Instance.grid[tx,ty].type = pipType.tftf;
						break;
					case Direction.Left://ignore,impossible to go back
						break;
					case Direction.Up://last is on bottom
						GameData.Instance.grid[tx,ty].type = pipType.fftt;
						break;
					}
					tx++;
				} else if (tpath [i] == Direction.Left) {

					switch (lastDirection) {
					case Direction.Right://ignore
						break;
					case Direction.Left:
						GameData.Instance.grid[tx,ty].type = pipType.tftf;
						break;
					case Direction.Up:
						GameData.Instance.grid[tx,ty].type = pipType.tfft;
						break;
					}
					tx--;
				} else if (tpath [i] == Direction.Up) {

					switch (lastDirection) {
					case Direction.Right:
						GameData.Instance.grid[tx,ty].type = pipType.ttff;
						break;
					case Direction.Left:
						GameData.Instance.grid[tx,ty].type = pipType.fttf;
						break;
					case Direction.Up:
						GameData.Instance.grid[tx,ty].type = pipType.ftft;
						break;
					}
					ty ++;
				}
				//				print ("txty" + tx + "_" + ty);
				lastDirection = tpath[i];

				GameData.Instance.grid [tx, ty].IsWall = false;



			}//for
			//process last block
			switch(lastDirection){
				case Direction.Right:
				GameData.Instance.grid[tx,ty].type = pipType.tftf;
				break;
				case Direction.Left:
				break;
				case Direction.Up:
				GameData.Instance.grid[tx,ty].type = pipType.fftt;
				break;
			}

			//instantiate grid gameobjects to display on the scene
			createGrid ();



		}




		void createGrid()
		{
			//Generate Gameobjects of GridBox to show on the Screen
			for (int i =0; i<gridHeight; i++) {
				for (int j =0; j<gridWidth; j++) {

					GameObject nobj;//
					if (GameData.Instance.grid [j, i].IsWall) {
						nobj = (GameObject)GameObject.Instantiate (gridBlock);
					} else {
						nobj =  (GameObject)GameObject.Instantiate (gridBox);
					}

					//random a pipe type
					System.Array values =  System.Enum.GetValues(typeof(pipType));
					var random = Random.Range(0, 1);
					pipType rType = (pipType)values.GetValue(Random.Range(0,values.Length));

					string tpipename = getSrcName(GameData.Instance.grid[j,i].type);
			
					if (tpipename == "") {//not on path,create random pipe for misleading
						if(!GameData.Instance.grid[j,i].IsWall)
						tpipename = getSrcName (rType);
					}

					GameObject tpipe = null;
					if (!GameData.Instance.originPipes.ContainsKey (tpipename)) {
						Sprite tpipesp = Resources.Load<Sprite> ("pipes/" + tpipename);
						tpipe = new GameObject ();
						tpipe.name = tpipename;
						tpipe.AddComponent<SpriteRenderer> ();
						tpipe.GetComponent<SpriteRenderer> ().sprite = tpipesp;
						tpipe.GetComponent<SpriteRenderer> ().sortingOrder = 2;

						GameData.Instance.originPipes [tpipe.name] = tpipe;
					} else {
						tpipe = GameData.Instance.originPipes [tpipename];
					}
					GameObject newpipe = Instantiate(tpipe,nobj.transform);

					int[] rotates = { 0, 90, 180, 270 };

					newpipe.transform.localEulerAngles = new Vector3 (0, 0, rotates [Random.Range (0, 3)]);


					newpipe.name = tpipename;
					newpipe.AddComponent<BoxCollider2D> ();
					newpipe.AddComponent<TouchPipe> ();
					newpipe.GetComponent<TouchPipe> ().X = j;
					newpipe.GetComponent<TouchPipe> ().Y = i;
//					Destroy (tpipe);

					nobj.transform.localScale *= scale;
					nobj.transform.position = new Vector2(gridBox.transform.position.x + (gridSize*j), gridBox.transform.position.y + (gridSize*i));
					nobj.name = j+","+i;


					nobj.gameObject.transform.parent = container.transform;


					nobj.SetActive(true);
					//setgridpassable
//					setGridpassable (nobj);

					GameData.Instance.setGrid (j, i,newpipe);
				}

				Destroy (gridBox);
				Destroy (gridBlock);
				foreach(GameObject tobject in GameData.Instance.originPipes.Values){
					DestroyObject (tobject);
				}
			}

			container.transform.Translate (-gridWidth * gridSize / 2+gridSize/2,-gridHeight * gridSize / 2+gridSize/2,0);
            

			GameData.Instance.refreshPath ();
		
		}









		// Update is called once per frame
		void Update () {

		}

		public void addWall (int x, int y)
		{
			GameData.Instance.grid [x, y].IsWall = true;
		}

		public void removeWall (int x, int y)
		{
			GameData.Instance.grid [x, y].IsWall = false;
		}


		string getSrcName(pipType type){
			string tpipename = "";
			switch (type) {
			case pipType.fftt:
				tpipename = "fftt";
				break;
			case pipType.ftft:
				tpipename = "ftft";
				break;
			case pipType.fttf:
				tpipename = "fttf";
				break;
			case pipType.fttt:
				tpipename = "fttt";
				break;
			case pipType.tfft:
				tpipename = "tfft";
				break;
			case pipType.tftf:
				tpipename = "tftf";
				break;
			case pipType.tftt:
				tpipename = "tftt";
				break;
			case pipType.ttff:
				tpipename = "ttff";
				break;
			case pipType.ttft:
				tpipename = "ttft";
				break;
			case pipType.tttf:
				tpipename = "tttf";
				break;
			case pipType.tttt:
				tpipename = "tttt";
				break;
			}
			tpipename = randomPipe (tpipename);
			return tpipename;
		}
		/// <summary>
		/// Randoms the pipe,add useless branches.
		/// </summary>
		/// <param name="pipeName">Pipe name.</param>
		string randomPipe(string pipeName){
			string newPipeName = "";
			for (int i = 0; i < pipeName.Length; i++) {
				string tempTF = pipeName.Substring (i, 1);
				if (tempTF == "f") {
					bool addBranch = Random.Range (0, 100) < 10 ? true:false;
					if (addBranch) {
						tempTF = "t";
					}
				}
				newPipeName += tempTF;
			}
			return newPipeName;
		}



	}




}
