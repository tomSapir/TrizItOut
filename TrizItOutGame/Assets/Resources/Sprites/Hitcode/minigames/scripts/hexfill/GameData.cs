using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace hexfill{
	public class GameData : Singleton<GameData> {

	


		string[] txtDatas;
		public void init(){
			isWin = false;
			txtDatas = Datas.getInstance().getData ("hexfill");






		}

		SimpleJSON.JSONNode levelData;
		public SimpleJSON.JSONNode getLevel(int no){
			string txtData = Datas.Instance.getData ("hexfill") [no];
			levelData = SimpleJSON.JSONArray.Parse(txtData);
			return levelData;
		}



		[HideInInspector]
		public bool isWin = false;
		[HideInInspector]
		public static int bsize = 5;


		[HideInInspector]
		public int nActiveBlock = 0;
		[HideInInspector]
		public List<GameObject> tempTouchable;

	}


}


