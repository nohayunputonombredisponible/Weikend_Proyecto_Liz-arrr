using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodyProyectile : MonoBehaviour {

    public float speed;

    private Transform player;
    private Transform Woody;

    

    
    

    private Vector3 target;
    private bool istargetingplayer = true;
    private bool canhitnow = false;

    public int damage;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        Woody = GameObject.FindGameObjectWithTag("tronco").transform;

                

        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    void Update()
    {
        if (istargetingplayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if (istargetingplayer == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            target = new Vector3(Woody.position.x, Woody.position.y, Woody.position.z);

        }




        if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
        {
            DestroyBossProyectile();
        }






    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyBossProyectile();
            Health.health -= 1;
        }

        if (other.CompareTag("Tail"))
        {
            istargetingplayer = false;
            canhitnow = true;

        }

        if (other.CompareTag("tronco") && canhitnow == true)
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            DestroyBossProyectile();
            



        }


    }






    void DestroyBossProyectile()
    {
        Destroy(gameObject);
    }

}
