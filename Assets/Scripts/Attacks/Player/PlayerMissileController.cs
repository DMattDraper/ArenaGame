using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissileController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	public float speed;
	public float rotationSpeed = 5.0f;
	public bool isExplosive;
	public bool rotates;
	public GameObject explosion;
	
	//Private Members
	private Vector2 direction;
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
		
		//Get Direction
		direction = GetDirection();
		
		//Destroy after lifetime expires
		Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rBody.velocity = direction * speed;
		if (rotates) {
			transform.Rotate(0.0f, 0.0f, rotationSpeed, Space.World);
		}
    }
	
	// Occurs upon entering a trigger collision
	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Enemy"){
			
			Hit(other.gameObject);
			if(isExplosive){
				Instantiate(explosion, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		} else if (other.gameObject.tag == "Wall"){
			
			if(isExplosive){
				Instantiate(explosion, transform.position, transform.rotation);
			}
			Destroy(gameObject);
		}
	}
	
	// Hit the opposing object
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
	
	// Get direction between the player and the mouse
	Vector2 GetDirection(){		
		//Get Mouse Position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
		return (mousePosition-playerRigidbody.position).normalized;
	}
	
	
	// Get angle between the player and the mouse
	float GetAngle(){		
		//Get Mouse Position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
		float angle = Mathf.Atan2(mousePosition.y-playerRigidbody.position.y, mousePosition.x-playerRigidbody.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
