using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coinspickeables : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {


            Health.health += 1;

              
            Destroy(gameObject);
        }
    }
}
    

