using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpAnimator : MonoBehaviour
{
    // Public Members
    public Animator animator;
    public ImpController impController;
    public ImpMovement impMovement;

    // Private Members
    private Vector2 destination;
    private enum Direction { Down, Right, Up, Left}
    private Direction direction = Direction.Down;

    // Update is called once per frame
    void Update()
    {
        if (getAngle() > -45.0f && getAngle() <= 45) {
            direction = Direction.Right;
        } else if (getAngle() > 45.0f && getAngle() <= 135.0f) {
            direction = Direction.Up;
        } else if (getAngle() > 135 || getAngle() <= -135.0f) {
            direction = Direction.Left;
        } else {
            direction = Direction.Down;
        }
    }

    public void Idle() {
        animator.Play("Imp_Idle_" + direction);
    }

    public void Walk() {
        animator.Play("Imp_Walk_" + direction);
    }

    public void Drop() {
        animator.Play("Imp_Drop_" + direction);
    }

    public void Stun() {
        animator.Play("Imp_Stun_" + direction);
    }

     // Get angle between the player and the skeleton
	float getAngle(){
        destination = impMovement.GetDestination();		
		float angle = Mathf.Atan2(destination.y-transform.position.y, destination.x-transform.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
