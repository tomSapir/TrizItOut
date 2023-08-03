using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace fillpath{

	public class fillpath : MonoBehaviour {
		SimpleJSON.JSONNode levelData;
		void Start(){
			GameData.Instance.init ();
			levelData = GameData.Instance.getLevel(3);
			print(levelData);
			initView ();
		}

		GameObject normalBlock;
		GameObject Number1,Number2,Number3,Number4,Number5;
		float boundsize;
		GameObject container;

		void initView(){
			container = transform.Find ("container").gameObject;
			Sprite normalBlocksp = Resources.Load<Sprite>("fillpath/block");
			normalBlock = new GameObject ();
			normalBlock.AddComponent <SpriteRenderer>();
			normalBlock.GetComponent<SpriteRenderer> ().sprite = normalBlocksp;
			boundsize = normalBlocksp.bounds.size.x;
			normalBlock.transform.position = new Vector2 (-100000, 0);


			int tmaxX = 0;
			int tmaxY = 0;
			SimpleJSON.JSONNode blocks = levelData [0];
			//create blank blocks
			for (int i = 0; i < blocks.Count-1; i+=2) {
				GameObject tblock = Instantiate (normalBlock, container.transform);
				int tx = blocks [i];int ty = blocks [i+1];
				tblock.transform.position = new Vector2 (tx*boundsize,ty*boundsize);
				if (tmaxX < tx)
					tmaxX = tx;
				if (tmaxY < ty)
					tmaxY = ty;

				tblock.name = tx + "_" + ty;
				tblock.AddComponent<touchblock> ();
				tblock.AddComponent<BoxCollider2D> ();
				tblock.GetComponent<touchblock> ().x = tx;
				tblock.GetComponent<touchblock> ().y = ty;

				GameData.Instance.blockNumber++;
			}



			SimpleJSON.JSONNode heros = levelData [1];

		
			//start blocks
			for (int i = 0; i < heros.Count - 1; i += 2) {
				GameObject  tblock = Instantiate (normalBlock, container.transform);
				int tx = heros [i];int ty = heros [i+1];
				tblock.transform.position = new Vector2 (tx*boundsize,ty*boundsize);
				tblock.GetComponent<SpriteRenderer> ().color = Color.green;

				tblock.AddComponent<touchblock> ();
				tblock.AddComponent<BoxCollider2D> ();
				tblock.GetComponent<touchblock> ().x = tx;
				tblock.GetComponent<touchblock> ().y = ty;

				tblock.GetComponent<touchblock> ().canPick = true;

				GameData.Instance.blockNumber++;
			}
			//block with number

			if (levelData.Count > 2) {
				SimpleJSON.JSONNode numbers = levelData [2];
				for (int i = 0; i < numbers.Count - 1; i += 3) {
					int tx = numbers [i];int ty = numbers [i+1];
					int tnum = numbers [i + 2];

					string tblockname = tx + "_" + ty;
//					print (tblockname);

					GameObject tblock = Instantiate (normalBlock, container.transform);

					tblock.transform.position = new Vector2 (tx*boundsize,ty*boundsize);
					tblock.name = tx + "_" + ty;
					Sprite numbersp = Resources.Load<Sprite>("fillpath/number"+tnum);
					GameObject blockNumber = new GameObject ();
					blockNumber.AddComponent <SpriteRenderer>();
					blockNumber.GetComponent<SpriteRenderer> ().sprite = numbersp;
					blockNumber.transform.parent = tblock.transform;
					blockNumber.transform.localPosition = Vector2.zero;

					tblock.AddComponent<touchblock> ();
					tblock.AddComponent<BoxCollider2D> ();
					tblock.GetComponent<touchblock> ().x = tx;
					tblock.GetComponent<touchblock> ().y = ty;

					tblock.GetComponent<touchblock> ().num = tnum;
					GameData.Instance.numblock.Add (tblock);

					GameData.Instance.blockNumber++;
				}

				GameData.Instance.retfreshNumBlock ();
			}

			container.transform.Translate(new Vector2(-boundsize*tmaxX/2,-boundsize*tmaxY/2));

		}


	}
}

