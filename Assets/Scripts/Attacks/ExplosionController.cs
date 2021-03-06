﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	
    // Start is called before the first frame update
    void Start()
    {
        // Destroy after lifetime expires
		Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
				Hit(other.gameObject);
			}
		} else if (other.gameObject.tag == "Enemy"){
			Hit(other.gameObject);
		} else if(other.gameObject.tag == "Caltrop"){
			Destroy(other.gameObject);
		}
	}
	
	// Hit the opposing object
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
