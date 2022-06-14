using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
	//Public Members
	public float power;
	public string playerSound;
	public string enemySound;
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pp.powerup != PlayerPowerup.Powerup.Invincible){
				AudioManager.Instance.Play(playerSound);
				Hit(other.gameObject);
			}
		} else if (other.gameObject.tag == "Enemy"){
			AudioManager.Instance.Play(enemySound);
			Hit(other.gameObject);
		}
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
