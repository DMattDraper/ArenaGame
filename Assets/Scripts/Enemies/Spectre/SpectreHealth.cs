using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreHealth : MonoBehaviour
{
	//Public Members
	public float maxHealth;
	public float health;
	public float power;
	public GameObject healthbar;
	
	//Private Members
	private Rigidbody2D rBody;
	private SpectreController sc;
	
    // Start is called before the first frame update
    void Start()
    {
        
		// Fill Health
		health = maxHealth;
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		sc = GetComponent<SpectreController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//Occurs when entering a Collider
	private void OnTriggerEnter2D(Collider2D other){
		
		if (sc.state == SpectreController.State.Rushing && other.gameObject.tag == "Player"){
			Hit(other.gameObject);
			Destroy(gameObject);
		} else if (sc.state == SpectreController.State.Rushing && other.gameObject.tag == "Wall"){
			Destroy(gameObject);
		} else if (other.gameObject.tag == "Player"){
			rBody.velocity = new Vector2(0,0);
		}
	}
	
	//Knock back the skeleton
	void Knockback(Collider2D other){
		if (sc.state != SpectreController.State.Rushing){
			//Get Vector between skeleton and the source
			Rigidbody2D attackRigidbody = other.GetComponent<Rigidbody2D>();
			Vector2 knockbackVector = rBody.position - attackRigidbody.position;
				
			//Knock the player backwards
			rBody.velocity = knockbackVector.normalized*15;
			sc.state = SpectreController.State.Stunned;		
			StartCoroutine("KnockbackCooldown");
		}
	}
	
	//Heal upon some event
	void Heal(float itemHealth){
		
		health += itemHealth;
		if (health > maxHealth) health = maxHealth;
		UpdateHealthBar();
	}
	
	//Take damage upon some event
	void TakeDamage(float damage){
		
		if (sc.state != SpectreController.State.Rushing){
			health -= damage;
			if (health <= 0) Destroy(gameObject);
			UpdateHealthBar();
		}
	}
	
	//Update the scale of the health bar on damage/heal
	void UpdateHealthBar(){
		healthbar.transform.localScale = new Vector3(1.0f * (health/maxHealth), 0.15f, 2.0f);
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
	
	//Stop the knockback after a quarter second
	IEnumerator KnockbackCooldown(){
		yield return new WaitForSeconds(0.25f);
		sc.state = SpectreController.State.Walking;
		rBody.velocity = new Vector2(0,0);
	}
}
