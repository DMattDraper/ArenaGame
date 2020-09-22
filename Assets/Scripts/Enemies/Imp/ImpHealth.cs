using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpHealth : MonoBehaviour
{
	//Public Members
	public float maxHealth;
	public float health;
	public GameObject healthbar;
	
	//Private Members
	private Rigidbody2D rBody;
	private ImpController ic;
	
    // Start is called before the first frame update
    void Start()
    {
		
		// Fill Health
		health = maxHealth;
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		ic = GetComponent<ImpController>();
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
		//Occurs when entering a Collider
	private void OnCollisionEnter2D(Collision2D other){
		
		if (other.gameObject.tag == "Player"){
			//Knockback
			Knockback(other.collider);
			//Apply Damage
			TakeDamage(10);
		}
	}
	
	// Occurs when entering a Trigger collider
	private void OnTriggerEnter2D(Collider2D other){
		
	}
	
	//Knock back the imp
	void Knockback(Collider2D other){
		
		//Get Vector between Imp and the hazard
		Rigidbody2D attackRigidbody = other.GetComponent<Rigidbody2D>();
		Vector2 knockbackVector = rBody.position - attackRigidbody.position;
			
		//Knock the Imp backwards
		rBody.velocity = knockbackVector.normalized*15;
		ic.state = ImpController.State.Stunned;
		StartCoroutine("KnockbackCooldown");
	}
	
	//Update the scale of the health bar on damage/heal
	void UpdateHealthBar(){
		
		// Scales health bar as a % of full health
		healthbar.transform.localScale = new Vector3(1.0f * (health/maxHealth), 0.15f, 2.0f);
	}
	
	void Heal(float itemHealth){
		
		// Heal imp, prevent overflow, and update healthbar
		health += itemHealth;
		if (health > maxHealth) health = maxHealth;
		UpdateHealthBar();
	}
	
	void TakeDamage(float damage){
		
		// Damage imp, kill, and update healthbar
		health -= damage;
		if (health <= 0) Destroy(gameObject);
		UpdateHealthBar();
	}
	
	//Stop the knockback after a quarter second
	IEnumerator KnockbackCooldown(){
		
		yield return new WaitForSeconds(0.25f);
		
		// Set state back to walking
		ic.state = ImpController.State.Walking;
	}
}
