using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RetryManager : MonoBehaviour {

    private Canvas textCanvas;

    private static RetryManager rm;

    void Awake()
    {
        if (rm == null)
        {
            rm = this;
        }
        else
        {
            Destroy(rm);
        }
    }

    // Use this for initialization
    void Start() {
        textCanvas = GetComponent<Canvas>();
        textCanvas.enabled = false;
    }
    
    public static void displayRetry()
    {
        rm.textCanvas.enabled = true;
    }

    public static void hideRetry()
    {
        rm.textCanvas.enabled = false;
    }
}
