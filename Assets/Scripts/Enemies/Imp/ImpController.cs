using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpController : MonoBehaviour
{
	//Private Members
	public enum State {Walking, Dropping, Stunned}
	public State state = State.Walking;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
