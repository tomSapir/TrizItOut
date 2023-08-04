using System;
using System.Collections.Generic;

/// <summary>
/// 寻路网格类, 用于实现寻路算法
/// </summary>
public class PathFindingMesh
{	
	/// <summary>
	/// 网格宽度
	/// </summary>
	private int width = 20;
	
	/// <summary>
	/// 网格高度
	/// </summary>
	private int height = 10;
	
	/// <summary>
	/// 寻路方格的二维数组
	/// </summary>
	private PathFindingGrid[,] grids;
	
	/// <summary>
	/// 起点列表， 寻路起点的链表
	/// </summary>
	private List<PathFindingGrid> startGrids = new List<PathFindingGrid>();
	
	/// <summary>
	/// 起点方格
	/// </summary>
	private PathFindingGrid startGrid;
	
	/// <summary>
	/// 目标方格
	/// </summary>
	private PathFindingGrid targetGrid;
	
	/// <summary>
	/// 起点方格是否有效
	/// </summary>
	private bool startGridIsValid = false;
	
	/// <summary>
	/// 目标方格是否有效
	/// </summary>
	private bool targetGridIsValid = false;
	
	/// <summary>
	/// 构造函数, 传入寻路网格的宽度和高度
	/// </summary>
	/// <param name="width"></param>
	/// <param name="height"></param>
	public PathFindingMesh(int width, int height)
	{
		// 保存目标寻路网格的高度和宽度
		this.width = width;
		this.height = height;
		
		// 根据寻路网格的宽度和高度， 为寻路方格数组开辟空间
		grids = new PathFindingGrid[width, height];
		
		// 实例化每个寻路方格
		InstantiateGrids();
		
		// 组织相邻的寻路方格的关系
		OrganizeGrids();
	}
	
	/// <summary>
	/// 设置寻路起点
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetStart(int x, int y)
	{
		// 如果寻路起点在寻路网格的范围内并且是一个有效的方格， 设置有效
		if (IsInRange(x, y) && IsValidGrid(grids[x, y]))
		{
			// 根据x坐标和y坐标， 从寻路网格中选取对应的寻路方格作为起点
			startGrid = grids[x, y];
			
			// 创建一条路径， 作为最初路径
			List<PathFindingGrid> originalPath = new List<PathFindingGrid>();
			
			// 将起点方格加入最初路径
			originalPath.Add(startGrid);
			
			// 将最初路径加入起点方格的待选路径
			startGrid.Paths.Add(originalPath);
			
			// 起点方格有效标记为真
			startGridIsValid = true;
		}
	}
		
	/// <summary>
	/// 设置寻路目标
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	public void SetTarget(int x, int y)
	{		
		// 如果寻路目标在寻路网格的范围内并且是一个有效的方格， 设置有效
		if (IsInRange(x, y) && IsValidGrid(grids[x, y]))
		{			
			// 根据x坐标和y坐标， 从寻路网格中选取对应的寻路方格作为目标
			targetGrid = grids[x, y];
			
			// 将目标方格的作为目标标志打开
			targetGrid.IsTarget = true;
			
			// 目标方格有效标记为真
			targetGridIsValid = true;
		}
	}
	
	/// <summary>
	/// 检测一个方格是否有效
	/// </summary>
	/// <param name="grid"></param>
	/// <returns></returns>
	public bool IsValidGrid(PathFindingGrid grid)
	{
		return new PathFindingGrid().IsValidGrid(grid);
	}
	
	/// <summary>
	/// 设置目标格是否为障碍
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <param name="isBlock"></param>
	public void SetBlock(int x, int y, bool isBlock)
	{
		grids[x, y].IsBlock = isBlock;
	}
	
