using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Public Members
    public Animator animator;
    public RectTransform rectTransform;
    public PlayerController playerController;
    
    // Private Members
    private enum Direction { Down, Right, Up, Left}
    private Direction direction = Direction.Down;

    // Update is called once per frame
    void Update() {
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

    public void PassiveUpdate() {
        if (playerController.state == PlayerController.State.Idle){
            Idle();
        } else if (playerController.state == PlayerController.State.Running) {
            Run();
        }
    }

    public void Idle() {
        animator.Play("Player_Idle_" + direction);
    }

    public void Run() {
        animator.Play("Player_Walk_" + direction);
    }

    public void Attack() {
        animator.Play("Player_Attack_" + direction);
    }

    public void Stun() {
        animator.Play("Player_Stun_" + direction);
    }

    public void Dash() {
        animator.Play("Player_Dash_" + direction);
    }

    // Get angle between the player and the mouse
	float getAngle(){		
		//Get Mouse Position
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		
		float angle = Mathf.Atan2(mousePosition.y-rectTransform.position.y, mousePosition.x-rectTransform.position.x)* Mathf.Rad2Deg;
		
		return angle;
	}
}
