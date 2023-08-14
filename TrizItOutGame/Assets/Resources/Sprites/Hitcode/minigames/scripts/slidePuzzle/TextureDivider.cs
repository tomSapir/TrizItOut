using UnityEngine;
using System.Collections;
namespace slidePuzzle
{
    public class TextureDivider : MonoBehaviour
    {



        public Texture2D source;
        int row, col;
        // Use this for initialization
        GameObject spritesRoot;

        void Start()
        {





            //resetGame();

        }

        //private void OnEnable()
        //{
        //    resetGame();
        //}
        int trow;
        int tcol;
        float tw, th;
        float frameSizeX;
        float frameSizeY;
        private void init()
        {
            GameData.Instance.init();

            GameObject white = Resources.Load("common/white") as GameObject;

            trow = GameData.Instance.col;
            tcol = GameData.Instance.row;
            row = trow; col = tcol;
            //			GameObject frame = GameObject.Find ("slidePuzzle");

            frameSizeX = gameObject.GetComponent<SpriteRenderer>().bounds.size.x;//*frame.transform.localScale.x;
            frameSizeY = gameObject.GetComponent<SpriteRenderer>().bounds.size.y;//*frame.transform.localScale.y;


            tw = source.width / trow;
            th = source.height / tcol;
            //			print (tw);


            spritesRoot = transform.Find("SpritesRoot").gameObject;

            foreach (Transform tsp in spritesRoot.transform)
            {
                Destroy(tsp.gameObject);
            }


            StartCoroutine("initgame");

        }

        IEnumerator initgame()
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < trow; i++)
            {
                for (int j = 0; j < tcol; j++)
                {
                    Sprite newSprite = Sprite.Create(source, new Rect(i * tw, j * th, tw, th), new Vector2(0f, 0f));




                    GameObject n = new GameObject();
                    SpriteRenderer sr = n.AddComponent<SpriteRenderer>();
                    sr.sprite = newSprite;
                    sr.transform.localScale = new Vector3(sr.transform.localScale.x * frameSizeX / sr.bounds.size.x / trow, frameSizeY / sr.bounds.size.y / tcol, 1);
                    n.transform.position = new Vector3(i * (tw / 100) * sr.transform.localScale.x - frameSizeX / 2, j * (th / 100) * sr.transform.localScale.y - frameSizeY / 2, 0) + transform.position;
                    n.transform.parent = spritesRoot.transform;


                    sr.name = i + "_" + j;
                    sr.GetComponent<SpriteRenderer>().sortingOrder = transform.GetComponent<SpriteRenderer>().sortingOrder + 1;

                    if (i == 0 && j == 0)
                    {
                        sr.enabled = false;
                        GameData.Instance.setData(i, j, 0);
                    }
                    else
                    {
                        GameData.Instance.setData(i, j, 1);
                    }
                    //					print (sr.transform.localPosition.y+"======");
                    GameData.Instance.setPos(i, j, sr.transform.localPosition);

                    sr.gameObject.AddComponent<BoxCollider>();
                    sr.gameObject.AddComponent<TouchBlock>();



                    sr.GetComponent<TouchBlock>().init(i, j);

                    sr.transform.localScale *= .95f;


                    GameData.Instance.allBlocks.Add(n);


                    //					n.layer = LayerMask.NameToLayer("game");

                }
            }

            spritesRoot.transform.localScale *= .9f;

            GameData.Instance.disOrder();
        }
    }


}