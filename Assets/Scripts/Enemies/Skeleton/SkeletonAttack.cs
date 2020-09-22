using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
	//Public Members
	public GameObject attack;
	public bool loaded = true;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private SkeletonController sc;
	
    // Start is called before the first frame update
    void Start()
    {
        
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		sc = GetComponent<SkeletonController>();		
    }

    // Update is called once per frame
    void Update()
    {
        if(sc.state == SkeletonController.State.Attacking && loaded){
			Windup();
		} 
    }
	
	// Windup
	void Windup(){
		
		StartCoroutine("WindupTimer");
		//Disable attacking until reloaded
		loaded = false;
		rBody.velocity = new Vector2(0,0);
	}
	
	// Attack
	void Attack(){
		
		//Get Vector2 for the attack
		Vector2 attackTransform =  playerRigidbody.position - rBody.position;
		
		//Get position for attack
		Vector2 attackPosition = rBody.position + attackTransform.normalized * 1.5f;
		
		//Create the attack object
		GameObject attackInstance = Instantiate(attack,attackPosition,new Quaternion(0,0,0,0));
		
		//Begin reloading
		StartCoroutine("AttackRecharge");
		
	}
	
	// Windup the attack
	IEnumerator WindupTimer(){
		
		yield return new WaitForSeconds(0.5f);
		Attack();
	}
	
	// Recharge the attack after a second
	IEnumerator AttackRecharge(){
		
		yield return new WaitForSeconds(1.0f);
		loaded = true;
		sc.state = SkeletonController.State.Walking;
	}
}
