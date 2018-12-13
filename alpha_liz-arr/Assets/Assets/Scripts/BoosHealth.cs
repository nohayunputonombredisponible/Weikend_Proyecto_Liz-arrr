using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosHealth : MonoBehaviour
{

    Image healthboss;
    float allvida = 100f;
    public static float healthofboss;
   
    public static float favores;

    // Use this for initialization
    void Start()
    {
        healthboss = GetComponent<Image>();
        healthofboss = allvida;

    }

    // Update is called once per frame
    void Update()
    {
        healthboss.fillAmount = healthofboss / allvida; 

    }
}
