using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour {

    public GameObject diamond;
    public GameObject player;
    public RoomScrolling roomScrolling;

    public float playerXStartOffsetValue = -4.2f;

    private float playerXThreshold;
    private float cameraPosition;

    private float savepointPlayerXThreshold;
    private float savepointX;

    private static Camera cam;

    private float maxPositionX;

    private void Awake()
    {
        if (cam == null)
        {
            cam = this;
        }
        else
        {
            Destroy(this);
        }
    }

	// Use this for initialization
	void Start () {
        playerXThreshold = playerXStartOffsetValue;

        maxPositionX = diamond.transform.position.x - 4.2f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (player.transform.position.x > playerXThreshold && maxPositionX > transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + (player.transform.position.x - playerXThreshold), transform.position.y, transform.position.z);

            roomScrolling.scroll(player.transform.position.x - playerXThreshold);

            playerXThreshold = player.transform.position.x;
        }
	}

    public static void setSavepoint()
    {
        cam.savepointPlayerXThreshold = cam.playerXThreshold;
        cam.savepointX = cam.transform.position.x;
    }

    public void setToLastSavePoint(Vector2 savePos)
    {
        transform.position = new Vector3(savepointX, 0, -10);
        playerXThreshold = savepointPlayerXThreshold;
    }

    
}
