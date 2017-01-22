using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("1");
        Destroy(collider.gameObject);
        GetComponent<ParticleSystem>().Play();
    }
}
