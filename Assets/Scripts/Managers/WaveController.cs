using UnityEngine;

public class WaveController : MonoBehaviour
{

	//Public Members
	public int spawnerCount;
	public bool active = true;

	//Private Members
	private int depletion = 0;

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
