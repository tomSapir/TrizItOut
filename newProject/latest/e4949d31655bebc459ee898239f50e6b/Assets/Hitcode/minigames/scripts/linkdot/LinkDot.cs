using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
namespace linkDot{
	public class LinkDot : MonoBehaviour {

		GameObject container;
		void Start () {
           
		}

        void init()
        {
            GameData.Instance.isHolding = false;
            GameData.Instance.isWin = false;
            Transform tcontainer = transform.Find("container");
            if (tcontainer.transform.childCount > 0)
            {
                foreach (Transform child in tcontainer.transform)
                {
                    GameObject.Destroy(child.gameObject);
                }

            }
            StartCoroutine("initGame");
        }

        Vector3 startPos = new Vector3(-9999,0,0), startScale = new Vector3(-9999,0,0);
        IEnumerator initGame()
        {

            yield return new WaitForEndOfFrame();
            GameData.Instance.init();

            container = transform.Find("container").gameObject;


            if (startPos.x == -9999)
            {
                startPos = container.transform.position;
                startScale = container.transform.localScale;
            }
            container.transform.position = startPos;




            GameObject tBg = Resources.Load("linkdots/square") as GameObject;
            float gridW = GetComponent<SpriteRenderer>().sprite.bounds.size.x / GameData.bsize;
            //
            float orginSize = tBg.GetComponent<SpriteRenderer>().bounds.size.x;
            float tscale = gridW / tBg.GetComponent<SpriteRenderer>().bounds.size.x;
            //

            float gap = tBg.GetComponent<SpriteRenderer>().bounds.size.x * (1f - tscale);
            //			print (tscale);
            GameObject tCircle = Resources.Load("linkdots/dot") as GameObject;
            GameObject tLink = Resources.Load("linkdots/link") as GameObject;

            

            float offsetx = gridW * 3f;
            float offsety = gridW * 2f;
            List<GameObject> tbgs = new List<GameObject>();
            for (int i = 0; i < GameData.bsize * GameData.bsize; i++)
            {
                int tx = Mathf.FloorToInt(i % GameData.bsize);
                int ty = Mathf.FloorToInt(i / GameData.bsize);
                GameObject tbg = Instantiate(tBg, container.transform);
                tbg.transform.localScale *= tscale;
                tbg.transform.localPosition = new Vector2(container.transform.localPosition.x + gridW * tx - offsetx + gridW / 2, container.transform.localPosition.y + gridW * ty - offsety - gridW / 2);
                tbg.GetComponent<SpriteRenderer>().sortingOrder = 1;
                tbg.name = "bg" + tx + "_" + ty;
                tbg.GetComponent<SpriteRenderer>().color = Color.clear;
                tbgs.Add(tbg);


                tbg.gameObject.AddComponent<BoxCollider>();
                tbg.gameObject.AddComponent<TouchDots>();

                tbg.gameObject.GetComponent<TouchDots>().tx = tx;
                tbg.gameObject.GetComponent<TouchDots>().ty = ty;

                GameData.Instance.ColorData[i] = 0;//no color
                GameData.Instance.DotColorData[i] = 0;//no color


                int[] rotation = new int[] { 0, 90, 180, 270 };
                for (int j = 0; j < 4; j++)
                {//add 4 link lines to each square
                    GameObject tlink = Instantiate(tLink, container.transform);
                    tlink.transform.localPosition = tbg.transform.localPosition;
                    tlink.transform.localScale = tbg.transform.localScale;
                    tlink.transform.localEulerAngles = new Vector3(0, 0, rotation[j]);
                    //					tlink.GetComponent<SpriteRenderer> ().color = Color.red;
                    tlink.GetComponent<SpriteRenderer>().color = Color.clear;
                    tlink.GetComponent<SpriteRenderer>().sortingOrder = 2;
                    switch (j)
                    {
                        case 0://right
                            tlink.name = "linkr" + tx + "_" + ty;
                            break;
                        case 1://up
                            tlink.name = "linku" + tx + "_" + ty;
                            break;
                        case 2://left
                            tlink.name = "linkl" + tx + "_" + ty;
                            break;
                        case 3://down
                            tlink.name = "linkd" + tx + "_" + ty;
                            break;
                    }
                }


            }



            int n = 1;//because 0 is no color
            foreach (string tdotPoses in GameData.Instance.dotPoses)
            {
                string[] pos = tdotPoses.Split(","[0]);




                int tx = Mathf.FloorToInt(int.Parse(pos[0]) % GameData.bsize);
                int ty = Mathf.FloorToInt(int.Parse(pos[0]) / GameData.bsize);


                GameObject tcircle = Instantiate(tCircle, tbgs[int.Parse(pos[0])].transform) as GameObject;


                tcircle.transform.localScale *= .9f;
                tcircle.GetComponent<SpriteRenderer>().sortingOrder = 3;
                tcircle.GetComponent<SpriteRenderer>().color = GameData.Instance.colors[n];


                tcircle.name = "dot";

                tx = Mathf.FloorToInt(int.Parse(pos[1]) % GameData.bsize);
                ty = Mathf.FloorToInt(int.Parse(pos[1]) / GameData.bsize);

                tcircle = Instantiate(tCircle, tbgs[int.Parse(pos[1])].transform) as GameObject;



                tcircle.GetComponent<SpriteRenderer>().sortingOrder = 3;
                tcircle.GetComponent<SpriteRenderer>().color = GameData.Instance.colors[n];

                tcircle.name = "dot";


                GameData.Instance.DotColorData[int.Parse(pos[0])] = n;
                GameData.Instance.DotColorData[int.Parse(pos[1])] = n;


                n++;
            }
            
            container.transform.Translate((tscale-1)* container.transform.parent.GetComponent<SpriteRenderer>().bounds.size.x/2, (tscale-1) * container.transform.parent.GetComponent<SpriteRenderer>().bounds.size.y/2,0);
            container.transform.localScale = startScale*.9f;
        }

        // Update is called once per frame
        void Update () {

		}
	}
}
