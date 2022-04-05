using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Transform target;

    public float speed = .2f;

    public int damage = 50;
//public GameObject impactEffect;

    Vector3 dir;
    public void Seek(Transform _target) //will be used in shootingTurrent.cs
    {
        target = _target;
        dir = target.position - transform.position; //this is what gets the direction

    }

// Update is called once per frame
    void Update()
    {

        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        float distanceThisFrame = speed * Time.deltaTime; //speed of the bullet

        if (dir.magnitude <= distanceThisFrame)
        {

            return;
        }
        //this will define the movement of the bullet
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this will conclude if the enemy is hit, it will destroy the enemy
        if(collision.gameObject.CompareTag("Enemy"))
        {

            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
