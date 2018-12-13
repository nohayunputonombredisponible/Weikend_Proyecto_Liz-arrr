using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timonespickeables : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            TimonScript.timones += 20f;
            Destroy(gameObject);
        }  
    }
}
