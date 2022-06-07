using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	public float dashSpeed;
	public string dashSound;
	public PlayerAnimation animator;
	public Abilities abilities;
	
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
		if(CanDash()){
			Dash();
		}
		
		//Move the Player
		if(CanMove()){
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
			if(movement.magnitude != 0){ 
				pc.state = PlayerController.State.Running; 
				animator.Run();
			} else { 
				pc.state = PlayerController.State.Idle;
				animator.Idle();
			}
		}
	}
	
	// Dash the player towards the mouse
	void Dash(){
		AudioManager.Instance.Play(dashSound);
		// Get Mouse Position and Convert to World Position
		Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		Vector2 dashVector =  mousePosition - rBody.position;
		
		// Set the dash speed & direction
		rBody.velocity = dashVector.normalized * dashSpeed;	
		
		// Consume dash charge and set state to "Dashing"
		dashCharged = false;
		pc.state = PlayerController.State.Dashing;
		animator.Dash();
		abilities.Activate();
		
		// Start the cool down
		StartCoroutine("DashCooldown");
		StartCoroutine("DashRecharge");
	}

	public bool CanDash() {
		return Input.GetKeyDown(KeyCode.Space) && dashCharged && pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Attacking && Time.timeScale != 0;
	}

	public bool CanMove() {
		return pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Dashing;
	}
	
	// Stop dashing after .15 of a second
	IEnumerator DashCooldown(){
		
		yield return new WaitForSeconds(.15f);
		pc.state = PlayerController.State.Idle;
		rBody.velocity = new Vector2(0,0);
		animator.Idle();
	}
	
	// Recharge the dash after one second
	IEnumerator DashRecharge(){
		
		yield return new WaitForSeconds(1.0f);
		dashCharged = true;
	}
	
}