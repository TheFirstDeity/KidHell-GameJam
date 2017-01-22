using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScrolling : MonoBehaviour {
    
    public float roomLength;

    public float windowOffsetSpeed = 2;
    public static bool isScrolling = true;

    private Vector2 originPosition;

    private float offset;
    private int scrollMultiplier;

    private int savepointScrollMultiplier;
    private float savepointOffset;
    private float savepointX;

    private Material windowMaterial;
    private Vector2 windowOffset;

    private static RoomScrolling rm;

    private void Awake()
    {
        if (rm == null)
        {
            rm = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        originPosition = transform.position;
        windowMaterial = GetComponentsInChildren<SpriteRenderer>()[1].sharedMaterial;
    }

    void Update()
    {
        windowOffset.Set(windowOffset.x + (windowOffsetSpeed * Time.deltaTime), 0);
        windowMaterial.SetTextureOffset("_MainTex", windowOffset);
    }

    public void scroll(float amount)
    {
        offset += amount;
        transform.position = new Vector2(originPosition.x + (scrollMultiplier * roomLength), transform.position.y);

        if (offset > roomLength)
        {
            scrollMultiplier++;
            offset -= roomLength;
        }
    }

    public static void setScrollMultiplier()
    {
        rm.savepointScrollMultiplier = rm.scrollMultiplier;
        rm.savepointOffset = rm.offset;
        rm.savepointX = rm.transform.position.x;
    }

    public void setLastSavePoint(Vector2 position)
    {
        transform.position = new Vector3(savepointX, 0, 0);
        scrollMultiplier = savepointScrollMultiplier;
        offset = savepointOffset;
    }
}
