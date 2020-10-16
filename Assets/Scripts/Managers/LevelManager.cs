﻿using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    //Public Members
    public int index = 0;
    public float waveGap = 2f;//This is an arbitrary delay, will change later
    public WaveController[] waves;

    // Update is called once per frame
    void Update()
    {
        if (!waves[index].active && index==(waves.Length-1)){
            Debug.Log("Level Complete!");
            gameObject.SetActive(false);
        } else if (!waves[index].active){
            waves[index].gameObject.SetActive(false);
            index++;
            StartCoroutine("ChangeWave");
            Debug.Log("Wave Completed...");
        }
    }

    IEnumerator ChangeWave(){
        yield return new WaitForSeconds(waveGap);
        waves[index].gameObject.SetActive(true);
    }
}
