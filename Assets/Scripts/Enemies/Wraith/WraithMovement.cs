using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithMovement : MonoBehaviour
{
	//Public Members
		
	//Private Members
	private Vector2 destination;
	private Rigidbody2D rBody;
	private WraithController wc;
	
    // Start is called before the first frame update
    void Start()
    {
     
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		wc = GetComponent<WraithController>();
	}

    // Update is called once per frame
    void Update()
    {
        if (wc.state == WraithController.State.Teleporting){
			wc.state = WraithController.State.Pausing;
			Teleport();
		}
    }
	
	// Set location, wait, and teleport
	public void Teleport(){
		SetDestination();
		StartCoroutine("TelportClock");
	}
	
	// Randomly select a destination for the Imp
	public void SetDestination(){
		
		// Sets the destination to a random position on the map
		destination = new Vector2(Random.Range(-14f,14f),Random.Range(-6f,6f));
	}
	
	//Wait and teleport
	IEnumerator TelportClock(){
		yield return new WaitForSeconds(0.75f);
		gameObject.transform.position = destination;
		
		yield return new WaitForSeconds(0.75f);
		wc.state = WraithController.State.Attacking;
	}
}
