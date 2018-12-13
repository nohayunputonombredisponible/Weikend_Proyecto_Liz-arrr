using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BarraAmarilla : MonoBehaviour
{

    Image barramarilla;
    float alltimones = 100f;
    float start = 0f;
    public static float favores;

    // Use this for initialization
    void Start()
    {
        barramarilla = GetComponent<Image>();
        favores = start;

    }

    // Update is called once per frame
    void Update()
    {
        barramarilla.fillAmount = favores / alltimones;

    }
}
