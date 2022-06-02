using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
	//Public Members
	public float midDistance;
	public float farDistance;
	public GameObject closeAttack;
	public GameObject midAttack;
	public GameObject farAttack;
	public GameObject closeSuper;
	public GameObject midSuper;
	public GameObject farSuper;
	public PlayerAnimation animator;
	
	//Private Members
	private float recharge;
	private bool loaded = true;
	private Rigidbody2D rBody;
	private PlayerController pc;
	private PlayerPowerup pp;
	
    // Start is called before the first frame update
    void Start()
    {
		
        //Get Components
		rBody = GetComponent<Rigidbody2D>();
        pc = GetComponent<PlayerController>();
		pp = GetComponent<PlayerPowerup>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack
		if(Input.GetMouseButton(0) && loaded && pc.state != PlayerController.State.Stunned && pc.state != PlayerController.State.Dashing && Time.timeScale != 0){
			Attack();
		}
    }
	
	// Attack
	void Attack(){
		
		//Get Mouse Position and Convert to World Position
		Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		Vector2 attackVector =  mousePosition - rBody.position;		
		
		//Get The Distance(Magnitude) between the mouse and the player
		float magnitude = attackVector.magnitude;
		//Debug.Log(magnitude);
		
		//Set the attack
		GameObject attack;
		
		if (pp.powerup == PlayerPowerup.Powerup.Damage){//When damage powerup is active
			if (magnitude >= farDistance){
				attack = farSuper;
				recharge = 0.65f;
			} else if (magnitude >= midDistance) {
				attack = midSuper;
				recharge = 0.15f;
			} else {
				attack = closeSuper;
				recharge = 0.20f;
			}
		} else { //When damage powerup is inactive
			if (magnitude >= farDistance){
				attack = farAttack;
				recharge = 0.75f;
			} else if (magnitude >= midDistance) {
				attack = midAttack;
				recharge = 0.2f;
			} else {
				attack = closeAttack;
				recharge = 0.25f;
			}
		}
		
		//Get position for attack
		Vector2 attackPosition = rBody.position + attackVector.normalized * 1.5f;
		Quaternion attackRotation = new Quaternion(0,0,0,0);
		
		//Create the attack object
		GameObject attackInstance = Instantiate(attack,attackPosition,attackRotation);
		
		//Disable attacking until reloaded & set state to attacking
		loaded = false;
		pc.state = PlayerController.State.Attacking;
		animator.Attack();

		// Start the cooldown
		StartCoroutine("AttackCooldown");
		StartCoroutine("AttackRecharge");
	}
	
	// Recharge the attack after a tenth of a second
	IEnumerator AttackCooldown(){
		
		yield return new WaitForSeconds(0.20f);
		pc.state = PlayerController.State.Idle;
		animator.Idle();
	}
	
	// Recharge the attack after a quarter second
	IEnumerator AttackRecharge(){
		
		yield return new WaitForSeconds(recharge);
		loaded = true;
	}
}
