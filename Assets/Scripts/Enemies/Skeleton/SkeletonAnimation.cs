using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimation : MonoBehaviour
{
    // Public Members
    public Animator animator;
    public Transform skeletonTransform;
    public SkeletonController skeletonController;

    // Private Members
    private RectTransform playerTransform;
    private enum Direction { Down, Right, Up, Left}
    private Direction direction = Direction.Down;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) {
            playerTransform = player.GetComponent<RectTransform>();
        }
    }

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
        animator.Play("Skeleton_Idle_" + direction);
    }

    public void Walk() {
        animator.Play("Skeleton_Walk_" + direction);
    }

    public void Attack() {
        animator.Play("Skeleton_Attack_" + direction);
    }

    public void Stun() {
        animator.Play("Skeleton_Stun_" + direction);
    }

     // Get angle between the player and the skeleton
	float getAngle(){		
		float angle = Mathf.Atan2(playerTransform.position.y-skeletonTransform.position.y, playerTransform.position.x-skeletonTransform.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
