using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    public Image abilityImage;
    public float cooldown = 1.0f;
    bool isCooldown = false;
    public KeyCode ability;

    // Start is called before the first frame update
    void Start()
    {
        abilityImage.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(ability) && isCooldown == false) {
            isCooldown = true;
            abilityImage.fillAmount = 1;
        }

        if(isCooldown) {
            abilityImage.fillAmount -= 1 /cooldown * Time.deltaTime;
        }

        if(abilityImage.fillAmount <= 0) {
            abilityImage.fillAmount = 0;
            isCooldown = false;
        }
    }
}
