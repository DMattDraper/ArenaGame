using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	public float followRange;
	public float attackRange;
	public SkeletonAnimation animator;

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
		animator.Walk();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		// Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		Vector2 path = playerPosition - rBody.position;
		float distance = path.magnitude;
		
		//Change State
		if (distance >= followRange && sc.state == SkeletonController.State.Walking){
			Walk(path);
			animator.Walk();
		} else if (distance < attackRange) {
			sc.state = SkeletonController.State.Attacking;
		}
    }
	
	// Walk Towards the player
	void Walk(Vector2 path){
		
		// Move towards the player
		rBody.velocity = path.normalized * speed;
	}
}
