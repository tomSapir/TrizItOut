using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace linkDot{
	public class GameData : Singleton<GameData> {

	

		SimpleJSON.JSONNode levelData;
	
		public void init(){
			
			string tData = Datas.getInstance().getData ("linkdots") [0];//0,1,2,3 is level diffcult.Now let's use the easist levels
			levelData = SimpleJSON.JSONArray.Parse(tData);
			
			SimpleJSON.JSONNode levelSize = levelData[1]["levels"];//dot postion imformation(in pairs) //1 6x6 


			

			colors = new Color[]{Color.clear,Color.red, Color.blue, Color.magenta, Color.cyan, Color.green, Color.yellow, Color.gray,Color.white,Color.black,new Color(252f/255f,157f/255f,154f/255f),new Color(249f/255f,205f/255f,173f/255f),new Color(200f/255f,200f/255f,169f/255f)};
			ColorData = new int[bsize*bsize];
			DotColorData = new int[bsize * bsize];

            int tlevel =  UnityEngine.Random.Range(0, 30);
			string clevelStr = GameData.Instance.getLevel (tlevel);
			//print (clevelStr);
			dotPoses = clevelStr.Split (";" [0]);

	

			GameData.Instance.paths = new List<List<int>> ();
			List<int> tpath0 = new List<int> ();//empty,this first color is none
			GameData.Instance.paths.Add (tpath0);

			for (int i = 0; i < dotPoses.Length; i++) {
				List<int> tpath = new List<int> ();
				GameData.Instance.paths.Add (tpath);

			}

			
			linkedLines = new int[paths.Count+1];
		}


		public string getLevel(int no){
            //print("level count:"+levelData[1]["levels"].Count);
			return levelData[1]["levels"][no];
		}
		public bool isHolding = false;
		public int pickColor = -1;

		[HideInInspector]
		public string[] dotPoses;
		[HideInInspector]
		public int[] ColorData;
		[HideInInspector]
		public int[] DotColorData;
		[HideInInspector]
		public Color[] colors;
		[HideInInspector]
		public int startId = -1;
		[HideInInspector]
		public int lasttx = -1;
		[HideInInspector]
		public int lastty = -1;
		[HideInInspector]
		public List<List<int>> paths;
		[HideInInspector]
		public int[] linkedLines;//check all colors whehter links
		[HideInInspector]
		public static int bsize = 6;

		[HideInInspector]
		public bool isWin = false;

	}


}


