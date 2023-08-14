using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace flipall{
	public class GameData : Singleton<GameData> {

	


		string[] txtDatas;
		public void init(){
			isWin = false;
			txtDatas = Datas.getInstance().getData ("flipall");
			wallData = new List<int> ();





		}

		SimpleJSON.JSONNode levelData;
		public SimpleJSON.JSONNode getLevel(int no){
			string txtData = Datas.Instance.getData ("flipall") [no];
			levelData = SimpleJSON.JSONArray.Parse(txtData);
			return levelData;
		}



		[HideInInspector]
		public bool isWin = false;
		[HideInInspector]
		public static int bsize = 5;


		public List<int> wallData;
		public int nActiveBlock = 0;

	}


}


