using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbedeQuestCompletada : MonoBehaviour
{
  

  

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
                      
            BarraAmarilla.favores += 50;
            Destroy(gameObject);
                 
           



        }  

    }
}
