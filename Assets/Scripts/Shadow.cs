using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    void applyButtonPower() {
        this.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("Lights").transform.GetChild(0).gameObject.GetComponent<UnityEngine.Rendering.Universal.Light2D>().intensity = 1;
    }
}
