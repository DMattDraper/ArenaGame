﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAttack : MonoBehaviour
{
	//Public Members
	public GameObject caltrop;
	public ImpAnimator animator;
	
	//Private Members
	private bool loaded = true;
	private ImpController ic;
	private ImpMovement im;
	
    // Start is called before the first frame update
    void Start()
    {
		
        // Get Components
		ic = GetComponent<ImpController>();
        im = GetComponent<ImpMovement>();
    }

    // Update is called once per frame
    void Update()
    {
		
		// When we arrive at the destination, drop a caltrop
		if(ic.state == ImpController.State.Dropping && loaded){
			Drop();
		}
	}
	 
	// Drop Caltrop
	void Drop(){
		
		// Drop Caltrop
		Instantiate(caltrop, transform.position, transform.rotation);
		animator.Drop();
		
		// Prevent Multidrops and wait
		loaded = false;
		StartCoroutine("Wait");
	}
	
	// Wait before moving to new postion
	IEnumerator Wait(){
		
		yield return new WaitForSeconds(0.4f);
		animator.Idle();
		yield return new WaitForSeconds(1.6f);
		
		// Set Destination & Start Walking
		im.SetDestination();
		ic.state = ImpController.State.Walking;
		
		// Reload
		loaded = true;
	}
}
