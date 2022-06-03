using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperController : MonoBehaviour
{
	public enum State {Walking, Attacking, Stunned};
	public State state = State.Walking;
	public bool ranged_attack;
	public bool spinning;
	
    // Start is called before the first frame update
    void Start()
    {
        ranged_attack = false;
		spinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
