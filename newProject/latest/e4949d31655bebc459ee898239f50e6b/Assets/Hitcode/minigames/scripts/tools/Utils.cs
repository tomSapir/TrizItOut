using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Utils : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static string reverse(  char[] char2, int i, int j)
	{

		for (int begin=i,end=j;begin < end; begin++, end--)
		{
			char temp = char2[begin];
			char2[begin] = char2[end];
			char2[end] = temp;
		}
		return new string(char2);
	}
	public static string leftshift( string str,int i ,int j)
	{
		char[] char1 = str.ToCharArray();
		reverse( char1,0,i-1);
		reverse( char1,i,j-1);
		reverse( char1, 0, j - 1);
		return new string(char1);
	}

    
    public static string GetPath
    {
        get
        {
            switch (Application.platform)
            {
                case RuntimePlatform.IPhonePlayer:
                    return System.IO.Path.Combine(Application.persistentDataPath, "");

                case RuntimePlatform.Android:
                    return System.IO.Path.Combine(Application.temporaryCachePath, "");

                default:
                    return System.IO.Path.Combine(Directory.GetParent(Application.dataPath).FullName, "");
            }
        }
    }
}
