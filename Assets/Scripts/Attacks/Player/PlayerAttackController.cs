using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
	public static int count = 0;

	//Public Members
	public float lifeTime;
	public float power;
	public string sound;
	public string hitSound;
	public string shatterSound;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;

	void Awake() {
		AudioManager.Instance.Play(sound);
	}
	
    // Start is called before the first frame update
    void Start()
    {
		count++;
		if (count % 2 == 0) {
			transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y * -1);
		}

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
			AudioManager.Instance.Play(hitSound);
			Hit(other.gameObject);
		} else if (other.gameObject.tag == "Caltrop"){
			AudioManager.Instance.Play(shatterSound);
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
