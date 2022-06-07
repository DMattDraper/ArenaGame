using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
	//Public Members
	public float lifeTime;
	public float power;
	public string sound;
	
	//Private Members
	private CameraShake cameraShake;

	void Awake() {
		AudioManager.Instance.Play(sound);
	}
	
    // Start is called before the first frame update
    void Start()
    {

		cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();
		StartCoroutine(cameraShake.Shake(power/400,lifeTime));
        // Destroy after lifetime expires
		Destroy(gameObject,lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player"){
			PlayerController pc = other.gameObject.GetComponent<PlayerController>();
			PlayerPowerup pp = other.gameObject.GetComponent<PlayerPowerup>();
			
			if(pc.state != PlayerController.State.Dashing && pc.state != PlayerController.State.Stunned && pp.powerup != PlayerPowerup.Powerup.Invincible){
				Hit(other.gameObject);
			}
		} else if (other.gameObject.tag == "Enemy"){
			Hit(other.gameObject);
		} else if(other.gameObject.tag == "Caltrop"){
			Destroy(other.gameObject);
		}
	}
	
	// Hit the opposing object
	public void Hit(GameObject other){
		other.SendMessage("TakeDamage",power);
		other.SendMessage("Knockback",gameObject.GetComponent<Collider2D>());
	}
}
