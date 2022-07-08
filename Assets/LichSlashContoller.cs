using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichSlashContoller : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	
    // Start is called before the first frame update
    void Start()
    {	
		//Start Decay Timer
		Destroy(gameObject,lifeTime);
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
				Hit(other.gameObject);
			}
		}
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
