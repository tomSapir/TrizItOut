using System;
using System.Collections.Generic;

/// <summary>
/// Ѱ·���� �������Ѱ·����Ļ������� 
/// </summary>
public class PathFindingGrid
{
	/// <summary>
	/// �����x����
	/// </summary>
	private int x;
	public int X 
	{
		get { return x; }
		set { x = value; }
	}
	
	/// <summary>
	/// �����y����
	/// </summary>
	private int y;
	public int Y 
	{
		get { return y; }
		set { y = value; }
	}

	/// <summary>
	/// �÷����Ƿ��ѱ���Ϊ��������Χ�ķ���
	/// </summary>
	private bool isChecked = false;
	public bool IsChecked
	{
		set {isTarget = value;}
		get {return isTarget;}
	}
	
	/// <summary>
	/// �÷����Ƿ�Ϊ����Ѱ·���յ�
	/// </summary>
	private bool isTarget = false;
	public bool IsTarget
	{
		set {isTarget = value;}
		get {return isTarget;}
	}
	
	/// <summary>
	/// �÷����Ƿ�Ϊ�ϰ�
	/// </summary>
	private bool isBlock = false;
	public bool IsBlock
	{
		set {isBlock = value;}
		get {return isBlock;}
	}
		
	/// <summary>
	/// ���Ե���÷����·���� ��Ϊ����÷��������·���Ĵ�ѡ·��
	/// </summary>
	private List<List<PathFindingGrid>> paths = new List<List<PathFindingGrid>>();
	public List<List<PathFindingGrid>> Paths
	{
		set {paths = value;}
		get {return paths;}
	}
		
	/// <summary>
	/// ������Ϸ��ķ���
	/// </summary>
	private PathFindingGrid topGrid = null;
	public PathFindingGrid TopGrid
	{
		set {topGrid = value;}
		get {return topGrid;}
	}
	
	/// <summary>
	/// ������·��ķ���
	/// </summary>
	private PathFindingGrid bottonGrid = null;
	public PathFindingGrid BottonGrid
	{
		set {bottonGrid = value;}
		get {return bottonGrid;}
	}
	
	/// <summary>
	/// ������󷽵ķ���
	/// </summary>
	private PathFindingGrid leftGrid = null;
	public PathFindingGrid LeftGrid
	{
		set {leftGrid = value;}
		get {return leftGrid;}
	}
	
	/// <summary>
	/// ������ҷ��ķ���
	/// </summary>
	private PathFindingGrid rightGrid = null;
	public PathFindingGrid RightGrid
	{
		set {rightGrid = value;}
		get {return rightGrid;}
	}
	
	/// <summary>
	/// ��������Ϊ��㣬 ������ܵķ����Ƿ���Ե���
	/// </summary>
	public void CheckAround()
	{
		// ���ĳ������Ե�� ���ڸ÷�������ӵ���÷��������·����֮һ��
		AddEnabledPath(TopGrid);
		AddEnabledPath(BottonGrid);
		AddEnabledPath(LeftGrid);
		AddEnabledPath(RightGrid);
		
		// �����Ϻ� �µ���㷽�񽫲��ڼ�Ȿ����
		isChecked = true;
		
		// ���������񱣴�Ĵ�ѡ·���� ��Լ�ռ临�Ӷȣ��ڴ�ռ䣩			
		paths = null;
	}
		
	/// <summary>
	/// �ڿ��Ե���ķ���������ܵ���÷��������·����֮һ��
	/// </summary>
	/// <param name="grid"></param>
	private void AddEnabledPath(PathFindingGrid grid)
	{
		// �����һ����Ч�ķ���
		if (IsValidGrid(grid))
		{
			// �������浽�ﵱǰ���������·���ĵĿ�¡������Ե�ǰ����·�������ý��в����� Ӱ���ж���������ĸ��ӣ�
			List <PathFindingGrid> cloneOptimalPath = new List<PathFindingGrid>();
			
			// ��ȡ���ﵱǰ���������·��
			List <PathFindingGrid> optimalPath = GetOptimalPath();
			
			// �����ﵱǰ���������·����¡����¡������·��
			foreach(PathFindingGrid gridOfOptimalPath in optimalPath)
			{
				cloneOptimalPath.Add(gridOfOptimalPath);
			}
			
			// ��Ŀ�귽����ӵ���¡������·���У� ����Ϊ���Ե���Ŀ�귽���·��
			cloneOptimalPath.Add(grid);
			
			// ������Ŀ�귽���·����ӵ���Ŀ�귽�������·���Ĵ�ѡ·����
			grid.Paths.Add(cloneOptimalPath);
		}
	}
	
	/// <summary>
	/// �Ƿ���һ����Ч�ķ���
	/// </summary>
	/// <returns></returns>
	public bool IsValidGrid(PathFindingGrid grid)
	{
		// �÷������ͬʱ������ڣ� δ�������� �����ϰ��� ����һ����Ч�ķ���
		if ((grid != null) && (!grid.isChecked) && (!grid.IsBlock))
		{
			return true;
		}
		return false;
	}
		
	/// <summary>
	/// ���ڿ��Ե���÷���Ĵ�ѡ·����ѡ��һ������·��
	/// </summary>
	/// <returns></returns>
	public List<PathFindingGrid> GetOptimalPath()
	{
		// ��һ�αȽ�ǰ������·���ÿ�
		List<PathFindingGrid> optimalPath = null;
		
		// ö�����д�ѡ·��
		foreach(List<PathFindingGrid> path in Paths)
		{
			// ��һ��·������ʱ�� ������·����Ϊ��һ��·��
			if (optimalPath == null)
			{
				optimalPath = path;
			}
			// �ڶ������Ժ��·���� ���ڵ�ǰ��֪������·���Ƚ�
			else
			{
				// ����µ�·�������Ľڵ������ڵ�ǰ��֪������·���� ����·����Ϊ����·��
				if (path.Count < optimalPath.Count)
				{
					optimalPath = path;	
				}
			}
		}
		
		// ��������·��
		return optimalPath;
	}
	
	/// <summary>
	/// �ӵ�ǰ��������ܻ�ȡ������Ϊ�µ�·����� 
	/// </summary>
	/// <returns></returns>
	public List<PathFindingGrid> GetStartGrids ()
	{
		// �µ�·����㷽�������
		List<PathFindingGrid> newStartGrids = new List<PathFindingGrid>();
		
		// ���÷�����Ϸ��ķ����Ƿ������Ϊ�µ���㣬 �������(��һ����Ч�Ľڵ�)�����
		if (IsValidGrid(TopGrid))
		{
			newStartGrids.Add(TopGrid);
		}
		
		// ���÷�����·��ķ����Ƿ������Ϊ�µ���㣬 �������(��һ����Ч�Ľڵ�)�����
		if (IsValidGrid(BottonGrid))
		{
			newStartGrids.Add(BottonGrid);
		}
		
		// ���÷�����󷽵ķ����Ƿ������Ϊ�µ���㣬 �������(��һ����Ч�Ľڵ�)�����
		if (IsValidGrid(LeftGrid))
		{
			newStartGrids.Add(LeftGrid);
		}
		
		// ���÷�����ҷ��ķ����Ƿ������Ϊ�µ���㣬 �������(��һ����Ч�Ľڵ�)�����
		if (IsValidGrid(RightGrid))
		{
			newStartGrids.Add(RightGrid);
		}
		
		// ���ؿ�����Ϊ���ķ��������
		return newStartGrids;
	}
}