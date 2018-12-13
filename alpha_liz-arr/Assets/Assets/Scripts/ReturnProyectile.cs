using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnProyectile : MonoBehaviour
{

    public float speed;

    public int damage = 1;

    private Transform player;
    private Transform Boss;

    private Vector3 target;
    private bool istargetingplayer = true;
    private bool canhitnow = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Boss = GameObject.FindGameObjectWithTag("Boss").transform;

        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    void Update()
    {
        if (istargetingplayer == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        }

        if(istargetingplayer == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            target = new Vector3(Boss.position.x, Boss.position.y, Boss.position.z);

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

        if (other.CompareTag("Boss") && canhitnow == true)
        {
            other.gameObject.GetComponent<EnemyBoss>().TakeDamage(damage);
            DestroyBossProyectile();
            istargetingplayer = true;
            BoosHealth.healthofboss -= 50;
        }


    }



    


    void DestroyBossProyectile()
    {
        Destroy(gameObject);
    }

    


}
