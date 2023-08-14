using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace flipall{
	public class flipall : MonoBehaviour {

		// Use this for initialization
		SimpleJSON.JSONNode tleveldata;
		void Start () {


            //initView();

        }

		SimpleJSON.JSONNode blocks;
		SimpleJSON.JSONNode walls;
		SimpleJSON.JSONNode answers;//not used yet
		GameObject container;
		public void init(){
            GameData.Instance.nActiveBlock = 0;
            Transform tcontainer = transform.Find("container");
            if (tcontainer.transform.childCount > 0)
            {
                foreach (Transform child in tcontainer.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

                
            }
            StartCoroutine("waitaframe");



            //container.transform.localScale *= .9f;
        }

        IEnumerator waitaframe()
        {
            yield return new WaitForEndOfFrame();
            GameData.Instance.init();






            int clevel = Random.Range(0, 40);
            tleveldata = GameData.Instance.getLevel(clevel);
            container = transform.Find("container").gameObject;
            float frameW = GetComponent<SpriteRenderer>().sprite.bounds.size.x;




            GameObject tblock = Resources.Load("flipall/block") as GameObject;
            GameObject twall = Resources.Load("flipall/wallblock") as GameObject;
            blocks = tleveldata[0];
            walls = tleveldata[1];







            //get the max board size
            int tmax = 0;
            for (int i = 0; i < walls.Count; i++)
            {
                if (tmax <= walls[i])
                {
                    tmax = walls[i];
                }
            }

            int tmin = tmax;
            for (int i = 0; i < walls.Count; i++)
            {
                if (tmin >= walls[i])
                {
                    tmin = walls[i];
                }
            }

            if (tmin < 3)
            {
                for (int i = 0; i < walls.Count; i++)
                {
                    walls[i] += (3 - tmin);
                }
                for (int i = 0; i < blocks.Count; i++)
                {
                    blocks[i] += (3 - tmin);
                }
            }

            for (int i = 0; i < walls.Count; i++)
            {
                if (tmax <= walls[i])
                {
                    tmax = walls[i];
                }
            }


            GameData.bsize = tmax - 2;
            float gridW = frameW / GameData.bsize;

            float tscale = gridW / tblock.GetComponent<SpriteRenderer>().bounds.size.x;

            for (int i = 0; i < GameData.bsize; i++)
            {
                for (int j = 0; j < GameData.bsize; j++)
                {
                    GameData.Instance.wallData.Add(0);//init all area to blank(not create wall)

                    GameObject newblock = Instantiate(tblock, container.transform);
                    newblock.transform.localPosition = new Vector3(j * gridW - frameW / 2 + gridW / 2, i * gridW - frameW / 2 + gridW / 2, 0);
                    newblock.transform.localScale *= tscale;
                    newblock.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
                    //				GameData.Instance.blockData[]
                    int tid = j + i * GameData.bsize;
                    //					print (tid);
                    newblock.gameObject.AddComponent<TouchBlock>();
                    newblock.gameObject.AddComponent<BoxCollider>();

                    newblock.GetComponent<TouchBlock>().gx = j;
                    newblock.GetComponent<TouchBlock>().gy = i;

                    newblock.GetComponent<TouchBlock>().id = tid;
                    newblock.name = "block" + j + "_" + i;

                    newblock.GetComponent<SpriteRenderer>().color = Color.clear;


                }
            }




            for (int i = 0; i < blocks.Count; i += 2)
            {
                int tx = (int.Parse(blocks[i]) - 3);
                int ty = (int.Parse(blocks[i + 1]) - 3);
                int tid = tx + (ty) * GameData.bsize;

                GameObject newblock = GameObject.Find("block" + tx + "_" + ty);
                newblock.transform.GetComponent<SpriteRenderer>().sortingOrder = 3;
                if (newblock != null)
                {
                    newblock.GetComponent<TouchBlock>().gx = tx;
                    newblock.GetComponent<TouchBlock>().gy = ty;
                    newblock.GetComponent<TouchBlock>().id = tid;
                }
                newblock.GetComponent<SpriteRenderer>().color = Color.white;

                newblock.GetComponent<TouchBlock>().isBlock();

                GameData.Instance.nActiveBlock++;//count how many block left,0 to win
            }

            for (int i = 0; i < walls.Count; i += 2)
            {
                int tx = (int.Parse(walls[i]) - 3);
                int ty = (int.Parse(walls[i + 1]) - 3);
                GameObject newall = Instantiate(twall, container.transform);
                newall.transform.localPosition = new Vector3(tx * gridW - frameW / 2 + gridW / 2, ty * gridW - frameW / 2 + gridW / 2, 0);
                newall.transform.localScale *= tscale;
                newall.transform.GetComponent<SpriteRenderer>().sortingOrder = 2;

                int tid = tx + (ty) * GameData.bsize;
                //				print (tid);
                //				print ("wallid" + tx+ "_"+ty);
                GameData.Instance.wallData[tid] = 1;

            }
        }

		// Update is called once per frame
		void Update () {

		}
	}
}