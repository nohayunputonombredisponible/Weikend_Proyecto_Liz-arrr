using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloodPlatform : MonoBehaviour
{

    public Animator anim;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("hundir", true);
        }
        else
        {
            anim.SetBool("hundir", false);
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("hundir", false);
        }

    }

}
