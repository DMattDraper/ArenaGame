using UnityEngine;
using UnityEngine.UI;

public class Attack : MonoBehaviour
{
    public Image bomb;
    public Image bow;
    public Image blade;

    public void activateBomb() {
        bomb.enabled = true;
        bow.enabled = false;
        blade.enabled = false;
    }

    public void activateBow() {
        bomb.enabled = false;
        bow.enabled = true;
        blade.enabled = false;
    }

    public void activateBlade() {
        bomb.enabled = false;
        bow.enabled = false;
        blade.enabled = true;
    }
}
