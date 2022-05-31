using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	public float followRange;
	public float BoomRange;
	public float SlashRange;
	public float ChargeRange;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private ReaperController rc;
	private bool charging;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		rc = GetComponent<ReaperController>();
		charging = false;
    }

    void FixedUpdate()
    {
		// Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		Vector2 path = playerPosition - rBody.position;
		float distance = path.magnitude;
		
		//Change State
		if (distance >= followRange && rc.state == ReaperController.State.Walking){
			if (charging) {Charge(path);}
			else {Walk(path);}
			
		}else if (distance < BoomRange && distance > ChargeRange && rc.state == ReaperController.State.Walking) {
			rc.Attack_Decide = Random.value < 0.01;
			if (rc.Attack_Decide){
				rc.state = ReaperController.State.Attacking;
				charging = false;
			}
			else {
				if (charging) {Charge(path);}
				else {Walk(path);}
			}
			
		}else if (distance < ChargeRange  && distance > SlashRange && rc.state == ReaperController.State.Walking) {
			charging = true;
			Charge(path);
			
		}else if (distance < SlashRange && rc.state == ReaperController.State.Walking) {
			rc.state = ReaperController.State.Attacking;
			charging = false;
		}
    }
	
	// Walk Towards the player
	void Walk(Vector2 path){
		rBody.velocity = path.normalized * speed;
	}
	
	// Charge Towards the player
	void Charge(Vector2 path){
		StartCoroutine("ChargeTimer");
		rBody.velocity = path.normalized * (speed * 3);
	}

	IEnumerator ChargeTimer(){
		yield return new WaitForSeconds(1.0f);
		charging = false;
	}
	
}
