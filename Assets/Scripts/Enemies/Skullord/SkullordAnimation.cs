using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullordAnimation : MonoBehaviour
{
    // Public Members
    public Animator animator;
    public SkullordController skeletonController;

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
        animator.Play("Skullord_Idle_" + direction);
    }

    public void Walk() {
        animator.Play("Skullord_Walk_" + direction);
    }

    public void Attack() {
        animator.Play("Skullord_Attack_" + direction);
    }

    public void Stun() {
        animator.Play("Skullord_Stun_" + direction);
    }

     // Get angle between the player and the skeleton
	float getAngle(){		
		float angle = Mathf.Atan2(playerTransform.position.y-transform.position.y, playerTransform.position.x-transform.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
