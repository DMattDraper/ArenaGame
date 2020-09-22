using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Public Members
	public bool paused = false;
	public Canvas ui;
	public GameObject mouseIcon;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
			if(paused){
				Resume();
			} else {
				Pause();
			}
		}
    }
	
	// Pause
	public void Pause(){
		paused = true;
		ui.gameObject.SetActive(true);
		mouseIcon.SetActive(false);
		Time.timeScale = 0;
	}
	
	// Resume
	public void Resume(){
		paused = false;
		ui.gameObject.SetActive(false);
		mouseIcon.SetActive(true);
		Time.timeScale = 1;
	}
	
	// Restart
	public void Restart(){
		SceneManager.LoadScene("Level1");
		Time.timeScale = 1;
	}
	
	// Quit
	public void Quit(){
		Application.Quit();
	}
}
