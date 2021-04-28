using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
	//Public Members
	public Rigidbody2D playerRigidbody;
	public GameObject closeIcon;
	public GameObject midIcon;
	public GameObject farIcon;
	public Attack attackIcon;
	public float midDistance;
	public float farDistance;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		//Follow Mouse
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
		Vector2 mousePosition = Camera.main.ScreenToWorldPoint(screenPosition);
		transform.position = mousePosition;
	
		//Set Sprite
		SetSprite();
	}
	
	void SetSprite(){
		//Get The Distance(Magnitude) between the mouse and the player
		Vector2 position = new Vector2(transform.position.x,transform.position.y);
		float magnitude = (playerRigidbody.position - position).magnitude;
		
		//Set Sprite Based on Mouse Distance
		if (magnitude >= farDistance){
			farIcon.SetActive(true);
			midIcon.SetActive(false);
			closeIcon.SetActive(false);
			attackIcon.activateBomb();
		} else if (magnitude >= midDistance) {
			farIcon.SetActive(false);
			midIcon.SetActive(true);
			closeIcon.SetActive(false);
			attackIcon.activateBow();
		} else {
			farIcon.SetActive(false);
			midIcon.SetActive(false);
			closeIcon.SetActive(true);
			attackIcon.activateBlade();
		}
		
	}
}
