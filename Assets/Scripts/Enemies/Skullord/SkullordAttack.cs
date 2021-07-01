using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullordAttack : MonoBehaviour
{
    //Public Members
	public float meleeRange;
	public GameObject meleeAttack;
	public GameObject missileAttack;
	public SkullordAnimation animator;
	
	//Private Members
	private bool loaded = true;
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private SkullordController sc;
	
	// Start is called before the first frame update
    void Start()
    {
		
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		sc = GetComponent<SkullordController>();
    }

    // Update is called once per frame
    void Update()
    {
		
		//Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		Vector2 path = playerPosition - rBody.position;
		float distance = path.magnitude;
        
		if(sc.state == SkullordController.State.Attacking && loaded){
			if (distance > meleeRange){
				MissileWindup();
			} else {
				MeleeWindup();
			}
		} 
    }
	
	//Windup
	void MeleeWindup(){
		
		StartCoroutine("MeleeWindupTimer");
		//Disable attacking until reloaded
		loaded = false;
	}

	void MissileWindup(){
		
		StartCoroutine("MissileWindupTimer");
		//Disable attacking until reloaded
		loaded = false;
	}

	// Attack
	void MeleeAttack(){
		
		//Get player position
		Vector2 playerPosition = playerRigidbody.position;
		Vector2 attackVector =  playerPosition - rBody.position;
		
		//Get position for attack
		Vector2 attackPosition = rBody.position + attackVector.normalized * 1.5f;
		//Get rotation for attack
		Quaternion attackRotation = new Quaternion(0,0,0,0);
		//Create the attack object
		GameObject attackInstance = Instantiate(meleeAttack,attackPosition,attackRotation);

		//Begin reloading
		StartCoroutine("MeleeRecharge");
		
	}
	
	// Attack
	void MissileAttack(){
		
		//Stop Moving
		rBody.velocity = new Vector2(0,0);
		
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
		StartCoroutine("MissileRecharge");
		animator.Attack();
	}
	
	// Recharge the melee attack after two seconds
	IEnumerator MeleeRecharge(){
		yield return new WaitForSeconds(2.0f);
		loaded = true;
		sc.state = SkullordController.State.Walking;
	}

	// Recharge the missile attack after three and a half seconds
	IEnumerator MissileRecharge(){
		yield return new WaitForSeconds(3.5f);
		loaded = true;
		sc.state = SkullordController.State.Walking;
	}
	
	// Windup the attack
	IEnumerator MeleeWindupTimer(){
		animator.Idle();
		yield return new WaitForSeconds(0.25f);
		animator.Attack();
		yield return new WaitForSeconds(0.25f);
		MeleeAttack();
		animator.Idle();
	}

	// Windup the attack
	IEnumerator MissileWindupTimer(){
		animator.Idle();
		yield return new WaitForSeconds(0.25f);
		animator.Attack();
		yield return new WaitForSeconds(0.25f);
		MissileAttack();
		animator.Idle();
	}
}
