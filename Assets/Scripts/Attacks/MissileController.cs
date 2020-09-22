using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	public float speed;
	public bool isExplosive;
	
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
		gameObject.transform.Rotate(0.0f, 0.0f, GetAngle(), Space.World);
		
		//Set Velocity
		rBody.velocity = GetDirection() * speed;
		
		//Destroy after lifetime expires
		Destroy(gameObject, lifeTime);
    }
	
	public void SetTarget(Rigidbody2D rb){
		//targetRigidbody = rb;
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
	
	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
				Hit(other.gameObject);
			}
			
			Destroy(gameObject);
		}
	}
	
	// Get direction from missile to player
	Vector2 GetDirection(){
		return (playerRigidbody.position - rBody.position).normalized;
	}
	
	// Get angle between the player and the missile
	float GetAngle(){		
		float angle = Mathf.Atan2(playerRigidbody.position.y-rBody.position.y, playerRigidbody.position.x-rBody.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
