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
	public float chargeSpeed;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private ReaperController rc;
	private ReaperHealth rh;
	private ReaperAttack ra;
	private bool charging;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		rc = GetComponent<ReaperController>();
		rh = GetComponent<ReaperHealth>();
		ra = GetComponent<ReaperAttack>();
		charging = false;
    }

    void FixedUpdate()
    {
		// Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		Vector2 path = playerPosition - rBody.position;
		float distance = path.magnitude;
		
		//Change State
		if (rc.spinning){
			SpinWalk(path);
		}
		else if (distance >= followRange && rc.state == ReaperController.State.Walking){
			if (charging) {Charge(path);}
			else {Walk(path);}	
		}
		else if (distance <= BoomRange && distance > ChargeRange && rc.state == ReaperController.State.Walking) {
			rc.ranged_attack = Random.value < 0.01f;
			if (rc.ranged_attack && ra.loaded){
				rc.state = ReaperController.State.Attacking;
				charging = false;
			}
			else {
				if (charging) {Charge(path);}
				else {Walk(path);}
			}
		}
		else if (distance <= ChargeRange  && distance > SlashRange && rc.state == ReaperController.State.Walking) {
			charging = true;
			Charge(path);
		}
		else if (distance <= SlashRange && rc.state == ReaperController.State.Walking) {
			bool spin = false;
			if((rh.health/rh.maxHealth) <= 0.5f){
				spin = Random.value > 0.7f;
			}
			
			if (spin && ra.loaded){
				rc.state = ReaperController.State.Attacking;
				rc.spinning = true;
			}
			else if (ra.loaded){
				rc.state = ReaperController.State.Attacking;
				charging = false;
			}
			else{
				if (charging) {Charge(path);}
				else {Walk(path);}
			}
		}
		else{
			if(rc.state != ReaperController.State.Walking){
				Vector2 stop = new Vector2(0.0f, 0.0f);
				Walk(stop);
			}
		}
		
    }
	
	// Walk Towards the player
	void Walk(Vector2 path){
		rBody.velocity = path.normalized * speed;
	}
	
	void SpinWalk(Vector2 path){
		rBody.velocity = path.normalized * speed * (chargeSpeed/2);
	}
	
	// Charge Towards the player
	void Charge(Vector2 path){
		StartCoroutine("ChargeTimer");
		rBody.velocity = path.normalized * (speed * chargeSpeed);
	}

	IEnumerator ChargeTimer(){
		yield return new WaitForSeconds(1.0f);
		charging = false;
	}
	
}