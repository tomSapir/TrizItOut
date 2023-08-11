using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Prim : MonoBehaviour{

/// <summary>
/// 普利姆迷宫生成法
/// </summary>
/// <param name="startX">起始点X坐标</param>
/// <param name="startY">起始点Y坐标</param>
/// <param name="widthLimit">迷宫宽度</param>
/// <param name="heightLimit">迷宫高度</param>
/// <param name="haveBorder">迷宫是否含有墙</param>
	public static int[,] Generate(int startX, int startY, int widthLimit, int heightLimit,bool haveBorder)
{
	//block:不可通行    unBlock:可通行
	const int block = 0,unBlock = 1;
	var r=Random.Range(0,1);
	//迷宫尺寸合法化
	if (widthLimit < 1)
		widthLimit = 1;
	if (heightLimit < 1)
		heightLimit = 1;
	//迷宫起点合法化
	if (startX < 0 || startX >= widthLimit)
			startX = Random.Range(0, widthLimit);
	if (startY < 0 || startY >= heightLimit)
			startY = Random.Range(0, heightLimit);
	//减去边框所占的格子
	if (!haveBorder)
	{
		widthLimit--;
		heightLimit--;
	}
	//迷宫尺寸换算成带墙尺寸
	widthLimit *= 2;
	heightLimit *= 2;
	//迷宫起点换算成带墙起点
	startX *= 2;
	startY *= 2;
	if (haveBorder)
	{
		startX++;
		startY++;
	}
	//产生空白迷宫
	var mazeMap = new int[widthLimit + 1, heightLimit + 1];
	for (int x = 0; x <= widthLimit; x++)
	{
		//mazeMap.Add(new BitArray(heightLimit + 1));
		for (int y = 0; y <= heightLimit; y++)
		{
			mazeMap[x, y] = block;
		}
	}

	//邻墙列表
	var blockPos = new List<int>();
	//将起点作为目标格
	int targetX = startX, targetY = startY;
	//将起点标记为通路
	mazeMap[targetX, targetY] = unBlock;

	//记录邻墙
	if (targetY > 1)
	{
		blockPos.AddRange(new int[] { targetX, targetY - 1, 0 });
	}
	if (targetX < widthLimit)
	{
		blockPos.AddRange(new int[] { targetX + 1, targetY, 1 });
	}
	if (targetY < heightLimit)
	{
		blockPos.AddRange(new int[] { targetX, targetY + 1, 2 });
	}
	if (targetX > 1)
	{
		blockPos.AddRange(new int[] { targetX - 1, targetY, 3 });
	}
	while (blockPos.Count > 0)
	{
		//随机选一堵墙
			var blockIndex = Random.Range(0, blockPos.Count / 3) * 3;
		//找到墙对面的墙
		if (blockPos[blockIndex + 2] == 0)
		{
			targetX = blockPos[blockIndex];
			targetY = blockPos[blockIndex + 1] - 1;
		}
		else if (blockPos[blockIndex + 2] == 1)
		{
			targetX = blockPos[blockIndex] + 1;
			targetY = blockPos[blockIndex + 1];
		}
		else if (blockPos[blockIndex + 2] == 2)
		{
			targetX = blockPos[blockIndex];
			targetY = blockPos[blockIndex + 1] + 1;
		}
		else if (blockPos[blockIndex + 2] == 3)
		{
			targetX = blockPos[blockIndex] - 1;
			targetY = blockPos[blockIndex + 1];
		}
		//如果目标格未连通
		if (mazeMap[targetX, targetY] == block)
		{
			//联通目标格
			mazeMap[blockPos[blockIndex], blockPos[blockIndex + 1]] = unBlock;
			mazeMap[targetX, targetY] = unBlock;
			//添加目标格相邻格
			if (targetY > 1 && mazeMap[targetX, targetY - 1] == block && mazeMap[targetX, targetY - 2] == block)
			{
				blockPos.AddRange(new int[] { targetX, targetY - 1, 0 });
			}
			if (targetX < widthLimit && mazeMap[targetX + 1, targetY] == block && mazeMap[targetX + 2, targetY] == block)
			{
				blockPos.AddRange(new int[] { targetX + 1, targetY, 1 });
			}
			if (targetY < heightLimit && mazeMap[targetX, targetY + 1] == block && mazeMap[targetX, targetY + 2] == block)
			{
				blockPos.AddRange(new int[] { targetX, targetY + 1, 2 });
			}
			if (targetX > 1 && mazeMap[targetX - 1, targetY] == block && mazeMap[targetX - 1, targetY] == block)
			{
				blockPos.AddRange(new int[] { targetX - 1, targetY, 3 });
			}
		}
		blockPos.RemoveRange(blockIndex, 3);
	}
	return mazeMap;
}
}