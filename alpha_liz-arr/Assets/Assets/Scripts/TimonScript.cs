using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimonScript : MonoBehaviour
{

    Image barrazul;
    float alltimones = 100f;
    float start = 0f;
    public static float timones;

	// Use this for initialization
	void Start ()
    {
        barrazul = GetComponent<Image>();
        timones = start;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        barrazul.fillAmount = timones / alltimones;
		
	}
}
