using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondTrigger : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag.Equals("Player"))
        {
            Debug.Log("123");
            GameManager.endingShift();
        }
    }
}
