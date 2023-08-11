using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace fillpath{
	public class GameData : Singleton<GameData> {

	


		string[] txtDatas;
		public void init(){
			isWin = false;
			numberPassed = 0;
			numblock = new List<GameObject> ();





		}

		SimpleJSON.JSONNode levelData;
		public SimpleJSON.JSONNode getLevel(int no){
			string txtData = ScriptableObject.CreateInstance<Datas>().getData ("fillpath") [no];
			levelData = SimpleJSON.JSONArray.Parse(txtData);
			return levelData;
		}



		public void retfreshNumBlock(){
			foreach (GameObject tblock in numblock) {
//				print (tblock.GetComponent<touchblock> ().num + "++" + numberPassed);
				if (numberPassed < tblock.GetComponent<touchblock> ().num   - 1) {
					tblock.GetComponent<SpriteRenderer> ().color = Color.gray;
					tblock.GetComponent<touchblock> ().canPass = false;
				} else {
					if (!tblock.GetComponent<touchblock> ().isColored) {
						tblock.GetComponent<SpriteRenderer> ().color = Color.white;
						tblock.GetComponent<touchblock> ().canPass = true;
					}

				}
			}
		}


		public void checkWin(){
			if (totalPassed == blockNumber) {
				print ("win");
			}
		}

		[HideInInspector]
		public bool isWin = false;
		[HideInInspector]
		public GameObject cPicked;
		public GameObject lastBlock;
		public int numberPassed = 0;
		public List<GameObject> numblock;
		public int totalPassed;//how many blocks passed total
		public int blockNumber = 0;//how many blocks total


	}


}


