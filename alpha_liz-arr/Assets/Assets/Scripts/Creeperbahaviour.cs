
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Creeperbahaviour : MonoBehaviour
{
    public enum State { Idle, Patrol, Chase, Explosion, Dead};
    public State state;

    float timecounter = 0;

    

    public float gotoexplode = 0;
    private Animator anim;
    private NavMeshAgent agent;

    public Collider col;
    //private SoundPlayer sound;

    private float Timecounter;
    public float idleTime = 1.0f;

    [Header("path properties")]
    public Transform[] nodes;
    public int currentnode;
    public bool stopateachnode;
    public float reachdistance = 0.1f;

    [Header("Target properties")]
    public LayerMask targetmask;
    public float radius;
    public bool targetdetected;
    public Transform targetransform;

    [Header("Explosion properties")]
    public float explosiondistance = 3.0f;
    public ParticleSystem explosionps;
    public float explosionforce;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();

        col.enabled = !col.enabled;
        //sound = GetComponentInChildren < SoundPlayer>();

        //AGENT : destino nodo mas cercano  
        gonearnode();
        Setidle();
    }

    void Update()
    {
        //switch tabulador * 2 state enter * 2 
        switch(state)
        {
            case State.Idle:
                IdleUpdate();
                break;
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;            
            default:
                break;
        }

    }
    void FixedUpdate()
    {
        //Importante 
        targetdetected = false;
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, radius, targetmask);
        if(hitcolliders.Length != 0)
        {
            targetdetected = true;
            targetransform = hitcolliders[0].transform;
        }


       

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            
            Health.health -= 2;
            col.enabled = !col.enabled;
            SetDead();
        }


    }

    void IdleUpdate()
    {
        if(Timecounter >= idleTime)
        {
            SetPatrol();
        }
        else Timecounter += Time.deltaTime;

        if(targetdetected) SetChase();
           


    }

    void PatrolUpdate()
    {
        if(Vector3.Distance(transform.position, nodes[currentnode].position) < reachdistance)
        {
            //ir al siguiente nodo
            gonextnode();
            if(stopateachnode) Setidle();


        }

        if(targetdetected) SetChase();

    }

    void ChaseUpdate()
    {
        if(!targetdetected)
        {
            gonearnode();
            Setidle();
            return;

        }
        
        if(Vector3.Distance(transform.position, targetransform.position)<= explosiondistance)
        {
            SetExplosion();
            return;
        }

        agent.SetDestination(targetransform.position);

    }

    #region Sets
    void Setidle()
    {
             
       // anim.SetBool("isMoving", false);
        agent.isStopped = true;
        int r = Random.Range(5, 9);
        //sound.Play(1, 1);
        radius = 6;
        state = State.Idle;
    }
    void SetPatrol()
    {

       // anim.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.stoppingDistance = 0;


        state = State.Patrol;


    }
    void SetChase()
    {
        state = State.Chase;
       // anim.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.stoppingDistance = 2;
        radius = 14;



    }
    void SetExplosion()
    {
        state = State.Explosion;
        //anim.SetTrigger("Explode");
        agent.isStopped = true;
        Explode();

        
       
        
        
      
        
       // sound.Play(9, 1);
    }
    void SetDead()
    {
        // sound.Play(0, 1);
        Destroy( gameObject);
        state = State.Dead;
        
    }




    #endregion
    
    void gonextnode()
    {
        //caminos cerrados 
        currentnode++;
        if(currentnode >= nodes.Length) currentnode = 0;

        agent.SetDestination(nodes[currentnode].position);
    }

    void gonearnode()
    {
        float minDistance = Mathf.Infinity;
        for(int i = 0; i < nodes.Length; ++i)
        {
            float dist = Vector3.Distance(transform.position, nodes[i].position);
            if(dist < minDistance)
            {
                 minDistance = dist;
                 currentnode = i;
 
            }
        }

        agent.SetDestination(nodes[currentnode].position);

    }

    private void OnDrawGizmos()
    {
        Color color = Color.red;
        color.a = 0.1f;

        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }

    public void Explode()
    {

        //explosionps.Play();

        
        //timetoexplosion -= Time.deltaTime;
        col.enabled = !col.enabled; 
 
        //if(timetoexplosion == 0)
       //{
        //     col.enabled = !col.enabled; 
        //     SetDead();
        //  }
        
        explosionps.transform.parent = null;

        int r = Random.Range(5, 9);
        //sound.Play(r, 1);

        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, explosiondistance + 1.0f);

       // for(int i = 0; i < hitcolliders.Length; i++)

        foreach(Collider c in hitcolliders)
        {
            if(c.attachedRigidbody != null)
            {
                c.attachedRigidbody.AddExplosionForce(explosionforce, transform.position, explosiondistance + 1.0f, 1, ForceMode.Impulse);
            }
        }

       

        


        
    }

    





}