using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonHealth : MonoBehaviour
{
	//Public Members
	public float maxHealth;
	public float health;
	public GameObject healthbar;
	
	//Private Members
	private Rigidbody2D rBody;
	private SkeletonController sc;
	
    // Start is called before the first frame update
    void Start()
    {
		
		// Fill Health
		health = maxHealth;
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		sc = GetComponent<SkeletonController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Occurs when entering a Trigger collider
	private void OnTriggerEnter2D(Collider2D other){
		
	}

	//Occurs when entering a Collider
	private void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Player"){
			rBody.velocity = new Vector2(0,0);
		}
	}
	
	//Knock back the skeleton
	void Knockback(Collider2D other){
		
		//Get Vector between skeleton and the source
		Rigidbody2D attackRigidbody = other.GetComponent<Rigidbody2D>();
		Vector2 knockbackVector = rBody.position - attackRigidbody.position;
			
		//Knock the player backwards
		rBody.velocity = knockbackVector.normalized*15;
		sc.state = SkeletonController.State.Stunned;		
		StartCoroutine("KnockbackCooldown");
	}
	
	//Heal upon some event
	void Heal(float itemHealth){
		
		health += itemHealth;
		if (health > maxHealth) health = maxHealth;
		UpdateHealthBar();
	}
	
	//Take damage upon some event
	void TakeDamage(float damage){
		
		health -= damage;
		if (health <= 0) Destroy(gameObject);
		UpdateHealthBar();
	}
	
	//Update the scale of the health bar on damage/heal
	void UpdateHealthBar(){
		healthbar.transform.localScale = new Vector3(1.0f * (health/maxHealth), 0.15f, 2.0f);
	}
	
	//Stop the knockback after a quarter second
	IEnumerator KnockbackCooldown(){
		yield return new WaitForSeconds(0.25f);
		sc.state = SkeletonController.State.Walking;
		rBody.velocity = new Vector2(0,0);
	}
}
