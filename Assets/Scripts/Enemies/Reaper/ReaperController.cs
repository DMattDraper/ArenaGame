using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : MonoBehaviour
{
	public enum State {Walking, Attacking, Stunned};
	public State state = State.Walking;
	public bool Attack_Decide;
	
    // Start is called before the first frame update
    void Start()
    {
        Attack_Decide = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
