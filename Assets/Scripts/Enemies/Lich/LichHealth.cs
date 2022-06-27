using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichHealth : MonoBehaviour
{
	//Public Members
	public float maxHealth;
	public float health;
	public GameObject healthbar;
	
	//Private Members
	private Rigidbody2D rbody;
	private LichController lc;
	
    // Start is called before the first frame update
    void Start()
    {
		//Fill Health
		health = maxHealth;
		//Get Components
		rbody = GetComponent<Rigidbody2D>();
		lc = GetComponent<LichController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter2D(Collider2D other){
		
	}
	
	private void OnCollision2D(Collision2D other){
		if(other.gameObject.tag == "Player"){
			rbody.velocity = new Vector2(0, 0);
		}
	}
	
	void Knockback(Collider2D other){
		
		/*
		//KnockBack the Reaper when attacked
		Rigidbody2D attackRigidBody = other.GetComponent<Rigidbody2D>();
		Vector2 knockbackVector = rbody.position - attackRigidBody.position;
		
		rbody.velocity = knockbackVector.normalized * 15;
		rc.state = ReaperController.State.Stunned;
		StartCoroutine("KnockBackCooldown");
		*/
	}
	
	IEnumerator KnockBackCooldown(){
		yield return new WaitForSeconds(0.25f);
		lc.state = LichController.State.Walking;
		rbody.velocity = new Vector2(0,0);
	}
	
	//Heal the Reaper in some event
	void Heal(float itemHealth){
		health += itemHealth;
		if(health >= maxHealth){health = maxHealth;};
		updateHealthBar();
	}
	
	//Update visual of health bar
	void updateHealthBar(){
		healthbar.transform.localScale = new Vector3(1.0f * (health/maxHealth), 0.15f, 2.0f);
	}
	
	void TakeDamage(float damage){
		health -= damage;
		if(health <= 0) {Destroy(gameObject);}
		updateHealthBar();
	}
	
}

