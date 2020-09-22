using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
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
		//gameObject.transform.Rotate(0.0f, 0.0f, getAngle(), Space.World);
		
		//Start Decay Timer
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
