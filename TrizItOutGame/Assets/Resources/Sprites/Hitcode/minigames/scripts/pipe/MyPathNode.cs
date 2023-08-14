using UnityEngine;
using System.Collections;
using System;
namespace pipe{
public class MyPathNode 
{
	public Int32 X { get; set; }
	public Int32 Y { get; set; }
	public Boolean IsWall {get; set;}

	public Boolean blocked{ get; set;}//be blocked

	public pipType type {get; set;}


	public int[ ] passable = { 0, 0, 0, 0 };

	public bool isChecked  = false;
	public bool IsWalkable(System.Object unused)
	{
		return !IsWall;//&& passable;
	}


}
}
