using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInObject : MonoBehaviour, IInteractable
{
    public float ZoomRatio = 0.5f;
    
    public void Interact(DisplayManagerLevel1 currDisplay)
    {
        Camera.main.orthographicSize *= ZoomRatio;
        Camera.main.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, Camera.main.transform.position.z);

        gameObject.layer = 2;
        currDisplay.CurrentState = DisplayManagerLevel1.State.zoom;

        //ConstrainCamera();
    }

    void ConstrainCamera()
    {
        var height = Camera.main.orthographicSize;
        var width = height * Camera.main.aspect;

        Debug.LogError("Camera zoomIn need to get fixed! object: ZoomInObject");

        var cameraBound = GameObject.Find("CameraBounds");
        if (Camera.main.transform.position.x + width > cameraBound.transform.position.x + cameraBound.GetComponent<BoxCollider2D>().size.x /2)
        {
            Camera.main.transform.position += new Vector3(cameraBound.transform.position.x + cameraBound.GetComponent<BoxCollider2D>().size.x / 2 - (Camera.main.transform.position.x + width), 0, 0);
        }

        if (Camera.main.transform.position.x - width < cameraBound.transform.position.x - cameraBound.GetComponent<BoxCollider2D>().size.x / 2)
        {
            Camera.main.transform.position += new Vector3(cameraBound.transform.position.x - cameraBound.GetComponent<BoxCollider2D>().size.x / 2 - (Camera.main.transform.position.x - width), 0, 0);
        }

        if (Camera.main.transform.position.y + height > cameraBound.transform.position.y + cameraBound.GetComponent<BoxCollider2D>().size.y / 2)
        {
            Camera.main.transform.position += new Vector3(0, cameraBound.transform.position.y + cameraBound.GetComponent<BoxCollider2D>().size.y / 2 - Camera.main.transform.position.y + height, 0);
        }

        if (Camera.main.transform.position.y - height < cameraBound.transform.position.y - cameraBound.GetComponent<BoxCollider2D>().size.y / 2)
        {
            Camera.main.transform.position += new Vector3(0, cameraBound.transform.position.y - cameraBound.GetComponent<BoxCollider2D>().size.y / 2 - (Camera.main.transform.position.y - height), 0);
        }
    }
}
