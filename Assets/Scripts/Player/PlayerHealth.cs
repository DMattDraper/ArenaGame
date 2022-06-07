using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	//Public Members
	public float health;
	public float maxHealth;
	public string wallHitSound;
	public GameObject healthbar;
	public HealthBar uiHealthBar;
	public PlayerAnimation animator;
	public CameraShake cameraShake;
	
	//Private Members
	private Rigidbody2D rBody;
	private PlayerController pc;
	private PlayerPowerup pp;
	
	// Start is called before the first frame update
    void Start()
    {
		
		// Fill Health	
		health = maxHealth;
		uiHealthBar.SetMaxHealth(maxHealth);
		// Get Components
		rBody = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
		pp = GetComponent<PlayerPowerup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Occurs when entering a Trigger collider
	private void OnTriggerEnter2D(Collider2D other){
		
		// Collided with a Caltrop
		if (other.gameObject.tag == "Caltrop"){
			Destroy(other.gameObject);
			TakeDamage(5);
		}
	}
	
	//Occurs when entering a Collider
	private void OnCollisionEnter2D(Collision2D other){
		
		// Collided with an enemy 
		if (other.gameObject.tag == "Enemy" && other.gameObject.name != "Imp(Clone)" && pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Dashing && pp.powerup != PlayerPowerup.Powerup.Invincible){
			//Knockback
			Knockback(other.collider);
			//Damage
			TakeDamage(5);
		// Collided with a wall while stunned or dashing (Cancel Knockback/Dash)
		} else if (other.gameObject.tag == "Wall" && (pc.state == PlayerController.State.Stunned || pc.state == PlayerController.State.Dashing)){
			if (pc.state == PlayerController.State.Dashing){
				AudioManager.Instance.Play(wallHitSound);
				StartCoroutine(cameraShake.Shake(.15f,.4f));
			}
			
			rBody.velocity = new Vector2(0,0);
			pc.state = PlayerController.State.Idle;
		}
	}
	
	// Heal the player
	void Heal(float itemHealth){
		
		health += itemHealth;
		if (health > maxHealth) health = maxHealth;
		UpdateHealthBar();
	}
	
	// Damage the player
	void TakeDamage(float damage){
		
		health -= damage;
		if (health <= 0) Application.Quit();
		UpdateHealthBar();
	}
	
	// Update the health bar when damage is taken
	void UpdateHealthBar(){
		
		healthbar.transform.localScale = new Vector3(1.0f * (health/maxHealth), 0.15f, 2.0f);
		uiHealthBar.SetHealth(health);
	}
	
	// Knock the player back
	void Knockback(Collider2D other){
		
		//Get Vector between Player and the source
		Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
		Vector2 knockbackVector = rBody.position - otherRigidbody.position;
			
		//Knock the player backwards & set state to "Stunned"
		rBody.velocity = knockbackVector.normalized*15;
		pc.state = PlayerController.State.Stunned;
		animator.Stun();

		// Start the cool down
		StartCoroutine("KnockbackCooldown");
	}
	
	//Stop the knockback after a quarter second
	IEnumerator KnockbackCooldown(){
		
		yield return new WaitForSeconds(0.15f);
		pc.state = PlayerController.State.Idle;
		rBody.velocity = new Vector2(0,0);
		animator.Idle();
	}
}
