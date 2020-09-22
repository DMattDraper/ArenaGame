using UnityEngine;

public class WaveController : MonoBehaviour
{

	//Public Members
	public int spawnerCount;
	
	//Private Members
	private int depletion = 0;
	private bool active = true;

    // Update is called once per frame
    void Update()
    {	
		if (depletion == spawnerCount && active){
			//Debug.Log("Oh mah gawd she fuckin dead");
			active = false;
		}
    }
	
	// Increase depletion by one
	public void incrementDepletion(){
		depletion++;
	}

}
