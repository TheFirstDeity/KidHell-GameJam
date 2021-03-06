﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour {

    private static PanelManager pm;

    private Canvas canvas;

    void Awake()
    {
        if (pm == null)
        {
            pm = this;
        }
        else
        {
            Destroy(pm);
        }
    }

    void Start()
    {
        canvas = GetComponent<Canvas>();
    }
	
    public static void setAndDisplayText(string text)
    {
        pm.canvas.enabled = true;
        pm.GetComponentInChildren<Text>().text = text;
    }

    public static void hideText()
    {
        pm.canvas.enabled = false;
    }
}
