using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savepoint : MonoBehaviour {
    BoxCollider2D bcollider;
	// Use this for initialization
	void Start () {
        bcollider = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void resetTrigger()
    {
        bcollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.setLastSavePosition(transform.position);
        bcollider.enabled = false;
        RoomScrolling.setScrollMultiplier();
        Camera.setSavepoint();
    }
}
