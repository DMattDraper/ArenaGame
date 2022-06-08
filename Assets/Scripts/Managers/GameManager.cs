﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	//Public Members
	public float baseTimeScale = 1.0f;
	public bool paused = false;
	public Canvas ui;
	public GameObject mouseIcon;
	
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = baseTimeScale;
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
	
	// Start
	public void StartGame() {
		SceneManager.LoadScene("Dave Dev");
	}

	// Pause
	public void Pause(){
		paused = true;
		ui.gameObject.SetActive(true);
		mouseIcon.SetActive(false);
		Time.timeScale = 0;
	}
	
	// Resume
	public void Resume() {
		paused = false;
		ui.gameObject.SetActive(false);
		mouseIcon.SetActive(true);
		Time.timeScale = baseTimeScale;
	}
	
	// Restart
	public void Restart() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		Time.timeScale = baseTimeScale;
	}
	
	// Quit to Menu
	public void QuitToMenu() {
		SceneManager.LoadScene("Main Menu");
	}

	// Quit
	public void Quit() {
		Application.Quit();
	}
}
