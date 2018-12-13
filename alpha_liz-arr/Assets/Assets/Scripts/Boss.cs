using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    // Use this for initialization

    private float timebetweenshots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public GameObject returnproyectile;
    public GameObject barradevida;

    public static bool isactive;
    
    
    private Transform player;
    public float ditancetoshoot;

    private float timetotrue = 0;



	void Start ()
    {
        timebetweenshots = startTimeBtwShots;
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (Vector3.Distance(transform.position, player.position) < ditancetoshoot)
        {

            barradevida.SetActive(true);
            isactive = true;
            


            if (timebetweenshots <= 0 && timetotrue < 3)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timebetweenshots = startTimeBtwShots;
                timetotrue++;

            }

            else
            {
                timebetweenshots -= Time.deltaTime;
            }


            if (timebetweenshots <= 0 && timetotrue == 3)
            {
                Instantiate(returnproyectile, transform.position, Quaternion.identity);
                timebetweenshots = startTimeBtwShots;
                timetotrue = 0;
            }

            else
            {
                timebetweenshots -= Time.deltaTime;
            }
        }

        


		
	}
}