	/// <summary>
	/// 获取从起点到目标的最优路径， 如果该路径不存在则返回空
	/// </summary>
	/// <returns></returns>
	public List<PathFindingGrid> GetPath()
	{
		// 起点无效或终点无效， 则不存在路径， 返回空
		if (!startGridIsValid || !targetGridIsValid)
		{
			return null;
		}
		
		// 将起点网格加入起点网格列表
		startGrids.Add(startGrid);
		
		// 通过循环找到最优路径
		while(startGrids.Count > 0)
		{					
			// 通过上一次寻路产生的新起点链表
			List<PathFindingGrid> newStartGrids = new List<PathFindingGrid>();
			
			// 枚举起点方格列表中的可以作为起点方格的节点
			foreach(PathFindingGrid startGridNode in startGrids)
			{				
				// 获取到达该节点的最优路径
				List<PathFindingGrid> optimalPath = startGridNode.GetOptimalPath();
				
				// 枚举该路径中的每个节点， 查找是否包含目标方格
				foreach(PathFindingGrid target in optimalPath)
				{
					// 如果该路径中有节点为目标方格， 返回该路径
					if (target.IsTarget)
					{					
						return optimalPath;
					}
				}
				
				// 每个节点作为起点， 检查周围方格
				startGridNode.CheckAround();
				
				// 获取每个节点周围可以作为下次寻路时作为起点的方格
				List<PathFindingGrid> nextTimeStartGrids = startGridNode.GetStartGrids();				
				
				// 枚举下次可以作为起点的方格， 并加入新起点方格链表
				foreach(PathFindingGrid nextTimeStartGrid in nextTimeStartGrids)
				{
					// 如果当前起点列表中不包含该起点， 则加入该起点
					if (!newStartGrids.Contains(nextTimeStartGrid))
					{
						newStartGrids.Add(nextTimeStartGrid);
					}
				}				
			}
			
			// 将下次寻路的起点链表设为本次寻路发现的新起点的链表
			startGrids = newStartGrids;
		}			
		
		// 遍历所有可以到达的地方也找不到目标， 则没有路径可以到达目标方格， 返回空
		return null;
	}
	
	/// <summary>
	/// 检测要寻找的位置是否在当前寻路网格的范围内
	/// </summary>
	/// <param name="x"></param>
	/// <param name="y"></param>
	/// <returns></returns>
	private bool IsInRange(int x, int y)
	{
		if (x >= 0 && x < width && y >= 0 && y < height)
		{
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// 实例化寻路网格中的每个方格, 并设定每个方格的坐标
	/// </summary>
	private void InstantiateGrids()
	{
		for (int i = 0; i < grids.GetLength(0); i++)
		{
			for (int j = 0; j < grids.GetLength(1); j++)
			{
				grids[i, j] = new PathFindingGrid();
				grids[i, j].X = i;
				grids[i, j].Y = j;
			}
		}
	}
	
	/// <summary>
	/// 组织每个方格之间的关系， 形成相互关联的网格
	/// </summary>
	private void OrganizeGrids()
	{
		// 从第一行开始到倒数第二列， 每行的下方方格为下一行的方格
		for (int i = 0; i < grids.GetLength(0); i++)
		{
			for (int j = 0; j < grids.GetLength(1) - 1; j++)
			{
				grids[i, j].BottonGrid = grids[i, j + 1];
			}
		}
		
		// 从第二行开始到最后一行， 每行的上方方格为上一行的方格
		for (int i = 0; i < grids.GetLength(0); i++)
		{
			for (int j = 1; j < grids.GetLength(1); j++)
			{
				grids[i, j].TopGrid = grids[i, j - 1];
			}
		}
		
		// 从第一列开始到倒数第二列， 每列的右方方格为下一列的方格 
		for (int i = 0; i < grids.GetLength(0) - 1; i++)
		{
			for (int j = 0; j < grids.GetLength(1); j++)
			{
				grids[i, j].RightGrid = grids[i + 1, j];
			}
		} 
				
		// 从第二列开始到最后一列， 每列的左方方格为上一列的方格 
		for (int i = 1; i < grids.GetLength(0); i++)
		{
			for (int j = 0; j < grids.GetLength(1); j++)
			{
				grids[i, j].LeftGrid = grids[i - 1, j];
			}
		}
	}
}