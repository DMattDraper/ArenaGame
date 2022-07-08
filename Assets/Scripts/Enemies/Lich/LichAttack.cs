using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichAttack : MonoBehaviour
{
	//Public Members
	public GameObject MeleeAttack;
	public GameObject RapidAttack;
	public GameObject NukeRangedAttack;
	public GameObject Skeleton;
	public bool loaded;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private LichController lc;
	
    // Start is called before the first frame update
    void Start()
    {
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		lc = GetComponent<LichController>();
		loaded = true;		
    }

    // Update is called once per frame
    void Update()
    {
        if(lc.state == LichController.State.Attacking && loaded){
			Windup();
		}
    }
	
	// Windup
	void Windup(){
		
		if(lc.summonSkull){
			StartCoroutine("WindupSummon");
			//Disable attacking until reloaded
			loaded = false;
			lc.summonSkull = false;
			//Stop moving to summon skeleton
			rBody.velocity = new Vector2(0,0);
		}
		else if (lc.nuke){
			StartCoroutine("WindupTimerNuke");
			//Disable attacking until reloaded
			loaded = false;
			lc.nuke = false;
			//Stop moving to make attack
			rBody.velocity = new Vector2(0,0);
		}
		else if (lc.rapidFire){
			StartCoroutine("WindupTimerRapid");
			//Disable attacking until reloaded
			loaded = false;
			//Stop moving to make attack
			rBody.velocity = new Vector2(0,0);
		}
		else {
			StartCoroutine("WindupTimerSlash");
			//Disable attacking until reloaded
			loaded = false;
			//Stop moving to make attack
			rBody.velocity = new Vector2(0,0);
		}
	}
	
	// Windup the Melee attack
	IEnumerator WindupTimerSlash(){
		yield return new WaitForSeconds(0.3f);
		SlashAttack();
	}
	
	// Windup the Ranged attack
	IEnumerator WindupTimerNuke(){
		yield return new WaitForSeconds(0.5f);
		NukeAttack();
	}
	
	IEnumerator WindupTimerRapid() {
		yield return new WaitForSeconds(0.2f);
		int count = 10;
		while(count != 0){
			RapidFireAttack();
			count--;
			yield return new WaitForSeconds(0.1f);
		}
		StartCoroutine("AttackRechargeRapid");
	}
	
	// Recharge the melee attack after a second
	IEnumerator WindupSummon(){
		yield return new WaitForSeconds(0.75f);
		SummonAttack();
	}
	
	//Recharges for each attack
	
	IEnumerator AttackRechargeSlash(){
		lc.state = LichController.State.Walking;
		yield return new WaitForSeconds(0.5f);
		loaded = true;
	}
	
	IEnumerator AttackRechargeRapid(){
		lc.state = LichController.State.Walking;
		lc.rapidFire = false;
		yield return new WaitForSeconds(0.75f);
		loaded = true;
	}
	
	// Recharge the ranged attack after 3 seconds
	IEnumerator AttackRechargeNuke(){
		lc.state = LichController.State.Walking;
		yield return new WaitForSeconds(0.75f);
		loaded = true;
		yield return new WaitForSeconds(5.0f);
		lc.nuke = true;
	}
		
	IEnumerator AttackRechargeSummon(){
		lc.state = LichController.State.Walking;
		yield return new WaitForSeconds(0.5f);
		loaded = true;
		yield return new WaitForSeconds(9.5f);
		lc.summonSkull = true;
	}
	
	void SlashAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(MeleeAttack, attackPosition, new Quaternion(0,0,0,0));
			attackInstance.transform.localScale = attackInstance.transform.localScale * 2;
			
			//Begin reloading
			StartCoroutine("AttackRechargeSlash");
	}
	
	void RapidFireAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(RapidAttack, attackPosition, new Quaternion(0,0,0,0));
	}
	
	void NukeAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(NukeRangedAttack, attackPosition, new Quaternion(0,0,0,0));
			
			//Begin reloading
			StartCoroutine("AttackRechargeNuke");
	}
	
	void SummonAttack(){
			//Get Vector2 for the attack
			Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
			//Get position for attack
			Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
			
			//Create the attack object
			GameObject attackInstance = Instantiate(Skeleton, attackPosition, new Quaternion(0,0,0,0));
			
			//Begin reloading
			StartCoroutine("AttackRechargeSummon");
	}
}
