using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodyscript : MonoBehaviour {

    private float timebetweenshots;
    public float startTimeBtwShots;

    public float shootingdistance;


    private Transform player;
    public GameObject returnproyectile;

    



    void Start()
    {
        timebetweenshots = startTimeBtwShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {


        if (Vector3.Distance(transform.position, player.position) < shootingdistance)
        {



            if (timebetweenshots <= 0)
            {
                Instantiate(returnproyectile, transform.position, Quaternion.identity);
                timebetweenshots = startTimeBtwShots;

            }

            else
            {
                timebetweenshots -= Time.deltaTime;
            }

        }



    }
}


