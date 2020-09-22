using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	
	//Private Members
	private bool stuck;
	private Vector2 destination;
	private Rigidbody2D rBody;
	private ImpController ic;
	
    // Start is called before the first frame update
    void Start()
    {
		
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		ic = GetComponent<ImpController>();
		//Set Destination
		SetDestination();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		
		// When walking, if in range of destination, switch to dropping caltrops
		if((InRange() || stuck) && ic.state == ImpController.State.Walking){
			ic.state = ImpController.State.Dropping;
			stuck = false;
			StopCoroutine("StuckTimer");
		// Other wise, just keep on walking
		} else if (ic.state == ImpController.State.Walking){
			rBody.velocity = (destination - rBody.position).normalized * speed;
		} else if (ic.state == ImpController.State.Dropping) {
			rBody.velocity = new Vector2(0,0);
		}
    }
	
	// Randomly select a destination for the Imp
	public void SetDestination(){
		
		// Sets the destination to a random position on the map
		destination = new Vector2(Random.Range(-13f,13f),Random.Range(-4f,4f));
		StartCoroutine("StuckTimer");
	}
	
	// Is the Imp within range of its destination?
	bool InRange(){
		
		// Returns whether or not the imp is within range
		return (destination - rBody.position).magnitude <= 0.15f;
	}
	
	IEnumerator StuckTimer(){
		yield return new WaitForSeconds(7.0f);
		stuck = true;
	}
}
