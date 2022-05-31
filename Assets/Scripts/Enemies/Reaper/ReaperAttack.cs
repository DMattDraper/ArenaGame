using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperAttack : MonoBehaviour
{
	//Public Members
	public GameObject SlashAttack;
	public GameObject BoomAttack;
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
		
		if (rc.Attack_Decide){
			StartCoroutine("WindupTimerRanged");
			//Disable attacking until reloaded
			loaded = false;
			rBody.velocity = new Vector2(0,0);
		}
		else {
			StartCoroutine("WindupTimerMelee");
			//Disable attacking until reloaded
			loaded = false;
			rBody.velocity = new Vector2(0,0);
		}
	}
	
	// Attack
	void Attack(bool ranged){
		
		if (ranged) {	//Ranged Attack
			
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
			
		} else {	//Melee Attack
			
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(SlashAttack,attackPosition,new Quaternion(0,0,0,0));
			
			//Begin reloading
			StartCoroutine("AttackRechargeMelee");
		}
	}
	
	// Windup the Melee attack
	IEnumerator WindupTimerMelee(){
		
		yield return new WaitForSeconds(0.25f);
		Attack(false);
	}
	
	// Windup the Ranged attack
	IEnumerator WindupTimerRanged(){
		
		yield return new WaitForSeconds(0.25f);
		Attack(true);
	}
	
	// Recharge the melee attack after a second
	IEnumerator AttackRechargeMelee(){
		
		yield return new WaitForSeconds(1.0f);
		loaded = true;
		rc.state = ReaperController.State.Walking;
	}
	
	// Recharge the ranged attack after 3 seconds
	IEnumerator AttackRechargeRanged(){
		
		rc.state = ReaperController.State.Walking;
		rc.Attack_Decide = false;
		yield return new WaitForSeconds(3.0f);
		loaded = true;

	}
}
