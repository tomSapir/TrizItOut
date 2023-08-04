using System;
using System.Collections.Generic;

/// <summary>
/// 寻路方格 用于组成寻路网格的基本方格 
/// </summary>
public class PathFindingGrid
{
	/// <summary>
	/// 方格的x坐标
	/// </summary>
	private int x;
	public int X 
	{
		get { return x; }
		set { x = value; }
	}
	
	/// <summary>
	/// 方格的y坐标
	/// </summary>
	private int y;
	public int Y 
	{
		get { return y; }
		set { y = value; }
	}

	/// <summary>
	/// 该方格是否已被作为起点检查过周围的方格
	/// </summary>
	private bool isChecked = false;
	public bool IsChecked
	{
		set {isTarget = value;}
		get {return isTarget;}
	}
	
	/// <summary>
	/// 该方格是否为本次寻路的终点
	/// </summary>
	private bool isTarget = false;
	public bool IsTarget
	{
		set {isTarget = value;}
		get {return isTarget;}
	}
	
	/// <summary>
	/// 该方格是否为障碍
	/// </summary>
	private bool isBlock = false;
	public bool IsBlock
	{
		set {isBlock = value;}
		get {return isBlock;}
	}
		
	/// <summary>
	/// 可以到达该方格的路径， 作为到达该方格的最优路径的待选路径
	/// </summary>
	private List<List<PathFindingGrid>> paths = new List<List<PathFindingGrid>>();
	public List<List<PathFindingGrid>> Paths
	{
		set {paths = value;}
		get {return paths;}
	}
		
	/// <summary>
	/// 本格的上方的方格
	/// </summary>
	private PathFindingGrid topGrid = null;
	public PathFindingGrid TopGrid
	{
		set {topGrid = value;}
		get {return topGrid;}
	}
	
	/// <summary>
	/// 本格的下方的方格
	/// </summary>
	private PathFindingGrid bottonGrid = null;
	public PathFindingGrid BottonGrid
	{
		set {bottonGrid = value;}
		get {return bottonGrid;}
	}
	
	/// <summary>
	/// 本格的左方的方格
	/// </summary>
	private PathFindingGrid leftGrid = null;
	public PathFindingGrid LeftGrid
	{
		set {leftGrid = value;}
		get {return leftGrid;}
	}
	
	/// <summary>
	/// 本格的右方的方格
	/// </summary>
	private PathFindingGrid rightGrid = null;
	public PathFindingGrid RightGrid
	{
		set {rightGrid = value;}
		get {return rightGrid;}
	}
	
	/// <summary>
	/// 将本格作为起点， 检查四周的方格是否可以到达
	/// </summary>
	public void CheckAround()
	{
		// 如果某方格可以到达， 将在该方格中添加到达该方格的最优路径（之一）
		AddEnabledPath(TopGrid);
		AddEnabledPath(BottonGrid);
		AddEnabledPath(LeftGrid);
		AddEnabledPath(RightGrid);
		
		// 检查完毕后， 新的起点方格将不在检测本方格
		isChecked = true;
		
		// 消除本方格保存的待选路径， 节约空间复杂度（内存空间）			
		paths = null;
	}
		
	/// <summary>
	/// 在可以到达的方格中添加能到达该方格的最优路径（之一）
	/// </summary>
	/// <param name="grid"></param>
	private void AddEnabledPath(PathFindingGrid grid)
	{
		// 如果是一个有效的方格
		if (IsValidGrid(grid))
		{
			// 用来保存到达当前方格的最优路径的的克隆（避免对当前最优路径的引用进行操作， 影响判断其他方向的格子）
			List <PathFindingGrid> cloneOptimalPath = new List<PathFindingGrid>();
			
			// 获取到达当前方格的最优路径
			List <PathFindingGrid> optimalPath = GetOptimalPath();
			
			// 将到达当前方格的最优路径克隆到克隆的最优路径
			foreach(PathFindingGrid gridOfOptimalPath in optimalPath)
			{
				cloneOptimalPath.Add(gridOfOptimalPath);
			}
			
			// 将目标方格添加到克隆的最优路径中， 即作为可以到达目标方格的路径
			cloneOptimalPath.Add(grid);
			
			// 将到达目标方格的路径添加到到目标方格的最优路径的待选路径中
			grid.Paths.Add(cloneOptimalPath);
		}
	}
	
	/// <summary>
	/// 是否是一个有效的方格
	/// </summary>
	/// <returns></returns>
	public bool IsValidGrid(PathFindingGrid grid)
	{
		// 该方格必须同时满足存在， 未被检查过， 不是障碍， 才是一个有效的方格
		if ((grid != null) && (!grid.isChecked) && (!grid.IsBlock))
		{
			return true;
		}
		return false;
	}
		
	/// <summary>
	/// 从在可以到达该方格的待选路径中选择一条最优路径
	/// </summary>
	/// <returns></returns>
	public List<PathFindingGrid> GetOptimalPath()
	{
		// 第一次比较前将最优路径置空
		List<PathFindingGrid> optimalPath = null;
		
		// 枚举所有待选路径
		foreach(List<PathFindingGrid> path in Paths)
		{
			// 第一条路径出现时， 将最优路径设为第一条路径
			if (optimalPath == null)
			{
				optimalPath = path;
			}
			// 第二条及以后的路径， 将于当前已知的最优路径比较
			else
			{
				// 如果新的路径经过的节点数少于当前已知的最优路径， 将新路径设为最优路径
				if (path.Count < optimalPath.Count)
				{
					optimalPath = path;	
				}
			}
		}
		
		// 返回最优路径
		return optimalPath;
	}
	
	/// <summary>
	/// 从当前方格的四周获取方格作为新的路径起点 
	/// </summary>
	/// <returns></returns>
	public List<PathFindingGrid> GetStartGrids ()
	{
		// 新的路径起点方格的链表
		List<PathFindingGrid> newStartGrids = new List<PathFindingGrid>();
		
		// 检测该方格的上方的方格是否可以作为新的起点， 如果可以(是一个有效的节点)则加入
		if (IsValidGrid(TopGrid))
		{
			newStartGrids.Add(TopGrid);
		}
		
		// 检测该方格的下方的方格是否可以作为新的起点， 如果可以(是一个有效的节点)则加入
		if (IsValidGrid(BottonGrid))
		{
			newStartGrids.Add(BottonGrid);
		}
		
		// 检测该方格的左方的方格是否可以作为新的起点， 如果可以(是一个有效的节点)则加入
		if (IsValidGrid(LeftGrid))
		{
			newStartGrids.Add(LeftGrid);
		}
		
		// 检测该方格的右方的方格是否可以作为新的起点， 如果可以(是一个有效的节点)则加入
		if (IsValidGrid(RightGrid))
		{
			newStartGrids.Add(RightGrid);
		}
		
		// 返回可以作为起点的方格的链表
		return newStartGrids;
	}
}