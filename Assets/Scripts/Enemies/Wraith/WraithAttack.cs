using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithAttack : MonoBehaviour
{
	//Public Members
	public int streakMax;
	public int streakCount = 0;
	public GameObject missileAttack;
	
	//Private Members
	private bool loaded = true;
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private WraithController wc;
	
    // Start is called before the first frame update
    void Start()
    {
        
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		wc = GetComponent<WraithController>();
    }

    // Update is called once per frame
    void Update()
    {
		
		if(wc.state == WraithController.State.Attacking && loaded && streakCount < streakMax){
			Attack();
		}
		if (streakCount >= streakMax && wc.state == WraithController.State.Attacking){
			StartCoroutine("EndAttack");
		}
    }
	
	// Attack
	void Attack(){
		
		//Increment Streak counter
		streakCount++;
		//Debug.Log(streakCount);
		
		//Get player position
		Vector2 playerPosition = playerRigidbody.position;
		Vector2 attackVector =  playerPosition - rBody.position;
		
		//Get position for attack
		Vector2 attackPosition = rBody.position + attackVector.normalized * 0.5f;
		//Get rotation for attack
		Quaternion attackRotation = new Quaternion(0,0,0,0);
		//Create the attack object
		GameObject attackInstance = Instantiate(missileAttack,attackPosition,attackRotation);
		
		//Begin reloading
		loaded = false;
		StartCoroutine("Recharge");
		
	}
	
	// Recharge the attack after half of a second
	IEnumerator Recharge(){
		yield return new WaitForSeconds(1f);
		loaded = true;
	}
	
	// Pause after attack
	IEnumerator EndAttack(){
		yield return new WaitForSeconds(.75f);
		wc.state = WraithController.State.Teleporting;
		streakCount = 0;
	}
}
