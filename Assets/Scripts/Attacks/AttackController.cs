using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	public string sound;
	public string hitSound;
	public Transform parent;

	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;

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
		if (parent != null) {
			gameObject.transform.Rotate(0.0f, 0.0f, GetAngle(), Space.World);
		}
		
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
				AudioManager.Instance.Play(hitSound);
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
	
	// Get angle between the player and the missile
	float GetAngle(){		
		float angle = Mathf.Atan2(rBody.position.y-parent.position.y, rBody.position.x-parent.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
