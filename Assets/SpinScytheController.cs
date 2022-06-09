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
	public Rigidbody2D target;
	public Collider2D parentCollider;
	
	//Private Members
	private Rigidbody2D playerRigidbody;
	private Vector2 positionOffset;
	private float angle;
	
    // Start is called before the first frame update
    void Start()
    {
		//Get Rigidbodies
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		transform.localScale = transform.localScale * 2.0f;
		
		//Start Decay Timer
		Destroy(gameObject,lifeTime);
    }
 
	// Update is called once per frame
	void FixedUpdate()
	{
		//positionOffset.Set(Mathf.Cos(angle) * circleRadius, Mathf.Sin(angle) * circleRadius);
		transform.position = target.position; //+ positionOffset;
		//angle += Time.deltaTime * rotationSpeed;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other != parentCollider){
			Debug.Log("Hitting non-parent");
			if (other.gameObject.tag == "Player"){
				PlayerController pc = other.gameObject.GetComponent<PlayerController>();
				PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
				if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
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
