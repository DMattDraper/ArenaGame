﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	
    // Start is called before the first frame update
    void Start()
    {
		//Get Rigidbodies
		rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		
		//Set Rotation
		gameObject.transform.Rotate(0.0f, 0.0f, getAngle(), Space.World);
		
		//Destroy after lifetime expires
		Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
		
    }
	
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Enemy"){
			Hit(other.gameObject);
		} else if (other.gameObject.tag == "Caltrop"){
			Destroy(other.gameObject);
		}
	}
	
	public void SetOrigin(Rigidbody2D rb){
		//originRigidbody = rb;
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
	
	// Get angle between the player and the mouse
	float getAngle(){		
		//Get Mouse Position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
		float angle = Mathf.Atan2(mousePosition.y-playerRigidbody.position.y, mousePosition.x-playerRigidbody.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
