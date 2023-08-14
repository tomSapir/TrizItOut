using UnityEngine;
using Hitcode_RoomEscape;
public class CameraControls : MonoBehaviour
{


    float dragSpeed = 12;                          //Camera drag speed;



    public Vector2 sizeX = new Vector2 (-15, -10);			//Default bound size;
	public Vector2 sizeY = new Vector2 (15, 10);

	public enum DragAxes									//Allowed drag axes;
	{
		XY,
		X,
		y
	}
	public DragAxes dragAxes = DragAxes.XY;	

	public enum CameraPosition
	{
		SaveCurrent,
		WithNextLevel
	}

	public CameraPosition cameraPosition = CameraPosition.SaveCurrent;
	
	public Bounds bounds;									//Camera bounds;

	public Vector3 defaultPosition;
	private Vector3 dragOrigin, touchPos, moveDir;
	private float mapX, mapY;
	private float minX, maxX, minY, maxY;
	private float vertExtent, horzExtent;


	void Awake()
	{
		gameObject.tag = "MainCamera"; //Assign default tag
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
#else
        dragSpeed = .8f;           
#endif


    }
	bool canDragX = true,canDragY = true;
    void Start() {

		//Positioning the camera
		if(defaultPosition != Vector3.zero)
			transform.position = new Vector3(defaultPosition.x, defaultPosition.y, transform.position.z);

		//Calculating bound;
		mapX = bounds.size.x;
		mapY = bounds.size.y;

		bounds.SetMinMax(sizeX, sizeY);

		vertExtent = Camera.main.GetComponent<Camera>().orthographicSize;
		horzExtent = vertExtent * Screen.width / Screen.height;

		minX = horzExtent - (mapX / 2.0F - bounds.center.x);
		maxX = (mapX / 2.0F + bounds.center.x) - horzExtent;

		minY = vertExtent - (mapY / 2.0F - bounds.center.y);
		maxY = (mapY / 2.0F + bounds.center.y) - vertExtent;

		
		if ((float)(sizeY.x - sizeX.x)/2f <= (float)horzExtent)
        {
			canDragX = false;
        }

		if ((float)(sizeY.y - sizeX.y)/2f <= (float)vertExtent)
		{
			canDragY = false;
		}

		print(canDragX + "  " + canDragY);
	}
	
	void FixedUpdate() 
	{
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
#else
        if (GameData.Instance.locked) return;
		DragCam();

		//Clamp camera movement with bound
		var v3 = transform.position;
		v3.x = Mathf.Clamp(v3.x, minX, maxX);
		v3.y = Mathf.Clamp(v3.y, minY, maxY);
		transform.position = v3;
#endif
    }
	bool initPos;
    void Update()
    {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
        if (GameData.getInstance().locked) return;
		
			DragCam();

		if (!canDragX || !canDragY)
		{
			if (!initPos)
			{
				
				var vx = (minX + maxX)/2f;
				var vy = (minY + maxY) / 2f;
				
				transform.position = new Vector3(vx ,vy,0);
				initPos = true;
			}
        }
        
			//Clamp camera movement with bound
			var v3 = transform.position;

			if (canDragX)
			{
				v3.x = Mathf.Clamp(v3.x, minX, maxX);
			}
			if (canDragY)
			{
				v3.y = Mathf.Clamp(v3.y, minY, maxY);
			}
			transform.position = v3;
			
		
#endif
    }


    //Camera movement
    void DragCam()
	{

		if (Input.GetMouseButtonDown(0))
		{
			dragOrigin = Input.mousePosition;
			return;
		}
		if (!Input.GetMouseButton(0)) return;
        
			touchPos = GameData.Instance.cameraList[GameData.Instance.cameraList.Count-1].ScreenToViewportPoint(Input.mousePosition - dragOrigin);


		switch(dragAxes)
		{
		case DragAxes.XY:
				if(canDragX && canDragY)
			moveDir = new Vector3(touchPos.x, touchPos.y, 0);
			break;
		case DragAxes.X:
				if (canDragX)
			moveDir = new Vector3(touchPos.x, 0, 0);
			break;
		case DragAxes.y:
				if (canDragY)
					moveDir = new Vector3(0, touchPos.y, 0);
			break;
		default:
			break;
		}
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_WEBGL
        moveDir *= dragSpeed * Time.deltaTime;
#else
        moveDir *= dragSpeed;
#endif


        transform.position -= moveDir;


#if UNITY_ANDROID || UNITY_IPHONE || UNITY_WP8
        /*
		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) 
		{
			touchPos = Input.GetTouch(0).deltaPosition;
			switch(dragAxes)
			{
			case DragAxes.XY:
				moveDir = new Vector3(touchPos.x, touchPos.y, 0);
				break;
			case DragAxes.X:
				moveDir = new Vector3(touchPos.x, 0, 0);
				break;
			case DragAxes.y:
				moveDir = new Vector3(0, touchPos.y, 0);
				break;
			default:
				break;
			}
			transform.position -= moveDir * dragSpeed * Time.deltaTime;
		}
        */
#endif
	}

	//Draw bounds in scene view
	void OnDrawGizmos()
	{
		bounds.SetMinMax(sizeX, sizeY);
		Gizmos.DrawWireCube(bounds.center, bounds.size);
	}
}
