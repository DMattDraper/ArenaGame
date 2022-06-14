using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	//Public Members
	public float baseTimeScale = 1.0f;
	public bool paused = false;
	public bool dead = false;
	public Canvas pauseMenu;
	public Canvas deathMenu;
	public GameObject mouseIcon;

	void Awake() {
		if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
	}
	
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = baseTimeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !dead){
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
		pauseMenu.gameObject.SetActive(true);
		mouseIcon.SetActive(false);
		Time.timeScale = 0;
	}
	
	// Resume
	public void Resume() {
		paused = false;
		pauseMenu.gameObject.SetActive(false);
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

	// Die
	public void Die() {
		AudioManager.Instance.Play("Death");
		MusicManager.Instance.Die();
		dead = true;
		deathMenu.gameObject.SetActive(true);
		mouseIcon.SetActive(false);
		Time.timeScale = 0;
	}
}
