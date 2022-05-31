using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectreMovement : MonoBehaviour
{
   //Public Members
	public float speed;
	public float rushSpeed;
	public float rushRange;
	public float followRange;
	public float attackRange;
	
	//Private Members
	private Vector2 path;
	private Vector2 endPath;
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private SpectreController sc;
	
    // Start is called before the first frame update
    void Start()
    {
		
		// Declare end path
		endPath = new Vector2(0,0);
		
		// Get Components
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		sc = GetComponent<SpectreController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		// Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		path = playerPosition - rBody.position;
		float distance = path.magnitude;
		
		//Change State
		if (sc.state == SpectreController.State.Rushing){
			Rush(endPath);
		} else if (distance >= rushRange && sc.state == SpectreController.State.Walking){
			sc.state = SpectreController.State.Rushing;
			StartCoroutine("ActivateRush");
		} else if (distance >= followRange && sc.state == SpectreController.State.Walking){
			Walk(path);
		} else if (distance < attackRange && sc.state != SpectreController.State.Rushing) {
			sc.state = SpectreController.State.Attacking;
		}
    }
	
	// Walk Towards the player
	void Walk(Vector2 path){
		
		// Move towards the player
		rBody.velocity = path.normalized * speed;
	}
	
	// Rush towards the player
	void Rush(Vector2 path){
		
		// Move towards the player
		rBody.velocity = path.normalized * rushSpeed;
	}
	
	// Pause before rushing
	IEnumerator ActivateRush(){
		yield return new WaitForSeconds(1.5f);
		endPath = path;
	}
}
