using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerup : MonoBehaviour
{
	// Public Members
	public string collectSound;
	public string endSound;
	public enum Powerup {None, Damage, Invincible}
	public Powerup powerup = Powerup.None;
	
	// Private Members
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// Collide with powerup
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag  == "Powerup"){
			AudioManager.Instance.Play(collectSound);
			// Apply Powerup
			PowerupController pc = other.gameObject.GetComponent<PowerupController>();
			powerup = (Powerup) pc.powerup;
			StartCoroutine("Cooldown");
			// Destroy the powerup
			Destroy(other.gameObject);
		}
	}
	
	// Reset the Powerup
	IEnumerator Cooldown(){
		
		yield return new WaitForSeconds(10.0f);
		AudioManager.Instance.Play(endSound);
		powerup = Powerup.None;
	}
}
