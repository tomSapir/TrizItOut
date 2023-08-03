using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Program : MonoBehaviour {

	void Start()
	{
		int mazeHeight = 3;
		int mazeWidth = 3;
		
		int meshWidth  = mazeWidth * 2  +  1;
		int meshHeight = mazeHeight * 2  +  1;
		int n = 0;

		while(n < 100)
		{			
//			Console.ForegroundColor = ConsoleColor.Gray;
//			Console.BackgroundColor = ConsoleColor.Blue;
			n++;
			
			PathFindingMesh pm = new PathFindingMesh(meshWidth, meshHeight);
			
			pm.SetStart(1, 1);
			pm.SetTarget(meshWidth - 2, meshHeight - 2);
//			
			Maze maze = new Maze(mazeWidth, mazeHeight);		
			bool[,] mazeArray = maze.GetBoolArray();
//			
			for (int i = 0; i < meshWidth; i++)
			{
				for (int j = 0; j < meshHeight; j++)
				{
					if (mazeArray[i, j])
					{
						pm.SetBlock(i, j, true);
//						Console.SetCursorPosition(i * 2, j);
//						{
							Console.Write("■");
//						}
					}
				}
			}
//			
//			List<PathFindingGrid> path = pm.GetPath();
//			
//			Console.SetCursorPosition(0, 22);
//			
//			Console.ForegroundColor = ConsoleColor.Green;
//			foreach(PathFindingGrid grid in path)
//			{
//				Console.SetCursorPosition(grid.X * 2, grid.Y);
//				Console.Write("□");
////				Thread.Sleep(50);
//			}
//			
////			Thread.Sleep(1000);
////			Console.Clear();
		}
	}
}