using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    private float Timebtwattack;
    public float starttimebtwattack;

    public Transform attakPos;
    public LayerMask WhatIsEnemies;
    public float attackRange;
    public int damage;
    public bool iknow = false;
    Animator anim;

    public Collider tail;



    // Use this for initialization

    // Update is called once per frame

    private void Start()
    {
        anim = GetComponent<Animator>();
        tail.enabled = !tail.enabled;
    }
    void Update ()
    {
        if(Timebtwattack <= 0)
        {
            if(Input.GetKeyDown(KeyCode.Z))
            {

                isattacking();
                
                iknow = true;

                //isnotattacking();
            }
            Timebtwattack = starttimebtwattack;
           
        }


        else
        {
            Timebtwattack -= Time.deltaTime;
            iknow = false;
            
        }


        isdoingdamage();

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(attakPos.position, attackRange);
       
    }

    private void isnotattacking()
    {
        anim.SetBool("isattacking", false);
    }

    private void isattacking()
    {
        anim.SetBool("isattacking", true);
    }

    private void isdoingdamage()
    {
        if (iknow)
        {
            Collider[] enemiesToDamage = Physics.OverlapSphere(attakPos.position, attackRange, WhatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }

    private void isnotdoingdamage()
    {
        iknow = false;
    }

    private void enbledisable()
    {
        tail.enabled = !tail.enabled;
    }
}
