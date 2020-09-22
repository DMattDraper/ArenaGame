using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardController : MonoBehaviour
{
	//Public Members
	public float power;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pp.powerup != PlayerPowerup.Powerup.Invincible){
				Hit(other.gameObject);
			}
		} else if (other.gameObject.tag == "Enemy"){
			Hit(other.gameObject);
		}
	}
	
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
