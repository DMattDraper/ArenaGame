using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttack : MonoBehaviour
{
	//Public Members
	public GameObject SlashAttack;
	public GameObject BoomAttack;
	public GameObject SpinAttack;
	public bool loaded = true;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private ReaperController rc;
	private Collider2D reaperCollider;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		rc = GetComponent<ReaperController>();	
		reaperCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rc.state == ReaperController.State.Attacking && loaded){
			Windup();
		}
    }
	
	// Windup
	void Windup(){
		
		if(rc.spinning){
			//Disable attacking until reloaded
			loaded = false;
			//Do not stop moving to start spinning
			spinAttack();
		}
		else if (rc.ranged_attack){
			StartCoroutine("WindupTimerRanged");
			//Disable attacking until reloaded
			loaded = false;
			//Stop moving to make attack
			rBody.velocity = new Vector2(0,0);
		}
		else {
			StartCoroutine("WindupTimerMelee");
			//Disable attacking until reloaded
			loaded = false;
			//Stop moving to make attack
			rBody.velocity = new Vector2(0,0);
		}
	}
	
	// Windup the Melee attack
	IEnumerator WindupTimerMelee(){
		yield return new WaitForSeconds(0.15f);
		meleeAttack();
	}
	
	// Windup the Ranged attack
	IEnumerator WindupTimerRanged(){
		yield return new WaitForSeconds(0.25f);
		boomerangAttack();
	}
	
	// Recharge the melee attack after a second (plus the 1.5 seconds of attacking)
	IEnumerator AttackRechargeMelee(){
		yield return new WaitForSeconds(2.0f);
		rc.state = ReaperController.State.Walking;
		loaded = true;
	}
	
	// Recharge the ranged attack after 3 seconds
	IEnumerator AttackRechargeRanged(){
		rc.state = ReaperController.State.Walking;
		rc.ranged_attack = false;
		yield return new WaitForSeconds(3.0f);
		loaded = true;
	}
	
	// Keep spinning for 3 seconds then reload to attack again
	IEnumerator AttackRechargeSpin(){
		yield return new WaitForSeconds(3.0f);
		rc.state = ReaperController.State.Walking;
		rc.spinning = false;
		loaded = true;
	}
	
	// Create three scythe attacks after 0.0 seconds, 0.5, and 1.0 seconds
	IEnumerator meleeAttackThree(int n){
		yield return new WaitForSeconds(0.5f * n);
		scytheAttack();
	}
	
	void meleeAttack(){
		for(int i = 0; i < 3; i++){
			StartCoroutine(meleeAttackThree(i));
		}
		StartCoroutine("AttackRechargeMelee");
	}
	
	void scytheAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(SlashAttack,attackPosition,new Quaternion(0,0,0,0));
			
			/*
			//Begin reloading
			StartCoroutine("AttackRechargeMelee");
			*/
	}
	
	void boomerangAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(BoomAttack,attackPosition,new Quaternion(0,0,0,0));
			attackInstance.GetComponent<BoomerangController>().reaperRbody = rBody;
			attackInstance.GetComponent<BoomerangController>().reaperCollider = reaperCollider;
			
			//Begin reloading
			StartCoroutine("AttackRechargeRanged");
	}
	
	void spinAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(SpinAttack,attackPosition,new Quaternion(0,0,0,0));
			attackInstance.GetComponent<SpinScytheController>().target = rBody;
			
			//Begin reloading
			StartCoroutine("AttackRechargeSpin");
	}
}
