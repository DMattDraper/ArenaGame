using UnityEngine.Audio;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }
    public AudioSource levelMusic;
    public AudioSource deathMusic;

    void Awake() {
		if (Instance != null && Instance != this) { 
            Destroy(this); 
        } else { 
            Instance = this; 
        }
	}

    public void Die() {
        levelMusic.Stop();
        deathMusic.Play();
    }
}
