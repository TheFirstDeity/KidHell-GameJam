﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour {

    private Rigidbody2D rigidBody;
    public int health;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D> ();
        health = 1;
        anim = GetComponent<Animator> ();
	}
    
    public void resetPlayer()
    {
        health = 1;
        anim.SetBool("Dying", false);
    }

    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("DeathObject")) {
            Debug.Log ("hit");
            health--;
        }
        if (health == 0) {
            //show bood spill animation
            Debug.Log("dead");
            anim.SetBool("Dying", true);
            //GetComponentInChildren<ParticleSystem>().Play();
            GameManager.playerDied();
          
            AudioManagerr.playAudioClip(2);

            RetryManager.displayRetry();
        }
    }
}
