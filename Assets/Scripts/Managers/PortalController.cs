using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    void Update()
    {
        transform.Rotate(0.0f, 0.0f, 1.0f);   
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            GameManager.Instance.EndLevel();
        }
    }
}
