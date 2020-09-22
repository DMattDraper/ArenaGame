using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	public float dashSpeed;
	
	//Private Members
	private bool dashCharged = true;
	private Rigidbody2D rBody;
	private PlayerController pc;
	
    // Start is called before the first frame update
    void Start(){
		
		//Get Components
		rBody = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
    }

    // FixedUpdate is called at a specific Interval
    void FixedUpdate(){
		
		// Trigger the Dash
		if(Input.GetKeyDown(KeyCode.Space) && dashCharged && pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Attacking && Time.timeScale != 0){
			Dash();
		}
		
		//Move the Player
		if(pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Dashing){
			Move();
		}
    }
	
	// Move the player according to the WASD/Arrow Keys
	void Move(){
		
		// Get Input
		float horizontalTranlation = Input.GetAxis("Horizontal") * speed;
		float verticalTranslation = Input.GetAxis("Vertical") * speed;
		
		// Create a motion vector with input
		Vector2 movement = new Vector2(horizontalTranlation,verticalTranslation);
		rBody.velocity = movement;
		
		// Set state to "Running" if in motion, else "Idle", if not attacking
		if(pc.state != PlayerController.State.Attacking){
			if(movement.magnitude != 0){ pc.state = PlayerController.State.Running; } 
			else { pc.state = PlayerController.State.Idle; }
		}
	}
	
	// Dash the player towards the mouse
	void Dash(){
		
		// Get Mouse Position and Convert to World Position
		Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		Vector2 dashVector =  mousePosition - rBody.position;
		
		// Set the dash speed & direction
		rBody.velocity = dashVector.normalized * dashSpeed;	
		
		// Consume dash charge and set state to "Dashing"
		dashCharged = false;
		pc.state = PlayerController.State.Dashing;
		
		// Start the cool down
		StartCoroutine("DashCooldown");
		StartCoroutine("DashRecharge");
	}
	
	// Stop dashing after .15 of a second
	IEnumerator DashCooldown(){
		
		yield return new WaitForSeconds(.15f);
		pc.state = PlayerController.State.Idle;
		rBody.velocity = new Vector2(0,0);
	}
	
	// Recharge the dash after one second
	IEnumerator DashRecharge(){
		
		yield return new WaitForSeconds(1.0f);
		dashCharged = true;
	}
	
}