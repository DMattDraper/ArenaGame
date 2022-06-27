using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichMovement : MonoBehaviour
{
	//Public Members
	public float speed;
	public float followRange;
	public float shootRange;
	public float slashRange;
	
	//Private Members
	private Rigidbody2D rBody;
	private Rigidbody2D playerRigidbody;
	private LichController lc;
	private LichAttack la;
	
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
		playerRigidbody = GameObject.Find("Player").GetComponent<Rigidbody2D>();
		lc = GetComponent<LichController>();
		la = GetComponent<LichAttack>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Get Distance to player
        Vector2 playerPosition = playerRigidbody.position;
		Vector2 path = playerPosition - rBody.position;
		float distance = path.magnitude;
		
		if(lc.state != LichController.State.Walking){
			//Stop moving if not in walking state
			Vector2 stop = new Vector2(0.0f, 0.0f);
			Walk(stop);
		}
		else if(lc.summonSkull){
			Vector2 stop = new Vector2(0.0f, 0.0f);
			Walk(stop);
			lc.state = LichController.State.Attacking;
			//Summon Skeleton enemy
		}
		else if(distance >= followRange){
			Walk(path);
		}
		else if(distance <= slashRange){
			//Sword Slash
			lc.state = LichController.State.Attacking;
		}
		else if(distance > shootRange){
			//Rapid fire or Nuke (depending on chance and health?)
			if(la.loaded){
				if(lc.nuke){
					lc.state = LichController.State.Attacking;
				}
				else{
					lc.rapidFire = true;
					lc.state = LichController.State.Attacking;
				}
			}
		}
		else if(distance <= shootRange){
			Walk(-path);
		}
		else {
			Walk(path);
		}
    }
	
	void Walk(Vector2 path){
		rBody.velocity = path.normalized * speed;
	}
	
}
