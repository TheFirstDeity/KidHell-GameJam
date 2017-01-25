using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireplace : MonoBehaviour {
    
    void OnTriggerEnter2D(Collider2D collider)
    {
        Destroy(collider.gameObject);
        GetComponent<ParticleSystem>().Play();

        PanelManager.setAndDisplayText("Next time I'll get that cookie...");
    }
}
