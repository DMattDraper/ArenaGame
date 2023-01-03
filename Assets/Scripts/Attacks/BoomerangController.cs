using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangController : MonoBehaviour
{
	//Public Members
	public float power;
	public float speed;
	public float rotationSpeed;
	public Rigidbody2D reaperRbody;
	public Collider2D reaperCollider;
	public string sound;
	public string hitSound;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private bool bounced = false;
	private bool reverse = false;
	
	void Awake() {
		AudioManager.Instance.Play(sound);
	}

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
		
		StartCoroutine("ReverseTimer");
    }
	
	// IDK how often FixedUpdate is called lol
	void FixedUpdate()
	{
		transform.Rotate(0.0f, 0.0f, rotationSpeed, Space.World);

		if(reverse == true){
			rBody.velocity = (reaperRbody.position - rBody.position).normalized * speed;
		}
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
				AudioManager.Instance.Play(hitSound);
				Hit(other.gameObject);
			}
			
			Destroy(gameObject);
		}
		
		// If it hits a wall bounce back early
		else if(other.gameObject.tag == "Wall" && !bounced){
			reverse = true;
			bounced = true;
		}
		
		// When the boomerang returns to the reaper destroy it
		else if(other == reaperCollider){
			Destroy(gameObject);
		}
	}
	
	// Get direction from Boomerang to player
	Vector2 GetDirection(){
		return (playerRigidbody.position - rBody.position).normalized;
	}
	
	// Get angle between the player and the Boomerang
	float GetAngle(){		
		float angle = Mathf.Atan2(playerRigidbody.position.y - rBody.position.y, playerRigidbody.position.x - rBody.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
	
	IEnumerator ReverseTimer(){
		yield return new WaitForSeconds(1.5f);
		reverse = true;
	}
}
