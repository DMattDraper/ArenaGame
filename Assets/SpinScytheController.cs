using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScytheController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	public float rotationSpeed = 1;
	public float circleRadius = 1;
	public string sound;
	public string hitSound;
	public Rigidbody2D target;
	public Collider2D parentCollider;
	
	//Private Members
	private Rigidbody2D playerRigidbody;
	private Vector2 positionOffset;
	private float angle;
	
	void Awake() {
		AudioManager.Instance.Play(sound);
	}

    // Start is called before the first frame update
    void Start()
    {
		//Get Rigidbodies
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		transform.localScale = transform.localScale * 2.5f;
		
		//Start Decay Timer
		Destroy(gameObject,lifeTime);
    }
 
	// Update is called once per frame
	void FixedUpdate()
	{
		transform.Rotate(0.0f, 0.0f, rotationSpeed, Space.World);

		transform.position = target.position;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other != parentCollider){
			Debug.Log("Hitting non-parent");
			if (other.gameObject.tag == "Player"){
				PlayerController pc = other.gameObject.GetComponent<PlayerController>();
				PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
				if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
					AudioManager.Instance.Play(hitSound);
					Hit(other.gameObject);
				}
			}
		}
	}

	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
