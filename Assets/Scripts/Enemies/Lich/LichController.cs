using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichController : MonoBehaviour
{
	public enum State {Walking, Attacking, Stunned};
	public State state = State.Walking;
	public bool summonSkull;
	public bool nuke;
	public bool rapidFire;
	
    // Start is called before the first frame update
    void Start()
    {
        summonSkull = false;
		nuke = false;
		rapidFire = false;
		StartCoroutine("SummonSkullCooldown");
		StartCoroutine("nukeCooldown");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	IEnumerator SummonSkullCooldown(){
		yield return new WaitForSeconds(10.0f);
		summonSkull = true;
	}
	
	IEnumerator nukeCooldown(){
		yield return new WaitForSeconds(5.0f);
		nuke = true;
	}
}
