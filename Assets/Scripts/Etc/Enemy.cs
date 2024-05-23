using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    
    public int health = 100;
    public Slider healthBar;
    public Animator animator;

    public float timeBtwnShots;
    public float startTimeBtwnShots;
    public GameObject shots;
    public Transform bulletSpawn1;
    public float fireRate;
    private float nextFire;


    void Start()
    {
       
        
    }
    void Update()
    { 
       
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shots, bulletSpawn1.position, bulletSpawn1.rotation);
        }
    }
    public void TakeDamage (int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(this.gameObject);
    }
   
    


}

