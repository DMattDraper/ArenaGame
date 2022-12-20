using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthController : MonoBehaviour
{

    public GameObject healthbar;
    public GameObject healthbarback;

    // Start is called before the first frame update
    void Start()
    {
        //Set Boss Healthbar position and scale
		healthbar.transform.position = new Vector3(0, 8.5f, 0);
		healthbarback.transform.position = new Vector3(0, 8.5f, 0);
		healthbar.transform.localScale = new Vector3(25.0f, 0.8f, 2.0f);
		healthbarback.transform.localScale = new Vector3(25.0f, 0.8f, 2.0f); 
    }

    // Update is called once per frame
    void Update()
    {
        //Keep Boss Healthbar locked to top of screen
        healthbar.transform.position = new Vector3(0, 8.5f, 0);
		healthbarback.transform.position = new Vector3(0, 8.5f, 0);
    }
}
