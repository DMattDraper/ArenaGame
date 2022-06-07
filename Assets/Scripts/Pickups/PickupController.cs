using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
	//Public Members
	public float power;
	public string sound;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "Player"){
			AudioManager.Instance.Play(sound);
			PlayerHealth ph = other.gameObject.GetComponent<PlayerHealth>();
				
			if (ph.health < ph.maxHealth){
				Hit(other.gameObject);
			}
		}
	}
	
	public void Hit(GameObject other){
		other.SendMessage("Heal",power);
		Destroy(gameObject);
	}
	
	
}
